using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[RequireComponent(typeof(Image))]
public class GlowPulse : MonoBehaviour
{
    [Header("Pulse Settings")]
    public float pulseSpeed = 2f;
    [Range(0f, 1f)] public float minAlpha = 0.2f;
    [Range(0f, 1f)] public float maxAlpha = 1f;

    private Image glowImage;
    private Coroutine pulseCoroutine;
    private Color baseColor;

    void Awake()
    {
        glowImage = GetComponent<Image>();
        baseColor = glowImage.color;
    }

    void OnEnable()
    {
        pulseCoroutine = StartCoroutine(PulseGlow());
    }

    void OnDisable()
    {
        if (pulseCoroutine != null)
            StopCoroutine(pulseCoroutine);
    }

    private IEnumerator PulseGlow()
    {
        while (true)
        {
            float t = 0f;

            // Fade In
            while (t < 1f)
            {
                if (!gameObject.activeInHierarchy) yield break;
                t += Time.deltaTime * pulseSpeed;
                SetAlpha(Mathf.Lerp(minAlpha, maxAlpha, t));
                yield return null;
            }

            t = 0f;

            // Fade Out
            while (t < 1f)
            {
                if (!gameObject.activeInHierarchy) yield break;
                t += Time.deltaTime * pulseSpeed;
                SetAlpha(Mathf.Lerp(maxAlpha, minAlpha, t));
                yield return null;
            }
        }
    }

    private void SetAlpha(float alpha)
    {
        glowImage.color = new Color(baseColor.r, baseColor.g, baseColor.b, alpha);
    }

    // Optional: Call this method from your button's OnClick() to stop the glow manually
    public void StopGlow()
    {
        if (pulseCoroutine != null)
        {
            StopCoroutine(pulseCoroutine);
            SetAlpha(0f);
        }
    }
}
