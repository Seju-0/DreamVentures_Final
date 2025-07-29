using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Scene1Settings : MonoBehaviour
{
    [SerializeField] private List<GameObject> SettingsPanel;

    public void OpenSettings()
    {
        foreach(GameObject component in SettingsPanel)
        {
            component.SetActive(true);
        }
    }

    public void CloseBTN()
    {
        foreach (GameObject component in SettingsPanel)
        {
            component.SetActive(false);
        }
    }

    public void MainMenuBTN()
    {
        SceneManager.LoadScene("Main_Menu");
    }

    public void QuitBTN()
    {
        Application.Quit();
        Debug.Log("Quit Game");
        // UnityEditor.EditorApplication.isPlaying = false;
    }
    public void MainMenuButton2()
    {
        ChoiceResults.ResetAllData(); // Fully reset all progress
        SceneManager.LoadScene("Main_Menu");
    }

}
