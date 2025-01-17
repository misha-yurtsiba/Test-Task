using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;
using System.Collections;

public class BuildingService : IBuildingService
{
    private readonly List<BuildingConfig> _buildingConfigs;
    private readonly List<SpawnPoint> _spawnPointList;
    private readonly List<Building> _buildingList = new();

    public event Action<string> BuildingBougth;

    private IResourcesService _resourcesService;

    private BuildingFactoty _buildingFactoty;

    public BuildingService(List<SpawnPoint> spawnPointList)
    {
        _buildingFactoty = new();

        _spawnPointList = spawnPointList;

        _buildingConfigs = Resources.LoadAll<BuildingConfig>("BuildingConfigs").ToList();
    }

    public void Init()
    {
        _resourcesService = ServiceLocator.Current.GetService<IResourcesService>();

        BuildingProfit buildingProfit = new GameObject().AddComponent<BuildingProfit>();
        buildingProfit.Init(_buildingList);
    }
    public void BuyBuilding(string buildingName)
    {
        BuildingConfig buildingConfig = GetBuildingConfigByName(buildingName);

        int price = _resourcesService.GetResource(ResourceType.Money).Amount;

        if (price < buildingConfig.Cost)
            return;

        _resourcesService.SubtractResource(ResourceType.Money, buildingConfig.Cost);

        Building building = _buildingFactoty.Create(buildingConfig);
        SpawnPoint spawnPoint = _spawnPointList.FirstOrDefault(s => s.BuidingName == buildingName);

        building.transform.position = spawnPoint.transform.position;
        _buildingList.Add(building);
        BuildingBougth?.Invoke(buildingName);
    }

    public void UpdateBuilding(Building building)
    {

        int price = _resourcesService.GetResource(ResourceType.Money).Amount;

        if (price < building.Cost)
            return;

        _resourcesService.SubtractResource(ResourceType.Money, building.Cost);

        building.UpgradeBuilding();

    }


    public List<BuildingData> GetBuildingDataList()
    {
        List<BuildingData> buildingDataList = new List<BuildingData>(_buildingList.Count);

        foreach(Building building in _buildingList)
        {
            BuildingData buildingData = new BuildingData();

            buildingData.cost = building.Cost;
            buildingData.level = building.Level;
            buildingData.buildingName = building.BuildingName;
            buildingData.resourcePerSecond = building.ResourcePerSecond;

            buildingDataList.Add(buildingData);
        }

        return buildingDataList;
    }

    public void LoadBuildings(List<BuildingData> buildingDataList)
    {
        foreach (BuildingData building in buildingDataList)
            LoadOneBuilding(building);
    }

    public Building GetBuilding(string name) => _buildingList.FirstOrDefault(b => b.BuildingName == name);

    private void LoadOneBuilding(BuildingData buildingData)
    {
        BuildingConfig buildingConfig = GetBuildingConfigByName(buildingData.buildingName);
        SpawnPoint spawnPoint = _spawnPointList.FirstOrDefault(s => s.BuidingName == buildingData.buildingName);
        Building building = _buildingFactoty.Create(buildingConfig);

        building.Init(buildingData.level,buildingData.cost,buildingData.resourcePerSecond,buildingData.buildingName);
        building.transform.position = spawnPoint.transform.position;

        _buildingList.Add(building);

    }

    private BuildingConfig GetBuildingConfigByName(string name)
    {
         return _buildingConfigs.FirstOrDefault(b => b.BuildingName == name);
    }
}

public class BuildingProfit : MonoBehaviour
{
    private List<Building> _buildingList;
    private IResourcesService _resourcesService;



    public void Init(List<Building> buildingList)
    {
        _buildingList = buildingList;
        _resourcesService = ServiceLocator.Current.GetService<IResourcesService>(); 

        StartCoroutine(CountProfitEverySecond());
    }

    private IEnumerator CountProfitEverySecond()
    {
        while (true)
        {
            int totalProfit = 0;

            foreach (var building in _buildingList)
            {
                totalProfit += building.ResourcePerSecond;
            }

            _resourcesService.AddResource(ResourceType.Money, totalProfit);

            yield return new WaitForSeconds(1f);
        }
    }
}
