using Microsoft.Xrm.Sdk;
using System;
using System.Activities;

namespace XrmHero.Extensions
{
    public abstract class WorkflowActivity : CodeActivity
    {
        protected abstract void Execute(WorkflowActivityExecutionBundle bundle);

        protected override void Execute(CodeActivityContext codeActivityContext)
        {
            WorkflowActivityExecutionBundle bundle = null;
            try
            {
                bundle = new WorkflowActivityExecutionBundle(codeActivityContext);
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