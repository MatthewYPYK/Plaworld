using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Stat
{
    [SerializeField]
    private BarScript bar;
    [SerializeField]
    private float maxVal;
    [SerializeField]
    private float currentVal;

    public float CurrentVal { get => currentVal; 
        set
        {
            currentVal = value;
            //bar.Value = currentVal;
        }
    }

    public float MaxVal { get => maxVal; 
        set
        {
            this.maxVal = value;
            //bar.MaxValue = maxVal;
        } 
    }

    public void Initialize()
    {
        this.MaxVal = maxVal;
        this.CurrentVal = currentVal;
    }
}
