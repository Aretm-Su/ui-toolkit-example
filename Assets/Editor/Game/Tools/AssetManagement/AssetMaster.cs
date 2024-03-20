using System;
using System.Collections.Generic;
using UnityEditor;
using Object = UnityEngine.Object;

namespace Editor.Game.Tools.AssetManagement
{
    public static class AssetMaster
    {
        public static string GetPath(Object asset, bool withoutName = true)
        {
            var assetPath = $"{AssetDatabase.GetAssetPath(asset)}";

            if (withoutName)
            {
                assetPath = assetPath.Remove(assetPath.LastIndexOf('/') + 1);
            }

            return assetPath;
        }
        
        public static TType Create<TType>(string assetName, string assetPath) where TType : Object
        {
            var asset = Activator.CreateInstance<TType>();
            asset.name = assetName;
            AssetDatabase.CreateAsset(asset, assetPath);
            AssetDatabase.SaveAssets();

            return asset;
        }
        
        public static void Save<TType>(TType asset) where TType : Object
        {
            EditorUtility.SetDirty(asset);
            AssetDatabase.SaveAssets();
        }

        public static void SaveTo(Object asset, Object container)
        {
            AssetDatabase.AddObjectToAsset(asset, container);
            EditorUtility.SetDirty(asset);
            EditorUtility.SetDirty(container);
            AssetDatabase.SaveAssets();
        }

        public static void DeleteFrom(Object asset, Object container)
        {
            AssetDatabase.RemoveObjectFromAsset(asset);
            EditorUtility.SetDirty(asset);
            EditorUtility.SetDirty(container);
            AssetDatabase.SaveAssets();
        }
        
        public static void Delete(Object asset)
        {
            AssetDatabase.DeleteAsset(GetPath(asset, false));
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }
        
        public static TType GetAsset<TType>() where TType : Object
        {
            string[] guids = AssetDatabase.FindAssets($"t:{typeof(TType).Name}");
            var path = AssetDatabase.GUIDToAssetPath(guids[0]);
            var asset = AssetDatabase.LoadAssetAtPath<TType>(path);

            return asset;
        }

        public static List<TType> GetAllAssets<TType>() where TType : Object
        {
            string[] guids = AssetDatabase.FindAssets($"t:{typeof(TType).Name}");
            List<TType> assets = new List<TType>(guids.Length);
            
            foreach (string guid in guids)
            {
                var path = AssetDatabase.GUIDToAssetPath(guid);
                var asset = AssetDatabase.LoadAssetAtPath<TType>(path);
                
                assets.Add(asset);
            }

            return assets;
        }
    }
}