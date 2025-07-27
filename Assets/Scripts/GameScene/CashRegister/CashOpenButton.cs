using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class CashOpenButton : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private List<GameObject> cashRegisterObjects; 
    [SerializeField] private GameObject wrongAmountText;

    public void OpenCashRegister()
    {
        foreach (GameObject obj in cashRegisterObjects)
        {
            // Reset state before showing
            CashRegister register = obj.GetComponent<CashRegister>();
            if (register != null)
                register.ResetState();

            obj.SetActive(true);
        }

        if (wrongAmountText != null)
            wrongAmountText.SetActive(false);
    }

    public void CloseBTN()
    {
        foreach (GameObject obj in cashRegisterObjects)
        {
            obj.SetActive(false);
        }
        if (wrongAmountText != null)
            wrongAmountText.SetActive(false);
    }
}
