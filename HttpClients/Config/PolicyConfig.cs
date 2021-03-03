namespace HttpClients.Config
{
    public class PolicyConfig : ICircuitBreakerPolicyConfig, IRetryPolicyConfig
    {
        public int RetryCount { get; set; } = 5;
        public int BreakDuration { get; set; } = 30;
    }
}
