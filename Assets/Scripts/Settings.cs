using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{
    private static string path;
    public Storage storage = new Storage();

    private void Awake()
    {
        Load();
    }

    private void Start()
    { 
        path = Path.Combine(Application.dataPath, "Settings");
        DontDestroyOnLoad(gameObject);
        foreach(GameObject go in Resources.FindObjectsOfTypeAll<GameObject>())
        {
            if(go.name == "Master Volume Slider")
            {
                go.GetComponent<Slider>().value = storage.masterVolume;
            }
        }
    }

    public void SetMasterVolume(float volume)
    {
        volume = Mathf.Clamp(volume, 0.0f, 1.0f);
        storage.masterVolume = volume;
        AudioListener.volume = volume;
    }

    public float GetMasterVolume()
    {
        return storage.masterVolume;
    }

    public void Load()
    {
        path = Path.Combine(Application.dataPath, "Settings");
        string json = "";
        if(!File.Exists(Path.Combine(path, "settings.json")))
        {
            Directory.CreateDirectory(path);
            File.Create(Path.Combine(path, "settings.json")).Dispose();
            File.SetAttributes(Path.Combine(path, "settings.json"), FileAttributes.Normal);
        }
        using (StreamReader sr = new StreamReader(Path.Combine(path, "settings.json")))
        {
            json = sr.ReadToEnd();
        }
        if(json.Equals(null))
        {
            return;
        }
        Storage s = JsonUtility.FromJson<Storage>(json);
        if(s != null)
        {
            storage = s;
        }
        //apply loaded stuff
        SetMasterVolume(storage.masterVolume);
    }

    public void Save()
    {
        string json = JsonUtility.ToJson(storage);
        using (StreamWriter sw = new StreamWriter(Path.Combine(path, "settings.json")))
        {
            sw.Write(json);
        }
    }

    [Serializable]
    public class Storage
    {
        public float masterVolume = 1.0f;
    }

    private void OnApplicationQuit()
    {
        Save();
    }
}
