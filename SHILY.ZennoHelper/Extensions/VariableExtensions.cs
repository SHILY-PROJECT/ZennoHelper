using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZennoLab.InterfacesLibrary.ProjectModel;

namespace SHILY.ZennoHelper.Extensions
{
    public static class VariableExtensions
    {
        private static readonly Random _rnd = new Random();

        /// <summary>
        /// Извлечение числа из переменной.
        /// Поддерживает извлечение из диапазона чисел. Разделители: '-', ':', ';', ' '.
        /// </summary>
        public static int ExtractNumber(this ILocalVariable variable, bool throwArgumentException = false)
            => variable.Value.ExtractNumber(throwArgumentException);

        /// <summary>
        /// Извлечение числа из переменной.
        /// Поддерживает извлечение из диапазона чисел. Разделители: '-', ':', ';', ' '.
        /// </summary>
        public static int ExtractNumber(this string variable, bool throwArgumentException = false)
        {
            var result = 0;
            var separators = new[] { '-', ':', ';', ' ' };

            if (variable is null)
                if (throwArgumentException)
                    throw new ArgumentNullException($"'{nameof(variable)}' - Argument connot be null.");
                else return result;

            if (separators.Any(x => variable.Contains(x)) &&
                int.TryParse(variable.Split(separators)[0], out var numberFrom) &&
                int.TryParse(variable.Split(separators)[1], out var numberTo))
            {
                result = numberFrom > numberTo ? _rnd.Next(numberTo, numberFrom + 1) : _rnd.Next(numberFrom, numberTo + 1);
            }
            else if (int.TryParse(variable, out var number))
            {
                result = number;
            }
            else
            {
                if (throwArgumentException)
                    throw new ArgumentException($"'{nameof(variable)}' - Argument is invalid.");
            }

            return result;
        }

        /// <summary>
        /// Проверить совпадение.
        /// </summary>
        public static bool AnyMatch(this string text, params string[] arrayPattern)
            => arrayPattern.Any(x => text.ToLower().Contains(x.ToLower()));
        
    }
}
