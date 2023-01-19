using System;
using System.Reflection;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
[CustomPropertyDrawer(typeof(EnumTypeAttribute))]
public class EnumTypeInspector : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        EnumTypeAttribute flagSettings = (EnumTypeAttribute)attribute;
        short tabindex = GetEnumIndex(flagSettings, property);
        bool enabled = flagSettings.enumType == tabindex;
        bool wasEnabled = GUI.enabled;
        GUI.enabled = enabled;
        if (enabled)
        {
            EditorGUI.PropertyField(position, property, label, true);
        }
        GUI.enabled = wasEnabled;
    }

    private short GetEnumIndex(EnumTypeAttribute attr, SerializedProperty property)
    {
        short enabled = -1;
        string propertyPath = property.propertyPath;
        string conditionPath = propertyPath.Replace(property.name, attr.enumTarget);
        SerializedProperty sourcePropertyValue = property.serializedObject.FindProperty(conditionPath);
        if (sourcePropertyValue != null)
        {
            enabled = (short)sourcePropertyValue.intValue;
        }
        else
        {
            Debug.LogWarning("Attempting to use a EnumTypeAttribute but no matching SourcePropertyValue found in object: " + attr.enumTarget);
        }
        return enabled;
    }
}
