using System.Collections;
using System.Collections.Generic;
using FPSDemo;
using UnityEngine;

public class Radar : MonoBehaviour
{
    public static Radar Instance { get; private set; }

    public float MapScale = 2f;
    
    private List<RadarObject> _objects = new List<RadarObject>();

    private void Awake()
    {
        if (Instance)
        {
            DestroyImmediate(this);
        }
        else
        {
            Instance = this;
        }
    }

    public void Register(RadarObject radarObject)
    {
        radarObject.Image.transform.SetParent(transform);
        _objects.Add(radarObject);
    }

    public void Remove(RadarObject radarObject)
    {
        if (radarObject.Image)
        {
            Destroy(radarObject.Image);
        }
        _objects.Remove(radarObject);
    }

    private void Draw()
    {
        var playerPosition = Main.Instance.PlayerController.transform.position;
        var playerEuler = Main.Instance.PlayerController.transform.eulerAngles;
        foreach (var radarObject in _objects)
        {
            var radarPos = radarObject.transform.position - playerPosition;
            var distToObject = Vector3.Distance(playerPosition, radarObject.transform.position) * MapScale;
            var deltay = Mathf.Atan2(radarPos.x, radarPos.z) * Mathf.Rad2Deg - 270 - playerEuler.y;
            radarPos.x = distToObject * Mathf.Cos(deltay * Mathf.Deg2Rad) * -1;
            radarPos.z = distToObject * Mathf.Sin(deltay * Mathf.Deg2Rad);
            
            (radarObject.Image.transform as RectTransform).anchoredPosition = new Vector3(radarPos.x, radarPos.z, 0);
        }
    }

    private void Update()
    {
        if (Time.frameCount % 3 == 0)
        {
            Draw();
        }
    }
}