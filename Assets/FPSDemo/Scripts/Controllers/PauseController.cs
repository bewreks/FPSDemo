using UnityEngine;
using UnityEngine.Events;

namespace FPSDemo
{
    public class PauseController : MonoBehaviour
    {
        public UnityAction OnPause;
        public UnityAction OnResume;
        
        private bool _isPaused;
        private PauseMainMenuScript _pauseMainMenu;

        public bool IsPaused => _isPaused;

        public void Init()
        {
            _pauseMainMenu = FindObjectOfType<PauseMainMenuScript>();
            _isPaused = false;
            _pauseMainMenu.Init();
            HideCursor();
        }
        
        public void PauseGame()
        {
            if (_isPaused)
            {
                return;
            }

            OnPauseMethod();
        }

        public void StartGame()
        {
            if (!_isPaused)
            {
                return;
            }

            OnResumeMethod();
        }

        public void SwitchPause()
        {
            if (_isPaused)
            {
                StartGame();
            }
            else
            {
                PauseGame();
            }
        }

        private void OnResumeMethod()
        {
            _isPaused = false;
            _pauseMainMenu.Hide();
            Time.timeScale = 1;
            HideCursor();
            OnResume?.Invoke();
        }

        private void OnPauseMethod()
        {
            _isPaused = true;
            _pauseMainMenu.Show();
            Time.timeScale = 0;
            ShowCursor();
            OnPause?.Invoke();
        }

        private void HideCursor()
        {
#if !UNITY_EDITOR
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
#endif
        }

        private void ShowCursor()
        {
#if !UNITY_EDITOR
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
#endif
        }
    }
}