using System;
using UnityEngine;

public class Building : MonoBehaviour 
{
    public event Action<Building> BuildingUpdated;

    public int Level { get; private set; }
    public int Cost { get; private set; }
    public int ResourcePerSecond { get; private set; }
    public string BuildingName { get; private set; }

    public void Init(int level, int cost, int resourcePerSecond, string buildingName)
    {
        Level = level;
        Cost = cost;
        ResourcePerSecond = resourcePerSecond;
        BuildingName = buildingName;

    }

    public virtual void UpgradeBuilding()
    {
        Level++;
        ResourcePerSecond += (int)(ResourcePerSecond * 0.1f);
        Cost += (int)(Cost * 0.1f);

        BuildingUpdated?.Invoke(this);
    }
}

