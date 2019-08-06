using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Activities;

namespace WFWindowsFormsDemo
{

    // Create a bookmark for stop the process and waiting for user-input, then continue the process agian.
    public sealed class Wait4InputDataCodeActivity : NativeActivity
    {
        // Define an activity input argument of type string
        public OutArgument<int> UserInputMoney { get; set; }

        public InArgument<string> BookmarkName { get;set; }

        protected override bool CanInduceIdle
        {
            get
            {
                return true;
            }
        }

        // If your activity returns a value, derive from CodeActivity<TResult>
        // and return the value from the Execute method.
        protected override void Execute(NativeActivityContext context)
        {
            // Create a bookmark for stop the activity
            string str = context.GetValue(BookmarkName);
            context.CreateBookmark(str, new BookmarkCallback(InputDataCallback));
        }

        // Excute when the proccess is continue agian
        private void InputDataCallback(NativeActivityContext context, Bookmark bookmark, object value)
        {
            // Get input/amount from user
            context.SetValue(UserInputMoney, (int)value);
        }
    }
}
