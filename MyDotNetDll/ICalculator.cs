using System;

namespace MyDotNetDll
{
    public delegate void MyCallbackDele(string msg);

    public interface ICalculator
    {
        int Add(int Number1, int Number2);

        string AddStr(string str1, string str2);

        event MyCallbackDele MyEvent;
    };
}
