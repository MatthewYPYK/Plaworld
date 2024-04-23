using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct Point
{
    public int X;
    public int Y;

    public Point(int x, int y)
    {
        this.X = x;
        this.Y = y;
    }

    public static bool operator ==(Point a, Point b)
    {
        return a.X == b.X && a.Y == b.Y;
    }

    public static bool operator !=(Point a, Point b)
    {
        return a.X != b.X || a.Y != b.Y;
    }

    public static Point operator -(Point a, Point b)
    {
        return new Point(a.X - b.X, a.Y - b.Y);
    }

    //override object.Equals
    public override bool Equals(object obj)
    {
        if (obj == null || GetType() != obj.GetType())
        {
            return false;
        }
        Point p = (Point)obj;
        return (X == p.X) && (Y == p.Y);
    }

    //override object.GetHashCode
    public override int GetHashCode()
    {
        return X ^ Y;
    }

}
