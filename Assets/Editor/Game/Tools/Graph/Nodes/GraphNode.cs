using Editor.Game.Tools.Graph.Interface;
using Editor.Game.Tools.Graph.Nodes.Interface;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

namespace Editor.Game.Tools.Graph.Nodes
{
    public abstract class GraphNode : Node, IPortFactory, IGraphElement
    {
        private string _id;
        private bool _initialized;
        
        public string Id => _id;

        public virtual void Draw() { }

        public virtual void Dispose() { }

        public Port CreateInputPort(Port.Capacity capacity) => InstantiatePort(Orientation.Horizontal, Direction.Input, Port.Capacity.Multi, null);

        public Port CreateOutputPort(Port.Capacity capacity) => InstantiatePort(Orientation.Horizontal, Direction.Output, Port.Capacity.Multi, null);

        public void SetPosition(Vector2 position)
        {
            SetPosition(new Rect(position, Vector2.zero));
            OnPositionChange(position);
        }

        protected virtual void OnPositionChange(Vector2 position) { }

        protected void Init(string id)
        {
            if (_initialized) return;

            _id = id;
            _initialized = true;
        }
    }
}