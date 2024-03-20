using System;

namespace Editor.Game.Events.Data.Models
{
    [Serializable]
    public class SituationNodeModel : EventNodeModel
    {
        protected override void OnCreate()
        {
            NodeName = "SITUATION NAME";
        }
    }
}