namespace GACHSLApi.DTOs.Dashboard
{
    public class DashboardSummaryDto
    {
        public int TotalFlats { get; set; }

        public int TotalShops { get; set; }


        // Members
        public int TotalMembers { get; set; }

        public int ActiveMembers { get; set; }


        // Consent
        public int ConsentYes { get; set; }

        public int ConsentNo { get; set; }

        public int ConsentPending { get; set; }

        public decimal ConsentPercentage { get; set; }


        // Meetings
        public int TotalMeetings { get; set; }


        // Notices
        public int TotalNotices { get; set; }


        // Documents
        public int TotalDocuments { get; set; }


        // Redevelopment
        public int CurrentStage { get; set; }

        public int TotalStages { get; set; }
    }
}