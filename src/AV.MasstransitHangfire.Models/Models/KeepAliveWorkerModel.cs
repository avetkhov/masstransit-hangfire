using AV.MasstransitHangfire.Common.Enums;

namespace AV.MasstransitHangfire.Models.Models;

public class KeepAliveWorkerModel
{
    public Guid Id { get; set; }
    public WorkerStatus Status { get; set; }
    public DateTime Timestamp { get; set; } = DateTime.UtcNow;
}