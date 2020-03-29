using System;
using System.Collections.Generic;

namespace DrawingBoard
{
    class Canvas
    { 
        private List<Widget> Shapes = new List<Widget>();

        public void AddShape(string typeName, object[] props)
        {
            var className = char.ToUpper(typeName[0]) + typeName.Substring(1).ToLower();
            var widgetType = Type.GetType("DrawingBoard." + className);
            dynamic shape = Activator.CreateInstance(widgetType ?? throw new InvalidOperationException(), props);
            Shapes.Add(shape);
        }
        public void RenderShapes()
        {
            if (Shapes.Count<1) Console.WriteLine("No shapes to draw! please add some.");
            Shapes.ForEach(shape => shape.Draw());
        }



    }
}
