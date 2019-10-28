using Microsoft.Xrm.Sdk;
using System;

namespace XrmHero.Extensions
{
    internal static class ITracingServiceExtensions
    {
        internal static void Trace(this ITracingService tracingService, Exception e)
        {
            tracingService.Trace($"Error: {e.Message}");
            tracingService.Trace("{0}", e.StackTrace);
        }
    }
}