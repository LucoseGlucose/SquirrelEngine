using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SquirrelEngine.Core;
using OpenTK.Graphics.OpenGL;
using SquirrelEngine.Graphics;
using OpenTK.Mathematics;

namespace SquirrelEngine.Components
{
    internal class SkyboxComponent : ModelComponent
    {
        public SkyboxComponent() : base()
        {

        }
        public override void OnBeforeRender()
        {
            base.OnBeforeRender();

            GL.Disable(EnableCap.DepthTest);
            material.SetProperty("_viewMat", Rendering.CurrentCamera.ViewMatrix.ClearTranslation());
        }
        public override void OnAfterRender()
        {
            GL.Enable(EnableCap.DepthTest);
        }
    }
}
