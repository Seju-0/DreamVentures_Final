using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class MonitorCounter : MonoBehaviour
{
    public static MonitorCounter Instance;

    [Header("TextMeshPro UI")]
    public TextMeshProUGUI clientsLeftText;
    public TextMeshProUGUI approvalsLeftText;
    public TextMeshProUGUI approvalLimitReachedText;
    public TextMeshProUGUI rejectionLimitReachedText;

    [Header("Counters")]
    public int totalClients = 5;
    public int approvalsLeft = 3;

    [Header("Button Reference")]
    public Button approveButton;
    public Button rejectButton; // New reject button reference

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

        if (rejectionLimitReachedText != null)
            rejectionLimitReachedText.gameObject.SetActive(false);

        UpdateUI();
    }

    public void DecreaseClientCount()
    {
        totalClients = Mathf.Max(0, totalClients - 1);
        clientsLeftText.text = "Clients Left: " + totalClients;
        UpdateUI();
    }

    public void DecreaseApprovalCount()
    {
        approvalsLeft = Mathf.Max(0, approvalsLeft - 1);
        approvalsLeftText.text = "Approval Quota: " + approvalsLeft;

        if (approvalsLeft <= 0)
        {
            if (approvalLimitReachedText != null)
                approvalLimitReachedText.gameObject.SetActive(true);

            if (approveButton != null)
                approveButton.interactable = false; 
        }

        UpdateUI();

    }

    public bool CanApprove()
    {
        return approvalsLeft > 0;
    }

    private void UpdateUI()
    {
        clientsLeftText.text = "Clients Left: " + totalClients;
        approvalsLeftText.text = "Approvals Left: " + approvalsLeft;

        if (approveButton != null)
            approveButton.interactable = (approvalsLeft > 0);

        if (rejectButton != null)
            rejectButton.interactable = (totalClients > approvalsLeft); // disable reject when quota must be met

        // Determine which warning to show
        bool showApprovalLimitReached = approvalsLeft <= 0;
        bool showRejectionLimitReached = approvalsLeft == totalClients;

        // Enforce mutual exclusivity
        if (approvalLimitReachedText != null)
            approvalLimitReachedText.gameObject.SetActive(showApprovalLimitReached && !showRejectionLimitReached);

        if (rejectionLimitReachedText != null)
            rejectionLimitReachedText.gameObject.SetActive(showRejectionLimitReached && !showApprovalLimitReached);
    }
}
