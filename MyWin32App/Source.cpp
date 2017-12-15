#include "stdafx.h"


void SimapleCallback(int num)
{
	wprintf(L"Call from .net to win32 : %d\n", num);
}

void SimapleCallback2(wchar_t* wstr)
{
	wprintf(L"Call from .net to win32 : %s\n", wstr);
}

