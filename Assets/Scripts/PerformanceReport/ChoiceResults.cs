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
        public int reputationValue;
        public string dreamDialogueText;
        public string sanityText;
        public int sanityValue;
        public string headline; // << NEW FIELD

        public ClientDecision(
            string clientName,
            bool approved,
            string evaluationText,
            string reputationMetrics,
            int reputationValue,
            string dreamDialogueText,
            string sanityText,
            int sanityValue,
            string headline)
        {
            this.clientName = clientName;
            this.approved = approved;
            this.evaluationText = evaluationText;
            this.reputationMetrics = reputationMetrics;
            this.reputationValue = reputationValue;
            this.dreamDialogueText = dreamDialogueText;
            this.sanityText = sanityText;
            this.sanityValue = sanityValue;
            this.headline = headline;
        }
    }

    public static List<ClientDecision> decisions = new List<ClientDecision>();

    public static int currentSanity = 100;
    public static int currentReputation = 50;

    public static void RecordDecision(
        string clientName,
        bool approved,
        string evaluationText,
        string reputationMetrics,
        int reputationValue,
        string dreamDialogueText,
        string sanityText,
        int sanityValue,
        string headline) 
    {
        decisions.Add(new ClientDecision(
            clientName,
            approved,
            evaluationText,
            reputationMetrics,
            reputationValue,
            dreamDialogueText,
            sanityText,
            sanityValue,
            headline));

        currentSanity += sanityValue;
        currentReputation += reputationValue;
    }

    public static void Clear()
    {
        decisions.Clear();
        currentSanity = 100;
        currentReputation = 50;
    }
}
