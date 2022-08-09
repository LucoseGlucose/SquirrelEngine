using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK.Mathematics;
using SquirrelEngine.Graphics;

namespace SquirrelEngine.Core
{
    internal class Transform
    {
        public Vector3 position = Vector3.Zero;
        public Vector3 rotation = Vector3.Zero;
        public Vector3 scale = Vector3.One;
        public Matrix4 matrix;
        public Quaternion Orientation 
        {
            get { return new Quaternion(GraphicsUtils.DegreesToRadians(rotation)); }
            set { rotation = value.ToEulerAngles(); }
        }
        public Vector3 Forward { get { return Orientation * Vector3.UnitZ; } }
        public Vector3 Up { get { return Orientation * Vector3.UnitY; } }
        public Vector3 Right { get { return Orientation * Vector3.UnitX; } }

        public void Start()
        {

        }
        public void Update()
        {
            matrix = Matrix4.CreateFromQuaternion(Orientation) * Matrix4.CreateTranslation(position) * Matrix4.CreateScale(scale);
        }
    }
}
