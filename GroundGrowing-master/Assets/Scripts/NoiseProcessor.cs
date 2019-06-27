﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LibNoise;
using LibNoise.Operator;
using LibNoise.Generator;

public static class NoiseProcessor
{
    public static ModuleBase InitNoise(NoiseFunction[] noiseStack, int seed)
    {
        ModuleBase _baseModule = null;
        for (int i = 0; i < noiseStack.Length; i++)
        {
            noiseStack[i].seed = seed + i;

            //for first valid noise pattern simply pass the noise function
            if ((noiseStack[i].enabled) && (_baseModule == null))
            {
                _baseModule = noiseStack[i].MakeNoise();
            }
            else if (noiseStack[i].enabled)
            {
                if (noiseStack[i].blendMode == NoiseFunction.BlendMode.Power)
                {
                    _baseModule = new Power(_baseModule, noiseStack[i].MakeNoise());
                }
                else if (noiseStack[i].blendMode == NoiseFunction.BlendMode.Subtract)
                {
                    _baseModule = new Subtract(_baseModule, noiseStack[i].MakeNoise());
                }
                else if (noiseStack[i].blendMode == NoiseFunction.BlendMode.Max)
                {
                    _baseModule = new Max(_baseModule, noiseStack[i].MakeNoise());
                }
                else if (noiseStack[i].blendMode == NoiseFunction.BlendMode.Min)
                {
                    _baseModule = new Min(_baseModule, noiseStack[i].MakeNoise());
                }
                else if (noiseStack[i].blendMode == NoiseFunction.BlendMode.Multiply)
                {
                    _baseModule = new Multiply(_baseModule, noiseStack[i].MakeNoise());
                }
                else
                {
                    _baseModule = new Add(_baseModule, noiseStack[i].MakeNoise());
                }
            }
        }
        return _baseModule;
    }


}


