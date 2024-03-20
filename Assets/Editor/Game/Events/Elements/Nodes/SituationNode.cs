using Editor.Game.Events.Data.Models;
using UnityEngine;

namespace Editor.Game.Events.Elements.Nodes
{
    public class SituationNode : PrimitiveNode<SituationNodeModel>
    {
        public SituationNode(SituationNodeModel model) : base(model) { }

        protected override string TagText => "SITUATION";
        protected override Color TagColor => Color.yellow;
        protected override int TagWidth => 70;
    }
}