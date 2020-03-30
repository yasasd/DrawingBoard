using System;
using System.Collections.Generic;

namespace DrawingBoard
{
    class Canvas : List<Widget>
    {
        public void AddShape(string typeName, object[] props)
        {
            var className = char.ToUpper(typeName[0]) + typeName.Substring(1).ToLower();
            var widgetType = Type.GetType("DrawingBoard." + className);
            dynamic shape = Activator.CreateInstance(widgetType ?? throw new InvalidOperationException(), props);
            Add(shape);
        }
        public void RenderShapes()
        {
            if (Count<1) Console.WriteLine("No shapes to draw! please add some.");
            ForEach(shape => shape.Draw());
        }

    }
}
