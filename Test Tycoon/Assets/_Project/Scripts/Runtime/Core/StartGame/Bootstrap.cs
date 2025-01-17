using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bootstrap : MonoBehaviour
{
    private void Start()
    {
        Application.targetFrameRate = 60;

        ServiceLocator.Init();

        RegisterServices();

        ServiceLocator.Current.GetService<ILoadSceenService>().LoadSceen(Sceens.Gameplay);
    }

    private void RegisterServices()
    {
        LoadSceenService loadSceenService = new();
        
        loadSceenService.Init();

        ServiceLocator.Current.Register<ILoadSceenService>(loadSceenService);
    }
}
