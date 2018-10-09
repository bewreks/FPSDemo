using FPSDemoEditor;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering;

namespace FPSDemoEditor
{
    public class FPSEditorShadowsTurner : FPSEditorBaseWindow
    {
        private GameObject _object;
        private int _count;
        private bool _isOn;
        
        protected override void OnGui()
        {
            SetSelected(ref _object);
            _object = FPSEditorLayout.CreateField("Object", _object);

            _isOn = EditorGUILayout.ToggleLeft("Cast shadows", _isOn);

            if (GUILayout.Button("Recalc"))
            {
                if (!_object)
                {
                    ShowMessage("Error", MessageType.Error);
                    return;
                }

                var component = _object.GetComponent<Renderer>();
                SetCastShadows(component);

                var components = _object.GetComponentsInChildren<Renderer>(true);
                foreach (var renderer in components)
                {
                    SetCastShadows(renderer);
                }
                
                ShowMessage($"Set {_count} renderrers", MessageType.Info);
                _count = 0;
            }
        }

        private void SetCastShadows(Renderer renderer)
        {
            if (!renderer)
            {
                return;
            }

            renderer.shadowCastingMode = _isOn ? ShadowCastingMode.On : ShadowCastingMode.Off;
            _count++;
        }
    }
}