using System.Collections;
using System.Collections.Generic;
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
                        () =>
                        {
                            // Approve logic
                            answersComponent.ShowAnswer(
                                string.IsNullOrEmpty(approvalResponse)
                                    ? "Loan approved."
                                    : approvalResponse
                            );

                            MonitorCounter.Instance.DecreaseApprovalCount();
                            clientData.folderPanel.gameObject.SetActive(false);
                            LeaveAfterDialogue();
                        },
                        () =>
                        {
                            // Reject logic
                            answersComponent.ShowAnswer(
                                string.IsNullOrEmpty(rejectionResponse)
                                    ? "Loan rejected."
                                    : rejectionResponse
                            );

                            clientData.folderPanel.gameObject.SetActive(false);
                            LeaveAfterDialogue();
                        },
                        () =>
                        {
                            // Close logic
                            clientData.folderPanel.gameObject.SetActive(false);
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

                QuestionsManager qm = FindFirstObjectByType<QuestionsManager>();
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
}
