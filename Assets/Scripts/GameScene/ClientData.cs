using UnityEngine;

public class ClientData : MonoBehaviour
{
    [Header("Folder Panel Reference")]
    public FolderPanel folderPanel;

    [System.Serializable]
    public class QA
    {
        public string question;
        public string answer;
    }
}
