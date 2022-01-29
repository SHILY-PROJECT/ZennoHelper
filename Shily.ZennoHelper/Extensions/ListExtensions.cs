using System;
using System.Collections.Generic;
using System.Threading;

namespace Shily.ZennoHelper.Extensions
{
    public delegate T LineOption<T>(List<T> list);

    public static class ListExtensions
    {
        private static readonly object locker = new object();

        public static T GetLine<T>(this List<T> list, LineOption<T> lineOption, bool useThreadLocker = false, bool throwExceptionIfListIsEmptyOrNull = false)
        {
            if (list is null || list.Count == 0)
            {
                if (throwExceptionIfListIsEmptyOrNull)
                    throw new Exception("Список пуст");
                return default;
            }

            if (useThreadLocker) Monitor.Enter(locker);

            var res = lineOption.Invoke(list);

            if (useThreadLocker) Monitor.Exit(locker);

            return res;
        }
    }

    public static class LineOption
    {
        private static readonly Random _rnd = new Random();

        /// <summary>
        /// Получение первой строки (без удаления).
        /// </summary>
        public static T GetFirstLine<T>(this List<T> list) => list[0];

        /// <summary>
        /// Получение первой строки с удалением её из списка.
        /// </summary>
        public static T GetFirstLineWithRemoved<T>(this List<T> list)
        {
            var line = list[0];
            list.RemoveAt(0);
            return line;
        }

        /// <summary>
        /// Получение рандомной строки из списка (без удаления).
        /// </summary>
        public static T GetRandomLine<T>(this List<T> list) => list[_rnd.Next(list.Count)];

        /// <summary>
        /// Получение рандомной строки с удалением её из списка.
        /// </summary>
        public static T GetRandomLineWithRemoved<T>(this List<T> list)
        {
            var index = _rnd.Next(list.Count);
            var line = list[index];
            list.RemoveAt(index);
            return line;
        }

        /// <summary>
        /// Получить первую строку с переносом в конец списка.
        /// </summary>
        public static T GetFirstLineWithMoveToEnd<T>(this List<T> list)
        {
            var line = list[0];
            list.RemoveAt(0);
            list.Add(line);
            return line;
        }
    }

}
