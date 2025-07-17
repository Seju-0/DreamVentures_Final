using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestionsManager : MonoBehaviour
{
    [Header("Dialogue Reference")]
    public Dialogue dialogueScript;

    [Header("Client Reference")]
    public Client currentClient;

    [Header("UI References")]
    public List<GameObject> questionsPanel;
    public QuestionButtons[] questionButtons;

    [HideInInspector]
    public ClientData.QA[] questionList;

    private void Start()
    {
        if (currentClient != null)
        {
            SetClient(currentClient);
        }
    }

    public void SetClient(Client newClient)
    {
        currentClient = newClient;
        dialogueScript = currentClient.dialogue;
        questionList = currentClient.questionList;

        UpdateButtons();
    }

    public void UpdateButtons()
    {
        if (questionList == null || questionButtons == null) return;

        for (int i = 0; i < questionButtons.Length; i++)
        {
            if (i < questionList.Length)
            {
                questionButtons[i].gameObject.SetActive(true);
                questionButtons[i].Setup(this, i);
            }
            else
            {
                questionButtons[i].gameObject.SetActive(false);
            }
        }

        if (questionsPanel != null)
        {
            foreach (GameObject panel in questionsPanel)
            {
                if (panel != null)
                    panel.SetActive(true);
            }
        }
    }

    public void HideQuestions()
    {
        if (questionsPanel != null)
        {
            foreach (GameObject panel in questionsPanel)
            {
                if (panel != null)
                    panel.SetActive(false);
            }
        }
    }
}
