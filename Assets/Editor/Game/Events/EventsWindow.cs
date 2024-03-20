using Editor.Game.Events.Data;
using Editor.Game.Events.Elements.Graph;
using Editor.Game.Tools.Graph.Interface;
using UnityEditor;

namespace Editor.Game.Events
{
    public class EventsWindow : EditorWindow
    {
        private IGraphPresenter _graph;

        [MenuItem("CLICK ON ME/EXAMPLE")]
        private static void OpenWindow()
        {
            var window = GetWindow<EventsWindow>("GRAPH EXAMPLE");
            var database = AssetDatabase.LoadAssetAtPath<EventGraphDatabase>("Assets/#EventGraphDatabase.asset");

            window.Initialize(database);
        }

        private void Initialize(EventGraphDatabase database)
        {
            _graph = new EventGraphPresenter(rootVisualElement, database);
            _graph.Build();
        }

        private void OnDestroy()
        {
            _graph.Dispose();
        }
    }
}