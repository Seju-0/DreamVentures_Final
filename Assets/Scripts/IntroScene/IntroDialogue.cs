using System.Collections;
using UnityEngine;
using TMPro;

public class IntroDialogue : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI Dialogue_Text;
    [SerializeField][TextArea] private string dialogue;
    [SerializeField] private float speed = 0.05f;

    [Header("Typewriter Sound")]
    [SerializeField] private AudioClip typewriterSound;
    [Range(0f, 1f)][SerializeField] private float volume = 0.5f;
    private AudioSource audioSource;

    private Coroutine typingCoroutine;
    private bool isTyping = false;

    private void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;
        audioSource.volume = volume;

        typingCoroutine = StartCoroutine(Typewriter());
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (isTyping)
            {
                StopCoroutine(typingCoroutine);
                Dialogue_Text.text = dialogue;
                isTyping = false;
            }
        }
    }

    IEnumerator Typewriter()
    {
        isTyping = true;
        Dialogue_Text.text = "";

        foreach (char letter in dialogue.ToCharArray())
        {
            Dialogue_Text.text += letter;

            if (!char.IsWhiteSpace(letter) && typewriterSound != null)
                audioSource.PlayOneShot(typewriterSound);

            yield return new WaitForSeconds(speed);
        }

        isTyping = false;
    }
}
