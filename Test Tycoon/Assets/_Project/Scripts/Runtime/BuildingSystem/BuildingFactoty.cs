public class BuildingFactoty
{
    public Building Create(BuildingConfig buildingConfig)
    {
        Building building = UnityEngine.Object.Instantiate(buildingConfig.BuildingPrefab).GetComponent<Building>();

        building.Init(1, buildingConfig.Cost, buildingConfig.ResourcePerSecond, buildingConfig.BuildingName);

        return building;
    }
}
