using System;
using System.Linq;

namespace DrawingBoard
{
    class Program
    {
        static void Main()
        {
            Canvas canvas = new Canvas();
            var exit = false;
            Console.Clear();
            Console.WriteLine("**************************************Drawing Board**************************************");
            while (exit == false)
            {
                Console.WriteLine();
                Console.Write("Enter command (help to display help): ");
                exit = CommandParser(Console.ReadLine(),canvas);
            }
        }

        private static bool CommandParser(string command, Canvas canvas)
        {
            try
            {
                string[] commandStrings = command.Split("-");
                switch (commandStrings[0].ToLower().Trim())
                {
                    case "help":
                        Console.WriteLine(HelpText);
                        return false;
                    case "add":
                        var addCommand = command.Substring(command.IndexOf("-", StringComparison.Ordinal)+1).TrimStart();
                        var typeName = addCommand.Split(',')[0];
                        var parameters = GetShapeParams(addCommand);
                        canvas.AddShape(typeName, parameters);
                        return false;
                    case "render":
                        Console.WriteLine();
                        canvas.RenderShapes();
                        return false;
                    case "exit":
                        return true;
                    default:
                        return false;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Command Error: "+ex.Message);
                return false;
            }

        }

        private static object[] GetShapeParams(string command)
        {
            var tokens = command.Split(",");
            //var commandParams = GetCommonParams(tokens[1..]); need c# 8.0, using linq instead
            var commandParams = GetCommonParams(tokens.Skip(1).ToArray());
            var shapeParams = commandParams.Append(Convert.ToDouble(tokens[4])).ToArray(); //length,radius, width or vertical diameter
            switch (tokens[0].Trim().ToLower())
            {
                case "circle":
                case "square":
                    return shapeParams;
                case "rectangle":
                case "ellipse":
                    return shapeParams.Append(Convert.ToDouble(tokens[5])).ToArray(); //height or horizontal diameter
                case "textbox":
                    var adParams = new object[3];
                    adParams[0] = Convert.ToDouble(tokens[5]); //height
                    adParams[1] = tokens[6];//color
                    adParams[2] = tokens.Length > 7 ? tokens[7] : ""; //text
                    return shapeParams.Concat(adParams).ToArray();
                default:
                    throw new Exception("Invalid shape type");
            }
        }

        private static object[] GetCommonParams(string[] tokens)
        {
            var result = new object[3];
            result[0] = tokens[0];//name
            result[1] = Convert.ToInt32(tokens[1]);//x
            result[2] = Convert.ToInt32(tokens[2]);//y
            return result;
        }

        private static readonly string HelpText = @"
    ***********************************************************************************
    *   add-[shape],[name],[x],[y],[properties]...                                    *
    *            properties must be entered in the following order for each shape     *
    *                    Square - Length                                              *
    *                    Rectangle - Height, Width                                    *
    *                    Circle - Radius                                              *
    *                    Ellipse - Vertical diameter , Horizontal diameter            *  
    *                    TextBox - Width, Height, Background Color, Text              *
    *            ex: to add a rectangle of width 10 and height 5 at x = -5 and y = 8  *
    *               enter the following command,                                      *
    *               add-Rectangle,testBox,-5,8,10,5                                   *
    *    render                                                                       *
    *        render all shapes on canvas                                              *
    *    exit                                                                         *
    *        exit application                                                         *
    ***********************************************************************************";


    }
}
