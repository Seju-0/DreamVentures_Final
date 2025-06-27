using TMPro;
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

            button.onClick.RemoveAllListeners();
            button.onClick.AddListener(() =>
            {
                string answer = list[questionIndex].answer;
                questionsManager.dialogueScript.StartSingleLine(answer);
            });
        }
    }
}
