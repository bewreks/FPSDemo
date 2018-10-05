using FPSDemo;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(EnemyModel))]
public class FPSAIEnemyModelEditor : Editor
{
    private bool _isFoldoutArmor;

    public override void OnInspectorGUI()
    {
        var model = (EnemyModel) target;

        model.Behaviour = (EnemyBehaviour) EditorGUILayout.EnumPopup("Behaviour", model.Behaviour);
        switch (model.Behaviour)
        {
            case EnemyBehaviour.CHASING:
                model.TrackingObject = (Transform) EditorGUILayout.ObjectField("    Chasing target", model.TrackingObject, typeof(Transform), true);
                break;
            case EnemyBehaviour.TRACKING:
                model.TrackingObject = (Transform) EditorGUILayout.ObjectField("    Tracking target", model.TrackingObject, typeof(Transform), true);
                break;
            case EnemyBehaviour.RANDOM_PATROL:
                model.MaxRandomSphereSize = EditorGUILayout.FloatField("    Sphere size", model.MaxRandomSphereSize);
                break;
        }
        model.BaseHp = EditorGUILayout.FloatField("Base HP", model.BaseHp);
        _isFoldoutArmor = EditorGUILayout.Foldout(_isFoldoutArmor, "Armor");
        if (_isFoldoutArmor)
        {
            model.Armor.ArmorType = EditorGUILayout.IntSlider("    Armor Type", model.Armor.ArmorType, 1, 4);
            model.Armor.BaseArmor = EditorGUILayout.FloatField("    Base Armor", model.Armor.BaseArmor);
        }

        model.AttackTime = EditorGUILayout.FloatField("AttackTime", model.AttackTime);
    }
}