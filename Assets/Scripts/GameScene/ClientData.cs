using UnityEngine;

public class ClientData : MonoBehaviour
{
    [Header("ID Info")]
    public string clientName;
    public string dateOfBirth;

    [Header("Loan Info")]
    public string requestedAmount;
    public string paymentDate;
    [TextArea] public string loanPurpose;

    [Header("Personal Details")]
    public string FirstName;
    public string LastName;
    public string Gender;
    public string Title;
    public string OtherNames;

    [Header("Business Info")]
    public string BusinessName;
    public string BusinessAddress;

    [Header("Employment Info")]
    public string employerName;
    public string workplaceAddress;

    [Header("Images")]
    public Sprite idPhoto;
    public Sprite backgroundSprite;
    public Sprite paperSprite;

    [Header("Folder Panel Reference")]
    public FolderPanel folderPanel;

    [System.Serializable]
    public class QA
    {
        public string question;
        public string answer;
    }

    [Header("Questions and Answers")]
    public QA[] questionList;
}
