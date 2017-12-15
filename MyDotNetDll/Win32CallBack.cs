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
        public void TestCallback(IntPtr callback)
        {
            var del = Marshal.GetDelegateForFunctionPointer<TestCallbackDelegate>(callback);
            del(10);
        }

        public void TestCallback2(IntPtr callback)
        {
            var del = Marshal.GetDelegateForFunctionPointer<TestCallbackDelegate2>(callback);
            var uniStr = Marshal.StringToCoTaskMemUni("hello world 안녕하세요 123");
            del(uniStr);
        }
    }
}
