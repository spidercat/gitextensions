using System.Diagnostics;
using GitCommands;
using Microsoft.ApplicationInsights.Channel;
using Microsoft.ApplicationInsights.Extensibility;

namespace GitUI.Infrastructure.Telemetry
{
    internal class AppInfoTelemetryInitializer : ITelemetryInitializer
    {
        private readonly bool _isDirty;

        public AppInfoTelemetryInitializer(bool isDirty)
        {
            _isDirty = isDirty;
        }

        public void Initialize(ITelemetry telemetry)
        {
            telemetry.Context.Component.Version = AppSettings.ProductVersion;
            telemetry.Context.GlobalProperties["IsRelease"] = (!_isDirty).ToString();

            // TODO: differentiate msi / portable?
            telemetry.Context.GlobalProperties["Environment"] = "local";

            // Always default to development if we're in the debugger
            if (Debugger.IsAttached)
            {
                telemetry.Context.GlobalProperties["Environment"] = "development";
            }
        }
    }
}
