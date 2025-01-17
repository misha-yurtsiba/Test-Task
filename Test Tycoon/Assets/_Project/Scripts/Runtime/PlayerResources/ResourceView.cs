using UnityEngine;
using TMPro;

public class ResourceView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _resourceText;
    [SerializeField] private ResourceType _resourceType;

    private ResourceModel _resourceModel;

    public void Init()
    {
        _resourceModel = ServiceLocator.Current.GetService<IResourcesService>().GetResource(_resourceType);
        _resourceModel.OnValueChanget += UpdateUI;

        UpdateUI(_resourceModel.Amount);
    }

    private void UpdateUI(int amount)
    {
        _resourceText.text = amount.ToString();
    }

    private void OnDestroy() => _resourceModel.OnValueChanget -= UpdateUI;
}


