using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Reflection.Metadata.Ecma335;
using ShapeLibrary;


namespace testPetroGM
{
    public class ShapeParser
    {
        private readonly IShapeFactory _shapeFactory;

        public ShapeParser(IShapeFactory shapeFactory)
        {
            _shapeFactory = shapeFactory;
        }

        public List<Shape> ParseShapes(string filePath)
        {
            List<Shape> shapes = new List<Shape>();

            try
            {
                using (FileStream fileStream = new FileStream(filePath, FileMode.Open))
                using (StreamReader streamReader = new StreamReader(fileStream))
                {
                    string line;
                    while ((line = streamReader.ReadLine()) != null)
                    {
                        Shape shape = _shapeFactory.CreateShape(line);
                        if (shape != null)
                        {
                            shapes.Add(shape);
                        }
                        else
                        {
                            Console.WriteLine($"Error: Failed to create shape from line '{line}'.");
                        }
                    }
                }
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine($"Error: The file '{filePath}' was not found.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: An unexpected error occurred while reading the file '{filePath}'. Details: {ex.Message}");
            }

            return shapes;
        }

    }
}
