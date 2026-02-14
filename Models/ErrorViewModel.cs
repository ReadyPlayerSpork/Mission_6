// This file is part of the default ASP.NET Core template. It is used when an unhandled error occurs.

namespace Mission_6.Models;

/// <summary>
/// A simple model that holds data we want to show on the Error page.
/// The controller passes this to the view so the view can display the request ID (useful for debugging).
/// </summary>
public class ErrorViewModel
{
    // The request ID helps developers find the error in logs. It can be null if not available.
    public string? RequestId { get; set; }

    // This is a "computed property": it doesn't store a value. When you read ShowRequestId, it runs the code on the right.
    // It returns true only when RequestId has a non-empty value. We use it in the view to decide whether to show the request ID.
    public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
}
