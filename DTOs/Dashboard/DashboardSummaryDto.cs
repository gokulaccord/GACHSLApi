namespace GACHSLApi.DTOs.Dashboard
{
    public class DashboardSummaryDto
    {
        public int TotalMembers { get; set; }

        public int Owners { get; set; }

        public int Tenants { get; set; }

        public int ActiveMembers { get; set; }

        public int InactiveMembers { get; set; }

        public int TotalFlats { get; set; }

        public int OccupiedFlats { get; set; }

        public int VacantFlats { get; set; }
    }
}