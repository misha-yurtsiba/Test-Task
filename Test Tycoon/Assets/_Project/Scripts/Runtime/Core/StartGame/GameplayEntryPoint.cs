using System.Collections.Generic;
using UnityEngine;

public class GameplayEntryPoint : MonoBehaviour
{
    [SerializeField] private List<SpawnPoint> _spawnPoints;
    [SerializeField] private AutoSave _autoSave;

    [Space(5)]
    private PlayerResourcesService _playerResourcesService;
    private BuildingService _buildingService;
    private SaveService _saveService;

    private void Awake()
    {
        RegisterServices();
        
        InitServices();

        if (_saveService.IsSaveExists())
            _saveService.LoadGameData();
        else
            _playerResourcesService.LoadResourses(null);

        _autoSave.Init();
    }

    private void RegisterServices()
    {
        _playerResourcesService = new PlayerResourcesService();
        _buildingService = new BuildingService(_spawnPoints);
        _saveService = new SaveService();

        ServiceLocator.Current.Register<IResourcesService>(_playerResourcesService);
        ServiceLocator.Current.Register<IBuildingService>(_buildingService);
        ServiceLocator.Current.Register<ISaveService>(_saveService);
    }

    private void InitServices()
    {
        _buildingService.Init();
        _saveService.Init();
    }
}
