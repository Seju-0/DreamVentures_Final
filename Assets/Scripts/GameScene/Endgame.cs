using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class EndGame : MonoBehaviour
{
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
