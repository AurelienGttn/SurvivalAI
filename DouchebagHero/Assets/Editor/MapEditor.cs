using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor (typeof(MapGenerator))]
public class MapEditor : Editor{

    public override void OnInspectorGUI()
    {
        MapGenerator map = target as MapGenerator;

        // Generate map when there is a change in settings
        if (DrawDefaultInspector())
            map.GenerateMap();

        // Create a button to generate a map
        if (GUILayout.Button("Generate Map"))
            map.GenerateMap();
    }
}
