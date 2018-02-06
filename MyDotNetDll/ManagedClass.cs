using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Threading;

namespace MyDotNetDll
{
    public class ManagedClass : ICalculator
    {
        private SynchronizationContext _uiContext = new SynchronizationContext();

        public event MyCallbackDele MyEvent;

        public int Add(int Number1, int Number2)
        {
            ThreadPool.QueueUserWorkItem((arg) =>
            {
                _uiContext.Post((arg2) =>
                {
                    var wnd = new MyWnd();
                    wnd.Show();
                }, null);
            });

            this.MyEvent?.Invoke($"call Add Number1 : {Number1} Number2 : {Number2}");
            return Number1 + Number2;
        }

        public string AddStr(string str1, string str2)
        {
            this.MyEvent?.Invoke($"call AddStr str1 : {str1} str2 : {str2}");
            var newStr = str1 + str2;
            return newStr;
        }

    }
}
