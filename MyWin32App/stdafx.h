// stdafx.h : include file for standard system include files,
// or project specific include files that are used frequently, but
// are changed infrequently
//

#pragma once

#include "targetver.h"

#include <stdio.h>
#include <tchar.h>


#define _ATL_CSTRING_EXPLICIT_CONSTRUCTORS      // some CString constructors will be explicit

#include <atlbase.h>
#include <atlstr.h>

// TODO: reference additional headers your program requires here




//https://support.microsoft.com/en-us/help/828736/how-to-call-a-managed-dll-from-native-visual-c-code-in-visual-studio-n
//Import the type library.
//C:\windows\Microsoft.NET\Framework\v4.0.30319\RegAsm.exe MyDotNetDll.dll /tlb:MyDotNetDll.tlb /codebase
#import "..\\bin\\Debug\\MyDotNetDll.tlb" raw_interfaces_only



using namespace MyDotNetDll;