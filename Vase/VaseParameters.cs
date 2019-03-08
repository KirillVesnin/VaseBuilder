using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Net.NetworkInformation;


namespace Vase
{
    public class VaseParameters
    {/// 
        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="lengthFullVase">Высота вазы</param>
        /// <param name="vaseBaseRad">Радиус основания</param>
        /// <param name="vaseBodyRad">Радиус тулова</param>
        /// <param name="vaseNeckRad">Радиус горлышка</param>
        /// <param name="vaseWallThickness">Толщина стенок</param>
        public VaseParameters(double lengthFullVase, double vaseBaseRad,
            double vaseBodyRad, double vaseNeckRad, double vaseWallThickness)
        {
            var errors = Validate(lengthFullVase, vaseBaseRad, vaseBodyRad,
                vaseNeckRad, vaseWallThickness);
            if (errors.Any())
                throw new ArgumentException(GetUnitedErrorMessage(errors));

            LengthFullVase = lengthFullVase;
            VaseBaseRad = vaseBaseRad;
            VaseBodyRad = vaseBodyRad;
            VaseNeckRad = vaseNeckRad;
            VaseWallThickness = vaseWallThickness;
        }

        public double LengthFullVase { get; }

        public double VaseBaseRad { get; }

        public double VaseBodyRad { get; }
        
        public double VaseNeckRad { get; }
        
        public double VaseWallThickness { get; }

        private static List<string> Validate(double lengthFullVase, double vaseBaseRad,
            double vaseBodyRad, double vaseNeckRad, double vaseWallThickness)
        {
            var errors = new List<string>();

          
            if (double.IsNaN(lengthFullVase))
                errors.Add("длина вазы имеет значение NaN");

            if (double.IsNaN(vaseBaseRad))
                errors.Add("Радиус основания не может быть NaN");

            if (double.IsNaN(vaseBodyRad))
                errors.Add("радиус тулова вазы не может быть NaN");

            if (double.IsNaN(vaseNeckRad))
                errors.Add("радиус горлышка не может быть NaN");

            if (double.IsNaN(vaseWallThickness))
                errors.Add("толщина стенки не может быть NaN");

            const double minLengthFullVase = 100;
            const double maxLengthFullVase = 400;

            if ( lengthFullVase < minLengthFullVase)
                errors.Add($"Минимальная длина бутылки = {minLengthFullVase}мм");
            if (lengthFullVase > maxLengthFullVase)
                errors.Add($"Максимальная длина бутылки = {maxLengthFullVase}мм");

            const double minVaseBaseRad = 50;
            const double maxVaseBaseRad = 200;

            if (vaseBaseRad < minVaseBaseRad)
                errors.Add($"Минимальный радиус основания = {minVaseBaseRad}");
            if (vaseBaseRad > maxVaseBaseRad)
                errors.Add($"Максимальный радиус основания = {maxVaseBaseRad}");

            const double minVaseBodyRad = 75;
            const double maxVaseBodyRad = 225;

            if (vaseBodyRad < minVaseBodyRad)
                errors.Add($"Минимальный радиус тулова = {minVaseBodyRad}");
            if (vaseBodyRad > maxVaseBodyRad)
                errors.Add($"Максимальный радиус тулова = {maxVaseBodyRad}");

            const double minVaseNeckRad = 50;
            const double maxVaseNeckRad = 200;

            if (vaseNeckRad < minVaseNeckRad)
                errors.Add($"Минимальный радиус горла = {minVaseNeckRad}");
            if (vaseNeckRad > maxVaseNeckRad)
                errors.Add($"Максимальный радиус горла = {maxVaseNeckRad}");

            const double minVaseWallThickness = 5;
            const double maxVaseWallThickness = 50;

            if (vaseWallThickness < minVaseWallThickness)
                errors.Add($"Минимальная толщина стенок = {minVaseWallThickness}");
            if (vaseWallThickness > maxVaseWallThickness)
                errors.Add($"Максимальная толщина стенок = {maxVaseWallThickness}");

            return errors;

        }

        private static string GetUnitedErrorMessage(IEnumerable<string> errorMessages)
        {
            var result = "Параметры некорректны: \n \n";

            foreach (var errorMessage in errorMessages)
                result += errorMessage + ";\n\n";

            result = result.Substring(0, result.Length - 3);

            result += '.';

            return result;
        }
    }
}
