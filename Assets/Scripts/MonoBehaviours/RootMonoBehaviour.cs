using System.Collections.Generic;
using System.Linq;
using Components;
using Constants;
using Extensions;
using GameUnits;
using Interfaces;
using Models;
using Services;
using TMPro;
using Unity.Collections;
using UnityEngine;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using Unity.Rendering;
using Space = Models.Space;

namespace MonoBehaviours
{
    public class RootMonoBehaviour : MonoBehaviour
    {
        [SerializeField]
        private CameraSwitch _cameraSwitch;

        private readonly Space _space = Space.XZ;
        private RectangularCellService _rectangularCellService;
        private GridMap2D<ListGameObject> _gridMap;
        private int2 _currentCell;
        private ListGameObject _selectorCell;
        private TwoItems<Options<GameObject, int2>> _pathItems;
        private ListGameObject _path;
        private Vector2 _cellSize;
        private PathfinderTaskGrid2D<RectangularPathfinderGrid2DNodeOptions> _pathfinder;
        private float _epsilon;
        private ListGameObject _roots;

        public RootMonoBehaviour()
        {
            _roots = new ListGameObject();
            var rotation = Quaternion.AngleAxis(-90, Vector3.left);
            var position = 10f.ToVector3Y();

            MonoBehaviourInject.Injector.RegisterSingleton<IEnumerable<IGameUnit>>(() => new IGameUnit[]
                {
                    MonoBehaviourInject.Injector.Resolve<MainCameraGameUnit>(),
                    MonoBehaviourInject.Injector.Resolve<MainLightGameUnit>()
                })
                .RegisterSingleton<IDictionary<string, EntityArchetype>>()
                .Reserve<MainCameraGameUnit>("Main Camera",
                    new TransformOptionsBuilder().SetRotation(rotation)
                        .SetPosition(position)
                        .BuilderToSourceItem(),
                    new CameraOptionsBuilder().SetOrthographic(true)
                        .BuilderToSourceItem())
                .Reserve<MainLightGameUnit>("Main Light",
                    new TransformOptionsBuilder().SetRotation(rotation)
                        .SetPosition(position)
                        .BuilderToSourceItem());

            MonoBehaviourInject.Injector.Inject(this);
        }

        private void InitDots()
        {
            var entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;
            MonoBehaviourInject.Injector.RegisterSingleton<EntityManager>(entityManager);

            var entityArchetype = entityManager.CreateArchetype(typeof(Translation),
                typeof(TestComponent),
                typeof(RenderMesh),
                typeof(LocalToWorld));

            using var entityArray = new NativeArray<Entity>(1, Allocator.Temp);
            entityManager.CreateEntity(entityArchetype, entityArray);

            for (var index = 0; index < entityArray.Length; index++)
            {
                var entity = entityArray[index];
                entityManager.SetComponentData(entity, new TestComponent());

                entityManager.SetSharedComponentData(entity,
                    new RenderMesh
                    {
                        mesh = MeshConstants.Cube,
                        material = MaterialConstants.Red
                    });
            }
        }

        private void Start()
        {
            foreach (var gameUnit in MonoBehaviourInject.Injector.Resolve<IEnumerable<IGameUnit>>())
            {
                gameUnit.Create().AddToCollection(_roots);
            }

            _epsilon = 0.01f;
            _cellSize = 1f.ToVector2();
            _path = new ListGameObject();
            _pathItems = new TwoItems<Options<GameObject, int2>>();
            _selectorCell = new ListGameObject();
            _rectangularCellService = new RectangularCellService(_space, _cellSize);
            _gridMap = new GridMap2D<ListGameObject>(_rectangularCellService);
        }

        private GameObject PathfinderCellText(Vector2 cellSize,
            PathfinderGrid2DNode<RectangularPathfinderGrid2DNodeOptions> node,
            ICellService cellService,
            Material material)
        {
            var localScale = new Vector3(cellSize.x, cellSize.y, 1);
            var center = cellService.GetCellCenter(node.Position);
            var rotation = Quaternion.AngleAxis(-90, Vector3.left);

            var parentTransformOptions = new TransformOptionsBuilder().SetLocalScale(localScale)
                .SetPosition(center)
                .SetRotation(rotation)
                .Build();

            var pathfinderCell = GameObjectBuilder.CreateQuad("Pathfinder Cell", parentTransformOptions, material);

            var childRectTransformOptions = new RectTransformOptionsBuilder().SetSizeDelta(cellSize)
                .SetParent(pathfinderCell.Transform)
                .Build();

            var textMeshProOptions = new TextMeshProOptionsBuilder().SetFontSize(2)
                .SetText(node.GCost.ToString("F0"));

            GameObjectBuilder.CreateTextMeshPro("Walking Cost from the Start Node",
                childRectTransformOptions,
                textMeshProOptions.Build());

            textMeshProOptions.SetText(node.HCost.ToString("F0")).SetAlignment(TextAlignmentOptions.BottomRight);

            GameObjectBuilder.CreateTextMeshPro("Heuristic Cost to reach End Node",
                childRectTransformOptions,
                textMeshProOptions.Build());

            textMeshProOptions.SetText(node.FCost.ToString("F0"))
                .SetAlignment(TextAlignmentOptions.Center)
                .SetFontStyle(FontStyles.Bold)
                .SetFontSize(4);

            GameObjectBuilder.CreateTextMeshPro("Total", childRectTransformOptions, textMeshProOptions.Build());

            return pathfinderCell.GameObject;
        }

        private void Update()
        {
            // var xPoint1 = _rectangularCellService.Space.XAxis.GetPoint(Vector3Helper.FromSingle(10));
            // var xPoint2 = _rectangularCellService.Space.XAxis.GetPoint(Vector3Helper.FromSingle(-10));
            // var yPoint1 = _rectangularCellService.Space.YAxis.GetPoint(Vector3Helper.FromSingle(10));
            // var yPoint2 = _rectangularCellService.Space.YAxis.GetPoint(Vector3Helper.FromSingle(-10));
            // Debug.DrawLine(xPoint1, xPoint2, Color.red);
            // Debug.DrawLine(yPoint1, yPoint2, Color.blue);
            // SelectCell();
            // CreateObject();
            // DeleteObject();
            // PathMoveNext();
            // CreatePathPoint();
        }

        private void CreatePathPoint()
        {
            if (!Input.GetMouseButtonDown(2))
            {
                return;
            }

            if (!_rectangularCellService.GetCellPositionByMouse(_cameraSwitch.CurrentCamera, out var cellPosition))
            {
                return;
            }

            if (!_gridMap.IsEmpty(cellPosition))
            {
                return;
            }

            var cellParameters = _rectangularCellService.GetCellParametersByCellPosition(cellPosition);
            var orchidCyan = CreateOrchidCyan(cellParameters.CellCenter);

            if (_pathItems.Item1 is null)
            {
                _pathItems.Item1 = new Options<GameObject, int2>(orchidCyan, cellPosition);
            }
            else if (_pathItems.Item2 is null)
            {
                _pathItems.Item2 = new Options<GameObject, int2>(orchidCyan, cellPosition);

                var grid2DPath = _gridMap.GetGrid2DPath(key =>
                        new PathfinderGrid2DNode<RectangularPathfinderGrid2DNodeOptions>(key.Key,
                            RectangularPathfinderGrid2DNodeOptions.Default,
                            !_gridMap.IsEmpty(key.Key)),
                    position => new PathfinderGrid2DNode<RectangularPathfinderGrid2DNodeOptions>(position,
                        RectangularPathfinderGrid2DNodeOptions.Default,
                        !_gridMap.IsEmpty(position)));

                _pathfinder = new PathfinderTaskGrid2D<RectangularPathfinderGrid2DNodeOptions>(grid2DPath,
                    VectorPathfinderDistance2D<RectangularPathfinderGrid2DNodeOptions>.Default,
                    new RectangularPathfinderNeighbours2D(grid2DPath),
                    _pathItems.Item1.Option,
                    _pathItems.Item2.Option);

                CreatePalate();
            }
            else
            {
                _pathfinder = null;
                _pathItems.Item1.Value.Destroy();
                _pathItems.Item2.Value.Destroy();
                _pathItems.Item1 = new Options<GameObject, int2>(orchidCyan, cellPosition);
                _pathItems.Item2 = null;
                _path.DestroyAllAndClear();
            }
        }

        private void PathMoveNext()
        {
            if (_pathfinder is null)
            {
                return;
            }

            if (!Input.GetKeyDown(KeyCode.Space))
            {
                return;
            }

            if (_pathfinder.IsFinished)
            {
                if (!_pathfinder.IsFound)
                {
                    return;
                }

                CreatePalate();

                foreach (var part in _pathfinder.Path)
                {
                    var center = _rectangularCellService.GetCellCenter(part.Position);

                    var transformOptions = new TransformOptionsBuilder().SetLocalScale(0.2f.ToVector3())
                        .SetPosition(center)
                        .Build();

                    GameObjectBuilder.CreateCube("Path Part", transformOptions, MaterialConstants.Blue)
                        .GameObject
                        .AddToCollection(_path);
                }
            }
            else
            {
                _pathfinder.MoveNext();
                CreatePalate();
            }
        }

        private void CreatePalate()
        {
            _path.DestroyAllAndClear();

            foreach (var node in _pathfinder.Grid)
            {
                if (float.MaxValue.Equals(node.Value.FCost, _epsilon))
                {
                    continue;
                }

                if (float.MaxValue.Equals(node.Value.GCost, _epsilon))
                {
                    continue;
                }

                if (float.MaxValue.Equals(node.Value.HCost, _epsilon))
                {
                    continue;
                }

                var materialPath = PathConstants.MaterialsDirectory.ToFileRelativePath("Blue.mat");

                if (_pathfinder.OpenList.Contains(node.Value))
                {
                    materialPath = PathConstants.MaterialsDirectory.ToFileRelativePath("Green.mat");
                }
                else if (_pathfinder.CloseList.Contains(node.Value))
                {
                    materialPath = PathConstants.MaterialsDirectory.ToFileRelativePath("Red.mat");
                }

                var pathfinderCellText = PathfinderCellText(_cellSize,
                    node.Value,
                    _rectangularCellService,
                    materialPath.LoadAssetAtPath<Material>());

                _path.Add(pathfinderCellText);
            }
        }

        private void CreateObject()
        {
            if (!Input.GetMouseButtonDown(0))
            {
                return;
            }

            if (!_rectangularCellService.GetCellPositionByMouse(_cameraSwitch.CurrentCamera, out var cellPosition))
            {
                return;
            }

            if (!_gridMap.IsEmpty(cellPosition))
            {
                return;
            }

            var cellParameters = _rectangularCellService.GetCellParametersByCellPosition(cellPosition);
            var coleus = CreateColeus(cellParameters.CellCenter);

            var text = CreateText(cellParameters.CellCenter + _rectangularCellService.Space.Options.Plane.Normal,
                $"{cellPosition.x};{cellPosition.y}");

            _gridMap.Add(cellPosition, new ListGameObject(coleus, text));
        }

        private void DeleteObject()
        {
            if (!Input.GetMouseButtonDown(1))
            {
                return;
            }

            if (!_rectangularCellService.GetCellPositionByMouse(_cameraSwitch.CurrentCamera, out var cellPosition))
            {
                return;
            }

            if (_gridMap.IsEmpty(cellPosition))
            {
                return;
            }

            _gridMap[cellPosition].DestroyAllAndClear();
            _gridMap.Remove(cellPosition);
        }

        private void SelectCell()
        {
            if (!_rectangularCellService.GetCellPositionByMouse(_cameraSwitch.CurrentCamera, out var cellPosition))
            {
                _selectorCell.DestroyAllAndClear();

                return;
            }

            if (_currentCell.Equals(cellPosition))
            {
                return;
            }

            _currentCell = cellPosition;
            _selectorCell.DestroyAllAndClear();
            var cellParameters = _rectangularCellService.GetCellParametersByCellPosition(cellPosition);
            CreateCellSelector(cellParameters, _selectorCell);
        }

        private void CreateCellSelector(CellParameters cellParameters, ICollection<GameObject> collection)
        {
            var localScale = 0.2f.ToVector3();
            var builder = new TransformOptionsBuilder().SetLocalScale(localScale);
            var transformOptionsBottomCenter = builder.SetPosition(cellParameters.BottomCenter).Build();
            var transformOptionsCellCenter = builder.SetPosition(cellParameters.CellCenter).Build();
            var transformOptionsLeftCenter = builder.SetPosition(cellParameters.LeftCenter).Build();
            var transformOptionsRightCenter = builder.SetPosition(cellParameters.RightCenter).Build();
            var transformOptionsTopCenter = builder.SetPosition(cellParameters.TopCenter).Build();

            GameObjectBuilder.CreateCube("Bottom Center", transformOptionsBottomCenter, MaterialConstants.White)
                .GameObject
                .AddToCollection(collection);

            GameObjectBuilder.CreateCube("Cell Center", transformOptionsCellCenter, MaterialConstants.White)
                .GameObject
                .AddToCollection(collection);

            GameObjectBuilder.CreateCube("Left Center", transformOptionsLeftCenter, MaterialConstants.White)
                .GameObject
                .AddToCollection(collection);

            GameObjectBuilder.CreateCube("Right Center", transformOptionsRightCenter, MaterialConstants.White)
                .GameObject
                .AddToCollection(collection);

            GameObjectBuilder.CreateCube("Top Center", transformOptionsTopCenter, MaterialConstants.White)
                .GameObject
                .AddToCollection(collection);
        }

        private GameObject CreateText(float3 position, string text)
        {
            var rotation = Quaternion.FromToRotation(transform.up, (float3)_space.Options.Plane.Normal);

            var rectTransformOptions = new RectTransformOptionsBuilder().SetLocalPosition(position)
                .SetLocalRotation(rotation)
                .Build();

            var textMeshProOptions = new TextMeshProOptionsBuilder().SetText(text).Build();

            return GameObjectBuilder.CreateTextMeshPro("Text", rectTransformOptions, textMeshProOptions).GameObject;
        }

        private GameObject CreateJonathanFoust(Vector3 position, string name, string meshName, string materialName)
        {
            var meshFile = PathConstants.JonathanFoustMeshesDirectory.ToFileRelativePath(meshName);
            var mesh = meshFile.LoadAssetAtPath<Mesh>();
            var materialFile = PathConstants.JonathanFoustMaterialsDirectory.ToFileRelativePath(materialName);
            var material = materialFile.LoadAssetAtPath<Material>();
            var rotation = Quaternion.FromToRotation(transform.up, (float3)_space.Options.Plane.Normal);

            var transformOptions = new TransformOptionsBuilder().SetPosition(position)
                .SetLocalScale(100f.ToVector3())
                .SetRotation(rotation)
                .Build();

            return GameObjectBuilder.Create(name, transformOptions, mesh, material).GameObject;
        }

        private GameObject CreateOrchidCyan(Vector3 position)
        {
            return CreateJonathanFoust(position, "Orchid Cyan", "Orchid.fbx", "Orchid Cyan.mat");
        }

        private GameObject CreateColeus(Vector3 position)
        {
            return CreateJonathanFoust(position, "Coleus", "Coleus 1.fbx", "Coleus.mat");
        }
    }
}