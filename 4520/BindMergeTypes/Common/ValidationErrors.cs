namespace Common
{
    public interface IValidationError
    {
        public string Code { get; }
    }

    public record ValidationErrors(IReadOnlyCollection<IValidationError> Errors);

    public record StringLengthValidationError(string Code) : IValidationError;
}
