using System;

public class ResourceModel
{
    public event Action<int> OnValueChanget;

    private int _amount;

    public ResourceModel(ResourceType resourceType, int startAmount)
    {
        ResourceType = resourceType;
        _amount = startAmount;
    }

    public ResourceType ResourceType { get; private set; }
    public int Amount
    {
        get => _amount;

        set
        {
            if (value < 0)
                _amount = 0;
            else
                _amount = value;

            OnValueChanget?.Invoke(_amount);
        }
    }
}


