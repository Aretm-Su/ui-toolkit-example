using UnityEditor.Experimental.GraphView;

namespace Editor.Game.Tools.Graph.Nodes.Interface
{
    public interface IPortFactory
    {
        Port CreateInputPort(Port.Capacity capacity);
        Port CreateOutputPort(Port.Capacity capacity);
    }
}