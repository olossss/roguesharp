using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RogueSharp.Extensions
{
    public class LightingMap
    {
        private float[,] _light;
        private float[,] _dynamicLight;

        public LightingMap(int width, int height)
        {
            Initialize(width, height);
        }

        public LightingMap(IMap map)
            : this(map.Width, map.Height)
        {
        }

        //stands for rows!
        public int Width { get; private set; }

        //stands for cols!
        public int Height { get; private set; }

        public void Initialize(int width, int height)
        {
            Width = width;
            Height = height;

            _light = new float[width, height];
            _dynamicLight = new float[width, height];

            for (int r = 0; r < width; r++)
                for (int c = 0; c < height; c++)
                {
                    _light[r, c] = 0.0f;
                    _dynamicLight[r, c] = 0.0f;
                }
        }

        public float GetCombinedLight(int x, int y)
        {
            return _light[x, y] + _dynamicLight[x, y];
        }

        public float GetStaticLight(int x, int y)
        {
            return _light[x, y];
        }

        public void SetLight(int x, int y, float light)
        {
            _light[x, y] = light;
        }

        public float GetDynamicLight(int x, int y)
        {
            return _dynamicLight[x, y];
        }

        public void SetDynamicLight(int x, int y, float light)
        {
            _dynamicLight[x, y] = light;
        }

        public void ResetDynamicLight()
        {
            for (int r = 0; r < this.Width; r++)
                for (int c = 0; c < this.Height; c++)
                {
                    _dynamicLight[r, c] = 0.0f;
                }
        }
    }
}
