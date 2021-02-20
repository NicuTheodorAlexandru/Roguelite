using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    private Text goldText;
    private Text soulsText;
    public Container container = new Container();
    private string path;

    public class Container
    {
        public int gold = 0;
        public int souls = 0;
    }

    public int Souls
    {
        get { return container.souls; }
        set 
        {
            container.souls = value;
            soulsText.text = container.souls.ToString();
        }
    }

    public int Gold
    {
        get { return container.gold; }
        set 
        {
            container.gold = value;
            goldText.text = container.gold.ToString();
        }
    }

    private void Start()
    {
        path = Path.Combine(Application.dataPath, "Inventory");
        //container = new Container();
        goldText = GameObject.Find("Gold Text").GetComponent<Text>();
        soulsText = GameObject.Find("Souls Text").GetComponent<Text>();
        Load();
        Souls = Souls;
        Gold = Gold;
    }

    private void OnDestroy()
    {
        Save();
    }

    public void Save()
    {
        string json = JsonUtility.ToJson(container);
        using (StreamWriter sw = new StreamWriter(Path.Combine(path, "inventory.json")))
        {
            sw.Write(json);
        }
    }

    public void Load()
    {
        string json = "";
        if (!File.Exists(Path.Combine(path, "inventory.json")))
        {
            Directory.CreateDirectory(path);
            File.Create(Path.Combine(path, "inventory.json")).Dispose();
            File.SetAttributes(Path.Combine(path, "inventory.json"), FileAttributes.Normal);
        }
        using (StreamReader sr = new StreamReader(Path.Combine(path, "inventory.json")))
        {
            json = sr.ReadToEnd();
        }
        if (json.Equals(""))
        {
            return;
        }
        container = JsonUtility.FromJson<Container>(json);
    }
}
