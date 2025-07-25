using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FolderManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> Folder_Components;
    [SerializeField] private List<GameObject> Questions_Panel;
    public GameObject ReplayDialogue_Button;

    public void OpenFolder()
    {
        foreach (GameObject component in Folder_Components)
        {
            component.SetActive(true);
        }
        foreach (GameObject component in Questions_Panel)
        {
            component.SetActive(false);
        }
        ReplayDialogue_Button.SetActive(false); 
    }

    public void CloseBTN()
    {
        foreach (GameObject component in Folder_Components)
        {
            component.SetActive(false);
        }
        foreach (GameObject component in Questions_Panel)
        {
            component.SetActive(true);
        }
        ReplayDialogue_Button.SetActive(true);
    }
}

