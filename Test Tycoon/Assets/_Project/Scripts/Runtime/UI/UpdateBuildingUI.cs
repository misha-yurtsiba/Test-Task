using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UpdateBuildingUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _levelText; 
    [SerializeField] private TextMeshProUGUI _updateCost; 
    [SerializeField] private TextMeshProUGUI _resourcePerSecond; 
    [SerializeField] private TextMeshProUGUI _buildingName;
    
    [SerializeField] private Button _updateButton;

    private Building _building;

    public void Init(Building building)
    {
        _building = building;

        _levelText.text = "Level " + _building.Level.ToString();
        _updateCost.text = _building.Cost.ToString();
        _resourcePerSecond.text = _building.ResourcePerSecond.ToString() + " / s";
        _buildingName.text = _building.BuildingName.ToString();

        _building.BuildingUpdated += UpdateUI;
        _updateButton.onClick.AddListener(() =>
        {
            ServiceLocator.Current.GetService<IBuildingService>().UpdateBuilding(building);
        });
    }

    private void UpdateUI(Building building)
    {
        _levelText.text = "Level " + building.Level.ToString();
        _updateCost.text = building.Cost.ToString();
        _resourcePerSecond.text = building.ResourcePerSecond.ToString() + " / s";
    }

    private void OnDestroy()
    {
        _building.BuildingUpdated -= UpdateUI;
    }
}


