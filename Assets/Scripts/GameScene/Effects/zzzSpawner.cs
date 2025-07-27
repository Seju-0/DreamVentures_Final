using UnityEngine;
using System.Collections;
using TMPro;

public class ZzzSpawner : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI zPrefab;
    [SerializeField] private Transform spawnParent; // Optional: UI parent to keep hierarchy tidy

    public void SpawnZs()
    {
        // First Z
        var z1 = Instantiate(zPrefab, spawnParent);
        z1.rectTransform.anchoredPosition = new Vector2(0, 0); // Starting position
        StartCoroutine(AnimateZ(z1, Vector2.zero));

        // Second Z
        var z2 = Instantiate(zPrefab, spawnParent);
        z2.rectTransform.anchoredPosition = new Vector2(20, 0); // Offset slightly
        StartCoroutine(AnimateZ(z2, new Vector2(20f, 10f))); // Slight curve

        // Third Z
        var z3 = Instantiate(zPrefab, spawnParent);
        z3.rectTransform.anchoredPosition = new Vector2(40, 0); // More offset
        StartCoroutine(AnimateZ(z3, new Vector2(40f, 20f))); // More dramatic curve
    }

    private IEnumerator AnimateZ(TextMeshProUGUI zText, Vector2 curveOffset)
    {
        float duration = 2f;
        float time = 0f;

        Vector3 startScale = Vector3.one;
        Vector3 endScale = Vector3.one * 2f;

        Color startColor = zText.color;
        Color endColor = new Color(startColor.r, startColor.g, startColor.b, 0f);

        RectTransform rect = zText.rectTransform;
        Vector2 p0 = rect.anchoredPosition;
        Vector2 p2 = p0 + new Vector2(100f, 100f);
        Vector2 p1 = p0 + new Vector2(50f, 50f) + curveOffset;

        while (time < duration)
        {
            float t = time / duration;

            Vector2 curvedPos = Mathf.Pow(1 - t, 2) * p0 +
                                2 * (1 - t) * t * p1 +
                                Mathf.Pow(t, 2) * p2;

            rect.anchoredPosition = curvedPos;
            rect.localScale = Vector3.Lerp(startScale, endScale, t);
            zText.color = Color.Lerp(startColor, endColor, t);

            time += Time.deltaTime;
            yield return null;
        }

        Destroy(zText.gameObject);
    }
}
