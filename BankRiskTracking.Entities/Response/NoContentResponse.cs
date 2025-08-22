namespace BankRiskTracking.Entities.Response
{
    public class NoContentResponse : Response 
    {
        public NoContentResponse() : base(true,"No Content")
        {

        }
        public static NoContentResponse Success()
        {

        return new NoContentResponse(); 
        }

    }
}
