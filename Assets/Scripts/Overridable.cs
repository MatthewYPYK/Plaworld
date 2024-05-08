using System;
using UnityEngine;

[Serializable]
public struct ExtendedObject<type, type2>
{
    [SerializeField] private type value;
    public type2 ExtendedValue;

    public ExtendedObject(type value, type2 value2)
    {
        this.value = value;
        this.ExtendedValue = value2;
    }

    public static implicit operator type(ExtendedObject<type, type2> extendedObject)
    {
        return extendedObject.value;
    }
}
[Serializable]
public struct OveridableObject<type>{
    public bool Overide;
    [SerializeField] private type value;

    public OveridableObject(bool overide, type value)
    {
        this.Overide = overide;
        this.value = value;
    }

    public static implicit operator type(OveridableObject<type> overidableObject)
    {
        return overidableObject.value;
    }
}