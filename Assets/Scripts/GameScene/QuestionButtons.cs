﻿using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuestionButtons : MonoBehaviour
{
    public int questionIndex;
    public QuestionsManager questionsManager;

    private Button button;
    private TextMeshProUGUI buttonText;

    void Awake()
    {
        button = GetComponent<Button>();
        buttonText = GetComponentInChildren<TextMeshProUGUI>();
    }

    public void Setup(QuestionsManager manager, int index)
    {
        questionsManager = manager;
        questionIndex = index;

        ClientData.QA[] list = questionsManager.questionList;

        if (list != null && questionIndex < list.Length)
        {
            buttonText.text = list[questionIndex].question;
            buttonText.gameObject.SetActive(false);

            button.interactable = false; // 🔒 Disable click until shown

            button.onClick.RemoveAllListeners();
            button.onClick.AddListener(() =>
            {
                string answer = list[questionIndex].answer;
                questionsManager.dialogueScript.StartSingleLine(answer);
            });
        }
    }

    public void ShowText()
    {
        if (buttonText != null)
        {
            buttonText.gameObject.SetActive(true);
            button.interactable = true; // ✅ Enable after text is shown
        }
    }
    public void ResetButton()
    {
        if (buttonText != null)
            buttonText.gameObject.SetActive(false);

        if (button != null)
            button.interactable = false;
    }
}
