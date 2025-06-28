using System.Collections.Generic;
using Unity.VisualScripting;
using System.Collections;
using UnityEngine;
using TMPro;

public class IntroDialogue : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI Dialogue_Text;
    [SerializeField] private string dialogue;
    [SerializeField] private float speed = 0.05f;

    private Coroutine typingCoroutine;
    private bool isTyping = false;

    private void Start()
    {
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
            yield return new WaitForSeconds(speed);
        }

        isTyping = false;
    }
}
