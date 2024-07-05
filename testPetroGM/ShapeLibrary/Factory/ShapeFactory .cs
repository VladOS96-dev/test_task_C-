using ShapeLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShapeLibrary
{
    public class ShapeFactory : IShapeFactory
    {
        public Shape CreateShape(string description)
        {
            if (string.IsNullOrWhiteSpace(description))
            {
                Console.WriteLine("Error: Description is empty.");
                return null;
            }

            string[] parts = description.Split(' ');
            if (parts.Length == 0)
            {
                Console.WriteLine("Error: Description does not contain enough parts.");
                return null;
            }

            string shapeType = parts[0].ToLower();

            try
            {
                switch (shapeType)
                {
                    case "point":
                        return CreatePoint(parts);
                    case "rect":
                        return CreateRect(parts);
                    case "line":
                        return CreateLine(parts);
                    case "circle":
                        return CreateCircle(parts);
                    default:
                        Console.WriteLine($"Error: Unknown shape type '{shapeType}'.");
                        return null;
                }
            }
            catch (FormatException ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return null;
            }
            catch (IndexOutOfRangeException ex)
            {
                Console.WriteLine($"Error: Not enough parameters for {shapeType}. Details: {ex.Message}");
                return null;
            }
        }

        private Shape CreatePoint(string[] parts)
        {
            if (parts.Length != 3)
                throw new IndexOutOfRangeException("Point requires exactly 2 parameters.");

            if (!int.TryParse(parts[1], out int x) || !int.TryParse(parts[2], out int y))
                throw new FormatException("Point parameters must be integers.");

            return new Point(x, y);
        }

        private Shape CreateRect(string[] parts)
        {
            if (parts.Length != 5)
                throw new IndexOutOfRangeException("Rect requires exactly 4 parameters.");

            if (!int.TryParse(parts[1], out int x1) || !int.TryParse(parts[2], out int y1) ||
                !int.TryParse(parts[3], out int x2) || !int.TryParse(parts[4], out int y2))
                throw new FormatException("Rect parameters must be integers.");

            return new Rect(x1, y1, x2, y2);
        }

        private Shape CreateLine(string[] parts)
        {
            if (parts.Length != 5)
                throw new IndexOutOfRangeException("Line requires exactly 4 parameters.");

            if (!int.TryParse(parts[1], out int x1) || !int.TryParse(parts[2], out int y1) ||
                !int.TryParse(parts[3], out int x2) || !int.TryParse(parts[4], out int y2))
                throw new FormatException("Line parameters must be integers.");

            return new Line(x1, y1, x2, y2);
        }

        private Shape CreateCircle(string[] parts)
        {
            if (parts.Length != 4)
                throw new IndexOutOfRangeException("Circle requires exactly 3 parameters.");

            if (!int.TryParse(parts[1], out int x) || !int.TryParse(parts[2], out int y) ||
                !int.TryParse(parts[3], out int radius))
                throw new FormatException("Circle parameters must be integers.");

            return new Circle(x, y, radius);
        }
    }
}
