namespace Application.Common.Validation
{
    public record ValidationResult
    {
        public bool IsSuccessful { get; set; } = true;
        public string[] Errors { get; init; }

        public static ValidationResult Success => new();

        public static ValidationResult Fail(string[] errors)
            => new() { IsSuccessful = false, Errors = errors };

        public static ValidationResult Fail(string error) 
            => new() { IsSuccessful = false, Errors = new string[] { error } };
    }
}
