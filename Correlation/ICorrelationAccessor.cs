namespace Correlation
{
    public interface ICorrelationAccessor
    {
        string GetId();
        void SetId(string id);
    }
}