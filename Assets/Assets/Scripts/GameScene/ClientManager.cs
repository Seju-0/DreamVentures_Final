using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ClientManager : MonoBehaviour
{
    public List<Client> clients = new List<Client>();
    public Transform startingPoint;
    public Transform targetPoint;
    public Transform exitPoint;
    public float enterSpeed = 2f;
    public float exitSpeed = 2f;

    private int currentClientIndex = 0;

    void Start()
    {
        foreach (var client in clients)
        {
            client.transform.position = startingPoint.position;
            client.gameObject.SetActive(false);
            client.Setup(this, targetPoint, exitPoint, enterSpeed, exitSpeed);
        }

        if (clients.Count > 0)
        {
            clients[0].gameObject.SetActive(true);
            clients[0].MoveToTarget();
        }
    }

    public void OnClientLeft()
    {
        MonitorCounter.Instance.DecreaseClientCount();

        currentClientIndex++;

        if (currentClientIndex < clients.Count)
        {
            clients[currentClientIndex].gameObject.SetActive(true);
            clients[currentClientIndex].MoveToTarget();
        }
        else
        {
            StartCoroutine(LoadReportSceneAfterDelay(1f));
        }
    }

    private IEnumerator LoadReportSceneAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        if (ChoiceResults.currentDay == 4)
        {
            int finalRep = ChoiceResults.currentReputation;
            int finalSanity = ChoiceResults.currentSanity;

            if (finalSanity <= 120 && finalRep <= 120)
            {
                if (finalSanity < finalRep)
                    SceneManager.LoadScene("LoseToSanity");
                else
                    SceneManager.LoadScene("LoseToReputation");
            }
            else if (finalSanity <= 120)
            {
                SceneManager.LoadScene("LoseToSanity");
            }
            else if (finalRep <= 120)
            {
                SceneManager.LoadScene("LoseToReputation");
            }
            else
            {
                SceneManager.LoadScene("Win");
            }

            Debug.Log($"End of Day 4: Rep={finalRep}, Sanity={finalSanity}");
        }
        else
        {
            string reportSceneName = "PRDAY" + ChoiceResults.currentDay;
            SceneManager.LoadScene(reportSceneName);

            Debug.Log("Loading report scene: " + reportSceneName);
        }
    }

}