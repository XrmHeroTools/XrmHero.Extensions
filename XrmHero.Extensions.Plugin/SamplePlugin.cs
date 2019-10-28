using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using System.Reflection;

namespace XrmHero.Extensions.Samples
{
    /// <summary>
    /// A simple plugin that will generate an error message as a means of testing that the plugin was executed fully.
    /// </summary>
    public class SamplePlugin : Plugin
    {
        protected override void Execute(PluginExecutionBundle bundle)
        {
            // Get mode of execution.
            var mode =
                bundle.PluginExecutionContext.Mode == 0 ?
                "synchronous " :
                bundle.PluginExecutionContext.Mode == 1 ?
                "asynchronous " :
                null;

            // Get name of calling user.
            var user = bundle.OrganizationService.Retrieve("systemuser", bundle.PluginExecutionContext.InitiatingUserId, new ColumnSet("fullname"));
            var userDisplayName = user.GetAttributeValue<string>("fullname");

            // Get extension identifiers.
            var className = typeof(SamplePlugin).FullName;
            var assemblyName = Assembly.GetExecutingAssembly().GetName().Name;

            // Trace details of operation.
            var operationName = bundle.PluginExecutionContext.MessageName;
            var recordTypeName = bundle.PluginExecutionContext.PrimaryEntityName;
            var recordId = bundle.PluginExecutionContext.PrimaryEntityId;
            if (recordTypeName != null)
            {
                bundle.TracingService.Trace($@"The plugin ""{className}"" is executing during a {operationName} operation for {recordTypeName} {recordId}.");
            }
            else
            {
                bundle.TracingService.Trace($@"The plugin ""{className}"" is executing during a {operationName} operation.");
            }
            bundle.TracingService.Trace($"Operation initiated by {bundle.PluginExecutionContext.InitiatingUserId}");
            bundle.TracingService.Trace($"Plugin may perform subsequent operations as {bundle.PluginExecutionContext.UserId}");

            // Construct message to be displayed in app.
            // If plugin is running synchronously, message will be displayed in popup modal and plugin trace logs (if enabled), and triggering action (e.g saving record) will be cancelled.
            // If plugin is running asynchronsouly, message can be found in plugin trace logs (if enabled) and system jobs.
            var msg = $"The {mode}plugin executed successfully from the {operationName} operation started by {userDisplayName}. To stop receiving these messages, unregister the {className} plugin in the {assemblyName} assembly.";
            throw new InvalidPluginExecutionException(msg);
        }
    }
}