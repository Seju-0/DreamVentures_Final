using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Dialogue : MonoBehaviour
{
    [Header("UI References")]
    public GameObject Dialogue_Panel;
    public TextMeshProUGUI Dialogue_Text;
    public GameObject NextButton;
    public GameObject StartOverButton;

    [Header("Settings")]
    [TextArea(3, 10)]
    public string[] DialogueLines;
    public int maxCharactersPerPage = 150;
    public float typeSpeed = 0.02f;

    private int currentPageIndex = 0;
    private List<string> pages = new List<string>();
    private Coroutine typingCoroutine;
    private bool isTyping = false;

    public System.Action OnDialogueComplete;
    private bool allowReplay = true; // default true

    void Awake()
    {
        if (NextButton != null)
            NextButton.SetActive(false);

        if (StartOverButton != null)
            StartOverButton.SetActive(false);
    }

    public void StartDialogue()
    {
        StartDialogue(DialogueLines);
    }

    public void StartDialogue(string[] newLines)
    {
        DialogueLines = newLines;
        GeneratePages(DialogueLines);
        ShowPage(0);
    }

    public void StartDialogue(string text)
    {
        GeneratePages(new string[] { text });
        ShowPage(0);
    }

    public void StartSingleLine(string line)
    {
        StartDialogue(line);
    }

    private void GeneratePages(string[] lines)
    {
        pages.Clear();

        foreach (string line in lines)
        {
            string remainingText = line.Trim();

            while (remainingText.Length > maxCharactersPerPage)
            {
                int splitIndex = remainingText.LastIndexOf(' ', maxCharactersPerPage);
                if (splitIndex == -1) splitIndex = maxCharactersPerPage;

                string pageText = remainingText.Substring(0, splitIndex).Trim();
                pages.Add(pageText);
                remainingText = remainingText.Substring(splitIndex).Trim();
            }

            if (!string.IsNullOrEmpty(remainingText))
                pages.Add(remainingText);
        }
    }

    private void ShowPage(int pageIndex)
    {
        if (Dialogue_Panel != null)
            Dialogue_Panel.SetActive(true);

        if (StartOverButton != null)
            StartOverButton.SetActive(false);

        if (NextButton != null)
            NextButton.SetActive(true);

        currentPageIndex = pageIndex;

        if (typingCoroutine != null)
            StopCoroutine(typingCoroutine);

        typingCoroutine = StartCoroutine(TypePage(pages[pageIndex]));
    }

    IEnumerator TypePage(string text)
    {
        isTyping = true;
        Dialogue_Text.text = "";

        foreach (char c in text)
        {
            Dialogue_Text.text += c;
            yield return new WaitForSeconds(typeSpeed);
        }

        isTyping = false;

        // Next button stays visible regardless
        if (NextButton != null)
            NextButton.SetActive(true);
    }

    public void OnNextButtonClicked()
    {
        if (isTyping)
        {
            if (typingCoroutine != null)
                StopCoroutine(typingCoroutine);

            Dialogue_Text.text = pages[currentPageIndex];
            isTyping = false;
            return;
        }

        currentPageIndex++;

        if (currentPageIndex < pages.Count)
        {
            ShowPage(currentPageIndex);
        }
        else
        {
            Dialogue_Panel.SetActive(false);
            NextButton.SetActive(false);

            OnDialogueComplete?.Invoke(); // ✅ First update allowReplay

            if (StartOverButton != null)
                StartOverButton.SetActive(allowReplay); // ✅ Then show/hide based on latest value

            Debug.Log($"Dialogue finished. allowReplay = {allowReplay}");
        }
    }
    public void SetAllowReplay(bool value)
    {
        allowReplay = value;
        Debug.Log($"SetAllowReplay({value}), button exists = {StartOverButton != null}");
    }


    public void OnStartOverButtonClicked()
    {
        StartDialogue();
    }
}