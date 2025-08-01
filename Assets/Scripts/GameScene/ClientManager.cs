using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class ClientManager : MonoBehaviour
{
    public List<Client> clients = new List<Client>();
    public Transform startingPoint;
    public Transform targetPoint;
    public Transform exitPoint;
    public float enterSpeed = 2f;
    public float exitSpeed = 2f;

    private int currentClientIndex = 0;

    [SerializeField] private DecisionText decisionDisplay;

    void Start()
    {
        if (decisionDisplay == null)
        {
            Debug.LogError("DecisionDisplay is not assigned in the inspector!");
        }

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
            decisionDisplay.SetClient(clients[0]);
        }
    }

    void OnClientArrives(Client newClient)
    {
        decisionDisplay.SetClient(newClient);
    }

    public void OnApprove()
    {
        decisionDisplay.ShowApproval();
        StartCoroutine(AnimateText(decisionDisplay.reputationText));
        StartCoroutine(AnimateText(decisionDisplay.sanityText));
    }

    public void OnReject()
    {
        decisionDisplay.ShowRejection();
        StartCoroutine(AnimateText(decisionDisplay.reputationText));
        StartCoroutine(AnimateText(decisionDisplay.sanityText));
    }

    private IEnumerator AnimateText(TextMeshProUGUI text)
    {
        text.gameObject.SetActive(true);

        float duration = 2f;
        float elapsed = 0f;

        Vector3 startPos = text.rectTransform.localPosition;
        Vector3 endPos = startPos + new Vector3(0, 50f, 0);

        Color startColor = text.color;
        Color endColor = new Color(startColor.r, startColor.g, startColor.b, 0);

        while (elapsed < duration)
        {
            float t = elapsed / duration;
            text.rectTransform.localPosition = Vector3.Lerp(startPos, endPos, t);
            text.color = Color.Lerp(startColor, endColor, t);
            elapsed += Time.deltaTime;
            yield return null;
        }

        text.rectTransform.localPosition = startPos;
        text.color = startColor;
        text.gameObject.SetActive(false);
    }

    public void OnClientLeft()
    {
        MonitorCounter.Instance.DecreaseClientCount();

        currentClientIndex++;

        if (currentClientIndex < clients.Count)
        {
            clients[currentClientIndex].gameObject.SetActive(true);
            clients[currentClientIndex].MoveToTarget();
            decisionDisplay.SetClient(clients[currentClientIndex]);
        }
        else
        {
            StartCoroutine(LoadReportSceneAfterDelay(1f));
        }
    }

    private IEnumerator LoadReportSceneAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        if (ChoiceResults.currentDay == 5)
        {
            int finalRep = ChoiceResults.currentReputation;
            int finalSanity = ChoiceResults.currentSanity;

            if (finalSanity < 180 && finalRep < 270)
            {
                if (finalSanity < finalRep)
                    SceneManager.LoadScene("LoseToSanity");
                else
                    SceneManager.LoadScene("LoseToReputation");
            }
            else if (finalSanity < 180)
            {
                SceneManager.LoadScene("LoseToSanity");
            }
            else if (finalRep < 270)
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