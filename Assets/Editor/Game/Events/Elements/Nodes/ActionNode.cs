using Editor.Game.Events.Data.Models;
using UnityEngine;

namespace Editor.Game.Events.Elements.Nodes
{
    public class ActionNode : PrimitiveNode<ActionNodeModel>
    {
        public ActionNode(ActionNodeModel model) : base(model) { }

        protected override string TagText => "ACTION";
        protected override Color TagColor => Color.green;
        protected override int TagWidth => 50;
    }
}