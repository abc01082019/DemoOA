using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Activities;
using MAP.OA.Common;
using Spring.Context;
using Spring.Context.Support;
using MAP.OA.IBLL;
using MAP.OA.BLL;
using MAP.OA.Model.Enum;

namespace MAP.OA.Workflow
{

    public sealed class SetProcedureCodeActivity : CodeActivity
    {
        // Define an activity input argument of type string
        public InArgument<string> ProcedureName { get; set; }
        public InArgument<bool> IsEnd { get; set; }

        // If your activity returns a value, derive from CodeActivity<TResult>
        // and return the value from the Execute method.
        protected override void Execute(CodeActivityContext context)
        {
            // Obtain the runtime value of the ProcedureName input argument
            string text = context.GetValue(this.ProcedureName);
            bool isEnd = context.GetValue(this.IsEnd);

            Guid instanceId = context.WorkflowInstanceId;
            LogHelper.WriteLog("Workflow instance id: " + context.WorkflowInstanceId);

            IApplicationContext ctx = ContextRegistry.GetContext();
            var WF_InstanceService = ctx.GetObject("WF_InstanceService") as IWF_InstanceService;
            var WF_ProcedureService = ctx.GetObject("WF_ProcedureService") as IWF_ProcedureService;

            var instance = WF_InstanceService.GetEntities(u=>u.WFInstanceId == instanceId).FirstOrDefault();

            if (instance == null)
            {
                LogHelper.WriteLog("wfInstance is null");
            }

            var procedureStatus = (short)WF_InstanceEnum.Unprocess;
            var procedure = instance.WF_Procedure.Where(p => p.ProcessStatus == procedureStatus).FirstOrDefault();
            LogHelper.WriteLog("Set procedure activity: " + text);

            procedure.ProcedureName = text;
            procedure.IsEndProcedure = isEnd;
            if (isEnd)
            {
                procedure.ProcessResult = "Approval done";
                instance.Status = (short)WF_InstanceEnum.Processed;
                WF_InstanceService.Update(instance);
            }
            WF_ProcedureService.Update(procedure);



        }
    }
}
