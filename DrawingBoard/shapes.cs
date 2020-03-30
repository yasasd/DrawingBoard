using System;

namespace DrawingBoard
{
    internal abstract class Widget
    {
        public string Name { get;  set; }
        public int X { get;  set; }
        public int Y { get;  set; }
        public virtual double Size { get; }

        protected Widget(string name, int x, int y)
        {
            Name = name;
            X = x;
            Y = y;
        }

        public virtual void Draw()
        {
            Console.WriteLine($"{Name} (x:{X}, y:{Y}) Size: {Size}");
        }


    }

    class Square : Widget
    {
        public double Length { get; set; }

        public Square(string name, int x, int y, double length) : base(name, x, y)
        {
            Length = length;
        }

        public override double Size => Length * Length;
    }

    class Rectangle: Widget
    {
        public double Height { get; set; }
        public double Width { get; set; }
        public Rectangle(string name, int x, int y, double width, double height) : base(name, x, y)
        {
            Height = height;
            Width = width;
        }

        public override double Size => Height * Width;

        public override void Draw()
        {
            Console.WriteLine($"{Name} (x:{X}, y:{Y}) Width:{Width} Height:{Height} Size: {Size}");
        }
    }

    class Circle : Widget
    {
        public double Radius { get; }
        public override double Size => Math.PI * Radius * Radius;

        public Circle(string name, int x, int y, double radius) : base(name, x, y)
        {
            Radius = radius;
        }

    }

    class Ellipse : Widget
    {

        public double VerticalDiameter { get; set; }
        public double HorizontalDiameter { get; set; }
        public override double Size => Math.PI * VerticalDiameter * HorizontalDiameter / 4;
        public Ellipse(string name, int x, int y, double verticalDiameter, double horizontalDiameter) : base(name, x, y)
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