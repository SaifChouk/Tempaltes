namespace HttpClients.Config
{
    public interface IRetryPolicyConfig
    {
        int RetryCount { get; set; }
    }
}
