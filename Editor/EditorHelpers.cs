using UnityEditor;

public static class EditorHelpers
{
    public static SerializedProperty[] FindProps(SerializedProperty prop, string[] propNames)
    {
        SerializedProperty[] props = new SerializedProperty[propNames.Length];
        for (int i = 0; i < propNames.Length; i++)
        {
            props[i] = prop.FindPropertyRelative(propNames[i]);
        }

        return props;
    }

    public static void DrawMultiplePropFields(SerializedProperty[] props)
    {
        foreach (var prop in props)
        {
            EditorGUILayout.PropertyField(prop);
        }
    }
}