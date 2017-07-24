// MyWin32App.cpp : Defines the entry point for the console application.
//

#include "stdafx.h"








int main()
{
    USES_CONVERSION;

    // Initialize COM.
    HRESULT hr = CoInitialize(NULL);

    // Create the interface pointer.
    ICalculatorPtr pICalc(__uuidof(ManagedClass));

    long lResult = 0;

    // Call the Add method.
    pICalc->Add(5, 10, &lResult);

    wprintf(L"The result is %d", lResult);

    BSTR bstrHello = CString("hello").AllocSysString();
    BSTR bstrWorld = CString("world").AllocSysString();
    BSTR bstrResult;
    pICalc->AddStr(bstrHello, bstrWorld, &bstrResult);
    wprintf(L"The result is %s", bstrResult);
    ::SysReleaseString(bstrHello);
    ::SysReleaseString(bstrWorld);
    ::SysReleaseString(bstrResult);

    // Uninitialize COM.
    CoUninitialize();
    return 0;
}

