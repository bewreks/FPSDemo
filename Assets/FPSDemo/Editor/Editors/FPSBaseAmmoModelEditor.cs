using System.Collections.Generic;
using FPSDemo;
using UnityEditor;
using UnityEngine;

namespace FPSDemoEditor.Editors
{
    public abstract class FPSBaseAmmoModelEditor<M> : Editor
        where M : BaseAmmoModel
    {
        protected M _model;

        private bool _isMinimizedReadonly = true;

        public override void OnInspectorGUI()
        {
            _model = (M) target;
            
            FPSEditorLayout.ShowHeader<M>(_model);
            
            FPSEditorLayout.ShowDelim();


            _model.Damage = EditorGUILayout.FloatField("Damage", _model.Damage);
            _model.Mask = LayerMaskField("Hittable layers", _model.Mask);
            
            OnGui();

            FPSEditorLayout.ShowDelim();

            _isMinimizedReadonly = EditorGUILayout.Foldout(_isMinimizedReadonly, "Read only fields", true);
            if (_isMinimizedReadonly)
            {
                GUI.enabled = false;
                EditorGUILayout.ObjectField("Owner", _model.Owner, typeof(GameObject), false);
                EditorGUILayout.FloatField("Speed", _model.Speed);
                EditorGUILayout.ToggleLeft("Is hitted", _model.IsHitted);
                OnGuiReadonly();
                GUI.enabled = true;
            }

        }

        protected static LayerMask LayerMaskField( string label, LayerMask layerMask) {
            List<string> layers = new List<string>();
    
            List<int> layerNumbers = new List<int>();
            for (int i = 0; i< 32; i++) {
                string layerName = LayerMask.LayerToName(i);
                if (layerName != "")
                {
                    layers.Add(layerName);
                    layerNumbers.Add(i);
                }
            }
    
            int maskWithoutEmpty = 0;
            for (int i = 0; i<layerNumbers.Count;
                i++) {
                if (((1 << layerNumbers[i]) & layerMask.value) > 0)
                    maskWithoutEmpty |= (1 << i);
            }
            maskWithoutEmpty = EditorGUILayout.MaskField( label, maskWithoutEmpty, layers.ToArray());
    
            int mask = 0;
            for (int i = 0; i<layerNumbers.Count;
                i++) {
                if ((maskWithoutEmpty & (1 << i)) > 0)
                    mask |= (1 << layerNumbers[i]);
            }
            layerMask.value = mask;
            return layerMask;
        }

        protected abstract void OnGui();
        protected abstract void OnGuiReadonly();
    }
}