using System;
using System.Collections;
using UnityEngine;
using TMPro;

public class LoadingCurtain : MonoBehaviour
{
    [SerializeField] private CanvasGroup _curtainImage;
    [SerializeField] private TextMeshProUGUI _loadingText;

    [SerializeField] private float _curtainAnimationTime;

    public void PlayActiveAnimation(Action callback)
    {
        StartCoroutine(FadeCoroutine(1, callback));
        _loadingText.gameObject.SetActive(true);
    }

    public void PlayDisactiveAnimation(Action callback)
    {
        StartCoroutine(FadeCoroutine(0, callback));
        _loadingText.gameObject.SetActive(false);
    }


    private IEnumerator FadeCoroutine(float newAlpha, Action callback)
    {
        float timer = 0f;
        float startAlpha = _curtainImage.alpha;

        while (timer < _curtainAnimationTime)
        {
            _curtainImage.alpha = Mathf.Lerp(startAlpha, newAlpha, timer / _curtainAnimationTime);

            timer += Time.deltaTime;

            yield return null;
        }

        callback?.Invoke();
    }
}
