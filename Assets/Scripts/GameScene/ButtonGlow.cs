using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonGlow : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject glowObject; 

    public void OnPointerEnter(PointerEventData eventData)
    {
            glowObject.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
            glowObject.SetActive(false);
    }
}
