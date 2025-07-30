using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class FolderPanel : MonoBehaviour
{
    [Header("Text Fields")]
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI dobText;
    public TextMeshProUGUI dob2Text;
    public TextMeshProUGUI amountText;
    public TextMeshProUGUI paymentDateText;
    public TextMeshProUGUI loanPurposeText;
    public TextMeshProUGUI FirstNameText;
    public TextMeshProUGUI LastNameText;
    public TextMeshProUGUI GenderText;
    public TextMeshProUGUI TitleText;
    public TextMeshProUGUI OtherNamesText;
    public TextMeshProUGUI BusinessText;
    public TextMeshProUGUI BusinessAddressText;
    public TextMeshProUGUI EmployerText;
    public TextMeshProUGUI WorkplaceAddressText;

    [Header("Image Fields")]
    public Image idImage;
    public Image backgroundImage;
    public Image paperImage;

    [Header("Buttons")]
    public Button approveButton;
    public Button rejectButton;
    public Button closeButton;
    public Button openFolderButton;

    private ClientSO currentClientData;

    public void UpdatePanel(ClientSO data)
    {
        currentClientData = data;

        if (data == null) return;

        if (nameText) nameText.text = data.clientName;
        if (dobText) dobText.text = data.dateOfBirth;
        if (dob2Text) dob2Text.text = data.dateofBirth2;
        if (amountText) amountText.text = data.requestedAmount;
        if (paymentDateText) paymentDateText.text = data.paymentDate;
        if (loanPurposeText) loanPurposeText.text = data.loanPurpose;
        if (FirstNameText) FirstNameText.text = data.FirstName;
        if (LastNameText) LastNameText.text = data.LastName;
        if (GenderText) GenderText.text = data.Gender;
        if (TitleText) TitleText.text = data.Title;
        if (OtherNamesText) OtherNamesText.text = data.OtherNames;
        if (BusinessText) BusinessText.text = data.BusinessName;
        if (BusinessAddressText) BusinessAddressText.text = data.BusinessAddress;
        if (EmployerText) EmployerText.text = data.employerName;
        if (WorkplaceAddressText) WorkplaceAddressText.text = data.workplaceAddress;

        if (idImage && data.idPhoto) idImage.sprite = data.idPhoto;
        if (backgroundImage && data.backgroundSprite) backgroundImage.sprite = data.backgroundSprite;
        if (paperImage && data.LoanSprite) paperImage.sprite = data.LoanSprite;
    }

    public void SetupButtonCallbacks(System.Action onApprove, System.Action onReject, System.Action onClose)
    {
        if (approveButton)
        {
            approveButton.onClick.RemoveAllListeners();
            approveButton.onClick.AddListener(() => onApprove?.Invoke());
        }

        if (rejectButton)
        {
            rejectButton.onClick.RemoveAllListeners();
            rejectButton.onClick.AddListener(() => onReject?.Invoke());
        }

        if (closeButton)
        {
            closeButton.onClick.RemoveAllListeners();
            closeButton.onClick.AddListener(() => onClose?.Invoke());
        }
    }
}
