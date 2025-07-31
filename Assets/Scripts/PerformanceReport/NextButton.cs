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
        string nextScene = $"IB2";
        SceneManager.LoadScene(nextScene);
    }
    public void NextBTN2()
    {
        string nextScene = $"IB3";
        SceneManager.LoadScene(nextScene);
    }
    public void NextBTN3()
    {
        string nextScene = $"DS1";
        SceneManager.LoadScene(nextScene);
    }
}
