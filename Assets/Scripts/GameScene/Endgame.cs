using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class EndGame : MonoBehaviour
{
    [Header("UI Button")]
    [SerializeField] private GameObject QuitButton; 

    private void Start()
    {
        if (QuitButton != null)
            QuitButton.SetActive(false); 
    }

    public void ShowButton()
    {
        if (QuitButton != null)
            QuitButton.SetActive(true);
    }

    public void RestartBTN()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Day1");
    }

    public void ExitBTN()
    {
        Application.Quit();
        Debug.Log("Program Closed");
    }

    public void MainMenuBTN()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
    }
}
