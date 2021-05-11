namespace Application.Common.Models
{
    public record CQRSResponse
    {
        public bool IsSuccessful { get; set; } = true;
        public string ErrorMessage { get; init; }

        public CQRSResponse(string error)
        {
            IsSuccessful = false;
            ErrorMessage = error;
        }

        public CQRSResponse()
        {
        }
    }
}
