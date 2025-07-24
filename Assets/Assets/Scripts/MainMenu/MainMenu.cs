using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private List<GameObject> Main;
    [SerializeField] private List<GameObject> Settings;
    [SerializeField] private List<GameObject> Credits;

    public void StartBTN()
    {
        SceneManager.LoadScene("Intro");
    }

    public void SettingsBTN()
    {
        foreach (GameObject gameObject in Settings)
        {
            gameObject.SetActive(true);
        }
    }
    public void CloseBTN()
    {
        foreach (GameObject gameObject in Main)
        {
            gameObject.SetActive(true);
        }

        foreach (GameObject gameObject in Settings)
        {
            gameObject.SetActive(false);
        }

        foreach (GameObject gameObject in Credits)
        {
            gameObject.SetActive(false);
        }
    }

    public void CreditsBTN()
    {
        foreach (GameObject gameObject in Credits)
        {
            gameObject.SetActive(true);
        }
    }
    public void ExitBTN()
    {
        Application.Quit();
        Debug.Log("Program Closed");
    }
}
