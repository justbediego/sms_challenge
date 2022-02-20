namespace SmsChallengeBackend.Business.Exception
{
    public class InvalidInputException : SmsException
    {
        public InvalidInputException(string input, string reason) : base(string.Format("Invalid input for '{0}': {1}", input, reason))
        {

        }
    }
}
