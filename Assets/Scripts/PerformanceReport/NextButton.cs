using UnityEngine;
using UnityEngine.SceneManagement;

public class NextButton : MonoBehaviour
{
    public void NextBTN()
    {
        string nextScene = $"DS1";
        SceneManager.LoadScene(nextScene);
    }
}
