using System;
using System.Collections.Generic;

public interface IResourcesService : IService
{
    public void LoadResourses(List<ResourceData> resourceDataList);
    public List<ResourceData> GetResourceDataList();
    public ResourceModel GetResource(ResourceType resourceType);
    public void AddResource(ResourceType resourceType, int amount);
    public void SubtractResource(ResourceType resourceType, int amount);
}


