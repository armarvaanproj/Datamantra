using Microsoft.ApplicationInsights;
using Microsoft.ApplicationInsights.DataContracts;
using Microsoft.ApplicationInsights.Extensibility;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Configuration;

namespace Datamantra.Web.Extensions
{
    public static class TelemetryLogger
    {
        private static readonly TelemetryClient telemetryClient = new TelemetryClient();

        static TelemetryLogger()
        {
            try
            {
                telemetryClient.InstrumentationKey = "4fe89551-9b47-4c6c-92f8-6876331f3344";//WebConfigurationManager.AppSettings["InsightKey"];
                TelemetryConfiguration.Active.InstrumentationKey = telemetryClient.InstrumentationKey;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void TrackException(Exception ex, string message)
        {
            telemetryClient.TrackException(ex, new Dictionary<string, string>
                    {{"Stack Trace:", ex.StackTrace},{"message:", message}});
        }

        public static void Write(string message, TraceEventType eventType, string category)
        {
            switch (eventType)
            {
                case TraceEventType.Error:
                    telemetryClient.TrackException(new Exception(message), new Dictionary<string, string> { { "Category", category } });
                    break;
                case TraceEventType.Information:
                    telemetryClient.TrackTrace(message, SeverityLevel.Information, new Dictionary<string, string> { { "Category", category } });
                    break;
                default:
                    telemetryClient.TrackTrace(message, SeverityLevel.Information, new Dictionary<string, string> { { "Category", category } });
                    break;
            }
        }

        public static void TrackTrace(string message, Dictionary<string, string> property)
        {
            try
            {
                telemetryClient.TrackTrace(message, SeverityLevel.Information, property);
            }
            catch (Exception ex)
            {
                TrackException(ex, "Error writing trace in App Insights");
            }
        }

        public static void TrackTrace(string message)
        {
            try
            {
                telemetryClient.TrackTrace(message, SeverityLevel.Information);
            }
            catch (Exception ex)
            {
                TrackException(ex, "Error writing trace in App Insights");
            }
        }
    }

}