using System;
using System.Activities;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinFormDemo
{
    public partial class Form1 : Form
    {
        private WorkflowApplication workflowApplication;

        public Form1()
        {
            InitializeComponent();
        }

        private void btnBeginWF_Click(object sender, EventArgs e)
        {
            //WorkflowInvoker.Invoke(new DemoActivity(), new Dictionary<string, object>() {
            //    {"BookmarkName", this.textBookmarkName.Text }
            //});

            workflowApplication = new WorkflowApplication(new DemoActivity(), new Dictionary<string, object>() {
                {"BookmarkName", this.textBookmarkName.Text }
            });


            workflowApplication.Idle += AfterWorkflowIdle;

            workflowApplication.Run();
        }

        private void AfterWorkflowIdle(WorkflowApplicationIdleEventArgs obj)
        {
            Console.WriteLine("Workflow idle...");
        }

        private void btnBookmarkContinue_Click(object sender, EventArgs e)
        {
            workflowApplication.ResumeBookmark(this.textBookmarkName.Text, int.Parse(this.textBoxContinue.Text));
        }
    }
}
