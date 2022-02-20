namespace SmsChallengeBackend.Business.Exception
{
    public class InvalidPageSizeException : SmsException
    {
        public InvalidPageSizeException(int pageSize) : base(string.Format("Invalid page size was given '{0}'", pageSize))
        {

        }
    }
}
