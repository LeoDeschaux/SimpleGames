using System;
using System.Collections.Generic;
using System.Text;

namespace myEngine.myProject.Snake
{
    public struct Vec2Int
    {
        public int X;
        public int Y;

        public Vec2Int(int x, int y)
        {
            X = x;
            Y = y;
        }

        public override string ToString()
        {
            return "{" + X + "," + Y + "}";
        }

        public static Vec2Int operator +(Vec2Int a) => a;

        public static Vec2Int operator +(Vec2Int a, Vec2Int b)
        => new Vec2Int(a.X + b.X, a.Y + b.Y);
    }
}
