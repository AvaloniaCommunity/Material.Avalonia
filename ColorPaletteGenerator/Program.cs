using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ColorPaletteGenerator
{
    internal class Program
    {
        private static void Main()
        {
            GenerateXamlColors("ColorsPalette.xaml", File.ReadAllLines("Colors.txt"));
        }

        /// <summary>
        ///     Генерируем файл стилей
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="text"></param>
        private static void GenerateXamlColors(string fileName, string[] text)
        {
            var data = ParseData(text);

            using var s = File.CreateText(fileName);
            s.WriteLine(
                "<Styles xmlns=\"https://github.com/avaloniaui\"\r\n        xmlns:x=\"http://schemas.microsoft.com/winfx/2006/xaml\"\r\n        xmlns:sys=\"clr-namespace:System;assembly=netstandard\">\r\n  <Styles.Resources>\r\n");
            s.WriteLine(GenerateColors(data));

            s.WriteLine("\n");

            s.WriteLine(GenerateBrushes(data));

            s.WriteLine("\r\n  </Styles.Resources>\r\n</Styles>");
        }

        /// <summary>
        ///     парсим файл
        /// </summary>
        /// <param name="lines"></param>
        /// <returns></returns>
        private static List<Dictionary<string, string>> ParseData(string[] lines)
        {
            var ret = new List<Dictionary<string, string>>();
            var d = new Dictionary<string, string>();

            foreach (var line in lines)
            {
                if (line == "")
                {
                    ret.Add(d);
                    d = new Dictionary<string, string>();
                    continue;
                }

                var i = line.IndexOf(' ');
                var key = line.Substring(0, i).Trim();
                var value = line.Substring(i + 1).Trim();
                d.Add(key, value);
            }

            ret.Add(d);

            return ret;
        }

        /// <summary>
        ///     Генерируем цвета
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        private static string GenerateColors(List<Dictionary<string, string>> data)
        {
            return data.Select(d => d.Select(b =>
                        $"    <Color x:Key=\"{b.Key}Color\">{b.Value}</Color>")
                    .Aggregate((accumulator, piece) => $"{accumulator}\n{piece}"))
                .Aggregate((accumulator, piece) => accumulator + "\n\n" + piece);
        }

        /// <summary>
        ///     Генерируем кисти
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        private static string GenerateBrushes(List<Dictionary<string, string>> data)
        {
            return data.Select(d => d.Select(b =>
                        $"    <SolidColorBrush x:Key=\"{b.Key}Brush\" Color=\"{{ DynamicResource {b.Key}Color}}\" />")
                    .Aggregate((accumulator, piece) => $"{accumulator}\n{piece}"))
                .Aggregate((accumulator, piece) => accumulator + "\n\n" + piece);
        }
    }
}