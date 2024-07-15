using System.ComponentModel.DataAnnotations;

namespace AV.MasstransitHangfire.WorkerServer.Api.Options;

public class KeepAliveWorkerOptions
{
    [Required(ErrorMessage = "TimeoutInSec is null or empty.")]
    public int TimeoutInSec { get; set; }
}