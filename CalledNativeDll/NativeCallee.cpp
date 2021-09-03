#include "NativeCallee.h"

#include <wchar.h>
#include <iostream>

void* GetStaticFunctionPointer()
{
	int(*multiplyFctPtr)(int, int) { &StaticFunctionPointer::Multiply };
	void* voidPtr = reinterpret_cast<void*>(multiplyFctPtr);
	return voidPtr;
}

int MultiplyWithFunctionPointer(int arg1, int arg2, int(*multiplyFcn)(int, int))
{
	wprintf(L"Displaying function pointer address: %p\n", multiplyFcn);
	return multiplyFcn(arg1, arg2);
}

int Multiply(int arg1, int arg2)
{
	return arg1 * arg2;
}