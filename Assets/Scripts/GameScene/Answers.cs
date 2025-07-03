using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Answers : MonoBehaviour
{
    [SerializeField] private List<GameObject> Folder;
    public Dialogue dialogueScript;

    public void ShowAnswer(string answer)
    {
        if (!string.IsNullOrEmpty(answer) && answer.Trim() != "0")
        {
            dialogueScript.StartSingleLine(answer);
        }

        foreach (GameObject component in Folder)
        {
            component.SetActive(false);
        }
    }
}
