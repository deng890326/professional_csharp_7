using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoggingSample
{
    internal class SampleController
    {
        public SampleController(ILogger<SampleController> logger) =>
            this.logger = logger;

        public async Task NetworkRequestSampleAsync(string url)
        {
            const string TAG = nameof(NetworkRequestSampleAsync);
            try
            {
                logger.LogInformation(LoggingEvents.Networking,
                    "{0} started with {1}", TAG, url);
                var client = new HttpClient();
                string ret = await client.GetStringAsync(url);
                logger.LogInformation(LoggingEvents.Networking,
                    "{0} completed, received {1} characters",
                    TAG, ret.Length);
            }
            catch (Exception ex)
            {
                logger.LogError(LoggingEvents.Networking, ex,
                    "Error in {0}, message: {1}, result: {2}",
                    TAG, ex.Message, ex.HResult);
            }
        }

        public async Task NetworkRequestSampleUsingLogScopeAsync(string url)
        {
            const string TAG = nameof(NetworkRequestSampleAsync);
            using var scope = logger.BeginScope($"{TAG}, url: {url}");
            try
            {
                logger.LogInformation(LoggingEvents.Networking, "started");
                var client = new HttpClient();
                string ret = await client.GetStringAsync(url);
                logger.LogInformation(LoggingEvents.Networking,
                    "completed, received {0} characters", ret.Length);
            }
            catch (Exception ex)
            {
                logger.LogError(LoggingEvents.Networking, ex,
                    "Error, message: {0}, result: {1}",
                    ex.Message, ex.HResult);
            }
        }

        private readonly ILogger<SampleController> logger;
    }
}
