using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class BuyBuildingUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _costText;
    [SerializeField] private Button _buyButton;

    public BuildingConfig buildingConfig;
    public void Init()
    {
        _costText.text = buildingConfig.Cost.ToString();

        _buyButton.onClick.AddListener(() =>
        {
            ServiceLocator.Current.GetService<IBuildingService>().BuyBuilding(buildingConfig.BuildingName);
        });

    }
}
