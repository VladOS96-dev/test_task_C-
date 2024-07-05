using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShapeLibrary
{
    public class Point : Shape
    {
        public int X { get; }
        public int Y { get; }

        public Point(int x, int y)
        {
            X = x;
            Y = y;
        }

        public override void Draw()
        {
            Console.WriteLine($"point at ({X}, {Y})");
        }

        public override bool Intersect(Shape other)
        {
            if (other is Point point)
            {
                bool intersects = X == point.X && Y == point.Y;
                Console.WriteLine(intersects ? $"point at ({X}, {Y}) intersects with point at ({point.X}, {point.Y})" : $"point at ({X}, {Y}) does not intersect with point at ({point.X}, {point.Y})");
                return intersects;
            }

            if (other is Line line)
            {
                bool intersects = X == line.X1 && Y == line.Y1 || X == line.X2 && Y == line.Y2;
                Console.WriteLine(intersects ? $"point at ({X}, {Y}) intersects with line at ({line.X1}, {line.Y1}), ({line.X2}, {line.Y2})" : $"point at ({X}, {Y}) does not intersect with Line at ({line.X1}, {line.Y1}), ({line.X2}, {line.Y2})");
                return intersects;
            }

            if (other is Rect rect)
            {
                bool intersects = X >= rect.X1 && X <= rect.X2 && Y >= rect.Y1 && Y <= rect.Y2;
                Console.WriteLine(intersects ? $"point at ({X}, {Y}) intersects with rect at ({rect.X1}, {rect.Y1}), ({rect.X2}, {rect.Y2})" : $"point at ({X}, {Y}) does not intersect with rect at ({rect.X1}, {rect.Y1}), ({rect.X2}, {rect.Y2})");
                return intersects;
            }

            if (other is Circle circle)
            {
                double distance = Math.Sqrt(Math.Pow(X - circle.X, 2) + Math.Pow(Y - circle.Y, 2));
                bool intersects = distance <= circle.Radius;
                Console.WriteLine(intersects ? $"point at ({X}, {Y}) intersects with circle at ({circle.X}, {circle.Y}), radius = {circle.Radius}" : $"point at ({X}, {Y}) does not intersect with circle at ({circle.X}, {circle.Y}), radius = {circle.Radius}");
                return intersects;
            }

            Console.WriteLine("The point cannot intersect the other shape.");
            return false;
        }
    }
}
