using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine.UI;

namespace FPSDemo
{
    public class MainMenuTab : BaseTab
    {
        public UnityAction OnResumeClick;
        public UnityAction OnSettingsClick;
        public UnityAction OnExitClick;
        
        private Button _resume;
        private Button _settings;
        private Button _exit;

        public override void Init()
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                var child = transform.GetChild(i);
                switch (child.name)
                {
                    case "ResumeButton":
                        _resume = child.GetComponent<Button>();
                        _resume.onClick.AddListener(OnResumeClick);
                        break;
                    case "SettingsButton":
                        _settings = child.GetComponent<Button>();
                        _settings.onClick.AddListener(OnSettingsClick);
                        break;
                    case "ExitButton":
                        _exit = child.GetComponent<Button>();
                        _exit.onClick.AddListener(OnExitClick);
                        break;
                }
            }
        }
    }
}