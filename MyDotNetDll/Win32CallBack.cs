using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace MyDotNetDll
{
    public class Win32CallBack : ICallBackFunc
    {
        static TestCallbackDelegate del;

        public void TestCallback(IntPtr callback)
        {
            del = Marshal.GetDelegateForFunctionPointer<TestCallbackDelegate>(callback);
            del(10);
        }
    }
}
