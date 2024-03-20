using Editor.Game.Events.Data.Models;
using Editor.Game.Tools.Graph.Nodes;
using StaticData;
using UnityEngine;

namespace Editor.Game.Events.Data
{
    [CreateAssetMenu(fileName = nameof(EventGraphDatabase), menuName = ConfigsName.Story + nameof(EventGraphDatabase), order = 0)]
    public class EventGraphDatabase : GraphDatabase<EventNodeModel>
    {

    }
}