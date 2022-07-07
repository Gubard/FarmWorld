using UnityEngine;

namespace Constants
{
    public static class MaterialConstants
    {
        public static readonly Material Red;
        public static readonly Material Green;
        public static readonly Material Blue;
        public static readonly Material White;

        static MaterialConstants()
        {
            Red = new Material(ShaderConstants.Standard)
            {
                color = Color.red
            };

            Green = new Material(ShaderConstants.Standard)
            {
                color = Color.green
            };

            Blue = new Material(ShaderConstants.Standard)
            {
                color = Color.blue
            };
            
            White = new Material(ShaderConstants.Standard)
            {
                color = Color.white
            };
        }
    }
}