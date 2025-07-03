using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HintsManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    [SerializeField] private List<GameObject> hints;

    public void OpenHints()
    {
        foreach (GameObject component in hints)
        {
            component.SetActive(true);
        }
    }
    public void CloseButton()
    {
        foreach (GameObject component in hints)
        {
            component.SetActive(false);
        }
    }
}
