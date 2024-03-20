using System;

namespace Editor.Game.Events.Data.Models
{
    [Serializable]
    public class ActionNodeModel :  EventNodeModel
    {
        protected override void OnCreate()
        {
            NodeName = "ACTION NAME";
        }
    }
}