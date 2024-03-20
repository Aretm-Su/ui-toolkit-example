using Editor.Game.Events.Data.Models;
using Editor.Game.Tools.Graph.Nodes;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

namespace Editor.Game.Events.Elements.Nodes
{
    public class StartNode : GraphNode 
    {
        private readonly StartNodeModel _model;
        
        public StartNode(StartNodeModel model)
        {
            _model = model;
            
            Init(_model.Id);
        }

        public override void Draw()
        {
            titleContainer.Clear();
            mainContainer.Clear();
            mainContainer.Add(BuildLabel());
            mainContainer.Add(BuildSocket());
            
            SetPosition(_model.Position);
            SetStyles();
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

        private void SetStyles()
        {
            mainContainer.style.minWidth = 70;
            mainContainer.style.flexDirection = new StyleEnum<FlexDirection>(FlexDirection.Row);
            mainContainer.style.justifyContent = new StyleEnum<Justify>(Justify.SpaceBetween);
        }

        private VisualElement BuildLabel()
        {
            var container = new VisualElement();
            var box = new Box();
            var label = new Label("START");

            box.style.borderBottomLeftRadius = 5;
            box.style.borderBottomRightRadius = 5;
            box.style.borderTopLeftRadius = 5;
            box.style.borderTopRightRadius = 5;
            box.style.backgroundColor = Color.white;
            box.style.maxWidth = 60;
            box.style.marginTop = 5;
            box.style.marginLeft = 5;

            label.style.alignSelf = new StyleEnum<Align>(Align.Center);
            label.style.color = Color.black;
            label.style.unityFontStyleAndWeight = new StyleEnum<FontStyle>(FontStyle.BoldAndItalic);
            
            box.Add(label);
            container.Add(box);
            
            return container;
        }

        private VisualElement BuildSocket()
        {
            var container = new VisualElement();
            var port = CreateOutputPort(Port.Capacity.Multi);
            
            container.Add(port);
            container.style.alignItems = new StyleEnum<Align>(Align.Center);
            container.style.flexDirection = new StyleEnum<FlexDirection>(FlexDirection.Row);
            container.style.justifyContent = new StyleEnum<Justify>(Justify.SpaceBetween);

            return container;
        }
    }
}