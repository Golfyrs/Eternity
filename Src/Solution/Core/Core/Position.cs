using System;

namespace Eternity.Core
{
    [Serializable]
    public class Position
    {
        public Position(float x, float y)
        {
            X = x;
            Y = y;
        }

        public float X;
        public float Y;
    }
}