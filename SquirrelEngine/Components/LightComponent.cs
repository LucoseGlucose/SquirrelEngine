using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SquirrelEngine.Core;
using OpenTK.Mathematics;
using SquirrelEngine.Graphics;

namespace SquirrelEngine.Components
{
    internal class LightComponent : Component
    {
        public Vector3 color = Vector3.One;
        public float strength = 1f;
        public Vector3 Output { get => color * strength; }
        public LightType type;

        public LightComponent() : base()
        {

        }
    }
}
