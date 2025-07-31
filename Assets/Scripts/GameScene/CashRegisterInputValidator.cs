using UnityEngine;

public class CashRegisterInputValidator : MonoBehaviour
{
    [SerializeField] private CashRegister cashRegister;

    private void OnEnable()
    {
        if (cashRegister == null)
            cashRegister = GetComponent<CashRegister>();

        if (cashRegister == null)
        {
            Debug.LogError("CashRegister reference is not set and not found on this GameObject.");
        }
    }

    void Update()
    {
        // Intercept Input: You can optionally hook this into your custom Print Button instead of using Update.
    }

    public void SafePrint()
    {
        if (cashRegister == null) return;

        string userInput = GetInputAmount();
        string correct = GetCorrectAmount();

        if (userInput == correct)
        {
            cashRegister.OnPrintButtonPressed(); // Proceed with normal printing
        }
        else
        {
            PlayOnlyErrorSound();
        }
    }

    private string GetInputAmount()
    {
        var inputField = typeof(CashRegister)
            .GetField("currentInput", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
        return inputField?.GetValue(cashRegister)?.ToString();
    }

    private string GetCorrectAmount()
    {
        var correctField = typeof(CashRegister)
            .GetField("correctAmount", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
        return correctField?.GetValue(cashRegister)?.ToString();
    }

    private void PlayOnlyErrorSound()
    {
        var errorClipField = typeof(CashRegister)
            .GetField("errorClip", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
        var audioSource = cashRegister.GetComponent<AudioSource>();
        var errorClip = errorClipField?.GetValue(cashRegister) as AudioClip;

        if (audioSource != null && errorClip != null)
        {
            audioSource.PlayOneShot(errorClip);
        }
    }
}
