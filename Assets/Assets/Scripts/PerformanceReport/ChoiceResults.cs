using System.Collections.Generic;
using UnityEngine;

public class ChoiceResults : MonoBehaviour
{
    public class ClientDecision
    {
        public string clientName;
        public bool approved;
        public string evaluationText;
    }

    public static List<ClientDecision> decisions = new List<ClientDecision>();

    public static void RecordDecision(string clientName, bool approved, string evaluationText)
    {
        ClientDecision decision = new ClientDecision
        {
            clientName = clientName,
            approved = approved,
            evaluationText = evaluationText
        };

        decisions.Add(decision);
    }

    public static void Clear()
    {
        decisions.Clear();
    }
}
