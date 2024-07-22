namespace eCommerceBase.Domain.SeedWork
{
    public interface IResult
    {
        public bool Success { get; }
        public string Message { get; }
    }
}
