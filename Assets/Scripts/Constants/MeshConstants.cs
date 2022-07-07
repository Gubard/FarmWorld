using UnityEngine;

namespace Constants
{
    public static class MeshConstants
    {
        public static readonly Mesh Cube;
        public static readonly Mesh Quad;

        static MeshConstants()
        {
            Cube = CreateCubeMesh();
            Quad = CreateQuadMesh();
        }

        private static Mesh CreateQuadMesh()
        {
            var mesh = new Mesh();

            var vertices = new[]
            {
                new Vector3(-0.5f, -0.5f, 0),
                new Vector3(0.5f, -0.5f, 0),
                new Vector3(-0.5f, 0.5f, 0),
                new Vector3(0.5f, 0.5f, 0)
            };

            var tris = new[]
            {
                // lower left triangle
                0,
                3,
                1,
                // upper right triangle
                3,
                0,
                2
            };

            var normals = new[]
            {
                Vector3.back,
                Vector3.back,
                Vector3.back,
                Vector3.back
            };

            var uv = new[]
            {
                new Vector2(0, 0),
                new Vector2(1, 0),
                new Vector2(0, 1),
                new Vector2(1, 1)
            };

            mesh.Clear();
            mesh.vertices = vertices;
            mesh.triangles = tris;
            mesh.normals = normals;
            mesh.uv = uv;
            mesh.Optimize();

            return mesh;
        }

        private static Mesh CreateCubeMesh()
        {
            var mesh = new Mesh();

            //Create a 'Cube' mesh...

            //2) Define the cube's dimensions
            var length = 1f;
            var width = 1f;
            var height = 1f;

            //3) Define the co-ordinates of each Corner of the cube 
            var c = new Vector3[8];
            c[0] = new Vector3(-length * .5f, -width * .5f, height * .5f);
            c[1] = new Vector3(length * .5f, -width * .5f, height * .5f);
            c[2] = new Vector3(length * .5f, -width * .5f, -height * .5f);
            c[3] = new Vector3(-length * .5f, -width * .5f, -height * .5f);
            c[4] = new Vector3(-length * .5f, width * .5f, height * .5f);
            c[5] = new Vector3(length * .5f, width * .5f, height * .5f);
            c[6] = new Vector3(length * .5f, width * .5f, -height * .5f);
            c[7] = new Vector3(-length * .5f, width * .5f, -height * .5f);

            //4) Define the vertices that the cube is composed of:
            //I have used 16 vertices (4 vertices per side). 
            //This is because I want the vertices of each side to have separate normals.
            //(so the object renders light/shade correctly) 
            var vertices = new[]
            {
                c[0],
                c[1],
                c[2],
                c[3], // BottomCenter
                c[7],
                c[4],
                c[0],
                c[3], // LeftCenter
                c[4],
                c[5],
                c[1],
                c[0], // Front
                c[6],
                c[7],
                c[3],
                c[2], // Back
                c[5],
                c[6],
                c[2],
                c[1], // RightCenter
                c[7],
                c[6],
                c[5],
                c[4] // TopCenter
            };

            //5) Define each vertex's Normal
            var up = Vector3.up;
            var down = Vector3.down;
            var forward = Vector3.forward;
            var back = Vector3.back;
            var left = Vector3.left;
            var right = Vector3.right;

            var normals = new[]
            {
                down,
                down,
                down,
                down, // BottomCenter
                left,
                left,
                left,
                left, // LeftCenter
                forward,
                forward,
                forward,
                forward, // Front
                back,
                back,
                back,
                back, // Back
                right,
                right,
                right,
                right, // RightCenter
                up,
                up,
                up,
                up // TopCenter
            };

            //6) Define each vertex's UV co-ordinates
            var uv00 = new Vector2(0f, 0f);
            var uv10 = new Vector2(1f, 0f);
            var uv01 = new Vector2(0f, 1f);
            var uv11 = new Vector2(1f, 1f);

            var uvs = new Vector2[]
            {
                uv11,
                uv01,
                uv00,
                uv10, // BottomCenter
                uv11,
                uv01,
                uv00,
                uv10, // LeftCenter
                uv11,
                uv01,
                uv00,
                uv10, // Front
                uv11,
                uv01,
                uv00,
                uv10, // Back	        
                uv11,
                uv01,
                uv00,
                uv10, // RightCenter 
                uv11,
                uv01,
                uv00,
                uv10 // TopCenter
            };

            //7) Define the Polygons (triangles) that make up the our Mesh (cube)
            //IMPORTANT: Unity uses a 'Clockwise Winding Order' for determining front-facing polygons.
            //This means that a polygon's vertices must be defined in 
            //a clockwise order (relative to the camera) in order to be rendered/visible.
            var triangles = new[]
            {
                3,
                1,
                0,
                3,
                2,
                1, // BottomCenter	
                7,
                5,
                4,
                7,
                6,
                5, // LeftCenter
                11,
                9,
                8,
                11,
                10,
                9, // Front
                15,
                13,
                12,
                15,
                14,
                13, // Back
                19,
                17,
                16,
                19,
                18,
                17, // RightCenter
                23,
                21,
                20,
                23,
                22,
                21, // TopCenter
            };

            //8) Build the Mesh
            mesh.Clear();
            mesh.vertices = vertices;
            mesh.triangles = triangles;
            mesh.normals = normals;
            mesh.uv = uvs;
            mesh.Optimize();

            return mesh;
        }
    }
}