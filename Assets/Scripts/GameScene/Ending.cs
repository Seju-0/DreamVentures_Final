using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Ending : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI Dialogue_Text;
    [SerializeField][TextArea] private List<string> dialogues;
    [SerializeField] private float speed = 0.05f;

    [Header("Button to Show")]
    [SerializeField] private GameObject QuitButton; 

    private int dialogueIndex = 0;
    private bool isTyping = false;
    private Coroutine typingCoroutine;

    [Header("Typewriter Sound")]
    [SerializeField] private AudioClip typewriterSound;
    [Range(0f, 1f)][SerializeField] private float volume = 0.5f;
    private AudioSource audioSource;

    private void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;
        audioSource.volume = volume;

        if (QuitButton != null)
            QuitButton.SetActive(false); // Hide button at start

        ShowCurrentDialogue();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (isTyping)
            {
                StopCoroutine(typingCoroutine);
                Dialogue_Text.text = dialogues[dialogueIndex];
                isTyping = false;
                return;
            }

            dialogueIndex++;

            if (dialogueIndex < dialogues.Count)
            {
                ShowCurrentDialogue();
            }
            else
            {
                if (QuitButton != null)
                    QuitButton.SetActive(true); // Show button after dialogue finishes
            }
        }
    }

    private void ShowCurrentDialogue()
    {
        if (dialogueIndex < dialogues.Count)
        {
            typingCoroutine = StartCoroutine(Typewriter(dialogues[dialogueIndex]));
        }
    }

    IEnumerator Typewriter(string text)
    {
        isTyping = true;
        Dialogue_Text.text = "";

        foreach (char letter in text.ToCharArray())
        {
            Dialogue_Text.text += letter;

            if (!char.IsWhiteSpace(letter) && typewriterSound != null)
                audioSource.PlayOneShot(typewriterSound);

            yield return new WaitForSeconds(speed);
        }

        isTyping = false;
    }
}
