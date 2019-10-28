using Microsoft.Xrm.Sdk;
using System;

namespace XrmHero.Extensions
{
    public abstract class Plugin : IPlugin
    {
        protected abstract void Execute(PluginExecutionBundle bundle);
        public void Execute(IServiceProvider serviceProvider)
        {
            PluginExecutionBundle bundle = null;
            try
            {
                bundle = new PluginExecutionBundle(serviceProvider);
                Execute(bundle);
            }
            catch (InvalidPluginExecutionException e)
            {
                // Force preservation of stack trace before rethrowing original exception.
                bundle?.TracingService?.Trace(e);
                throw;
            }
            catch (Exception e)
            {
                // Force preservation of stack trace before wrapping original exception.
                bundle?.TracingService?.Trace(e);
                throw new InvalidPluginExecutionException(e.Message, e);
            }
        }
    }
}