using System;
using System.IO;
using UnityEngine;
using UnityEngine.Events;

namespace FPSDemo
{
    public enum SaveType
    {
        TXT,
        XML,
        JSON
    }

    public class Main : MonoBehaviour
    {
        public static UnityAction OnInitialize;
        public static bool _isInitialized = false;
        public static bool IsInitialized => _isInitialized;


        public static Main Instance { get; private set; }

        public InputController InputController { get; private set; }
        public FlashlightController FlashlightController { get; private set; }
        public WeaponsController WeaponsController { get; private set; }
        public TeammateController TeammateController { get; private set; }
        public PlayerController PlayerController { get; private set; }
        public EnemiesController EnemiesController { get; private set; }

        public SaveType SaveType;

        private Texture2D _screenshot;

        private int _uninitedControllersCounter;
        private string _screenshotDirectory;
        private string _savesDirectory;

        private BaseSaver _saver;

        private void Awake()
        {
            if (Instance)
                DestroyImmediate(this);
            else
            {
                Instance = this;
                _screenshotDirectory = CreateDirectory("Screenshots");
                _savesDirectory = CreateDirectory("Saves");
            }
        }

        private void Start()
        {
#if !UNITY_EDITOR
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
#endif

            // В будущем планируется ввести контроллер инвентаря
            // Количество патронов в каждом оружии будет храниться там
            // Состояние тиммейта не сохраняется. Скорее всего он будет в последствии удален как не нужный
            WeaponsController = FindObjectOfType<WeaponsController>();
            RegisterController<WeaponsController, WeaponsModel>(WeaponsController);
            TeammateController = FindObjectOfType<TeammateController>();
            RegisterController<TeammateController, TeammateModel>(TeammateController);
            PlayerController = FindObjectOfType<PlayerController>();
            RegisterController<PlayerController, PlayerModel>(PlayerController);
            EnemiesController = FindObjectOfType<EnemiesController>();
            RegisterController<EnemiesController, EnemiesModel>(EnemiesController);


            InputController = gameObject.AddComponent<InputController>();
            FlashlightController = gameObject.AddComponent<FlashlightController>();

            CheckInit();
        }

        #region Controllers

        private void RegisterController<C, M>(C controller)
            where C : BaseController<M>
            where M : BaseModel
        {
            if (!controller.IsInitialized)
            {
                _uninitedControllersCounter++;
                controller.OnControllerInitialize += OnControllerInitialize;
            }
        }

        private void OnControllerInitialize()
        {
            _uninitedControllersCounter--;
            CheckInit();
        }

        private void CheckInit()
        {
            if (_uninitedControllersCounter <= 0)
            {
                _isInitialized = true;
                OnInitialize?.Invoke();
            }
        }

        #endregion

        #region Screenshot

        public void TakeScreenShot()
        {
            _screenshot = ScreenCapture.CaptureScreenshotAsTexture();
            Invoke("SaveTextureToFile", 0);
        }

        #endregion

        #region Saves

        private void SetSaver()
        {
            switch (SaveType)
            {
                case SaveType.XML:
                    _saver = new XMLSaver();
                    break;
                case SaveType.JSON:
                    _saver = new JSONSaver();
                    break;
                case SaveType.TXT:
                default:
                    _saver = new TextSaver();
                    break;
            }
        }

        public void Save()
        {
            SetSaver();


            _saver.AddSavable(PlayerController);
            _saver.AddSavable(WeaponsController);
            _saver.AddSavable(EnemiesController);
            _saver.Save(_savesDirectory);
        }

        public void Load()
        {
            SetSaver();

            _saver.AddLoadable(PlayerController);
            _saver.AddLoadable(WeaponsController);
            _saver.AddLoadable(EnemiesController);
            _saver.Load(_savesDirectory);
        }

        private void SaveTextureToFile()
        {
            var filename = String.Format("{0:ddMMyyyy_HHmmssfff}.png", DateTime.Now);
            var bytes = _screenshot.EncodeToPNG();
            File.WriteAllBytes(Path.Combine(_screenshotDirectory, filename), bytes);
        }

        private string CreateDirectory(string name)
        {
            var prefsName = $"{name}Directory";
            var directory = PlayerPrefs.HasKey(prefsName)
                ? PlayerPrefs.GetString(prefsName)
                : Path.Combine(Application.dataPath, name);

            PlayerPrefs.SetString(prefsName, directory);
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            return directory;
        }

        #endregion
    }
}