namespace SmsChallengeBackend.Business.Exception
{
    public class EntityNotFoundException : SmsException
    {
        public EntityNotFoundException(string entityType) : base(string.Format("Was not able to find Entity of type '{0}'", entityType))
        {

        }
    }
}
