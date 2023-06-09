﻿using WirePlacer.Models;
using Point = WirePlacer.Models.Point;

namespace WirePlacer.Solvers;

/// <summary>
///     Solves smallest enclosing circle problem using a variant of Welzl’s algorithm.
///     Source:
///     https://www.nayuki.io/page/smallest-enclosing-circle
/// </summary>
public static class SmallestEnclosingCircle
{
    /// <summary>
    ///     Returns the smallest circle that encloses all the given points.
    ///     Runs in expected O(n) time, randomized.
    ///     Note:
    ///     If 0 points are given, a circle of radius -1 is returned.
    ///     If 1 point is given, a circle of radius 0 is returned.
    /// </summary>
    /// <param name="points"></param>
    /// <returns></returns>
    public static Circle MakeCircle(IList<Point> points)
    {
        // Clone list to preserve the caller's data, do Durstenfeld shuffle
        var shuffled = new List<Point>(points);
        var rand = new Random();
        for (var i = shuffled.Count - 1; i > 0; i--)
        {
            var j = rand.Next(i + 1);
            var temp = shuffled[i];
            shuffled[i] = shuffled[j];
            shuffled[j] = temp;
        }

        // Progressively add points to circle or recompute circle
        var c = Circle.Invalid;
        for (var i = 0; i < shuffled.Count; i++)
        {
            var p = shuffled[i];
            if (c.Radius < 0 || !c.Contains(p))
            {
                c = MakeCircleOnePoint(shuffled.GetRange(0, i + 1), p);
            }
        }

        return c;
    }

    // One boundary point known
    private static Circle MakeCircleOnePoint(List<Point> points, Point p)
    {
        var c = new Circle(p, 0);
        for (var i = 0; i < points.Count; i++)
        {
            var q = points[i];
            if (!c.Contains(q))
            {
                if (c.Radius == 0)
                {
                    c = MakeDiameter(p, q);
                }
                else
                {
                    c = MakeCircleTwoPoints(points.GetRange(0, i + 1), p, q);
                }
            }
        }

        return c;
    }

    // Two boundary points known
    private static Circle MakeCircleTwoPoints(List<Point> points, Point p, Point q)
    {
        var circ = MakeDiameter(p, q);
        var left = Circle.Invalid;
        var right = Circle.Invalid;

        // For each point not in the two-point circle
        var pq = q.Subtract(p);
        foreach (var r in points)
        {
            if (circ.Contains(r))
            {
                continue;
            }

            // Form a circumcircle and classify it on left or right side
            var cross = pq.Cross(r.Subtract(p));
            var c = MakeCircumcircle(p, q, r);
            if (c.Radius < 0)
            {
                continue;
            }

            if (cross > 0 && (left.Radius < 0 || pq.Cross(c.Center.Subtract(p)) > pq.Cross(left.Center.Subtract(p))))
            {
                left = c;
            }
            else if (cross < 0 &&
                     (right.Radius < 0 || pq.Cross(c.Center.Subtract(p)) < pq.Cross(right.Center.Subtract(p))))
            {
                right = c;
            }
        }

        // Select which circle to return
        if (left.Radius < 0 && right.Radius < 0)
        {
            return circ;
        }

        if (left.Radius < 0)
        {
            return right;
        }

        if (right.Radius < 0)
        {
            return left;
        }

        return left.Radius <= right.Radius ? left : right;
    }

    private static Circle MakeDiameter(Point a, Point b)
    {
        var c = new Point((a.X + b.X) / 2, (a.Y + b.Y) / 2);
        return new Circle(c, Math.Max(c.Distance(a), c.Distance(b)));
    }

    private static Circle MakeCircumcircle(Point a, Point b, Point c)
    {
        // Mathematical algorithm from Wikipedia: Circumscribed circle
        var ox = (Math.Min(Math.Min(a.X, b.X), c.X) + Math.Max(Math.Max(a.X, b.X), c.X)) / 2;
        var oy = (Math.Min(Math.Min(a.Y, b.Y), c.Y) + Math.Max(Math.Max(a.Y, b.Y), c.Y)) / 2f;
        var ax = a.X - ox;
        var ay = a.Y - oy;
        var bx = b.X - ox;
        var by = b.Y - oy;
        var cx = c.X - ox;
        var cy = c.Y - oy;
        var d = (ax * (by - cy) + bx * (cy - ay) + cx * (ay - by)) * 2;
        if (d == 0)
        {
            return Circle.Invalid;
        }

        var X = ((ax * ax + ay * ay) * (by - cy) + (bx * bx + by * by) * (cy - ay) + (cx * cx + cy * cy) * (ay - by)) /
                d;
        var Y = ((ax * ax + ay * ay) * (cx - bx) + (bx * bx + by * by) * (ax - cx) + (cx * cx + cy * cy) * (bx - ax)) /
                d;
        var p = new Point(ox + X, oy + Y);
        var r = Math.Max(Math.Max(p.Distance(a), p.Distance(b)), p.Distance(c));
        return new Circle(p, r);
    }
}
