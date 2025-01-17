using System;
using System.Collections.Generic;

public interface IBuildingService : IService
{
    public event Action<string> BuildingBougth;
    public void UpdateBuilding(Building building);
    public void BuyBuilding(string buildingName);
    public Building GetBuilding(string name);
    public List<BuildingData> GetBuildingDataList();
    public void LoadBuildings(List<BuildingData> buildingDataList);
}
