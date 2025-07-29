using System.Collections.Generic;

public static class ChoiceResults
{
    public struct ClientDecision
    {
        public int dayNumber;
        public string clientName;
        public bool approved;
        public string evaluationText;
        public string reputationMetrics;
        public int reputationValue;
        public string dreamDialogueText;
        public string sanityText;
        public int sanityValue;
        public string mainHeadline;
        public string subHeadline;
        public string obituary;

        public ClientDecision(
            int dayNumber,
            string clientName,
            bool approved,
            string evaluationText,
            string reputationMetrics,
            int reputationValue,
            string dreamDialogueText,
            string sanityText,
            int sanityValue,
            string mainHeadline,
            string subHeadline,
            string obituary)
        {
            this.dayNumber = dayNumber;
            this.clientName = clientName;
            this.approved = approved;
            this.evaluationText = evaluationText;
            this.reputationMetrics = reputationMetrics;
            this.reputationValue = reputationValue;
            this.dreamDialogueText = dreamDialogueText;
            this.sanityText = sanityText;
            this.sanityValue = sanityValue;
            this.mainHeadline = mainHeadline;
            this.subHeadline = subHeadline;
            this.obituary = obituary;
        }
    }

    public static int currentDay = 1;
    public static List<ClientDecision> allDecisions = new List<ClientDecision>();

    public static int currentSanity = 100;
    public static int currentReputation = 50;
    public static int startingReputationAtDay = 50;
    public static int warningsThisDay = 0;

    public static void RecordDecision(
        string clientName,
        bool approved,
        string evaluationText,
        string reputationMetrics,
        int reputationValue,
        string dreamDialogueText,
        string sanityText,
        int sanityValue,
        string mainHeadline,
        string subHeadline,
        string obituary)
    {
        var decision = new ClientDecision(
            currentDay,
            clientName,
            approved,
            evaluationText,
            reputationMetrics,
            reputationValue,
            dreamDialogueText,
            sanityText,
            sanityValue,
            mainHeadline,
            subHeadline,
            obituary
        );

        allDecisions.Add(decision);
        currentSanity += sanityValue;
        currentReputation += reputationValue;
    }

    public static List<ClientDecision> GetDecisionsForCurrentDay()
    {
        return allDecisions.FindAll(d => d.dayNumber == currentDay);
    }

    public static List<ClientDecision> GetDecisionsForPreviousDay()
    {
        return allDecisions.FindAll(d => d.dayNumber == currentDay - 1);
    }

    public static void StartNewDay()
    {
        startingReputationAtDay = currentReputation;
        warningsThisDay = 0;
        currentDay++;
    }
    public static void RegisterWarning()
    {
        warningsThisDay++;
        currentReputation -= 1;
    }
    public static void ResetAllData()
    {
        allDecisions.Clear();
        currentDay = 1;
        currentSanity = 100;
        currentReputation = 50;
        startingReputationAtDay = 50;
        warningsThisDay = 0;
    }
}
