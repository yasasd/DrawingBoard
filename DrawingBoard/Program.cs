using System;
using System.Linq;

namespace DrawingBoard
{
    class Program
    {
        static string HelpText = @"
        add-[shape],[name],[x],[y],[properties],...
                available shapes and properties
                        Square - Length (required)
                        Rectangle - Height (required), Width (required)
                        Circle - Radius (required)
                        Ellipse - axis a (required), axis b (required)
                        TextBox - Width (required), Height (required), Background Color (required), Text (optional)
                
                ex: to add a rectangle of height 5 and width 10 at x = -5 and y = 8 enter the following command
                add-Circle,testCircle,-5,8,10
        render
            render all shapes on canvas
        exit
            exit application";

        static void Main()
        {
            Canvas canvas = new Canvas();
            var exit = false;
            while (exit == false)
            {
                Console.WriteLine();
                Console.Write("Enter command (help to display help): ");
                exit = CommandParser(Console.ReadLine(),canvas);
            }
        }

        static bool CommandParser(string command, Canvas canvas)
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
                        Console.WriteLine("**********************************Drawing Board**********************************");
                        Console.WriteLine();
                        canvas.RenderShapes();
                        Console.WriteLine();
                        Console.WriteLine("*********************************************************************************");
                        Console.WriteLine();
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
            var commandParams = GetCommonParams(tokens[1..]);
            var shapeParams = commandParams.Append(Convert.ToDouble(tokens[3])).ToArray();
            switch (tokens[0].Trim().ToLower())
            {
                case "circle":
                case "square":
                    return shapeParams;
                case "rectangle":
                case "ellipse":
                    return shapeParams.Append(Convert.ToDouble(tokens[4])).ToArray();
                case "textbox":
                    var adParams = new object[3];
                    adParams[0] = Convert.ToDouble(tokens[4]);
                    adParams[1] = tokens[5];
                    adParams[2] = tokens[6];
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

    }
}
