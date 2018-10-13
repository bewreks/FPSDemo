using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FPSDemo
{
    public enum Tabs
    {
        MAIN,
        SETTINGS
    }
    public class PauseMainMenuScript : MonoBehaviour
    {

        private Tabs _tab;

        private MainMenuTab _mainTab;
        private SettingsTab _settingsTab;

        public void Init()
        {
            _mainTab = GetComponentInChildren<MainMenuTab>(true);
            _mainTab.OnExitClick = OnExitClick;
            _mainTab.OnResumeClick = OnResumeClick;
            _mainTab.OnSettingsClick = OnSettingsClick;
            _mainTab.Init();
            
            _settingsTab = GetComponentInChildren<SettingsTab>(true);
            _settingsTab.OnBack = OnBack;
            _settingsTab.Init();
            
            Hide();
        }

        private void OnBack()
        {
            _tab = Tabs.MAIN;
            Show();
        }

        private void OnResumeClick()
        {
            PauseCommand.GetCommand<PauseCommand>().Execute();
        }

        private void OnExitClick()
        {
            ExitCommand.GetCommand<ExitCommand>().Execute();
        }

        private void OnSettingsClick()
        {
            _tab = Tabs.SETTINGS;
            Show();
        }

        public void Hide()
        {
            _tab = Tabs.MAIN;
            gameObject.SetActive(false);
        }

        public void Show()
        {
            gameObject.SetActive(true);
            HideTabs();
            switch (_tab)
            {
                case Tabs.MAIN:
                    _mainTab.Show();
                    break;
                case Tabs.SETTINGS:
                    _settingsTab.Show();
                    break;
            }
        }

        private void HideTabs()
        {
            _mainTab.Hide();
            _settingsTab.Hide();
        }
    }
}