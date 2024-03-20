using Editor.Game.Events.Data.Models;
using Editor.Game.Events.Elements.Nodes.Utility;
using Editor.Game.Tools.Graph.Nodes;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace Editor.Game.Events.Elements.Nodes
{
    public abstract class PrimitiveNode<TModel> : GraphNode where TModel : EventNodeModel
    {
        private readonly TModel _model;
        private readonly SerializedObject _serializedModel;
        private readonly VisualElement _tag;
        private readonly VisualElement _nodeName;
        private readonly VisualElement _socket;
        private readonly TextField _nameField;
        private readonly Port _inPort;
        private readonly Port _outPort; 
        
        protected abstract string TagText { get; }
        protected abstract Color TagColor { get; }
        protected abstract int TagWidth { get; }

        protected PrimitiveNode(TModel model)
        {
            _model = model;
            _serializedModel = new SerializedObject(_model);
            
            _tag = ElementBuilder.BuildTag(TagText, TagColor, TagWidth);
            _nodeName = ElementBuilder.BuildName("NAME", out _nameField);
            _socket = ElementBuilder.BuildSocket(this, out _inPort, out _outPort);

            Init(_model.Id);
        }

        public override void Draw()
        {
            titleContainer.Clear();
            mainContainer.Clear();
            mainContainer.Add(_tag);
            mainContainer.Add(_nodeName);
            mainContainer.Add(_socket);
            
            SetPosition(_model.Position);
            SetStyles();
            BindProperties();
            RefreshExpandedState();
        }

        public override void OnSelected()
        {
            Selection.activeObject = _model;
        }

        protected override void OnPositionChange(Vector2 position)
        {
            _model.Position = position;
        }

        private void BindProperties()
        {
            _nameField.BindProperty(_serializedModel.FindProperty(nameof(_model.NodeName)));
        }

        private void SetStyles()
        {
            mainContainer.style.minWidth = 100;
        }
    }
}