using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class CashRegister : MonoBehaviour
{
    [Header("UI References")]
    public TextMeshProUGUI displayText;
    public TextMeshProUGUI loanFormText;
    public TextMeshProUGUI wrongAmountText;
    public TextMeshProUGUI requestedAmountText;
    public Button closeButton;
    public Button loanFormButton;

    [Header("Animation Settings")]
    public Vector2 hiddenPosition = new Vector2(0, -150f);
    public Vector2 visiblePosition = new Vector2(0, 0f);
    public float slideDuration = 1f;

    [Header("Audio Settings")]
    public AudioClip printSound;
    [Range(0f, 2f)] public float printVolume;

    public AudioClip buttonClickSound;
    [Range(0f, 2f)] public float buttonClickVolume;

    public AudioClip errorSound;
    [Range(0f, 2f)] public float errorVolume;

    private AudioSource audioSource;

    private string currentInput = "";
    private string correctAmount = "";
    private Client assignedClient;
    private bool isAnimating = false;

    public GameObject cashOpenButton;

    void Start()
    {
        ResetState();

        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        if (closeButton != null)
            closeButton.onClick.AddListener(CloseCashRegister);

        if (loanFormButton != null)
            loanFormButton.onClick.AddListener(OnLoanFormClicked);
    }

    public void ResetState()
    {
        currentInput = "";
        UpdateDisplay();

        if (wrongAmountText != null)
            wrongAmountText.gameObject.SetActive(false);

        if (loanFormText != null)
            loanFormText.text = "";

        if (requestedAmountText != null)
            requestedAmountText.text = "";

        if (loanFormButton != null)
        {
            RectTransform rt = loanFormButton.GetComponent<RectTransform>();
            rt.anchoredPosition = hiddenPosition;

            loanFormButton.interactable = false;
            loanFormButton.gameObject.SetActive(false);
        }

        isAnimating = false;
    }

    public void AssignClient(Client client)
    {
        assignedClient = client;

        if (client != null)
        {
            correctAmount = client.ClientInfo.requestedAmount.Replace(",", "").Trim();

            if (requestedAmountText != null)
                requestedAmountText.text = client.ClientInfo.requestedAmount;
        }

        if (cashOpenButton != null)
            cashOpenButton.SetActive(true);
    }

    public void OnDigitButtonPressed(string digit)
    {
        currentInput += digit;
        UpdateDisplay();
        PlayButtonClick();
    }

    public void OnBackspaceButtonPressed()
    {
        if (!string.IsNullOrEmpty(currentInput))
        {
            currentInput = currentInput.Substring(0, currentInput.Length - 1);
            UpdateDisplay();
            PlayButtonClick();
        }
    }

    public void OnClearButtonPressed()
    {
        currentInput = "";
        UpdateDisplay();
        PlayButtonClick();
    }

    public void OnPrintButtonPressed()
    {
        if (string.IsNullOrEmpty(correctAmount))
            return;

        if (currentInput == correctAmount)
        {
            if (wrongAmountText != null)
                wrongAmountText.gameObject.SetActive(false);

            if (loanFormText != null)
            {
                if (long.TryParse(currentInput, out long number))
                    loanFormText.text = $"Bank Check:\n{number:N0}\nCredits";
                else
                    loanFormText.text = $"Bank Check:\n{currentInput}";
            }

            currentInput = "";
            UpdateDisplay();

            if (loanFormButton != null)
            {
                loanFormButton.gameObject.SetActive(true);
                loanFormButton.interactable = false;

                RectTransform rt = loanFormButton.GetComponent<RectTransform>();
                rt.anchoredPosition = hiddenPosition;

                isAnimating = true;
                StartCoroutine(SlideUp(rt));
            }

            if (printSound != null && audioSource != null)
                audioSource.PlayOneShot(printSound, printVolume);
        }
        else
        {
            if (wrongAmountText != null)
            {
                StopCoroutine(nameof(FadeOutWrongAmountText));
                wrongAmountText.color = new Color(
                    wrongAmountText.color.r,
                    wrongAmountText.color.g,
                    wrongAmountText.color.b,
                    1f);
                wrongAmountText.gameObject.SetActive(true);
                StartCoroutine(FadeOutWrongAmountText());
            }

            PlayErrorSound();
        }

        PlayButtonClick();
    }

    private void UpdateDisplay()
    {
        if (displayText != null)
        {
            if (string.IsNullOrEmpty(currentInput))
            {
                displayText.text = "";
            }
            else
            {
                if (long.TryParse(currentInput, out long number))
                    displayText.text = number.ToString("N0");
                else
                    displayText.text = currentInput;
            }

            Debug.Log("Updated Display Text: " + displayText.text);
        }
        else
        {
            Debug.LogWarning("Display Text is not assigned!");
        }
    }

    private IEnumerator SlideUp(RectTransform rt)
    {
        Vector2 startPos = hiddenPosition;
        Vector2 endPos = visiblePosition;
        float elapsed = 0f;

        while (elapsed < slideDuration)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / slideDuration);
            rt.anchoredPosition = Vector2.Lerp(startPos, endPos, t);
            yield return null;
        }

        rt.anchoredPosition = endPos;

        if (loanFormButton != null)
            loanFormButton.interactable = true;

        isAnimating = false;
    }

    public void OnLoanFormClicked()
    {
        if (assignedClient != null)
            assignedClient.OnCashRegisterComplete();

        if (MonitorCounter.Instance != null)
            MonitorCounter.Instance.DecreaseApprovalCount();

        PlayButtonClick();

        StartCoroutine(HideCashRegisterAfterDelay());
    }

    private IEnumerator HideCashRegisterAfterDelay()
    {
        while (isAnimating)
            yield return null;

        if (loanFormButton != null)
        {
            loanFormButton.interactable = false;
            loanFormButton.gameObject.SetActive(false);

            RectTransform rt = loanFormButton.GetComponent<RectTransform>();
            rt.anchoredPosition = hiddenPosition;
        }

        CashOpenButton opener = FindFirstObjectByType<CashOpenButton>();
        if (opener != null)
            opener.CloseBTN();
        else
            gameObject.SetActive(false);

        if (cashOpenButton != null)
            cashOpenButton.SetActive(false);
    }

    public void CloseCashRegister()
    {
        currentInput = "";
        UpdateDisplay();

        if (wrongAmountText != null)
            wrongAmountText.gameObject.SetActive(false);

        if (loanFormButton != null)
        {
            loanFormButton.interactable = false;
            loanFormButton.gameObject.SetActive(false);

            RectTransform rt = loanFormButton.GetComponent<RectTransform>();
            rt.anchoredPosition = hiddenPosition;
        }

        CashOpenButton opener = FindFirstObjectByType<CashOpenButton>();
        if (opener != null)
            opener.CloseBTN();
        else
            gameObject.SetActive(false);

        if (cashOpenButton != null)
            cashOpenButton.SetActive(true);

        PlayButtonClick();
    }

    private void PlayButtonClick()
    {
        if (buttonClickSound != null && audioSource != null)
            audioSource.PlayOneShot(buttonClickSound, buttonClickVolume);
    }

    private void PlayErrorSound()
    {
        if (errorSound != null && audioSource != null)
            audioSource.PlayOneShot(errorSound, errorVolume);
    }

    private IEnumerator FadeOutWrongAmountText()
    {
        if (wrongAmountText == null)
            yield break;

        wrongAmountText.gameObject.SetActive(true);

        Color originalColor = wrongAmountText.color;
        Color fadeColor = originalColor;

        yield return new WaitForSeconds(2f);

        float fadeDuration = 0.5f;
        float elapsed = 0f;

        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / fadeDuration;
            fadeColor.a = Mathf.Lerp(originalColor.a, 0f, t);
            wrongAmountText.color = fadeColor;
            yield return null;
        }

        wrongAmountText.gameObject.SetActive(false);
        wrongAmountText.color = originalColor;
    }

}
