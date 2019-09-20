namespace CinemAPI.Domain.Contracts.Models
{
    public class NewCreationSummary
    {
        public NewCreationSummary(bool isCreated)
        {
            this.IsCreated = isCreated;
        }

        public NewCreationSummary(bool status, string msg)
            : this(status)
        {
            this.Message = msg;
        }

        public string Message { get; set; }

        public bool IsCreated { get; set; }
    }
}
