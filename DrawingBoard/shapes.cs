using System;

namespace DrawingBoard
{
    internal abstract class Widget
    {
        public string Name { get; private set; }
        public int X { get; private set; }
        public int Y { get; private set; }

        public virtual double Size => dimensions[0] * dimensions[1];

        // ReSharper disable once InconsistentNaming
        private readonly double[] dimensions;
        public Widget(string name, int x, int y, double[] d)
        {
            Name = name;
            X = x;
            Y = y;
            dimensions = d;
        }

        public virtual void Draw()
        {
            Console.WriteLine($"{Name} (x:{X}, y:{Y}) Size: {Size}");
        }


    }

    class Square : Widget
    {
        public Square(string name, int x, int y, double length) : base(name, x, y,new []{length,length}){}

    }

    class Rectangle: Widget
    {
        public double Height { get; set; }
        public double Width { get; set; }
        public Rectangle(string name, int x, int y, double width, double height) : base(name, x, y,new[]{ width,height})
        {
            Height = height;
            Width = width;
        }

        public override void Draw()
        {
            Console.WriteLine($"{Name} (x:{X}, y:{Y}) Width:{Width} Height:{Height} Size: {Size}");
        }
    }

    class Circle : Widget
    {
        public double Radius { get; }
        public override double Size => Math.PI * Radius * Radius;

        public Circle(string name, int x, int y, double radius) : base(name, x, y, new double[] {})
        {
            Radius = radius;
        }

    }

    class Ellipse : Widget
    {

        public double VerticalDiameter { get; set; }
        public double HorizontalDiameter { get; set; }
        public override double Size => Math.PI * VerticalDiameter * HorizontalDiameter / 4;
        public Ellipse(string name, int x, int y, double verticalDiameter, double horizontalDiameter) : base(name, x, y, new double[]{})
        {
            VerticalDiameter = verticalDiameter;
            HorizontalDiameter = horizontalDiameter;
        }

        public override void Draw()
        {
            Console.WriteLine($@"{Name} (x:{X}, y:{Y}) Vertical D:{HorizontalDiameter} Horizontal D:{VerticalDiameter} Size: {Size}");
        }
    }

    class Textbox : Rectangle
    {
        public string Text { get; set; }
        public string BackColor { get; set; }
        public Textbox(string name, int x, int y, double width, double height,  string backColor, string text) : base(name, x, y, width, height)
        {
            Text = text;
            BackColor = backColor;
        }

        public override void Draw()
        {
            string color = string.IsNullOrEmpty(Text) ? "Red" : BackColor;
            Console.WriteLine($"{Name} (x:{X}, y:{Y}) Width:{Width} Height:{Height} Size: {Size} Background Color:{color} Text:{Text}");
        }
    }


}