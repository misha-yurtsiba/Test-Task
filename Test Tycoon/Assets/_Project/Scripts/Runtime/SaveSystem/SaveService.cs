public interface ISaveService : IService
{
    public void LoadGameData();
    public void SaveGameData();
}
public class SaveService : ISaveService
{
    private const string SAVE_KEY = "gameData";

    private ISaveStrategy _saveStrategy;
    private IBuildingService _buildingService;
    private IResourcesService _resourcesService;
    
    public SaveService()
    {
        _saveStrategy = new JsonSaveStrategy();
    }
    public void Init()
    {
        _buildingService = ServiceLocator.Current.GetService<IBuildingService>();
        _resourcesService = ServiceLocator.Current.GetService<IResourcesService>();
    }

    public void LoadGameData()
    {
        GameData gameData = _saveStrategy.Load<GameData>(SAVE_KEY);

        _resourcesService.LoadResourses(gameData.resourceDataList);
        _buildingService.LoadBuildings(gameData.buildingDataList);
    }

    public void SaveGameData()
    {
        GameData gameData = new();

        gameData.resourceDataList = _resourcesService.GetResourceDataList();
        gameData.buildingDataList = _buildingService.GetBuildingDataList();

        _saveStrategy.Save(gameData, SAVE_KEY);
    }

    public bool IsSaveExists() => _saveStrategy.IsFileExists(SAVE_KEY);
}
