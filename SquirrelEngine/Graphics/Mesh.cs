﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK.Mathematics;
using System.IO;
using Assimp;

namespace SquirrelEngine.Graphics
{
    internal class Mesh
    {
        public Vector3[] Vertices { get; private set; }
        public Vector3[] Normals { get; private set; }
        public Vector2[] UVs { get; private set; }

        public Mesh(Vector3[] vertices, Vector3[] normals, Vector2[] uVs)
        {
            Vertices = vertices;
            Normals = normals;
            UVs = uVs;
        }

        public Mesh(Vector3D[] vertices, Vector3D[] normals, Vector3D[] uVs)
        {
            Vector3[] newVerts = new Vector3[vertices.Length];
            for (int i = 0; i < vertices.Length; i++)
            {
                newVerts[i] = new Vector3(vertices[i].X, vertices[i].Y, vertices[i].Z);
            }
            Vertices = newVerts;

            Vector3[] newNorms = new Vector3[normals.Length];
            for (int i = 0; i < normals.Length; i++)
            {
                newNorms[i] = new Vector3(normals[i].X, normals[i].Y, normals[i].Z);
            }
            Normals = newNorms.ToArray();

            Vector2[] newUVs = new Vector2[uVs.Length];
            for (int i = 0; i < uVs.Length; i++)
            {
                newUVs[i] = new Vector2(uVs[i].X, uVs[i].Y);
            }
            UVs = newUVs;
        }

        public static Mesh LoadFromOBJ(string path)
        {
            if (Path.GetExtension(path) != ".obj") throw new ArgumentException("This function can only load .obj files!");
            string[] lines = File.ReadAllLines(path);

            List<Vector3> verts = new();
            List<Vector3> norms = new();
            List<Vector2> uvs = new();
            List<Vector3> faces = new();

            foreach (string line in lines)
            {
                string[] blocks = line.Split(' ');
                switch (blocks.First())
                {
                    case "v":
                        List<float> vertexVals = new();
                        for (int i = 1; i < blocks.Length; i++) { vertexVals.Add(float.Parse(blocks[i])); }
                        verts.Add(new Vector3(vertexVals[0], vertexVals[1], vertexVals[2]));
                        break;
                    case "vn":
                        List<float> normalVals = new();
                        for (int i = 1; i < blocks.Length; i++) { normalVals.Add(float.Parse(blocks[i])); }
                        norms.Add(new Vector3(normalVals[0], normalVals[1], normalVals[2]));
                        break;
                    case "vt":
                        List<float> uvVals = new();
                        for (int i = 1; i < blocks.Length; i++) { uvVals.Add(float.Parse(blocks[i])); }
                        uvs.Add(new Vector2(uvVals[0], uvVals[1]));
                        break;
                    case "f":
                        for (int i = 1; i < blocks.Length; i++)
                        {
                            List<float> faceVals = new();
                            foreach (string index in blocks[i].Split('/')) { faceVals.Add(float.Parse(index)); }
                            faces.Add(new Vector3(faceVals[0], faceVals[1], faceVals[2]));
                        }
                        break;
                    default:
                        break;
                }
            }

            List<Vector3> finalVerts = new();
            List<Vector3> finalNorms = new();
            List<Vector2> finalUVs = new();

            for (int i = 0; i < faces.Count; i++)
            {
                finalVerts.Add(verts[(int)faces[i].X - 1]);
                finalNorms.Add(norms[(int)faces[i].Z - 1]);
                finalUVs.Add(uvs[(int)faces[i].Y - 1]);
            }

            return new Mesh(finalVerts.ToArray(), finalNorms.ToArray(), finalUVs.ToArray());
        }
        
        public static Mesh ImportMesh(string path)
        {
            AssimpContext ctx = new();
            Scene model = ctx.ImportFile(path);
            Assimp.Mesh modelMesh = model.Meshes.FirstOrDefault();
            return new Mesh(modelMesh.Vertices.ToArray(), modelMesh.Normals.ToArray(), modelMesh.TextureCoordinateChannels.FirstOrDefault().ToArray());
        }
    }
}
