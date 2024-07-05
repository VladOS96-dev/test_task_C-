namespace ShapeLibrary
{
    public class Circle : Shape
    {
        public int X { get; }
        public int Y { get; }
        public int Radius { get; }

        public Circle(int x, int y, int radius)
        {
            X = x;
            Y = y;
            Radius = radius;
        }

        public override void Draw()
        {
            Console.WriteLine($"circle at ({X}, {Y}), radius = {Radius}");
        }

        public override bool Intersect(Shape other)
        {
            if (other is Circle circle)
            {
                double distance = Math.Sqrt(Math.Pow(X - circle.X, 2) + Math.Pow(Y - circle.Y, 2));
                bool intersects = distance <= Radius + circle.Radius;
                Console.WriteLine(intersects ? $"circle at ({X}, {Y}), radius = {Radius} intersects with circle at ({circle.X}, {circle.Y}), radius = {circle.Radius}" : $"circle at ({X}, {Y}), radius = {Radius} does not intersect with circle at ({circle.X}, {circle.Y}), radius = {circle.Radius}");
                return intersects;
            }

            if (other is Rect rect)
            {
                return rect.Intersect(this);
            }

            if (other is Line line)
            {
                return line.Intersect(this);
            }

            if (other is Point point)
            {
                return point.Intersect(this);
            }

            Console.WriteLine("circle and other shape do not have intersections.");
            return false;
        }
    }
}
