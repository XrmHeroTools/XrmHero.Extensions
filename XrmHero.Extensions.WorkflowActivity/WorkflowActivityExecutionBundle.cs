using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Workflow;
using System;
using System.Activities;

namespace XrmHero.Extensions
{
    public class WorkflowActivityExecutionBundle : ExecutionBundle
    {
        internal protected WorkflowActivityExecutionBundle(CodeActivityContext codeActivityContext)
        {
            CodeActivityContext = codeActivityContext ?? throw new ArgumentNullException(nameof(codeActivityContext));
            TracingService = codeActivityContext.GetExtension<ITracingService>();
            WorkflowContext = codeActivityContext.GetExtension<IWorkflowContext>();
            OrganizationServiceFactory = codeActivityContext.GetExtension<IOrganizationServiceFactory>();
            OrganizationService = OrganizationServiceFactory.CreateOrganizationService(WorkflowContext.UserId);
        }

        internal protected readonly CodeActivityContext CodeActivityContext;
        internal protected readonly IWorkflowContext WorkflowContext;
    }
}