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
            if (bar is not null)
            {
                bar.Value = currentVal;
            }
            //bar.Value = currentVal;
        }
    }

    public float MaxVal { get => maxVal; 
        set
        {
            this.maxVal = value;
            if (bar is not null)
            {
                bar.MaxValue = maxVal;
            }
            //bar.MaxValue = maxVal;
        } 
    }

    public void Initialize()
    {
        this.MaxVal = maxVal;
        this.CurrentVal = currentVal;
    }
}
