using ShapeLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShapeLibrary
{
    public interface IShapeFactory
    {
        Shape CreateShape(string shapeType);
    }
}
