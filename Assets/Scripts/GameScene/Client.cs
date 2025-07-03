using UnityEngine;

public class Client : MonoBehaviour
{
    [Header("Client Settings")]
    public ClientData clientData;

    private ClientManager manager;
    private Transform targetPoint;
    private Transform exitPoint;
    private float enterSpeed;
    private float exitSpeed;
    private bool hasReachedTarget = false;
    private bool isLeaving = false;

    [Header("Dialogue")]
    public Dialogue dialogue;
    public string[] customDialogueLines;

    [Header("Answers Reference")]
    public Answers answersComponent;

    [Header("Approval and Rejection Responses")]
    public string approvalResponse;
    public string rejectionResponse;

    [Header("Approval and Rejection Reputation Texts")]
    public string approvalReputationText;
    public string rejectionReputationText;

    [Header("Reputation Values")]
    public int approvalReputationValue;
    public int rejectionReputationValue;

    [Header("Dream Scene Texts")]
    public string approvalDreamDialogueText;
    public string approvalSanityText;
    public int approvalSanityValue;

    public string rejectionDreamDialogueText;
    public string rejectionSanityText;
    public int rejectionSanityValue;

    [Header("Newspaper Headlines")]
    public string approveNewspaper;
    public string rejectNewspaper; 

    public void Setup(ClientManager mgr, Transform target, Transform exit, float enterSpd, float exitSpd)
    {
        manager = mgr;
        targetPoint = target;
        exitPoint = exit;
        enterSpeed = enterSpd;
        exitSpeed = exitSpd;
    }

    public void MoveToTarget()
    {
        hasReachedTarget = false;
        isLeaving = false;
    }

    void Update()
    {
        if (!hasReachedTarget)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPoint.position, enterSpeed * Time.deltaTime);

            if (Vector3.Distance(transform.position, targetPoint.position) < 0.01f)
            {
                hasReachedTarget = true;

                if (clientData != null && clientData.folderPanel != null)
                {
                    clientData.folderPanel.UpdatePanel(clientData);
                    clientData.folderPanel.gameObject.SetActive(true);

                    clientData.folderPanel.SetupButtonCallbacks(
                        // APPROVE
                        () =>
                        {
                            if (!MonitorCounter.Instance.CanApprove())
                                return;

                            clientData.folderPanel.gameObject.SetActive(false);

                            // Show Cash Register
                            CashRegister cashRegister = FindAnyObjectByType<CashRegister>();
                            if (cashRegister != null)
                            {
                                cashRegister.AssignClient(this);
                                cashRegister.gameObject.SetActive(true);
                            }

                        },
                        // REJECT
                        () =>
                        {
                            answersComponent.ShowAnswer(
                                string.IsNullOrEmpty(rejectionResponse)
                                    ? "Loan rejected."
                                    : rejectionResponse
                            );

                            ChoiceResults.RecordDecision(
                                clientData.clientName,
                                false,
                                string.IsNullOrEmpty(clientData.rejectionEvaluationText)
                                    ? "No evaluation provided."
                                    : clientData.rejectionEvaluationText,
                                rejectionReputationText,
                                rejectionReputationValue,
                                rejectionDreamDialogueText,
                                rejectionSanityText,
                                rejectionSanityValue,
                                rejectNewspaper
                            );

                            clientData.folderPanel.gameObject.SetActive(false);
                            LeaveAfterDialogue();
                        },
                        // CLOSE
                        () =>
                        {
                            clientData.folderPanel.gameObject.SetActive(false);
                            // Do nothing else (does not show cashOpenButton)
                        }
                    );
                }

                if (customDialogueLines != null && customDialogueLines.Length > 0)
                {
                    dialogue.StartDialogue(customDialogueLines);
                }
                else
                {
                    dialogue.StartDialogue();
                }

                QuestionsManager qm = FindAnyObjectByType<QuestionsManager>();
                if (qm != null)
                {
                    qm.SetClient(this);
                }
            }
        }
        else if (isLeaving)
        {
            transform.position = Vector3.MoveTowards(transform.position, exitPoint.position, exitSpeed * Time.deltaTime);

            if (Vector3.Distance(transform.position, exitPoint.position) < 0.01f)
            {
                isLeaving = false;
                gameObject.SetActive(false);

                if (manager != null)
                {
                    manager.OnClientLeft();
                }
            }
        }
    }

    public void LeaveAfterDialogue()
    {
        dialogue.OnDialogueComplete += StartLeaving;
    }

    private void StartLeaving()
    {
        isLeaving = true;
        dialogue.OnDialogueComplete -= StartLeaving;
    }

    public void OnCashRegisterComplete()
    {
        answersComponent.ShowAnswer(
            string.IsNullOrEmpty(approvalResponse)
                ? "Loan approved."
                : approvalResponse
        );

        ChoiceResults.RecordDecision(
            clientData.clientName,
            true,
            string.IsNullOrEmpty(clientData.approvalEvaluationText)
                ? "No evaluation provided."
                : clientData.approvalEvaluationText,
            approvalReputationText,
            approvalReputationValue,
            approvalDreamDialogueText,
            approvalSanityText,
            approvalSanityValue,
            approveNewspaper
        );

        LeaveAfterDialogue();
    }

    public bool HasReachedTarget()
    {
        return hasReachedTarget;
    }

    public bool IsLeaving()
    {
        return isLeaving;
    }
}
