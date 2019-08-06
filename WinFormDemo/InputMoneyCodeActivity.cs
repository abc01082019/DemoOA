using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Activities;

namespace WinFormDemo
{

    public sealed class InputMoneyCodeActivity : NativeActivity
    {
        public OutArgument<int> OutMoney { get; set;}

        public InArgument<string> InBookmarkName { get; set; }

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
            // string text = context.GetValue(this.Text);

            //int money = int.Parse(Console.ReadLine());

            // 1. stop the workflow
            // 2. continue the workflow when we get the data

            // create a bookmark
            Console.WriteLine("[Create bookmark]");
            string bookmarkName = context.GetValue(InBookmarkName);
            context.CreateBookmark(bookmarkName, new BookmarkCallback(CallbackFunc));

            //context.SetValue(OutMoney, money);


        }

        // excute when the bookmark continue
        private void CallbackFunc(NativeActivityContext context, Bookmark bookmark, object value)
        {
            context.SetValue(OutMoney, (int)value);
            Console.WriteLine("[Continue bookmark]");
        }
    }
}
