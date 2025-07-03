using UnityEngine;
using UnityEngine.SceneManagement;

public class NextButton : MonoBehaviour
{
    public void NextBTN()
    {
        SceneManager.LoadScene("DreamSceneDay1");
    }
}
