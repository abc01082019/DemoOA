using System;
using System.Activities;
using System.Activities.DurableInstancing;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WFWindowsFormsDemo
{
    public partial class MainForm : Form
    {
        private WorkflowApplication WFApplication { get; set; }
        public MainForm()
        {
            InitializeComponent();
        }

        private void btnBegin_Click(object sender, EventArgs e)
        {
            // Sql workflow persistence
            SqlWorkflowInstanceStore store = new SqlWorkflowInstanceStore("Server=.;Initial Catalog=MAPOADb;uid=sa;pwd=a5576279;");

            // Create a workflow application
            // Pass the bookmark name as parameter to the DemoActivity activity
            WorkflowApplication workflowApplication = new WorkflowApplication(
                new WFStateMachineActivity(),
                new Dictionary<string, object>() {
                    { "InBookmarkName", this.textBoxBookmark.Text }
                });

            workflowApplication.InstanceStore = store;
            // This method must be used to serialize data
            workflowApplication.PersistableIdle = a => { return PersistableIdleAction.Unload; };
            workflowApplication.Idle += OnWorkflowAppIdle;
            workflowApplication.OnUnhandledException += OnWorkflowException;
            workflowApplication.Unloaded = a => { Console.WriteLine("workflow unload..."); };
            workflowApplication.Aborted = a => { Console.WriteLine("workflow abort"); };

            workflowApplication.Run();

            this.textBoxGuid.Text = workflowApplication.Id.ToString();

            WFApplication = workflowApplication;
        }

        private UnhandledExceptionAction OnWorkflowException(WorkflowApplicationUnhandledExceptionEventArgs arg)
        {
            Console.WriteLine("on unhandled exception: " + arg.UnhandledException.ToString());
            return UnhandledExceptionAction.Terminate;
        }

        private void OnWorkflowAppIdle(WorkflowApplicationIdleEventArgs obj)
        {
            Console.WriteLine("workflow is idle...");
        }

        private void btnContinue_Click(object sender, EventArgs e)
        {
            #region Old version
            // Continue the workflow and pass a bookmark name and amount of the funding application
            // Pass those data direct to Wait4InputDataCodeActivity.InputDataCallback method
            //WFApplication.ResumeBookmark(this.textBoxBookmark.Text, int.Parse(this.textBoxContinue.Text));
            #endregion

            // Get the data info from database and continues the process
            // Sql workflow persistence
            SqlWorkflowInstanceStore store = new SqlWorkflowInstanceStore("Server=.;Initial Catalog=MAPOADb;uid=sa;pwd=a5576279;");

            // Create a workflow application
            WorkflowApplication workflowApplication = new WorkflowApplication(new WFStateMachineActivity());

            workflowApplication.InstanceStore = store;
            // This method must be used to serialize data
            workflowApplication.PersistableIdle = a => { return PersistableIdleAction.Unload; };
            workflowApplication.Idle += OnWorkflowAppIdle;
            workflowApplication.OnUnhandledException += OnWorkflowException;
            workflowApplication.Unloaded = a => { Console.WriteLine("workflow unload..."); };
            workflowApplication.Aborted = a => { Console.WriteLine("workflow abort"); };

            // Load the instance status from database
            workflowApplication.Load(Guid.Parse(this.textBoxGuid.Text));

            // Continue the workflow and pass a bookmark name and amount of the funding application
            // Pass those data direct to Wait4InputDataCodeActivity.InputDataCallback method
            workflowApplication.ResumeBookmark(this.textBoxBookmark.Text, int.Parse(this.textBoxContinue.Text));
        }

        private void btnStateMachine_Click(object sender, EventArgs e)
        {
            WorkflowApplication workflowApplication = new WorkflowApplication(
                new StateMachineActivity());

            workflowApplication.Run();
        }
    }
}
