using System;
using UnityEngine;
using System.IO;

public class JsonSaveStrategy : ISaveStrategy
{
    private string _path;

    public T Load<T>(string key)
    {
        _path = Combine(key);

        if (!File.Exists(_path))
        {
            Debug.LogError($"{_path} save does not exists");
            throw new InvalidOperationException();
        }

        string json = File.ReadAllText(_path);

        T data = JsonUtility.FromJson<T>(json);

        return data;
    }

    public void Save<T>(T data, string key)
    {
        _path = Combine(key);

        string json = JsonUtility.ToJson(data);

        File.WriteAllText(_path, json);

        Debug.Log(_path);
    }

    public bool IsFileExists(string key) => File.Exists(Combine(key));

    private string Combine(string key) => Path.Combine(Application.persistentDataPath, key + ".json");
}