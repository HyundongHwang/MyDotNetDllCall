using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyDotNetDll
{
    delegate void TestCallbackDelegate(int value);
    delegate void TestCallbackDelegate2(IntPtr value);

    public interface ICallBackFunc
    {
        void TestCallback(IntPtr callback);
        void TestCallback2(IntPtr callback);
    }
}
