using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class sly114_StartController : MonoBehaviour
{
    public CanvasGroup fadeCanvas;
    public CanvasGroup buttonCanvas;

    public float fadeTime = 2.0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        fadeCanvas.alpha = 1;
        buttonCanvas.alpha = 1;
    }

    public void pressStart()
    {
        StartCoroutine(FadeIn());
    }

    IEnumerator FadeIn()
    {
        float elapsedTime = 0f;

        while (elapsedTime < fadeTime)
        {
            elapsedTime += Time.deltaTime;
            float alphaValue = 1 - (elapsedTime / fadeTime);

            fadeCanvas.alpha = alphaValue;
            buttonCanvas.alpha = alphaValue;
            yield return null;
        }

        fadeCanvas.alpha = 0;
        buttonCanvas.alpha = 0; // 버튼도 완전히 사라짐
        buttonCanvas.interactable = false; // 버튼 클릭 비활성화
        buttonCanvas.blocksRaycasts = false; // 버튼 터치/클릭 막기
    }

}
