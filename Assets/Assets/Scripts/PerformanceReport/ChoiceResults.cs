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
        public string dreamDialogueText;
        public string sanityText;

        public ClientDecision(
            string clientName,
            bool approved,
            string evaluationText,
            string reputationMetrics,
            string dreamDialogueText,
            string sanityText)
        {
            this.clientName = clientName;
            this.approved = approved;
            this.evaluationText = evaluationText;
            this.reputationMetrics = reputationMetrics;
            this.dreamDialogueText = dreamDialogueText;
            this.sanityText = sanityText;
        }
    }

    public static List<ClientDecision> decisions = new List<ClientDecision>();

    public static void RecordDecision(
        string clientName,
        bool approved,
        string evaluationText,
        string reputationMetrics,
        string dreamDialogueText,
        string sanityText)
    {
        decisions.Add(new ClientDecision(
            clientName,
            approved,
            evaluationText,
            reputationMetrics,
            dreamDialogueText,
            sanityText));
    }

    public static void Clear()
    {
        decisions.Clear();
    }
}
