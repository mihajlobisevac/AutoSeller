namespace Application.Common.Models
{
    public record CQRSResponse
    {
        public bool IsSuccessful { get; init; } = true;
        public string ErrorMessage { get; init; }
    }
}
