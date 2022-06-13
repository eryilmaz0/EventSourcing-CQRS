namespace Command.API.ApiResponse;

public class ApiResponse<TResponse>
{
    public Guid TrackId { get; }
    public DateTime TransactionDate { get; }
    public TResponse Response { get; }

    public ApiResponse(Guid trackId, TResponse response)
    {
        TransactionDate = DateTime.UtcNow;
        TrackId = trackId;
        Response = response;
    }
}