using UnityEngine;
using UnityEngine.SceneManagement;

public class ContinueBTN : MonoBehaviour
{
    public void Continue()
    {
        SceneManager.LoadScene("Day2");
    }
}
