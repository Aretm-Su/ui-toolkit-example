using UnityEditor.Experimental.GraphView;

namespace Editor.Game.Tools.Graph.Interface
{
    public interface IElementContainer
    {
        void AddElement(GraphElement element);
        void RemoveElement(GraphElement element);
    }
}