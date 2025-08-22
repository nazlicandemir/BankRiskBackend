namespace BankRiskTracking.Entities.Response
{
    public  interface IResponse<T>
    {
        bool IsSuccess { get; }
        string Message { get; }

        public T Data { get; set; }
    }
}
