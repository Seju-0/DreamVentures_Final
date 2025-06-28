using System.Collections.Generic;
using UnityEngine;

public static class ChoiceResults
{
    public struct ClientDecision
    {
        public string clientName;
        public bool approved;
        public string evaluationText;
        public string reputationMetrics;

        public ClientDecision(string clientName, bool approved, string evaluationText, string reputationMetrics)
        {
            this.clientName = clientName;
            this.approved = approved;
            this.evaluationText = evaluationText;
            this.reputationMetrics = reputationMetrics;
        }
    }

    public static List<ClientDecision> decisions = new List<ClientDecision>();

    public static void RecordDecision(string clientName, bool approved, string evaluationText, string reputationMetrics)
    {
        decisions.Add(new ClientDecision(clientName, approved, evaluationText, reputationMetrics));
    }

    public static void Clear()
    {
        decisions.Clear();
    }
}
