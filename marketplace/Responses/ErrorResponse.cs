namespace marketplace.Responses
{
    public class ErrorResponse
    {
        public List<string> Messages { get; set; }

        public ErrorResponse()
        {
            Messages = new List<string>();
        }
    }
}
