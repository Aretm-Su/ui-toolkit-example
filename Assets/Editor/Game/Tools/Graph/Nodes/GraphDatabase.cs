using System.Collections.Generic;
using Editor.Game.Tools.AssetManagement;
using UnityEngine;

namespace Editor.Game.Tools.Graph.Nodes
{
    public class GraphDatabase<TModel>: ScriptableObject where TModel : GraphNodeModel
    {
        [SerializeField] protected List<TModel> models = new();
        
        public TModel GetNode(string id)
        {
            return models.Find(x => x.Id == id);
        }

        public IEnumerable<TModel> GetAllNodes()
        {
            return models;
        }

        public TModel CreateNode<TConcreteModel>() where TConcreteModel : TModel
        {
            TModel newModel = CreateModel(); SaveModel(newModel);
            
            return newModel;

            #region Internal

            TModel CreateModel()
            {
                TModel model = CreateInstance<TConcreteModel>();
                model.name = model.GetType().Name;
                model.GenerateId();
                return model;
            }

            void SaveModel(TModel model)
            {
                models.Add(model);
                AssetMaster.SaveTo(model, this);
            }

            #endregion
        }

        public void DeleteNode(string id)
        {
            if (IsModelExist())
            {
                var model = FindModel(); DeleteModel(model);
            }

            #region Internal

            bool IsModelExist()
            {
                return models.Exists(x => x.Id == id);
            }

            TModel FindModel()
            {
                return models.Find(x => x.Id == id);
            }

            void DeleteModel(TModel model)
            {
                models.Remove(model);
                model.Dispose();
                AssetMaster.DeleteFrom(model, this);
            }
            
            #endregion
        }
    }
}