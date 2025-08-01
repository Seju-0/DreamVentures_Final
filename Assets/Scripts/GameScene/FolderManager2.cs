using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FolderManager2 : MonoBehaviour
{
    [SerializeField] private List<GameObject> Folder_Components;
    [SerializeField] private List<GameObject> Newspaper_Components;
    [SerializeField] private List<GameObject> Questions_Panel;

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
        foreach (GameObject component in Newspaper_Components)
        {
            component.SetActive(false);
        }
    }

    public void OpenNewspaper()
    {
        foreach (GameObject component in Newspaper_Components)
        {
            component.SetActive(true);
        }
        foreach (GameObject component in Questions_Panel)
        {
            component.SetActive(false);
        }
        foreach (GameObject component in Folder_Components)
        {
            component.SetActive(false);
        }
    }

    public void CloseBTN()
    {
        foreach (GameObject component in Folder_Components)
        {
            component.SetActive(false);
        }
        foreach (GameObject component in Newspaper_Components)
        {
            component.SetActive(false);
        }
        foreach (GameObject component in Questions_Panel)
        {
            component.SetActive(true);
        }
    }
}

