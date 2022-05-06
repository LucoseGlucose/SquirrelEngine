using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK.Mathematics;
using System.IO;
using OpenTK.Graphics.OpenGL;

namespace SquirrelEngine.Graphics
{
    internal class Texture
    {
        public byte[] Values { get; private set; }
        public uint width;
        public uint height;
        public int ID { get; private set; }

        public Texture(byte[] vals, uint width, uint height)
        {
            Values = vals;
            this.width = width;
            this.height = height;
        }
        public static implicit operator byte[](Texture texture)
        {
            return texture.Values;
        }
        public static Texture LoadFromBMP(string path)
        {
            if (Path.GetExtension(path) != ".bmp") throw new ArgumentException("Can only read .bmp files!");

            byte[] imageData = File.ReadAllBytes(path);
            byte[] header = imageData.Take(54).ToArray();

            int dataPos = header[0x0A];
            uint witdh = header[0x12];
            uint height = header[0x16];
            
            if (dataPos == 0) dataPos = 54;

            return new Texture(imageData.Skip(dataPos).ToArray(), witdh, height);
        }
    }
}
