using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SquirrelEngine.Core;
using OpenTK.Mathematics;

namespace SquirrelEngine.Graphics
{
    internal class CameraComponent : Component
    {
        public Matrix4 ProjectionMatrix { get; private set; }
        public Matrix4 ViewMatrix { get; private set; }
        public Vector4 settings;

        public CameraComponent() : base()
        {
            
        }

        public override void Start()
        {
            ProjectionMatrix = Matrix4.CreatePerspectiveFieldOfView(settings.X, settings.Y, settings.Z, settings.W);
        }
        public override void Update()
        {
            ViewMatrix = Matrix4.LookAt(Transform.position, Transform.rotation * Vector3.UnitZ, Transform.rotation * Vector3.UnitY);
        }
    }
}
