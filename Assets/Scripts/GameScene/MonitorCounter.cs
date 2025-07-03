using UnityEngine;
using TMPro;

public class MonitorCounter : MonoBehaviour
{
    public static MonitorCounter Instance;

    [Header("TextMeshPro UI")]
    public TextMeshProUGUI clientsLeftText;
    public TextMeshProUGUI approvalsLeftText;
    public TextMeshProUGUI approvalLimitReachedText; 

    [Header("Counters")]
    public int totalClients = 5;
    public int approvalsLeft = 3;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        if (approvalLimitReachedText != null)
            approvalLimitReachedText.gameObject.SetActive(false);

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

        if (approvalsLeft <= 0 && approvalLimitReachedText != null)
        {
            approvalLimitReachedText.gameObject.SetActive(true);
        }
    }
    public bool CanApprove()
    {
        return approvalsLeft > 0;
    }

    private void UpdateUI()
    {
        clientsLeftText.text = "Clients Left: " + totalClients;
        approvalsLeftText.text = "Approvals Left: " + approvalsLeft;
    }
}
