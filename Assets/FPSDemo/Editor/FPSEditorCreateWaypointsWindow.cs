using UnityEditor;
using UnityEngine;

public class FPSEditorCreateWaypointsWindow : FPSEditorBaseWindow
{
    internal enum CreationBehaviourEnum
    {
        Circle,
        Random
    }

    public GameObject WaypointsContainer;
    public GameObject WaypointPrefab;
    CreationBehaviourEnum CreationBehaviour;
    private int _countObject = 1;
    private float _radius = 10;
    private float _maxRandom = 10;
    private float _minRandom = -10;

    private float _messageTimeout;
    private string _message;
    private MessageType _messageType;
    
    protected override void OnGui()
    {
        SetSelected(ref WaypointsContainer);

        WaypointsContainer = CreateField("WP Container", WaypointsContainer);
        CreateNewField("Create new container", "_Container");
        WaypointPrefab = CreateField("WP Prefab", WaypointPrefab);
        CreationBehaviour = (CreationBehaviourEnum) EditorGUILayout.EnumPopup("Behaviour", CreationBehaviour);
        _countObject = EditorGUILayout.IntSlider("WP Count", _countObject, 1, 100);

        switch (CreationBehaviour)
        {
            case CreationBehaviourEnum.Circle:
                _radius = EditorGUILayout.Slider("Radius of circle", _radius, 10, 50);
                break;
            case CreationBehaviourEnum.Random:
                _maxRandom = EditorGUILayout.Slider("Max random position", _maxRandom, 10, 50);
                _minRandom = EditorGUILayout.Slider("Max random position", _minRandom, -10, -50);
                break;
        }

        if (GUILayout.Button("Generate waypoints"))
        {
            if (!WaypointsContainer)
            {
                ShowMessage("Waypoints container is missing", MessageType.Error);
                return;
            }

            if (!WaypointPrefab)
            {
                ShowMessage("Waypoints prefab is missing", MessageType.Error);
                return;
            }

            
            for (int i = 0; i < _countObject; i++)
            {
                var angle = i * Mathf.PI * 2 / _countObject;
                Vector3 pos = Vector3.zero;
                switch (CreationBehaviour)
                {
                    case CreationBehaviourEnum.Circle:
                        pos = new Vector3(Mathf.Cos(angle), 0, Mathf.Sin(angle)) * _radius;
                        break;
                    case CreationBehaviourEnum.Random:
                        pos = new Vector3(Random.Range(_minRandom, _maxRandom), 0, Random.Range(_minRandom, _maxRandom));
                        break;
                }
                var temp = Instantiate(WaypointPrefab, pos, Quaternion.identity);
                temp.name = "WP" + i.ToString("D2");
                temp.transform.parent = WaypointsContainer.transform;
            }
            
            ShowMessage("Waypoints succesfully created", MessageType.Info);
        }
    }
}