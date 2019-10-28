using Microsoft.Xrm.Sdk;
using System;

namespace XrmHero.Extensions
{
    public class PluginExecutionBundle : ExecutionBundle
    {
        internal protected PluginExecutionBundle(IServiceProvider serviceProvider)
        {
            ServiceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
            TracingService = (ITracingService)serviceProvider.GetService(typeof(ITracingService));
            PluginExecutionContext = (IPluginExecutionContext)serviceProvider.GetService(typeof(IPluginExecutionContext));
            OrganizationServiceFactory = (IOrganizationServiceFactory)serviceProvider.GetService(typeof(IOrganizationServiceFactory));
            OrganizationService = OrganizationServiceFactory.CreateOrganizationService(PluginExecutionContext.UserId);
        }

        internal protected readonly IPluginExecutionContext PluginExecutionContext;
        internal protected readonly IServiceProvider ServiceProvider;
    }
}