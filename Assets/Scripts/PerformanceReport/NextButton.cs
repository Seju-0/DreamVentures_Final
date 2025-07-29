using UnityEngine;
using UnityEngine.SceneManagement;

public class NextButton : MonoBehaviour
{
    public void NextBTN()
    {
        string nextScene = $"IB1";
        SceneManager.LoadScene(nextScene);
    }
    public void NextBTN1()
    {
        string nextScene = $"DS2";
        SceneManager.LoadScene(nextScene);
    }
    public void NextBTN2()
    {
        string nextScene = $"DS3";
        SceneManager.LoadScene(nextScene);
    }
}
