using GACHSLApi.Entities;
using GACHSLApi.Enums;

public class Meeting : BaseEntity
{
    public string MeetingTitle { get; set; } = string.Empty;
    public DateTime MeetingDate { get; set; }
    public TimeSpan MeetingTime { get; set; }
    public string Venue { get; set; } = string.Empty;
    public string Agenda { get; set; } = string.Empty;
    public string? Description { get; set; }
    public MeetingType MeetingType { get; set; }
    public MeetingStatus Status { get; set; }
}