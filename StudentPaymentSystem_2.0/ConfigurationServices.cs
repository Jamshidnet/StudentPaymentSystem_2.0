using Microsoft.AspNetCore.RateLimiting;
using System.Threading.RateLimiting;

namespace StudentPaymentSystem_2._0
{
    public static class ConfigurationServices
    {
        public static void AddRateLimiters(WebApplicationBuilder builder)
        {
            builder.Services.AddRateLimiter(options =>
            {
                options.GlobalLimiter = PartitionedRateLimiter.Create<HttpContext, string>(httpContext =>
                    RateLimitPartition.GetSlidingWindowLimiter(
                        partitionKey: httpContext.User.Identity?.Name ?? httpContext.Request.Headers.Host.ToString(),
                        factory: partition => new SlidingWindowRateLimiterOptions
                        {
                            AutoReplenishment = false,
                            PermitLimit = 5,
                            QueueLimit = 0,
                            Window = TimeSpan.FromSeconds(30),
                            SegmentsPerWindow = 2
                        }));
            });


            builder.Services.AddRateLimiter(options =>
            {
                options.AddTokenBucketLimiter("Token", c =>
                {
                    c.TokenLimit = 5;
                    c.QueueLimit = 0;
                    c.TokensPerPeriod = 2;
                    c.ReplenishmentPeriod = TimeSpan.FromSeconds(20);
                });
            });


            builder.Services.AddRateLimiter(options =>
            {
                options.AddSlidingWindowLimiter("Sliding", s =>
                {
                    s.PermitLimit = 6;
                    s.QueueLimit = 0;
                    s.AutoReplenishment = false;
                    s.SegmentsPerWindow = 2;
                    s.Window = TimeSpan.FromSeconds(50);
                });
            });

        }
    }
}
