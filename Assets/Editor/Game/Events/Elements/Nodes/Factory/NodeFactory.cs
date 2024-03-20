using System;
using Editor.Game.Events.Data;
using Editor.Game.Events.Data.Models;
using Editor.Game.Tools.Graph.Nodes;

namespace Editor.Game.Events.Elements.Nodes.Factory
{
    public class NodeFactory
    {
        private readonly EventGraphDatabase _database;
        
        public NodeFactory(EventGraphDatabase database)
        {
            _database = database;
        }
        
        public GraphNode Create(string tag)
        {
            switch (tag)
            {
                case "action": return Create(_database.CreateNode<ActionNodeModel>());
                case "situation": return Create(_database.CreateNode<SituationNodeModel>());
                case "start": return Create(_database.CreateNode<StartNodeModel>());
                
                default: throw new Exception($"Unable to create node by tag: {tag}");
            }
        }

        public GraphNode Create(GraphNodeModel graphModel)
        {
            switch (graphModel)
            {
                case ActionNodeModel model: return new ActionNode(model);
                case SituationNodeModel model: return new SituationNode(model);
                case StartNodeModel model: return new StartNode(model);
                
                default: throw new Exception($"Unable to restore node by model: {graphModel}");
            }
        }
    }
}