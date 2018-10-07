using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public abstract class FPSEditorBaseWindow : EditorWindow
{
    private bool _isInited = false;
    
    private float _messageTimeout;
    private string _message;
    private MessageType _messageType;

    private void OnGUI()
    {
        if (!_isInited)
        {
            OnInit();
            _isInited = true;
        }
        
        OnGui();
        
        if (_messageTimeout >= 0)
        {
            _messageTimeout -= Time.deltaTime;
            EditorGUILayout.HelpBox(_message, _messageType);
        }
    }

    protected GameObject CreateField(string fieldName, GameObject gameObject)
    {
        return EditorGUILayout.ObjectField(fieldName, gameObject, typeof(GameObject), true) as GameObject;
    }

    protected void ShowMessage(string message, MessageType type)
    {
        _message = message;
        _messageType = type;
        _messageTimeout = 5;
    }

    protected void SetSelected(ref GameObject gameObject)
    {
        var o = Selection.activeObject as GameObject;
        if (o)
        {
            gameObject = o;
        }
    }

    protected GameObject CreateNewField(string buttonName, string objectName)
    {
        GameObject fieldGameObject = null;
        if (GUILayout.Button(buttonName))
        {
            fieldGameObject = new GameObject(objectName);
            Selection.SetActiveObjectWithContext(fieldGameObject, null);
        }

        return fieldGameObject;
    }

    protected void ShowDelim()
    {
        EditorGUILayout.LabelField("==============================================================", 
            EditorStyles.centeredGreyMiniLabel);
    }

    protected virtual void OnInit()
    {
        
    }

    protected abstract void OnGui();
}