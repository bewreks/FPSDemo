using FPSDemo;
using UnityEditor;
using UnityEngine;

namespace FPSDemoEditor.Waypoints
{
    internal enum WaypointsCreationBehaviourEnum
    {
        Circle,
        Random
    }

    internal enum WaypointsWaittimeSetterEnum
    {
        Total,
        Random
    }

    public class FPSEditorCreateWaypointsWindow : FPSEditorBaseWindow
    {
        private WaypointCreationBehaviourState _behaviourState;
        private WaypointsWaittimeSetterState _waittimeSetterState;
        private GameObject _waypointsContainer;
        private GameObject _waypointPrefab;
        private WaypointsCreationBehaviourEnum _waypointsCreationBehaviour;
        private WaypointsWaittimeSetterEnum _waittimeSetter;
        private int _countObject = 1;

        protected override void OnGui()
        {
            SetSelected(ref _waypointsContainer);

            _waypointsContainer = FPSEditorLayout.CreateField("WP Container", _waypointsContainer);
            CreateNewField("Create new container", "WP_Container");
            
            FPSEditorLayout.ShowDelim();
            _waypointPrefab = FPSEditorLayout.CreateField("WP Prefab", _waypointPrefab);
            
            FPSEditorLayout.ShowDelim();
            _countObject = EditorGUILayout.IntSlider("WP Count", _countObject, 1, 100);
            
            FPSEditorLayout.ShowDelim();
            _waypointsCreationBehaviour = (WaypointsCreationBehaviourEnum) EditorGUILayout.EnumPopup(_waypointsCreationBehaviour, EditorStyles.toolbarPopup);
            _behaviourState = WaypointCreationBehaviourState.GetState(_waypointsCreationBehaviour);
            _behaviourState.Show();

            FPSEditorLayout.ShowDelim();
            _waittimeSetter = (WaypointsWaittimeSetterEnum) EditorGUILayout.EnumPopup(_waittimeSetter, EditorStyles.toolbarPopup);
            _waittimeSetterState = WaypointsWaittimeSetterState.GetState(_waittimeSetter);
            _waittimeSetterState.Show();
            
            FPSEditorLayout.ShowDelim();
            if (GUILayout.Button("Generate waypoints"))
            {
                WaypointsCreation();
            }
        }

        private void WaypointsCreation()
        {
            if (!_waypointsContainer)
            {
                ShowMessage("Waypoints container is missing", MessageType.Error);
                return;
            }

            if (!_waypointPrefab)
            {
                ShowMessage("Waypoints prefab is missing", MessageType.Error);
                return;
            }


            for (int i = 0; i < _countObject; i++)
            {
                var pos = _behaviourState.GetPosition(i, _countObject);
                var temp = Instantiate(_waypointPrefab, pos, Quaternion.identity);
                temp.name = "WP" + i.ToString("D2");
                temp.GetComponent<Waypoint>().WaitTime = _waittimeSetterState.GetWaitTime();
                temp.transform.parent = _waypointsContainer.transform;
            }

            ShowMessage("Waypoints succesfully created", MessageType.Info);
        }
    }
}