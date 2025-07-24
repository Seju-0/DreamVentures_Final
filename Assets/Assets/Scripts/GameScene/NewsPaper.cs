using NUnit.Framework;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NewsPaper : MonoBehaviour
{
    public TextMeshProUGUI headlinesText;
    [SerializeField] private List<GameObject> Newspaper;

    void Start()
    {
        var todaysHeadlines = ChoiceResults.GetDecisionsForPreviousDay();

        if (todaysHeadlines.Count == 0)
        {
            headlinesText.text = "No news today.";
            return;
        }

        StringBuilder builder = new StringBuilder();

        bool anyHeadline = false;

        // MAIN HEADLINES header
        builder.AppendLine("<size=36><b>MAIN HEADLINES</b></size>");
        builder.AppendLine();

        // Loop through each decision
        for (int i = 0; i < todaysHeadlines.Count; i++)
        {
            var d = todaysHeadlines[i];

            // Skip clients with no headlines
            if (string.IsNullOrWhiteSpace(d.mainHeadline)
                && string.IsNullOrWhiteSpace(d.subHeadline)
                && string.IsNullOrWhiteSpace(d.obituary))
            {
                continue;
            }

            anyHeadline = true;

            // Main Headline
            if (!string.IsNullOrWhiteSpace(d.mainHeadline))
            {
                builder.AppendLine($"<b>{d.mainHeadline}</b>");
            }

            // Subheadline (smaller size)
            if (!string.IsNullOrWhiteSpace(d.subHeadline))
            {
                builder.AppendLine($"<size=15><i>{d.subHeadline}</i></size>");
            }

            // Obituary
            if (!string.IsNullOrWhiteSpace(d.obituary))
            {
                builder.AppendLine();
                builder.AppendLine($"<color=#888888>Obituary:</color>");
                builder.AppendLine(d.obituary);
            }

            // Separator between articles
            builder.AppendLine();
            builder.AppendLine("<color=#AAAAAA>-----------------------------------------</color>");
            builder.AppendLine();
        }

        if (!anyHeadline)
        {
            headlinesText.text = "No news today.";
        }
        else
        {
            headlinesText.text = builder.ToString();
        }
    }

    public void OpenNewspaper()
    {
        if (Newspaper != null && Newspaper.Count > 0)
        {
            foreach (var paper in Newspaper)
            {
                paper.SetActive(true);
            }
        }
    }

    public void CloseNewspaper()
    {
        if (Newspaper != null && Newspaper.Count > 0)
        {
            foreach (var paper in Newspaper)
                paper.SetActive(false);
        }
    }
}
