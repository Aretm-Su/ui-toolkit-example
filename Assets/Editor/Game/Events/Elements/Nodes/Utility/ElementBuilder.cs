using Editor.Game.Tools.Graph.Nodes.Interface;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

namespace Editor.Game.Events.Elements.Nodes.Utility
{
    public static class ElementBuilder
    {
        public static VisualElement BuildTag(string text, Color color, int maxWidth)
        {
            var container = new VisualElement();
            var box = new Box();
            var label = new Label(text);

            box.style.borderBottomLeftRadius = 5;
            box.style.borderBottomRightRadius = 5;
            box.style.borderTopLeftRadius = 5;
            box.style.borderTopRightRadius = 5;
            box.style.backgroundColor = color;
            box.style.maxWidth = maxWidth;
            box.style.marginTop = 5;
            box.style.marginLeft = 5;

            label.style.alignSelf = new StyleEnum<Align>(Align.Center);
            label.style.color = Color.black;
            label.style.unityFontStyleAndWeight = new StyleEnum<FontStyle>(FontStyle.BoldAndItalic);
            
            box.Add(label);
            container.Add(box);
            
            return container;
        }

        public static VisualElement BuildName(string name, out TextField field)
        {
            var container = new VisualElement();
            var textField = new TextField
            {
                multiline = true,
                value = name,
                style =
                {
                    marginBottom = 5,
                    marginLeft = 5,
                    marginTop = 5,
                    marginRight = 5,
                    minHeight = 30,
                    fontSize = 25,
                }
            };

            container.Add(textField);
            
            field = textField;
            return container;
        }

        public static VisualElement BuildSocket(IPortFactory portFactory, out Port input, out Port output)
        {
            var container = new VisualElement();

            input = portFactory.CreateInputPort(Port.Capacity.Multi);
            output = portFactory.CreateOutputPort(Port.Capacity.Multi);
            container.Add(input);
            container.Add(output);
            container.style.alignItems = new StyleEnum<Align>(Align.Center);
            container.style.flexDirection = new StyleEnum<FlexDirection>(FlexDirection.Row);
            container.style.justifyContent = new StyleEnum<Justify>(Justify.SpaceBetween);

            return container;
        }
    }
}