using Extensions;
using UnityEditor;
using UnityEngine;

namespace Editor.Game.Tools.DataModels
{
    public abstract class ModelBase : ScriptableObject
    {
        [SerializeField] public string Id;

        public void GenerateId()
        {
            if (Id.HasSymbols()) return;

            Id = GUID.Generate().ToString();
            OnCreate();
        }

        public virtual void Dispose()
        {
            AssetDatabase.RemoveObjectFromAsset(this);
            AssetDatabase.SaveAssets();
        }

        protected virtual void OnCreate()
        {
        }
    }
}