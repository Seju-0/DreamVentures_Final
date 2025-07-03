using System.Text;
using TMPro;
using UnityEngine;

public class NewsPaper : MonoBehaviour
{
    public TextMeshProUGUI headlinesText;

    void Start()
    {
        if (ChoiceResults.decisions.Count == 0)
        {
            headlinesText.text = "No news today.";
            return;
        }

        StringBuilder builder = new StringBuilder();

        for (int i = 0; i < ChoiceResults.decisions.Count; i++)
        {
            var d = ChoiceResults.decisions[i];

            builder.AppendLine($"<b>Headline {i + 1}:</b>");
            builder.AppendLine(d.headline);
            builder.AppendLine();
        }

        headlinesText.text = builder.ToString();
    }
}
