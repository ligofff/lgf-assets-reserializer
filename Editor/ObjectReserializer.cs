using System.Collections.Generic;
using System.Linq;
using UnityEditor;

namespace Editor
{
    public  class ObjectReserializer
    {
        [MenuItem("Ligofff/Reserialize All Assets")]
        public static void OpenWindow()
        {
            if (EditorUtility.DisplayDialog("Reserialize All Assets?", "It can take a while. Are you sure?", "Yes",
                    "No"))
            {
                var assets = AssetDatabase.FindAssets("").Select(AssetDatabase.GUIDToAssetPath);
                AssetDatabase.ForceReserializeAssets(assets);
            }
        }
        
        [MenuItem("Assets/Reserialize Asset")]
        private static void ReserializeAsset()
        {
            // If folder - reserialize all assets in this folder
            if (Selection.activeObject is DefaultAsset)
            {
                if (EditorUtility.DisplayDialog($"Reserialize All Assets in folder {Selection.activeObject}?", "Are you sure?", "Yes", "No"))
                {
                    var path = AssetDatabase.GetAssetPath(Selection.activeObject);
                    var assets = AssetDatabase.FindAssets("", new[] {path}).Select(AssetDatabase.GUIDToAssetPath);
                    AssetDatabase.ForceReserializeAssets(assets);
                }
            }
            else
            {
                AssetDatabase.ForceReserializeAssets(new List<string>(){AssetDatabase.GetAssetPath(Selection.activeObject)});
            }
        }
        
        [MenuItem("Assets/Reserialize Asset", true)]
        private static bool ReserializeAssetValidation()
        {
            return Selection.activeObject != null && AssetDatabase.Contains(Selection.activeObject);
        }
    }
}