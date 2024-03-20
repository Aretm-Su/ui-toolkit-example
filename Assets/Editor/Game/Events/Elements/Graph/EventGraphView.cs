using System;
using System.Collections.Generic;
using Editor.Game.Tools.Graph.Interface;
using Editor.Game.Tools.Graph.Nodes;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

namespace Editor.Game.Events.Elements.Graph
{
    public class EventGraphView : GraphView, IGraphView, IElementContainer
    {
        public event Action<string, Vector2> OnCreateElementRequest = delegate { };
        public event Action<string, string> OnRemoveElementRequest = delegate { };
        public event Action<string, Vector2> OnElementPositionChange = delegate { };
        public event Action<Port, Port> OnConnectionEstablished = delegate { };
        public event Action<Port, Port> OnConnectionLost = delegate {  }; 

        public EventGraphView()
        {
            AddGrid();
            AddInput();
            AddContextualMenu();
            AddSelectionDeleteListener();
            AddGraphChangeListener();
        }

        #region Visual

        private void AddGrid()
        {
            GridBackground background = new GridBackground();

            background.StretchToParentSize();
            Insert(0, background);
        }

        #endregion

        #region Interaction

        private void AddInput()
        {
            this.AddManipulator(new ContentDragger());
            this.AddManipulator(new ContentZoomer());
            this.AddManipulator(new SelectionDragger());
            this.AddManipulator(new RectangleSelector());
        }

        private void AddContextualMenu()
        {
            this.AddManipulator(CreateDeleteContextualMenu());
            this.AddManipulator(CreateElementFromContextualMenu("story/situation","situation"));
            this.AddManipulator(CreateElementFromContextualMenu("story/action","action"));
            this.AddManipulator(CreateElementFromContextualMenu("control/start","start"));
        }

        #endregion

        #region Instating
        
        private IManipulator CreateDeleteContextualMenu()
        {
            ContextualMenuManipulator manipulator = new(
                menuEvent => menuEvent.menu.AppendAction(
                    "delete",
                    action => deleteSelection.Invoke("delete", AskUser.DontAskUser))
            );

            return manipulator;
        }
        
        private IManipulator CreateElementFromContextualMenu(string menuName, string elementTag)
        {
            ContextualMenuManipulator manipulator = new(
                menuEvent => menuEvent.menu.AppendAction(
                    menuName,
                    action => OnCreateElementRequest(elementTag, GetMousePosition(action.eventInfo.localMousePosition)))
            );

            return manipulator;
        }

        #endregion

        private void AddGraphChangeListener()
        {
            graphViewChanged += change =>
            {
                if (change.movedElements != null)
                {
                    foreach (GraphElement moved in change.movedElements)
                    {
                        if (moved is IGraphElement element) OnElementPositionChange(element.Id, moved.GetPosition().position);
                    }
                }

                if (change.edgesToCreate != null)
                {
                    foreach (var edge in change.edgesToCreate)
                    {
                        OnConnectionEstablished(edge.input, edge.output);
                    }
                }

                return change;
            };
        }

        private void AddSelectionDeleteListener()
        {
            deleteSelection += (_, _) =>
            {
                List<GraphElement> elementsToDelete = new();

                foreach (ISelectable element in selection)
                {
                    if (element is GraphNode node) elementsToDelete.Add(node);
                }

                foreach (var element in elementsToDelete)
                {
                    if (element is GraphNode node)
                    {
                        node.Query<Port>()
                            .Where(x => x.connected)
                            .ForEach(port =>
                        {
                            foreach (var edge in port.connections)
                            {
                                OnConnectionLost(edge.input, edge.output);
                                RemoveElement(edge);
                            }
                        });
                    }

                    if (element is GraphNode graphNode) OnRemoveElementRequest("node", graphNode.Id);
                }

                elementsToDelete.Clear();
            };
        }

        private Vector2 GetMousePosition(Vector2 mousePosition)
        {
            var worldPos = ElementAt(0).LocalToWorld(mousePosition);
            var localPos = ElementAt(1).WorldToLocal(worldPos);
            
            return localPos;
        }

        public override List<Port> GetCompatiblePorts(Port startPort, NodeAdapter nodeAdapter)
        {
            List<Port> compatiblePorts = new List<Port>();

            ports.ForEach(port =>
            {
                if (startPort == port) return;

                if (startPort.node == port.node) return;

                if (startPort.direction == port.direction) return;

                compatiblePorts.Add(port);
            });

            return compatiblePorts;
        }

        public override void BuildContextualMenu(ContextualMenuPopulateEvent evt)
        {
            
        }
    }
}