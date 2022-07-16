namespace Beacons.Models.Api
{
    public class ApiResponse<T>
    {
        public T? Data { get; init; }
        public IReadOnlyList<string> Errors { get; }

        public bool Successful => !Errors.Any();

        public ApiResponse() : this(Array.Empty<string>())
        {
        }

        public ApiResponse(IReadOnlyList<string> errors)
        {
            Errors = errors;
        }
    }
}