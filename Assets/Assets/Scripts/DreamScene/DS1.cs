using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class DS1 : MonoBehaviour
{
    [Header("UI References")]
    public Image dreamImage;
    public Image DialogueBox;
    public TextMeshProUGUI dreamText;
    public Button nextButton;
    public TextMeshProUGUI totalSanityText;
    public Button continueButton;

    [Header("Dream Images")]
    public List<Sprite> approvalImages;
    public List<Sprite> rejectionImages;

    [Header("Settings")]
    public int maxCharactersPerPage = 150;
    public float typeSpeed = 0.02f;

    private int currentDreamIndex = 0;
    private int currentPageIndex = 0;
    private List<string> currentPages = new List<string>();

    private Coroutine typingCoroutine;
    private bool isTyping = false;

    void Start()
    {
        var decisions = ChoiceResults.GetDecisionsForCurrentDay();
        if (decisions.Count == 0)
        {
            ShowNoDreams();
            return;
        }

        if (nextButton != null)
            nextButton.onClick.AddListener(OnNextClicked);

        if (continueButton != null)
            continueButton.gameObject.SetActive(false);

        DialogueBox.gameObject.SetActive(true);

        currentDreamIndex = 0;
        ShowCurrentDream();
    }

    private void ShowNoDreams()
    {
        if (dreamImage != null)
            dreamImage.gameObject.SetActive(false);

        if (dreamText != null)
            dreamText.text = "You have no dreams tonight.";

        if (totalSanityText != null)
        {
            totalSanityText.gameObject.SetActive(true);
            totalSanityText.text = $"Total Sanity: {ChoiceResults.currentSanity}";
        }

        if (nextButton != null)
            nextButton.gameObject.SetActive(false);

        if (continueButton != null)
        {
            continueButton.gameObject.SetActive(true);
            continueButton.onClick.RemoveAllListeners();
            continueButton.onClick.AddListener(OnContinueClicked);
        }
    }

    private void ShowCurrentDream()
    {
        var decisions = ChoiceResults.GetDecisionsForCurrentDay();

        if (currentDreamIndex >= decisions.Count)
        {
            // All dreams shown
            if (dreamImage != null)
                dreamImage.gameObject.SetActive(false);

            if (dreamText != null)
                dreamText.gameObject.SetActive(false);

            if (nextButton != null)
                nextButton.gameObject.SetActive(false);

            if (totalSanityText != null)
            {
                DialogueBox.gameObject.SetActive(false);
                totalSanityText.gameObject.SetActive(true);
                totalSanityText.text = $"Total Sanity: {ChoiceResults.currentSanity}";
            }

            if (continueButton != null)
            {
                continueButton.gameObject.SetActive(true);
                continueButton.onClick.RemoveAllListeners();
                continueButton.onClick.AddListener(OnContinueClicked);
            }

            return;
        }

        var d = decisions[currentDreamIndex];

        Sprite selectedSprite = null;
        if (d.approved)
        {
            if (currentDreamIndex < approvalImages.Count)
                selectedSprite = approvalImages[currentDreamIndex];
        }
        else
        {
            if (currentDreamIndex < rejectionImages.Count)
                selectedSprite = rejectionImages[currentDreamIndex];
        }

        if (dreamImage != null)
        {
            if (selectedSprite != null)
            {
                dreamImage.gameObject.SetActive(true);
                dreamImage.sprite = selectedSprite;
            }
            else
            {
                dreamImage.gameObject.SetActive(false);
            }
        }

        if (dreamText != null)
            dreamText.gameObject.SetActive(true);

        if (totalSanityText != null)
            totalSanityText.gameObject.SetActive(false);

        if (continueButton != null)
            continueButton.gameObject.SetActive(false);

        currentPages.Clear();
        string rawText =
            $"<b>Dream {currentDreamIndex + 1}:</b>\n" +
            d.dreamDialogueText +
            $" Sanity {(d.sanityValue >= 0 ? "+" : "")}{d.sanityValue}";

        SplitIntoPages(rawText);
        currentPageIndex = 0;
        ShowPage(currentPageIndex);
    }

    private void SplitIntoPages(string text)
    {
        currentPages.Clear();

        string remainingText = text.Trim();

        while (remainingText.Length > maxCharactersPerPage)
        {
            int splitIndex = remainingText.LastIndexOf(' ', maxCharactersPerPage);
            if (splitIndex == -1)
                splitIndex = maxCharactersPerPage;

            string pageText = remainingText.Substring(0, splitIndex).Trim();
            currentPages.Add(pageText);
            remainingText = remainingText.Substring(splitIndex).Trim();
        }

        if (!string.IsNullOrEmpty(remainingText))
            currentPages.Add(remainingText);
    }

    private void ShowPage(int pageIndex)
    {
        if (dreamText == null) return;

        if (typingCoroutine != null)
            StopCoroutine(typingCoroutine);

        typingCoroutine = StartCoroutine(TypePage(currentPages[pageIndex]));
    }

    private IEnumerator TypePage(string text)
    {
        isTyping = true;
        dreamText.text = "";

        foreach (char c in text)
        {
            dreamText.text += c;
            yield return new WaitForSeconds(typeSpeed);
        }

        isTyping = false;

        if (nextButton != null)
            nextButton.gameObject.SetActive(true);
    }

    private void OnNextClicked()
    {
        if (isTyping)
        {
            if (typingCoroutine != null)
                StopCoroutine(typingCoroutine);

            dreamText.text = currentPages[currentPageIndex];
            isTyping = false;
            return;
        }

        currentPageIndex++;

        if (currentPageIndex < currentPages.Count)
        {
            ShowPage(currentPageIndex);
        }
        else
        {
            currentDreamIndex++;
            ShowCurrentDream();
        }
    }

    private void OnContinueClicked()
    {
        ChoiceResults.StartNewDay();
        string nextScene = $"Day{ChoiceResults.currentDay}";
        SceneManager.LoadScene(nextScene);
    }
}
