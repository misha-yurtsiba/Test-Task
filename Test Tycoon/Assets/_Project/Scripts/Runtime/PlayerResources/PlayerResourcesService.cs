using System.Collections.Generic;

public class PlayerResourcesService : IResourcesService
{
    private Dictionary<ResourceType, ResourceModel> _resources;

    public PlayerResourcesService()
    {
        _resources = new();
    }

    public void LoadResourses(List<ResourceData> resourceDataList)
    {
        if(resourceDataList != null)
        {
            foreach (ResourceData resource in resourceDataList)
            {
                _resources.Add(resource.resourceType, new ResourceModel(resource.resourceType, resource.amount));
            }
        }
        else
        {
            _resources.Add(ResourceType.Money, new ResourceModel(ResourceType.Money, 1000));
            _resources.Add(ResourceType.Gem, new ResourceModel(ResourceType.Gem, 50));
        }
    }

    public List<ResourceData> GetResourceDataList()
    {
        List<ResourceData> resourceDataList = new(_resources.Count);

        foreach(ResourceModel resource in _resources.Values)
            resourceDataList.Add(new ResourceData(resource.ResourceType, resource.Amount));

        return resourceDataList;
    }

    public ResourceModel GetResource(ResourceType resourceType)
    {
        return _resources[resourceType];
    }

    public void AddResource(ResourceType resourceType, int amount)
    {
        if (_resources.ContainsKey(resourceType))
            _resources[resourceType].Amount += amount;
    }

    public void SubtractResource(ResourceType resourceType, int amount)
    {
        if (_resources.ContainsKey(resourceType))
            _resources[resourceType].Amount -= amount;
    }
}


