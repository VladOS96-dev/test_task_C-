namespace ShapeLibrary
{
    public class Line : Shape
    {
        public int X1 { get; }
        public int Y1 { get; }
        public int X2 { get; }
        public int Y2 { get; }

        public Line(int x1, int y1, int x2, int y2)
        {
            X1 = x1;
            Y1 = y1;
            X2 = x2;
            Y2 = y2;
        }

        public override void Draw()
        {
            Console.WriteLine($"line at ({X1}, {Y1}), ({X2}, {Y2})");
        }

        public override bool Intersect(Shape other)
        {
            if (other is Line line)
            {
                bool intersects = LinesIntersect(this, line);
                Console.WriteLine(intersects ? $"line at ({X1}, {Y1}), ({X2}, {Y2}) intersects with line at ({line.X1}, {line.Y1}), ({line.X2}, {line.Y2})" : $"line at ({X1}, {Y1}), ({X2}, {Y2}) does not intersect with line at ({line.X1}, {line.Y1}), ({line.X2}, {line.Y2})");
                return intersects;
            }

            if (other is Rect rect)
            {
                return rect.Intersect(this);
            }

            if (other is Circle circle)
            {
                bool intersects = CircleLineIntersection(circle, this);
                Console.WriteLine(intersects ? $"line at ({X1}, {Y1}),  ({X2}, {Y2}) intersects with circle at ({circle.X}, {circle.Y}), radius = {circle.Radius}" : $"line at ({X1}, {Y1}), ({X2}, {Y2}) does not intersect with circle at ({circle.X}, {circle.Y}), radius = {circle.Radius}");
                return intersects;
            }

            if (other is Point point)
            {
                return point.Intersect(this);
            }

            Console.WriteLine("line and other shape do not have intersections.");
            return false;
        }

        private bool LinesIntersect(Line line1, Line line2)
        {
            return LineIntersectsLine(line1.X1, line1.Y1, line1.X2, line1.Y2, line2.X1, line2.Y1, line2.X2, line2.Y2);
        }

        private bool CircleLineIntersection(Circle circle, Line line)
        {
            float dx = line.X2 - line.X1;
            float dy = line.Y2 - line.Y1;
            float fx = line.X1 - circle.X;
            float fy = line.Y1 - circle.Y;

            float a = dx * dx + dy * dy;
            float b = 2 * (fx * dx + fy * dy);
            float c = fx * fx + fy * fy - circle.Radius * circle.Radius;

            float discriminant = b * b - 4 * a * c;
            if (discriminant >= 0)
            {
                discriminant = (float)Math.Sqrt(discriminant);
                float t1 = (-b - discriminant) / (2 * a);
                float t2 = (-b + discriminant) / (2 * a);

                if (t1 >= 0 && t1 <= 1 || t2 >= 0 && t2 <= 1)
                {
                    return true;
                }
            }

            return false;
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