using ShilyHelper.Library.Enums;
using ShilyHelper.Library.Macros;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Sockets;
using System.Resources;
using System.Text;
using ZennoLab.CommandCenter;
using ZennoLab.Emulation;
using ZennoLab.InterfacesLibrary.ProjectModel;
using ZennoLab.InterfacesLibrary.ProjectModel.Enums;

namespace ShilyHelper.Library.Extensions
{
    public static class ArrayExtensions
    {
        /// <summary>
        /// Проверка массива на пустые элементы, элементы состоящие из пробельных символов или null.
        /// </summary>
        public static bool AnyMatchIsNullOrWhiteSpace(this string[] array)
        {
            return array.Any(x => string.IsNullOrWhiteSpace(x)) ? true : false;
        }
        public static bool AnyMatch(this string text, string[] arrayPattern, SearchTypeForAnyMatch searchTypeForAnyMatch = SearchTypeForAnyMatch.Contains)
        {
            switch (searchTypeForAnyMatch)
            {
                default:
                case SearchTypeForAnyMatch.Contains: return arrayPattern.Any(x => text.Contains(x)) ? true : false;
                case SearchTypeForAnyMatch.EqualsOrdinal: return arrayPattern.Any(x => text.Equals(x, StringComparison.Ordinal)) ? true : false;
            }
        }

    }
}