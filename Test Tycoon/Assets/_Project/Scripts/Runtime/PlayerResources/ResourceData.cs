using System;

[Serializable]
public class ResourceData
{
    public ResourceType resourceType;
    public int amount;

    public ResourceData(ResourceType resourceType, int amount)
    {
        this.resourceType = resourceType;
        this.amount = amount;
    }
}


