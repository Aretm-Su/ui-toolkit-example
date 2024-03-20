using System.Collections.Generic;
using Editor.Game.Tools.DataModels;

using UnityEngine;

namespace Editor.Game.Tools.Graph.Nodes
{
    public abstract class GraphNodeModel : ModelBase
    {
        [SerializeField] public string NodeName;
        [SerializeField] public List<GraphNodeModel> Inputs = new();
        [SerializeField] public List<GraphNodeModel> Outputs = new();
        [SerializeField] public Vector2 Position;
    }
}