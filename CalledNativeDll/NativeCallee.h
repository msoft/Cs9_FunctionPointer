#ifndef NATIVECALLEE_H
#define NATIVECALLEE_H

#include "StaticFunctionPointer.h"

#pragma once

extern "C" __declspec(dllexport) void* GetMultiplyFunctionPointer();
extern "C" __declspec(dllexport) int MultiplyWithFunctionPointer(int arg1, int arg2, int(*multiplyFcn)(int, int));
extern "C" __declspec(dllexport) int Multiply(int arg1, int arg2);
extern "C" __declspec(dllexport) void PerformBenchmarkWithFunctionPointer(int loopCount, int(*multiplyFcn)(int, int));

class NativeCallee
{
};

#endif