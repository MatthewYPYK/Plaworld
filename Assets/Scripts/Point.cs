using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct Point
{
    public int X { get; set; }
    public int Y { get; set; }

    public Point(int x, int y)
    {
        this.X = x;
        this.Y = y;
    }

    public static bool operator == (Point a, Point b)
    {
        return a.X == b.X && a.Y == b.Y;
    }

    public static bool operator != (Point a, Point b)
    {
        return a.X != b.X || a.Y != b.Y;
    }

    public static Point operator - (Point a, Point b)
    {
        return new Point(a.X - b.X, a.Y - b.Y);
    }
}
