using MAP.OA.Common;
using System;
using System.Activities;
using System.Activities.DurableInstancing;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MAP.OA.Workflow
{
    public class WorkflowApplicationHelper
    {
        private static readonly string StrSql;

        static WorkflowApplicationHelper()
        {
            StrSql = ConfigurationManager.ConnectionStrings["wfsql"].ConnectionString;
        }

        public static WorkflowApplication CreateWorkflowApp(Activity activity, Dictionary<string, object> dictParam)
        {
            AutoResetEvent autoResetEvent = new AutoResetEvent(false);
            WorkflowApplication wfApp;
            if (dictParam == null)
            {
                wfApp = new WorkflowApplication(activity);
            }
            else
            {
                wfApp = new WorkflowApplication(activity, dictParam);
            }
            wfApp.Idle += a =>
            {
                Console.WriteLine("Workflow stopped");
                LogHelper.WriteLog("Workflow stopped");
            };

            wfApp.PersistableIdle = delegate(WorkflowApplicationIdleEventArgs e2)
            {
                Console.WriteLine("Unload the workflow and persistent the data into the database when the bookmark is create");
                LogHelper.WriteLog("Unload the workflow and persistent the data");
                return PersistableIdleAction.Unload;
            };

            wfApp.Unloaded += a =>
            {
                autoResetEvent.Set();
                Console.WriteLine("Workflow is unloaded");
                LogHelper.WriteLog("Workflow is unloaded");
            };

            wfApp.OnUnhandledException += a =>
            {
                autoResetEvent.Set();
                Console.WriteLine("exception occurred");
                LogHelper.WriteLog("exception occurred");
                return UnhandledExceptionAction.Terminate;
            };
            wfApp.Aborted += a =>
            {
                autoResetEvent.Set();
                Console.WriteLine("Aborted");
                LogHelper.WriteLog("Aborted");
            };

            // Create a instance to store the workflow into sql
            SqlWorkflowInstanceStore store = new SqlWorkflowInstanceStore(StrSql);
            wfApp.InstanceStore = store;
            wfApp.Run();
            return wfApp;
        }

        public static WorkflowApplication ResumeBookMark(Activity activity, Guid instanceId, string bookmarkName, object value)
        {
            AutoResetEvent autoResetEvent = new AutoResetEvent(false);
            WorkflowApplication wfApp = new WorkflowApplication(activity);
            
            wfApp.Idle += a =>
            {
                Console.WriteLine("Workflow stopped");
                LogHelper.WriteLog("Workflow stopped");
            };

            wfApp.PersistableIdle = delegate (WorkflowApplicationIdleEventArgs e2)
            {
                Console.WriteLine("Unload the workflow and persistent the data into the database when the bookmark is create");
                LogHelper.WriteLog("Unload the workflow and persistent the data");
                return PersistableIdleAction.Unload;
            };

            wfApp.Unloaded += a =>
            {
                autoResetEvent.Set();
                Console.WriteLine("Workflow is unloaded");
                LogHelper.WriteLog("Workflow is unloaded");
            };

            wfApp.OnUnhandledException += a =>
            {
                autoResetEvent.Set();
                Console.WriteLine("exception occurred");
                LogHelper.WriteLog("exception occurred");
                return UnhandledExceptionAction.Terminate;
            };
            wfApp.Aborted += a =>
            {
                autoResetEvent.Set();
                Console.WriteLine("Aborted");
                LogHelper.WriteLog("Aborted");
            };

            // Create a instance to store the workflow into sql
            SqlWorkflowInstanceStore store = new SqlWorkflowInstanceStore(StrSql);
            wfApp.InstanceStore = store;

            // Load the workflow from database by the instance id
            wfApp.Load(instanceId);

            // Continue the workflow
            wfApp.ResumeBookmark(bookmarkName, value);

            return wfApp;
        }
    }
}
