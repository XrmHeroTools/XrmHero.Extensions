using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using System.Reflection;

namespace XrmHero.Extensions.Samples
{
    /// <summary>
    /// A simple workflow activity that will generate an error message as a means of testing that the workflow activity was executed fully.
    /// </summary>
    public class SampleWorkflowActivity : WorkflowActivity
    {
        protected override void Execute(WorkflowActivityExecutionBundle bundle)
        {
            // Get mode of execution.
            var mode =
                bundle.WorkflowContext.Mode == 0 ?
                "synchronous " :
                bundle.WorkflowContext.Mode == 1 ?
                "asynchronous " :
                null;

            // Get name of calling user.
            var user = bundle.OrganizationService.Retrieve("systemuser", bundle.WorkflowContext.InitiatingUserId, new ColumnSet("fullname"));
            var userDisplayName = user.GetAttributeValue<string>("fullname");

            // Get extension identifiers.
            var className = typeof(SampleWorkflowActivity).FullName;
            var assemblyName = Assembly.GetExecutingAssembly().GetName().Name;

            // Trace details of operation.
            var operationName = bundle.WorkflowContext.MessageName;
            var recordTypeName = bundle.WorkflowContext.PrimaryEntityName;
            var recordId = bundle.WorkflowContext.PrimaryEntityId;
            if (recordTypeName != null)
            {
                bundle.TracingService.Trace($@"The workflow activity ""{className}"" is executing during a {operationName} operation for {recordTypeName} {recordId}.");
            }
            else
            {
                bundle.TracingService.Trace($@"The workflow activity ""{className}"" is executing during a {operationName} operation.");
            }
            bundle.TracingService.Trace($"Operation initiated by {bundle.WorkflowContext.InitiatingUserId}");
            bundle.TracingService.Trace($"Workflow activity may perform subsequent operations as {bundle.WorkflowContext.UserId}");

            // Construct message to be displayed in app.
            // If workflow activity is running synchronously, message will be displayed in popup modal and plugin trace logs (if enabled), and triggering action (e.g saving record) will be cancelled.
            // If workflow activity is running asynchronsouly, message can be found in plugin trace logs (if enabled), system jobs and process sessions (if enabled).
            var msg = $"The {mode}workflow activity executed successfully from the {operationName} operation started by {userDisplayName}. To stop receiving these messages, unregister the {className} workflow activity in the {assemblyName} assembly.";
            throw new InvalidPluginExecutionException(msg);
        }
    }
}