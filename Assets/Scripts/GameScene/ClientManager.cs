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
    }
}
