using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomPropertyDrawer(typeof(NoiseSettings))]
public class NoiseSettingsPropertyDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        SerializedProperty typeProp = property.FindPropertyRelative("m_type");
        SerializedProperty seedProp = property.FindPropertyRelative("m_seed");
        
        EditorGUILayout.PropertyField(typeProp);
        EditorGUILayout.PropertyField(seedProp);

        NoiseSettings.NoiseType type = (NoiseSettings.NoiseType)typeProp.intValue;

        switch (type)
        {
            case NoiseSettings.NoiseType.Perlin:
                DrawPerlinProp(property);
                return;
            case NoiseSettings.NoiseType.Voronoi:
                DrawVoronoiProp(property);
                return;
        }
    }

    private void DrawPerlinProp(SerializedProperty property)
    {
        SerializedProperty[] props = EditorHelpers.FindProps(property, new string[]
        {
            "m_scale", "m_octaves", "m_lacunarity", "m_persistance", "m_offset"
        });
        EditorHelpers.DrawMultiplePropFields(props);
    }

    private void DrawVoronoiProp(SerializedProperty property)
    {
        SerializedProperty[] props = EditorHelpers.FindProps(property, new string[]
        {
            "m_cellCount"
        });
        EditorHelpers.DrawMultiplePropFields(props);
    }
}