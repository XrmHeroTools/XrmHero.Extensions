using Microsoft.Xrm.Sdk;

namespace XrmHero.Extensions
{
    public abstract class ExecutionBundle
    {
        internal protected virtual IOrganizationService OrganizationService { get; protected set; }
        internal protected virtual IOrganizationServiceFactory OrganizationServiceFactory { get; protected set; }
        internal protected virtual ITracingService TracingService { get; protected set; }
    }
}