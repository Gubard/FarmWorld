using Unity.Entities;
using UnityEngine;

namespace Systems
{
    public class CreateColeusSystem : ComponentSystem
    {
        protected override void OnUpdate()
        {
            if (!Input.GetMouseButtonDown(0))
            {
                return;
            }
            
            
        }
    }
}