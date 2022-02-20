namespace SmsChallengeBackend.Business.Exception
{
    public abstract class SmsException: System.Exception
    {
        public SmsException(string message) : base(message)
        {
            
        }
    }
}
