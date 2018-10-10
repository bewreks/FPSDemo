using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace FPSDemo
{
    // TODO: сделать миникарту, которая по нескольким этажам в автоматическом режиме работает
    public class Minimap : MonoBehaviour
    {
        public UnityAction OnMinimapInit;
        
        public Camera _camera;
        public GameObject _quad;
        public LayerMask MapObjectsCullingMask;
        public LayerMask MinimapCullingMask;
        public RenderTexture _minimapRT;
        public Light _light;

        private bool _screenshotTaked = false;
        
        private RenderTexture _mainMiniCameraBG;

        public bool IsInited => _screenshotTaked;

        private void Awake()
        {
            _mainMiniCameraBG = new RenderTexture(Screen.width, Screen.height, 1);
            _light.enabled = true;
            _quad.SetActive(true);


            _camera.cullingMask = MapObjectsCullingMask;
            _light.cullingMask = MapObjectsCullingMask; 
            
            _camera.targetTexture = _mainMiniCameraBG;

            StartCoroutine(TakeScreenshot());
        }
        
        private void Update()
        {
            if (!_screenshotTaked)
            {
                return;
            }
        }

        private IEnumerator TakeScreenshot()
        {
            yield return new WaitForEndOfFrame();
            _camera.cullingMask = MinimapCullingMask;
            _light.cullingMask = MinimapCullingMask;
            _camera.targetTexture = _minimapRT;
            _camera.orthographicSize = 5f;

            var material = _quad.GetComponent<Renderer>().material;
            material.mainTexture = _mainMiniCameraBG;
            
            
            _screenshotTaked = true;
            OnMinimapInit?.Invoke();
        }
    }
}
