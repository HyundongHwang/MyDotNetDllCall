# MyDotNetDllCall

<!-- TOC -->

- [MyDotNetDllCall](#mydotnetdllcall)
    - [소개](#%EC%86%8C%EA%B0%9C)
    - [구조](#%EA%B5%AC%EC%A1%B0)
    - [개발순서](#%EA%B0%9C%EB%B0%9C%EC%88%9C%EC%84%9C)
    - [실행](#%EC%8B%A4%ED%96%89)
    - [참고자료](#%EC%B0%B8%EA%B3%A0%EC%9E%90%EB%A3%8C)

<!-- /TOC -->

## 소개
- .net dll 과 win32 exe 의 통신
- 여러가지 방식이 있을수 있으나 여기서는 .net dll이 com export하고 win32 exe가 이를 사용하는 방식으로 한다.

## 구조
- MyDotNetDll.dll
    - .net 라이브러리
    - com export
    - Add, AddStr 등 간단한 샘플기능 제공
- MyWin32App.exe
    - win32 exe 응용프로그램
    - com 클라이언트
    - MyDotNetDll.dll 를 com을 통해 사용함.

## 개발순서
- .net dll(=MyDotNetDll) 개발
    - com export 할 부분들은 interface를 이용해서 개발한다.

- .net dll com export
    - Strong Name 을 위한 snk 파일 한개를 제작

    ```powershell
    PS> "C:\Program Files (x86)\Microsoft SDKs\Windows\v10.0A\bin\NETFX 4.6.1 Tools\sn.exe" -k "MyKeyFile.SNK"
    ```

    - `MyKeyFile.SNK` 파일을 .net dll 프로젝트에 포함시킴
    - `AssemblyInfo.cs` 파일을 변경

    ```csharp
    [assembly: ComVisible(true)]
    [assembly: AssemblyDelaySign(false)]
    [assembly: AssemblyKeyFile("MyKeyFile.SNK")]
    ```

    - .net dll 빌드
    - dll 파일에서 tlb 파일 생성

    ```powershell
    PS> C:\windows\Microsoft.NET\Framework\v4.0.30319\RegAsm.exe MyDotNetDll.dll /tlb:MyDotNetDll.tlb /codebase
    ```

- win32 exe com import
    - `stdafx.h` 에서 전역적으로 tlb 를 임포트

    ```cpp
    #import "..\\bin\\Debug\\MyDotNetDll.tlb" raw_interfaces_only
    using namespace MyDotNetDll;
    ```

- win32 exe 에서 사용

```cpp
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

	
    wprintf(L"The result is %d\n", lResult);

    BSTR bstrHello = CString("hello").AllocSysString();
    BSTR bstrWorld = CString("world").AllocSysString();
    BSTR bstrResult;
    pICalc->AddStr(bstrHello, bstrWorld, &bstrResult);

    wprintf(L"The result is %s", bstrResult);
	::SysFreeString(bstrHello);
	::SysFreeString(bstrWorld);
	::SysFreeString(bstrResult);

    wprintf(L"The result is %s\n", bstrResult);
    
	ICallBackFuncPtr  pICb(__uuidof(Win32CallBack));
	pICb->TestCallback((long)SimapleCallback);
    pICb->TestCallback2((long)SimapleCallback2);
    // Uninitialize COM.
    CoUninitialize();
    return 0;
}
```

## 실행
- 먼저 MyDotNetDll.dll 을 com 등록한후 MyWin32App.exe 을 수행하도록 한다.

```powershell
PS C:\project\170723_MyDotNetDllCall\bin\Debug> ls


    디렉터리: C:\project\170723_MyDotNetDllCall\bin\Debug


Mode                LastWriteTime         Length Name
----                -------------         ------ ----
-a----     2017-07-24   오후 3:12           6656 MyDotNetDll.dll
-a----     2017-07-24   오후 3:12          13824 MyDotNetDll.pdb
-a----     2017-07-24   오후 3:12           3352 MyDotNetDll.tlb
-a----     2017-07-24   오후 3:17         107008 MyWin32App.exe
-a----     2017-07-24   오후 3:17         608820 MyWin32App.ilk
-a----     2017-07-24   오후 3:17        1740800 MyWin32App.pdb
-a----     2017-07-24  오전 10:47             74 register-dll.ps1


PS C:\project\170723_MyDotNetDllCall\bin\Debug> C:\windows\Microsoft.NET\Framework\v4.0.30319\RegAsm.exe .\MyDotNetDll.dll
Microsoft .NET Framework 버전 4.7.2046.0용
Microsoft .NET Framework Assembly Registration Utility 버전 4.7.2046.0
Copyright (C) Microsoft Corporation.  All rights reserved.

형식이 등록되었습니다.
PS C:\project\170723_MyDotNetDllCall\bin\Debug> .\MyWin32App.exe
The result is 15The result is helloworld
PS C:\project\170723_MyDotNetDllCall\bin\Debug>
```

## 참고자료

- https://support.microsoft.com/en-us/help/828736/how-to-call-a-managed-dll-from-native-visual-c-code-in-visual-studio-n
