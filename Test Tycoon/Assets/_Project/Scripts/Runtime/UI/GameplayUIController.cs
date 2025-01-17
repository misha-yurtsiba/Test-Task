using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class GameplayUIController : MonoBehaviour
{
    [SerializeField] private List<ResourceView> _resourceViewList;
    [SerializeField] private List<BuyBuildingUI> _buyBuildingUIList;

    [SerializeField] private UpdateBuildingUI _prefab;
    [SerializeField] private Canvas _buildingCanvas;

    private IBuildingService _buildingService;

    private void Start()
    {
        _resourceViewList.ForEach(r => r.Init());
        _buyBuildingUIList.ForEach(b => b.Init());

        _buildingService = ServiceLocator.Current.GetService<IBuildingService>();
        _buildingService.BuildingBougth += SpawnUpdateUI;

        SpawnUI();
    }

    private void SpawnUI()
    {
        List<BuildingData> buildingDatas = _buildingService.GetBuildingDataList();

        for (int i = 0; i < buildingDatas.Count; i++)
        {
            BuyBuildingUI buyBuildingUI = _buyBuildingUIList.FirstOrDefault(b => buildingDatas[i].buildingName == b.buildingConfig.BuildingName);

            if (buyBuildingUI != null)
            {
                CreateUpdateUI(buyBuildingUI);
            }
        }
    }

    public void CreateUpdateUI(BuyBuildingUI buyBuildingUI)
    {
        UpdateBuildingUI updateBuildingUI = Instantiate(_prefab);

        updateBuildingUI.transform.position = buyBuildingUI.transform.position;
        updateBuildingUI.transform.SetParent(_buildingCanvas.transform);
        updateBuildingUI.Init(_buildingService.GetBuilding(buyBuildingUI.buildingConfig.BuildingName));

        Destroy(buyBuildingUI.gameObject);
    }

    public void SpawnUpdateUI(string name)
    {
        BuyBuildingUI buyBuildingUI = _buyBuildingUIList.FirstOrDefault(b => name == b.buildingConfig.BuildingName);

        CreateUpdateUI(buyBuildingUI);
    }

    private void OnDestroy()
    {
        _buildingService.BuildingBougth -= SpawnUpdateUI;
    }
}


