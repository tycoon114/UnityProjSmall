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
        buttonCanvas.alpha = 0; // ��ư�� ������ �����
        buttonCanvas.interactable = false; // ��ư Ŭ�� ��Ȱ��ȭ
        buttonCanvas.blocksRaycasts = false; // ��ư ��ġ/Ŭ�� ����
    }

}
