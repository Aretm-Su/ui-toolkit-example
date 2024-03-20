using UnityEngine;

namespace Editor.Game.Tools.Graph.Interface
{
    public interface IGraphElement
    {
        string Id { get; }

        void Draw();
        void Dispose();

        void SetPosition(Vector2 value);
    }
}