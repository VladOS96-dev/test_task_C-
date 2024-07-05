using ShapeLibrary;

using System.IO;
namespace testPetroGM
{
    internal class Program
    {
        static void Main(string[] args)
        {
            IShapeFactory shapeFactory = new ShapeFactory();
            ShapeParser shapeParser = new ShapeParser(shapeFactory);


            string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "input.txt");

            if (!File.Exists(filePath))
            {
                Console.WriteLine($"Error: The file '{filePath}' does not exist.");
                return;
            }

            List<Shape> shapes = shapeParser.ParseShapes(filePath);

            if (shapes.Count == 0)
            {
                Console.WriteLine("No shapes were parsed from the file.");
                return;
            }


            foreach (Shape shape in shapes)
            {
                    shape.Draw();
            }

 
            for (int i = 0; i < shapes.Count; i++)
            {
                for (int j = i + 1; j < shapes.Count; j++)
                {

                        shapes[i].Intersect(shapes[j]);
                   
                }
            }
        }
    }
}
