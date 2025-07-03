using UnityEngine;
using TMPro;
using System.Text;

public class DreamResults : MonoBehaviour
{
    public TextMeshProUGUI combinedDreamsText;
    public TextMeshProUGUI totalSanityText;

    void Start()
    {
        if (ChoiceResults.decisions.Count == 0)
        {
            if (combinedDreamsText != null)
                combinedDreamsText.text = "You have no dreams tonight.";
            if (totalSanityText != null)
                totalSanityText.text = $"Total Sanity: {ChoiceResults.currentSanity}";
            return;
        }

        StringBuilder builder = new StringBuilder();

        for (int i = 0; i < ChoiceResults.decisions.Count; i++)
        {
            var d = ChoiceResults.decisions[i];

            builder.AppendLine($"<b>Dream {i + 1}:</b>");
            builder.AppendLine(d.dreamDialogueText);
            builder.AppendLine($"Sanity {(d.sanityValue >= 0 ? "+" : "")}{d.sanityValue}");
            builder.AppendLine();
        }

        if (combinedDreamsText != null)
            combinedDreamsText.text = builder.ToString();

        if (totalSanityText != null)
            totalSanityText.text = $"Total Sanity: {ChoiceResults.currentSanity}";
    }
}
