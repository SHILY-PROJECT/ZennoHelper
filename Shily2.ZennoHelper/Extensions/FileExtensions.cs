using System.IO;
using System.Threading;

namespace Shily.ZennoHelper.Extensions
{
    public static class FileExtensions
    {
        private static readonly object locker = new object();

        public static void DeleteFile(this FileInfo file, bool useThreadLocker = true)
        {
            if (file.Exists is false) return;

            if (useThreadLocker) Monitor.Enter(locker);
            {
                try
                {
                    File.Delete(file.FullName);
                }
                catch { }
            }
            if (useThreadLocker) Monitor.Exit(locker);
        }

    }
}
