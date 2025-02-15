using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.Maui
{
    internal static class TaskExtension
    {
        public static async void FireAndForget(
    this Task task,
    Action<Exception>? errorCallback = null
    )
        {
            try
            {
                await task.ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                errorCallback?.Invoke(ex);
#if DEBUG
                throw;
#endif
            }
        }
#if !WEBVIEW2_MAUI
        public static void FireAndForget<T>(this Task task, T? viewHandler, [CallerMemberName] string? callerName = null)
            where T : IElementHandler
        {
            task.FireAndForget(ex => { /*Log(viewHandler?.CreateLogger<T>(), ex, callerName)*/});
        }

        static void Log(ILogger? logger, Exception ex, string? callerName) =>
            logger?.LogError(ex, "Unexpected exception in {Member}.", callerName);
#endif
    }
}
