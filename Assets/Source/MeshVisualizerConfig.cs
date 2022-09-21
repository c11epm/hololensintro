using Microsoft.MixedReality.Toolkit.Environment;
using UnityEditor;
using UnityEngine;

namespace Source
{
    public class MeshVisualizerConfig : GenericSpatialMeshVisualizerConfig
    {
        #if UNITY_EDITOR
        [MenuItem("Assets/Create/MeshVisualizerConfig")]
        public static void CreateAsset()
        {
            MeshVisualizerConfig asset = ScriptableObject.CreateInstance<MeshVisualizerConfig>();
            AssetDatabase.CreateAsset(asset, "Assets/New MeshVisualizerConfig.asset");
            AssetDatabase.SaveAssets();
            
            EditorUtility.FocusProjectWindow();
            Selection.activeObject = asset;
        }
        #endif
    }
}