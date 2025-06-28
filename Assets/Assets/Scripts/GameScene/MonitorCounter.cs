using UnityEngine;
using TMPro;

public class MonitorCounter : MonoBehaviour
{
    public static MonitorCounter Instance;

    [Header("TextMeshPro UI")]
    public TextMeshProUGUI clientsLeftText;
    public TextMeshProUGUI approvalsLeftText;

    [Header("Counters")]
    public int totalClients = 5;
    public int approvalsLeft = 3;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    private void Start()
    {
        UpdateUI();
    }

    public void DecreaseClientCount()
    {
        totalClients = Mathf.Max(0, totalClients - 1);
        clientsLeftText.text = "Clients Left: " + totalClients;
    }

    public void DecreaseApprovalCount()
    {
        approvalsLeft = Mathf.Max(0, approvalsLeft - 1);
        approvalsLeftText.text = "Approvals Left: " + approvalsLeft;
    }
    private void UpdateUI()
    {
        clientsLeftText.text = "Clients Left: " + totalClients;
        approvalsLeftText.text = "Approvals Left: " + approvalsLeft;
    }
}
