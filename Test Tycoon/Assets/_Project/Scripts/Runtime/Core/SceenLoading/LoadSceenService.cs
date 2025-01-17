using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceenService : ILoadSceenService, IService
{
    private LoadingCurtain _loadingCurtain;
    private AsyncOperation _loadingAsyncOperation;

    private Sceens _sceenToLoad;

    public void Init()
    {
        _loadingCurtain = Resources.Load<LoadingCurtain>("LoadingCurtainCanvas");
        _loadingCurtain = Object.Instantiate(_loadingCurtain);

        Object.DontDestroyOnLoad(_loadingCurtain);
    }

    public void LoadSceen(Sceens sceen)
    {
        _sceenToLoad = sceen;
        _loadingCurtain.PlayActiveAnimation(Load);
    }

    private async void Load()
    {
        _loadingAsyncOperation = SceneManager.LoadSceneAsync(_sceenToLoad.ToString());

        while (_loadingAsyncOperation.isDone)
        {
            await Task.Yield();
        }

        _loadingCurtain.PlayDisactiveAnimation(null);
    }
}
