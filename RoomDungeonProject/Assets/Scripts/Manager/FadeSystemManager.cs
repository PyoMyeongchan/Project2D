using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class FadeSystemManager : MonoBehaviour
{
    public static FadeSystemManager instance;

    public Image fadeImage;
    public float fadeDuration = 0.5f;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public IEnumerator FadeOut()
    {
        yield return StartCoroutine(Fade(0f, 1f));
    }

    public IEnumerator FadeIn()
    {
        yield return StartCoroutine(Fade(1f, 0f));
    }

    IEnumerator Fade(float from, float to)
    {
        float elapsed = 0f;
        Color color = fadeImage.color;

        while (elapsed < fadeDuration)
        {
            float alpha = Mathf.Lerp(from, to, elapsed / fadeDuration);
            color.a = alpha;
            fadeImage.color = color;

            elapsed += Time.deltaTime;
            yield return null;
        }

        color.a = to;
        fadeImage.color = color;
    }
}


