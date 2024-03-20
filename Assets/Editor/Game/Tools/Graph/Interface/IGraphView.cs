using System;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

namespace Editor.Game.Tools.Graph.Interface
{
    public interface IGraphView
    {
        event Action<string, string> OnRemoveElementRequest;
        event Action<string, Vector2> OnCreateElementRequest;
        event Action<string, Vector2> OnElementPositionChange;
        event Action<Port, Port> OnConnectionEstablished;
        event Action<Port, Port> OnConnectionLost;
    }
}