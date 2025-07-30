using System;
using UnityEngine;
using UnityEngine.UI;
using static ClientData;

public class Client : MonoBehaviour
{
    [SerializeField] private ClientSO clientInfo;
    public ClientSO ClientInfo => clientInfo;

    [Header("Client Settings")]

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

    [Header("Folder")]
    public FolderPanel folderPanel;

    [Header("Answers")]
    public Answers answers;

    [Header("Questions")]
    public QA[] questionList;

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
    public string approveMainHeadline;
    public string approveSubHeadline;
    public string approveObituary;

    public string rejectMainHeadline;
    public string rejectSubHeadline;
    public string rejectObituary;

    [Header("Audio Settings")]
    public AudioClip walkingSound;
    [Range(0f, 2f)] public float walkingVolume = 1f;

    private AudioSource walkingAudioSource;

    [Header("Error Sound")]
    public AudioClip errorSound;
    [Range(0f, 2f)] public float errorVolume = 1f;

    private AudioSource sfxAudioSource;

    [Header("Validation Settings")]
    public Image warningImageDisplay; 
    public Button warningCloseButton; 
    public int reputationPenalty = 5;

    private bool waitingApprovalAfterWarning = false;
    private bool hasCompletedCashRegister = false;
    private System.Action onDialogueCompleteHandler;
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

    void Start()
    {

        walkingAudioSource = gameObject.AddComponent<AudioSource>();
        walkingAudioSource.loop = true;
        walkingAudioSource.playOnAwake = false;
        walkingAudioSource.volume = walkingVolume;
        walkingAudioSource.clip = walkingSound;

        sfxAudioSource = gameObject.AddComponent<AudioSource>();
        sfxAudioSource.playOnAwake = false;
        sfxAudioSource.loop = false;

        if (warningImageDisplay != null)
            warningImageDisplay.gameObject.SetActive(false);

        if (warningCloseButton != null)
        {
            warningCloseButton.gameObject.SetActive(false);
            warningCloseButton.onClick.AddListener(OnWarningClosed);
        }

        if (folderPanel != null && folderPanel.openFolderButton != null)
        {
            folderPanel.openFolderButton.gameObject.SetActive(false); // 👈 Hides the button fully
        }
    }

    void Update()
    {
        if (!hasReachedTarget)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPoint.position, enterSpeed * Time.deltaTime);

            if (walkingSound != null && walkingAudioSource != null && !walkingAudioSource.isPlaying)
                PlayWalkingSound();

            if (Vector3.Distance(transform.position, targetPoint.position) < 0.01f)
            {
                hasReachedTarget = true;
                StopWalkingSound();
                OnArrivedAtTarget();
            }
        }
        else if (isLeaving)
        {
            transform.position = Vector3.MoveTowards(transform.position, exitPoint.position, exitSpeed * Time.deltaTime);

            if (walkingSound != null && walkingAudioSource != null && !walkingAudioSource.isPlaying)
                PlayWalkingSound();

            if (Vector3.Distance(transform.position, exitPoint.position) < 0.01f)
            {
                isLeaving = false;
                StopWalkingSound();
                OnExited();
            }
        }
    }

    private void OnArrivedAtTarget()
    {
        if (folderPanel.openFolderButton != null)
        {
            folderPanel.openFolderButton.gameObject.SetActive(true);
            folderPanel.openFolderButton.interactable = true; 
        }

        if (clientInfo != null && folderPanel != null)
        {
            folderPanel.UpdatePanel(clientInfo);

            folderPanel.SetupButtonCallbacks(
                // APPROVE
                () =>
                {
                    if (!MonitorCounter.Instance.CanApprove())
                        return;

                    bool isValid = ValidateClientInfo();
                    if (!isValid)
                    {
                        ShowWarning();
                        ChoiceResults.warningsThisDay += reputationPenalty;
                        ChoiceResults.currentReputation -= reputationPenalty;

                        waitingApprovalAfterWarning = true;
                        return;
                    }

                    ProceedApproval();
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
                        clientInfo.clientName,
                        false,
                        string.IsNullOrEmpty(clientInfo.rejectionEvaluationText)
                            ? "No evaluation provided."
                            : clientInfo.rejectionEvaluationText,
                        rejectionReputationText,
                        rejectionReputationValue,
                        rejectionDreamDialogueText,
                        rejectionSanityText,
                        rejectionSanityValue,
                        rejectMainHeadline,
                        rejectSubHeadline,
                        rejectObituary
                    );

                    folderPanel.gameObject.SetActive(false);

                    QuestionsManager qm = FindAnyObjectByType<QuestionsManager>();
                    if (qm != null)
                    {
                        qm.ResetAll(); 
                    }

                    dialogue.SetAllowReplay(false); 

                    LeaveAfterDialogue();
                },
                // CLOSE
                () =>
                {
                    folderPanel.gameObject.SetActive(false);
                }
            );
        }

        if (customDialogueLines != null && customDialogueLines.Length > 0)
            dialogue.StartDialogue(customDialogueLines);
        else
            dialogue.StartDialogue();

        dialogue.OnDialogueComplete += EnableOpenFolderButton; // ✅ Subscribe once

        QuestionsManager qm = FindAnyObjectByType<QuestionsManager>();
        if (qm != null)
            qm.SetClient(this);
    }

    private void ShowWarning()
    {
        if (warningImageDisplay != null)
            warningImageDisplay.gameObject.SetActive(true);

        if (warningCloseButton != null)
            warningCloseButton.gameObject.SetActive(true);

        if (folderPanel != null && folderPanel.openFolderButton != null)
            folderPanel.openFolderButton.interactable = false; 

        if (sfxAudioSource != null && errorSound != null)
            sfxAudioSource.PlayOneShot(errorSound, errorVolume);
    }

    private void OnExited()
    {
        gameObject.SetActive(false);

        if (manager != null)
            manager.OnClientLeft();
    }

    private bool ValidateClientInfo()
    {
        Debug.Log($"DOB1: {clientInfo.dateOfBirth}");
        Debug.Log($"DOB2: {clientInfo.dateofBirth2}");

        bool lastNameMatches = false;

        string[] splitName = clientInfo.clientName.Trim().Split(' ');
        if (splitName.Length > 1)
        {
            string clientLastName = splitName[1].Trim();
            lastNameMatches = clientLastName.Equals(clientInfo.LastName.Trim(), System.StringComparison.OrdinalIgnoreCase);
        }

        bool dobMatches = clientInfo.dateOfBirth.Trim() == clientInfo.dateofBirth2.Trim();

        return lastNameMatches && dobMatches;
    }

    private void ProceedApproval()
    {
        if (clientInfo != null && folderPanel != null)
            folderPanel.gameObject.SetActive(false);

        CashRegister cashRegister = FindAnyObjectByType<CashRegister>();
        if (cashRegister != null)
        {
            cashRegister.AssignClient(this);
            cashRegister.gameObject.SetActive(true);
        }
    }

    private void OnWarningClosed()
    {
        if (warningImageDisplay != null)
            warningImageDisplay.gameObject.SetActive(false);

        if (warningCloseButton != null)
            warningCloseButton.gameObject.SetActive(false);

        if (waitingApprovalAfterWarning)
        {
            waitingApprovalAfterWarning = false;
            ProceedApproval();
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
        if (hasCompletedCashRegister)
            return;

        hasCompletedCashRegister = true;

        answersComponent.ShowAnswer(
        string.IsNullOrEmpty(approvalResponse)
            ? "Loan approved."
            : approvalResponse
    );

    ChoiceResults.RecordDecision(
        clientInfo.clientName,
        true,
        string.IsNullOrEmpty(clientInfo.approvalEvaluationText)
            ? "No evaluation provided."
            : clientInfo.approvalEvaluationText,
        approvalReputationText,
        approvalReputationValue,
        approvalDreamDialogueText,
        approvalSanityText,
        approvalSanityValue,
        approveMainHeadline,
        approveSubHeadline,
        approveObituary
    );
        QuestionsManager qm = FindAnyObjectByType<QuestionsManager>();
        if (qm != null)
        {
            qm.ResetAll();
        }

        dialogue.SetAllowReplay(false);

        LeaveAfterDialogue();

        if (MonitorCounter.Instance != null)
            MonitorCounter.Instance.DecreaseApprovalCount();
    }
    private void EnableOpenFolderButton()
    {
        if (folderPanel != null && folderPanel.openFolderButton != null)
        {
            folderPanel.openFolderButton.gameObject.SetActive(true);
            folderPanel.openFolderButton.interactable = true;
        }

        dialogue.OnDialogueComplete -= EnableOpenFolderButton; 
    }

    public bool HasReachedTarget()
    {
        return hasReachedTarget;
    }

    public bool IsLeaving()
    {
        return isLeaving;
    }

    private void PlayWalkingSound()
    {
        if (walkingAudioSource != null && walkingSound != null)
        {
            walkingAudioSource.volume = walkingVolume;
            walkingAudioSource.clip = walkingSound;
            walkingAudioSource.Play();
        }
    }

    private void StopWalkingSound()
    {
        if (walkingAudioSource != null)
            walkingAudioSource.Stop();
    }
}
