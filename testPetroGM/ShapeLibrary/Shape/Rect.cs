using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShapeLibrary
{
    public class Rect : Shape
    {
        public int X1 { get; }
        public int Y1 { get; }
        public int X2 { get; }
        public int Y2 { get; }

        public Rect(int x1, int y1, int x2, int y2)
        {
            X1 = x1;
            Y1 = y1;
            X2 = x2;
            Y2 = y2;
        }

        public override void Draw()
        {
            Console.WriteLine($"rect at ({X1}, {Y1}), ({X2}, {Y2})");
        }



        public override bool Intersect(Shape other)
        {
            if (other is Rect rect)
            {
                bool intersects = X1 <= rect.X2 && X2 >= rect.X1 && Y1 <= rect.Y2 && Y2 >= rect.Y1;
                Console.WriteLine(intersects ? $"rect at ({X1}, {Y1}), ({X2}, {Y2}) intersects with rect at ({rect.X1}, {rect.Y1}), ({rect.X2}, {rect.Y2})" : $"rect at ({X1}, {Y1}), ({X2}, {Y2}) does not intersect with rect at ({rect.X1}, {rect.Y1}), ({rect.X2}, {rect.Y2})");
                return intersects;
            }

            if (other is Line line)
            {
                bool intersects = LineRectIntersection(line, this);
                Console.WriteLine(intersects ? $"line at ({line.X1}, {line.Y1}), ({line.X2}, {line.Y2}) intersects with rect at ({X1}, {Y1}), ({X2}, {Y2})" : $"line at ({line.X1}, {line.Y1}), ({line.X2}, {line.Y2}) does not intersect with rect at ({X1}, {Y1}), ({X2}, {Y2})");
                return intersects;
            }

            if (other is Circle circle)
            {
                bool intersects = CircleRectIntersection(circle, this);
                Console.WriteLine(intersects ? $"circle at ({circle.X}, {circle.Y}), radius = {circle.Radius} intersects with rect at ({X1}, {Y1}), ({X2}, {Y2})" : $"circle at ({circle.X}, {circle.Y}), radius = {circle.Radius} does not intersect with rect at ({X1}, {Y1}), ({X2}, {Y2})");
                return intersects;
            }

            if (other is Point point)
            {
                return point.Intersect(this);
            }

            Console.WriteLine("rect and other shape do not have intersections.");
            return false;
        }

        private bool LineRectIntersection(Line line, Rect rect)
        {
            return LineIntersectsLine(line.X1, line.Y1, line.X2, line.Y2, rect.X1, rect.Y1, rect.X2, rect.Y1) ||
                   LineIntersectsLine(line.X1, line.Y1, line.X2, line.Y2, rect.X2, rect.Y1, rect.X2, rect.Y2) ||
                   LineIntersectsLine(line.X1, line.Y1, line.X2, line.Y2, rect.X2, rect.Y2, rect.X1, rect.Y2) ||
                   LineIntersectsLine(line.X1, line.Y1, line.X2, line.Y2, rect.X1, rect.Y2, rect.X1, rect.Y1);
        }

        private bool CircleRectIntersection(Circle circle, Rect rect)
        {
            int closestX = Clamp(circle.X, rect.X1, rect.X2);
            int closestY = Clamp(circle.Y, rect.Y1, rect.Y2);

            int distanceX = circle.X - closestX;
            int distanceY = circle.Y - closestY;

            int distanceSquared = distanceX * distanceX + distanceY * distanceY;
            return distanceSquared <= circle.Radius * circle.Radius;
        }

        private int Clamp(int value, int min, int max)
        {
            return Math.Max(min, Math.Min(max, value));
        }

        private bool LineIntersectsLine(int x1, int y1, int x2, int y2, int x3, int y3, int x4, int y4)
        {
            float denominator = (x4 - x3) * (y2 - y1) - (x2 - x1) * (y4 - y3);
            if (denominator == 0)
                return false;

            float numeratorA = (y3 - y4) * (x1 - x3) + (x4 - x3) * (y1 - y3);
            float numeratorB = (y1 - y2) * (x1 - x3) + (x2 - x1) * (y1 - y3);

            float uA = numeratorA / denominator;
            float uB = numeratorB / denominator;

            return uA >= 0 && uA <= 1 && uB >= 0 && uB <= 1;
        }
    }
}
