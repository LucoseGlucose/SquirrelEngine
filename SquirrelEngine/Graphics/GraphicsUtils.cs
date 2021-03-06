using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK.Mathematics;
using System.IO;
using OpenTK.Graphics.OpenGL;
using SquirrelEngine.Core;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

namespace SquirrelEngine.Graphics
{
    internal static class GraphicsUtils
    {
        public static float[] Expand(Vector3[] array)
        {
            float[] result = new float[array.Length * 3];

            for (int i = 0; i < array.Length; i++)
            {
                result[i * 3] = array[i].X;
                result[i * 3 + 1] = array[i].Y;
                result[i * 3 + 2] = array[i].Z;
            }

            return result;
        }

        public static float[] Expand(Vector2[] array)
        {
            float[] result = new float[array.Length * 2];

            for (int i = 0; i < array.Length; i++)
            {
                result[i * 2] = array[i].X;
                result[i * 2 + 1] = array[i].Y;
            }

            return result;
        }

        public static float[] Expand(Vector4[] array)
        {
            float[] result = new float[array.Length * 4];

            for (int i = 0; i < array.Length; i++)
            {
                result[i * 2] = array[i].X;
                result[i * 2 + 1] = array[i].Y;
                result[i * 3 + 2] = array[i].Z;
                result[i * 3 + 3] = array[i].W;
            }

            return result;
        }

        public static int[] ByteToIntArray(byte[] bytes)
        {
            int[] result = new int[bytes.Length];
            for (int i = 0; i < bytes.Length; i++) { result[i] = bytes[i]; }
            return result;
        }

        public static Type UniformToCSType(ActiveUniformType type)
        {
            return type switch
            {
                ActiveUniformType.Int => typeof(int),
                ActiveUniformType.UnsignedInt => typeof(uint),
                ActiveUniformType.Float => typeof(float),
                ActiveUniformType.Double => typeof(double),
                ActiveUniformType.FloatVec2 => typeof(Vector2),
                ActiveUniformType.FloatVec3 => typeof(Vector3),
                ActiveUniformType.FloatVec4 => typeof(Vector4),
                ActiveUniformType.FloatMat2 => typeof(Matrix2),
                ActiveUniformType.FloatMat3 => typeof(Matrix3),
                ActiveUniformType.FloatMat4 => typeof(Matrix4),
                ActiveUniformType.Sampler2D => typeof(Texture),
                _ => null,
            };
        }

        public static byte[] GetImagePixelData(Image<Rgba32> image)
        {
            Rgba32[] pixels = new Rgba32[image.Width * image.Height];
            for (int x = 0; x < image.Width; x++)
            {
                for (int y = 0; y < image.Height; y++)
                {
                    pixels[x + image.Width * y] = image[x, y];
                }
            }

            byte[] rawData = new byte[pixels.Length * 4];
            for (int i = 0; i < pixels.Length; i++)
            {
                rawData[i * 4] = pixels[i].R;
                rawData[i * 4 + 1] = pixels[i].G;
                rawData[i * 4 + 2] = pixels[i].B;
                rawData[i * 4 + 3] = pixels[i].A;
            }

            return rawData;
        }

        /*public static int[] BytesToInts(byte[] bytes)
        {
            int[] ints = new int[bytes.Length / 4];
            for (int i = 0; i < ints.Length; i++)
            {
                ints[i] = bytes[i * 4];
            }
        }*/

        public static Vector2 DegreesToRadians(Vector2 vector)
        {
            return new(MathHelper.DegreesToRadians(vector.X), MathHelper.DegreesToRadians(vector.Y));
        }

        public static Vector3 DegreesToRadians(Vector3 vector)
        {
            return new(MathHelper.DegreesToRadians(vector.X), MathHelper.DegreesToRadians(vector.Y), MathHelper.DegreesToRadians(vector.Z));
        }

        public static Vector2 RadiansToDegrees(Vector2 vector)
        {
            return new(MathHelper.RadiansToDegrees(vector.X), MathHelper.RadiansToDegrees(vector.Y));
        }

        public static Vector3 RadiansToDegrees(Vector3 vector)
        {
            return new(MathHelper.RadiansToDegrees(vector.X), MathHelper.RadiansToDegrees(vector.Y), MathHelper.RadiansToDegrees(vector.Z));
        }

        public static Vector3 Abs(Vector3 vector)
        {
            return new Vector3(Math.Abs(vector.X), Math.Abs(vector.Y), Math.Abs(vector.Z));
        }
    }
}
