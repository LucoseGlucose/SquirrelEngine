using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SquirrelEngine.Core;
using OpenTK.Mathematics;

namespace SquirrelEngine.Graphics
{
    internal class LightComponent : Component
    {
        public Vector3 color = Vector3.One;
        public float strength = 1f;
        public Vector3 Output { get => color * strength; }

        public LightComponent() : base()
        {

        }
    }
}
