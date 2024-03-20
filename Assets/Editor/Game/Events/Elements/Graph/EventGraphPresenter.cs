using System.Collections.Generic;
using Editor.Game.Events.Data;
using Editor.Game.Events.Elements.Nodes.Factory;
using Editor.Game.Tools.AssetManagement;
using Editor.Game.Tools.Graph.Interface;
using Editor.Game.Tools.Graph.Nodes;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

namespace Editor.Game.Events.Elements.Graph
{
    public class EventGraphPresenter : IGraphPresenter
    {
        private readonly EventGraphDatabase _database;
        private readonly List<GraphNode> _nodes;
        private readonly NodeFactory _nodeFactory;
        private readonly EventGraphView _view;

        public EventGraphPresenter(VisualElement root, EventGraphDatabase database)
        {
            _database = database;
            _nodes = new();
            _nodeFactory = new(_database);
            _view = new();

            root.Add(_view);
        }

        public void Build()
        {
            _view.StretchToParentSize();
            _view.OnCreateElementRequest += ElementCreateRequestListener;
            _view.OnRemoveElementRequest += ElementRemoveRequestListener;
            _view.OnElementPositionChange += ElementPositionChangeListener;
            _view.OnConnectionEstablished += ConnectionEstablishedListener;
            _view.OnConnectionLost += ConnectionLostListener;

            LoadAllNodes();
            LoadConnections();
        }

        public void Dispose()
        {
            _view.OnCreateElementRequest -= ElementCreateRequestListener;
            _view.OnRemoveElementRequest -= ElementRemoveRequestListener;
            _view.OnElementPositionChange -= ElementPositionChangeListener;
            _view.OnConnectionEstablished -= ConnectionEstablishedListener;
            _view.OnConnectionLost -= ConnectionLostListener;
        }

        #region Listeners

        private void ElementRemoveRequestListener(string tag, string guid)
        {
            DeleteNode(guid);
        }

        private void ElementCreateRequestListener(string tag, Vector2 position)
        {
            CreateNewNode(tag, position);
        }

        private void ElementPositionChangeListener(string guid, Vector2 position)
        {
            var model = _database.GetNode(guid);

            model.Position = position;
        }

        private void ConnectionEstablishedListener(Port input, Port output)
        {
            var inputNode = input.node as IGraphElement;
            var outputNode = output.node as IGraphElement;

            var inputModel = _database.GetNode(inputNode?.Id);
            var outputModel = _database.GetNode(outputNode?.Id);

            if (!outputModel.Outputs.Contains(inputModel))
            {
                outputModel.Outputs.Add(inputModel);
            }

            if (!inputModel.Inputs.Contains(outputModel))
            {
                inputModel.Inputs.Add(outputModel);
            }

            AssetMaster.Save(_database);
            AssetMaster.Save(inputModel);
        }

        private void ConnectionLostListener(Port input, Port output)
        {
            var inputNode = input.node as IGraphElement;
            var outputNode = output.node as IGraphElement;

            var inputModel = _database.GetNode(inputNode?.Id);
            var outputModel = _database.GetNode(outputNode?.Id);

            if (inputModel == null || outputModel == null) return;
            
            outputModel.Outputs.Remove(inputModel);

            AssetMaster.Save(_database);
            AssetMaster.Save(inputModel);
        }

        #endregion

        #region Nodes

        private void LoadAllNodes()
        {
            foreach (var model in _database.GetAllNodes())
            {
                GraphNode node = _nodeFactory.Create(model);

                _view.AddElement(node);
                _nodes.Add(node);
                
                node.Draw();
            }
        }

        private void CreateNewNode(string tag, Vector2 position)
        {
            GraphNode node = _nodeFactory.Create(tag);
            
            _view.AddElement(node);
            _nodes.Add(node);
            
            node.SetPosition(position);
            node.Draw();
        }

        private void DeleteNode(string guid)
        {
            GraphNode node = _nodes.Find(x => x.Id == guid);

            _view.RemoveElement(node);
            _nodes.Remove(node);
            _database.DeleteNode(node.Id);
            
            node.Dispose();
        }
        
        #endregion

        #region Connections

        private void LoadConnections()
        {
            foreach (var model in _database.GetAllNodes())
            {
                foreach (var output in model.Outputs)
                {
                    GraphNode outputNode = _view
                        .Query<GraphNode>()
                        .Where(x => x.Id == model.Id)
                        .First();
                    
                    GraphNode inputNode = _view
                        .Query<GraphNode>()
                        .Where(x => x.Id == output.Id)
                        .First();

                    Port outputPort = outputNode
                        .Query<Port>()
                        .Where(x => x.direction == Direction.Output)
                        .First();

                    Port inputPort = inputNode
                        .Query<Port>()
                        .Where(x => x.direction == Direction.Input)
                        .Where(x => !x.connected)
                        .First();

                    if (outputPort != null && inputPort != null)
                    {
                        Edge edge = outputPort.ConnectTo(inputPort);

                        _view.AddElement(edge);
                    }
                }
            }
        }

        #endregion
    }
}