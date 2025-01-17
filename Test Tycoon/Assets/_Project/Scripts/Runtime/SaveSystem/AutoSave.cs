using System.Collections;
using UnityEngine;

public class AutoSave : MonoBehaviour
{
    [SerializeField] private int _autoSaveInterval;

    private ISaveService _saveService;

    public void Init()
    {
        _saveService = ServiceLocator.Current.GetService<ISaveService>();

        StartCoroutine(AutoSaveCoroutine());
    }

    public void StopAutoSave() => StopCoroutine(AutoSaveCoroutine());

    private IEnumerator AutoSaveCoroutine()
    {
        while (true)
        {
            _saveService.SaveGameData();

            yield return new WaitForSeconds(_autoSaveInterval);
        }
    }
}