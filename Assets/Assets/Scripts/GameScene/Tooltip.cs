using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonLocalTooltip : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [TextArea]
    public string tooltipMessage;

    private TextMeshProUGUI tooltipText;

    private void Awake()
    {
        tooltipText = GetComponentInChildren<TextMeshProUGUI>(true);
        if (tooltipText != null)
        {
            tooltipText.gameObject.SetActive(false);
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (tooltipText != null)
        {
            tooltipText.text = tooltipMessage;
            tooltipText.gameObject.SetActive(true);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (tooltipText != null)
        {
            tooltipText.text = "";
            tooltipText.gameObject.SetActive(false);
        }
    }
}
