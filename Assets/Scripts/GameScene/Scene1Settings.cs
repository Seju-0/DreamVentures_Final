using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scene1Settings : MonoBehaviour
{
    [SerializeField] private List<GameObject> SettingsPanel;

    public void OpenSettings()
    {
        foreach(GameObject component in SettingsPanel)
        {
            component.SetActive(true);
        }
    }

    public void CloseBTN()
    {
        foreach (GameObject component in SettingsPanel)
        {
            component.SetActive(false);
        }
    }
}
