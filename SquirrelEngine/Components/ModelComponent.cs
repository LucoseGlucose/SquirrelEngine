using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SquirrelEngine.Core;
using OpenTK.Mathematics;

namespace SquirrelEngine.Graphics
{
    internal class ModelComponent : Component
    {
        public Mesh mesh;
        public Material material;
        public ShaderProgram shader;

        public ModelComponent() : base()
        {

        }
        public override void Start()
        {
            mesh ??= new Mesh(null, null, null);
            material ??= new Material(shader ?? ShaderManager.defaultShader);
        }
        public override void Update()
        {
            material.SetProperty("_modelMat", Transform.matrix);
            material.SetProperty("_viewMat", Rendering.CurrentCamera.ViewMatrix);
            material.SetProperty("_projMat", Rendering.CurrentCamera.ProjectionMatrix);
            material.SetProperty("_ambientLight", Rendering.ambientLight);
            material.SetProperty("_camPos", Rendering.CurrentCamera.Transform.position);
            material.SetProperty("_specFalloff", Rendering.specularFalloff);

            LightComponent[] lights = App.CurentScene.FindComponentsOfType<LightComponent>();
            switch (lights.Length)
            {
                case 1:
                    material.SetProperty("_lightColor", lights[0].Output);
                    material.SetProperty("_lightPos", lights[0].Transform.position);
                    break;
                case > 1:
                    LightComponent closestLight = lights.OrderBy(l => Vector3.DistanceSquared(l.Transform.position, Transform.position)).First();
                    material.SetProperty("_lightColor", closestLight.Output);
                    material.SetProperty("_lightPos", closestLight.Transform.position);
                    break;
                default:
                    break;
            }
        }
    }
}
