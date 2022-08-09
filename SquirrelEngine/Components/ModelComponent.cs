using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SquirrelEngine.Core;
using OpenTK.Mathematics;
using OpenTK.Graphics.OpenGL;
using SquirrelEngine.Graphics;

namespace SquirrelEngine.Components
{
    internal class ModelComponent : Component
    {
        public Mesh mesh;
        public Material material;
        public int glVao;
        public int glVerts;
        public int glNormals;
        public int glUVs;
        public int FaceCount { get; private set; }

        public ModelComponent() : base()
        {

        }
        public override void Start()
        {
            mesh ??= new Mesh(Array.Empty<Vector3>(), Array.Empty<Vector3>(), Array.Empty<Vector2>());
            material ??= new Material(ShaderManager.defaultVert, ShaderManager.defaultFrag);
            UpdateMesh(mesh);
        }
        public override void End()
        {
            GL.DeleteVertexArray(glVao);
            GL.DeleteBuffer(glVerts);
            GL.DeleteBuffer(glNormals);
            GL.DeleteBuffer(glUVs);

            for (int i = 0; i < material.Textures.Count; i++)
                GL.DeleteTexture(material.Textures[i].Value.ID);

            GL.DeleteShader(material.Shader.ID);
        }
        public virtual void OnBeforeRender()
        {
            material.SetProperty("_modelMat", Transform.matrix);
            material.SetProperty("_viewMat", Rendering.CurrentCamera.ViewMatrix);
            material.SetProperty("_projMat", Rendering.CurrentCamera.ProjectionMatrix);
            material.SetProperty("_ambientLight", Rendering.ambientLight);
            material.SetProperty("_camPos", Rendering.CurrentCamera.Transform.position);
            material.SetProperty("_specFalloff", Rendering.specularFalloff);
            material.SetProperty("_skybox", App.CurentScene.FindComponentOfType<SkyboxComponent>().material.Textures.First().Value);

            LightComponent[] lights = App.CurentScene.FindComponentsOfType<LightComponent>();
            LightComponent light = lights.Length <= 1 ? lights[0] : 
                lights.OrderBy(l => Vector3.DistanceSquared(l.Transform.position, Transform.position)).First();

            material.SetProperty("_lightColor", light.Output);
            material.SetProperty("_lightPos", light.type switch { LightType.Directional => light.Transform.Forward, _ => light.Transform.position, });
            material.SetProperty("_lightType", (int)light.type);
        }
        public virtual void OnAfterRender() {  }
        public void UpdateMesh(Mesh newMesh)
        {
            mesh = newMesh;

            float[] verts = GraphicsUtils.Expand(mesh.Vertices);
            FaceCount = verts.Length / 3;

            glVerts = GL.GenBuffer();
            glVao = GL.GenVertexArray();

            GL.BindVertexArray(glVao);
            GL.BindBuffer(BufferTarget.ArrayBuffer, glVerts);
            GL.BufferData(BufferTarget.ArrayBuffer, verts.Length * sizeof(float), verts, BufferUsageHint.StaticCopy);

            GL.EnableVertexAttribArray(0);
            GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 0, 0);

            float[] normals = GraphicsUtils.Expand(mesh.Normals);
            glNormals = GL.GenBuffer();

            GL.BindBuffer(BufferTarget.ArrayBuffer, glNormals);
            GL.BufferData(BufferTarget.ArrayBuffer, normals.Length * sizeof(float), normals, BufferUsageHint.StaticCopy);

            GL.EnableVertexAttribArray(1);
            GL.VertexAttribPointer(1, 3, VertexAttribPointerType.Float, false, 0, 0);

            float[] uvs = GraphicsUtils.Expand(mesh.UVs);
            glUVs = GL.GenBuffer();

            GL.BindBuffer(BufferTarget.ArrayBuffer, glUVs);
            GL.BufferData(BufferTarget.ArrayBuffer, uvs.Length * sizeof(float), uvs, BufferUsageHint.StaticCopy);

            GL.EnableVertexAttribArray(2);
            GL.VertexAttribPointer(2, 2, VertexAttribPointerType.Float, false, 0, 0);

            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
            GL.BindVertexArray(0);
            GL.BindTexture(TextureTarget.Texture2D, 0);
        }
    }
}
