using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnumTypeAttribute : PropertyAttribute
{
    public short enumType;
    public string enumTarget;
    public EnumTypeAttribute() { }
    public EnumTypeAttribute(string target, short type)
    {
        enumTarget = target;
        enumType = type;
    }
}
