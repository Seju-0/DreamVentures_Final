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

    public GameObject openFolderButton;

    [HideInInspector]
    public ClientData.QA[] questionList;

    private bool isClientLeaving = false;

    private void Start()
    {
        if (currentClient != null)
        {
            SetClient(currentClient);
        }
    }

    public void SetClient(Client newClient)
    {
        if (currentClient != null && currentClient.dialogue != null)
        {
            currentClient.dialogue.OnDialogueComplete = null;
        }

        isClientLeaving = false;
        currentClient = newClient;
        dialogueScript = currentClient.dialogue;
        questionList = currentClient.questionList;

        UpdateButtons();

        if (openFolderButton != null)
            openFolderButton.SetActive(false);

        dialogueScript.OnDialogueComplete = null; // Clear again just in case
        dialogueScript.OnDialogueComplete += () =>
        {
            if (isClientLeaving) return;

            RevealQuestionTexts();

            if (dialogueScript != null)
                dialogueScript.SetAllowReplay(true);

            if (openFolderButton != null)
                openFolderButton.SetActive(true);
        };
    }

    public void UpdateButtons()
    {
        if (questionList == null || questionButtons == null) return;

        for (int i = 0; i < questionButtons.Length; i++)
        {
            if (i < questionList.Length)
            {
                questionButtons[i].gameObject.SetActive(true);
                questionButtons[i].Setup(this, i); // This hides text
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

    public void RevealQuestionTexts()
    {
        foreach (var button in questionButtons)
        {
            if (button != null)
                button.ShowText();
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
    public void ResetAll()
    {
        isClientLeaving = true; // ✅ Mark to prevent button reactivation

        foreach (var button in questionButtons)
        {
            if (button != null)
                button.ResetButton();
        }

        foreach (GameObject panel in questionsPanel)
        {
            if (panel != null)
                panel.SetActive(false);
        }

        if (openFolderButton != null)
            openFolderButton.SetActive(false);
    }
}
