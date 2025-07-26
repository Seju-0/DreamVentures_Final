using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using UnityEngine.UI;

public class RejectButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [Header("Hover Label")]
    public TextMeshProUGUI unavailableText;

    private Button button;

    void Awake()
    {
        button = GetComponent<Button>();
        if (unavailableText != null)
            unavailableText.gameObject.SetActive(false);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (button != null && !button.interactable)
        {
            if (unavailableText != null)
                unavailableText.gameObject.SetActive(true);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (unavailableText != null)
            unavailableText.gameObject.SetActive(false);
    }
}
