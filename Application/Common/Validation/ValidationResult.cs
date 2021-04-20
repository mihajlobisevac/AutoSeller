namespace Application.Common.Validation
{
    public record ValidationResult
    {
        public bool IsSuccessful { get; set; } = true;
        public string ErrorMessage { get; init; }

        public static ValidationResult Success => new();
        public static ValidationResult Fail(string error) => new() { IsSuccessful = false, ErrorMessage = error };
    }
}
