using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Client Information", menuName = "ScriptableObjects/ClientSO")]
public class ClientSO : ScriptableObject
{
    [Header("ID Info")]
    public string clientName;
    public string dateOfBirth;

    [Header("Loan Info")]
    public string requestedAmount;
    public string paymentDate;

    [TextArea]
    public string loanPurpose;

    [Header("Personal Details")]
    public string FirstName;
    public string LastName;
    public string dateofBirth2;
    public string Gender;
    public string Title;
    public string OtherNames;

    [Header("Employment Info")]
    public string employerName;
    public string workplaceAddress;

    [Header("Business Info")]
    public string BusinessName;
    public string BusinessAddress;

    [Header("Images")]
    public Sprite idPhoto;
    public Sprite backgroundSprite;
    public Sprite LoanSprite;

    [Header("Evaluation Texts")]
    [TextArea]
    public string approvalEvaluationText;

    [TextArea]
    public string rejectionEvaluationText;
}
