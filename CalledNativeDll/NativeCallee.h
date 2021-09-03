#ifndef NATIVECALLEE_H
#define NATIVECALLEE_H

#include "StaticFunctionPointer.h"

#pragma once

#ifndef NATIVECALLEE_EXPORTS
#define NATIVECALLEE_API __declspec(dllexport)
#else
#define NATIVECALLEE_API __declspec(dllimport)
#endif

extern "C" NATIVECALLEE_API void* GetStaticFunctionPointer();
extern "C" NATIVECALLEE_API int MultiplyWithFunctionPointer(int arg1, int arg2, int(*multiplyFcn)(int, int));
extern "C" NATIVECALLEE_API int Multiply(int arg1, int arg2);

class NativeCallee
{
};

#endif