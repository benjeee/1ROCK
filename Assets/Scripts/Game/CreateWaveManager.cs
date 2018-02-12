using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class CreateWaveManager {
    [MenuItem("Assets/Create/Wave Manager")]
    public static void Create()
    {
        WaveManager asset = ScriptableObject.CreateInstance<WaveManager>();
        AssetDatabase.CreateAsset(asset, "Assets/WaveManager.asset");
        AssetDatabase.SaveAssets();
        EditorUtility.FocusProjectWindow();

        Selection.activeObject = asset;
    }
}
