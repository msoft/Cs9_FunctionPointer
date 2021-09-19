#include "NativeCallee.h"

#include <wchar.h>
#include <iostream>

void* GetMultiplyFunctionPointer()
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

void PerformBenchmarkWithFunctionPointer(int loopCount, int(*multiplyFcn)(int, int))
{
	const int arrayLength = 20;

	int firstArray[arrayLength] = { 23, 87, 51, 98, 29, 75, 93, 48, 24, 83, 47, 38, 62, 22, 97, 15, 52, 41, 74, 13 };
	int secondArray[arrayLength];

	for (int i = 0; i < arrayLength; i++)
	{
		secondArray[i] = firstArray[arrayLength - i];
	}

	int value = 0;
	int offset = 0;
	bool add = true;
	for (int i = 0; i < loopCount; i++)
	{
		for (int j = 0; j < arrayLength; j++)
		{
			int index = (offset + j) % arrayLength;
			int multiplicationResult = multiplyFcn(firstArray[index], secondArray[index]);
			if (add)
				value += multiplicationResult;
			else
				value -= multiplicationResult;

			add = !add;
		}

		offset++;
	}
}

int Multiply(int arg1, int arg2)
{
	return arg1 * arg2;
}