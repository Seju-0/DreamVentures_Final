using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FadeScreen : MonoBehaviour
{
    public Image fadeImage; // assign your panel's Image component
    public float fadeDuration = 1f;

    private void Start()
    {
        StartCoroutine(FadeIn());
    }

    private IEnumerator FadeIn()
    {
        float t = fadeDuration;
        Color color = fadeImage.color;

        while (t > 0f)
        {
            t -= Time.deltaTime;
            float alpha = Mathf.Clamp01(t / fadeDuration);
            fadeImage.color = new Color(color.r, color.g, color.b, alpha);
            yield return null;
        }

        // Ensure it's fully transparent
        fadeImage.color = new Color(color.r, color.g, color.b, 0f);
        fadeImage.raycastTarget = false; // 🔥 This allows clicks to pass through
    }

    public void FadeAndLoadScene(string sceneName)
    {
        StartCoroutine(FadeOutAndLoad(sceneName));
    }

    private IEnumerator FadeOutAndLoad(string sceneName)
    {
        yield return StartCoroutine(FadeOut());

        SceneManager.LoadScene(sceneName);
    }

    private IEnumerator FadeOut()
    {
        fadeImage.raycastTarget = true; // 🔒 block clicks during fade

        float t = 0f;
        Color color = fadeImage.color;

        while (t < fadeDuration)
        {
            t += Time.deltaTime;
            float alpha = Mathf.Clamp01(t / fadeDuration);
            fadeImage.color = new Color(color.r, color.g, color.b, alpha);
            yield return null;
        }

        // Ensure it's fully opaque
        fadeImage.color = new Color(color.r, color.g, color.b, 1f);
    }
}
