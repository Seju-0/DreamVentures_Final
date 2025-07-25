using UnityEngine;
using TMPro;
using System.Collections;

public class DecisionText : MonoBehaviour
{
    public TextMeshProUGUI reputationText;
    public TextMeshProUGUI sanityText;

    private Client currentClient;

    public void SetClient(Client client)
    {
        currentClient = client;

        // Hide at start
        reputationText.text = "";
        sanityText.text = "";
        reputationText.gameObject.SetActive(false);
        sanityText.gameObject.SetActive(false);
    }

    public void ShowApproval()
    {
        if (currentClient != null)
        {
            reputationText.text = currentClient.approvalReputationText;
            sanityText.text = currentClient.approvalSanityText;
            StartCoroutine(AnimateText(reputationText));
            StartCoroutine(AnimateText(sanityText));
        }
    }

    public void ShowRejection()
    {
        if (currentClient != null)
        {
            reputationText.text = currentClient.rejectionReputationText;
            sanityText.text = currentClient.rejectionSanityText;
            StartCoroutine(AnimateText(reputationText));
            StartCoroutine(AnimateText(sanityText));
        }
    }

    private IEnumerator AnimateText(TextMeshProUGUI text)
    {
        text.gameObject.SetActive(true);

        float duration = 2f;
        float elapsed = 0f;

        Vector3 startPos = text.rectTransform.localPosition;
        Vector3 endPos = startPos + new Vector3(0, 50f, 0); // move up

        Color startColor = text.color;
        Color endColor = new Color(startColor.r, startColor.g, startColor.b, 0); // fade out

        while (elapsed < duration)
        {
            float t = elapsed / duration;
            text.rectTransform.localPosition = Vector3.Lerp(startPos, endPos, t);
            text.color = Color.Lerp(startColor, endColor, t);

            elapsed += Time.deltaTime;
            yield return null;
        }

        // Ensure fully faded and hidden
        text.rectTransform.localPosition = startPos; // reset position
        text.color = startColor;                     // reset color
        text.gameObject.SetActive(false);            // hide
    }
}
