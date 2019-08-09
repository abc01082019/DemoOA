using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Activities;

namespace MAP.OA.Workflow
{

    public sealed class Wait4InputDataCodeActivity<T> : NativeActivity
    {
        // Define an activity input argument of type string
        public InArgument<string> InBookmarkName { get; set; }
        public OutArgument<T> OutData { get; set; }
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
            // Obtain the runtime value of the Text input argument
            string text = context.GetValue(this.InBookmarkName);

            context.CreateBookmark(text, new BookmarkCallback(MethodCallback));

        }

        // Obtain the bookmark and continue
        private void MethodCallback(NativeActivityContext context, Bookmark bookmark, object value)
        {
            context.SetValue(OutData, (T)value);
        }
    }
}
