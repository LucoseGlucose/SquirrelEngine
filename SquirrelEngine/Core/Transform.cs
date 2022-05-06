using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK.Mathematics;

namespace SquirrelEngine.Core
{
    internal class Transform
    {
        public Vector3 position;
        public Quaternion rotation;
        public Vector3 scale;
        public Matrix4 matrix;
        public Vector3 EulerAngles { get { Quaternion.ToEulerAngles(in rotation, out Vector3 angles); return angles; }
            set { rotation = Quaternion.FromEulerAngles(value); } }

        public Transform(Vector3 pos = new(), Quaternion rot = new(), Vector3 scale = new())
        {
            position = pos;
            rotation = rot;
            this.scale = scale == Vector3.Zero ? Vector3.One : scale;
        }
        public void Start()
        {

        }
        public void Update()
        {
            matrix = Matrix4.CreateFromQuaternion(rotation) * Matrix4.CreateTranslation(position) * Matrix4.CreateScale(scale);
        }
    }
}
