using System.Collections.Generic;
using GitCommands;

namespace GitUI.Infrastructure.Telemetry
{
    public sealed class AppStartDiagnosticsReporter
    {
        private readonly ISshPathLocator _sshPathLocator = new SshPathLocator();

        public void Report()
        {
            string sshClient;
            var sshPath = _sshPathLocator.Find(AppSettings.GitBinDir);
            if (string.IsNullOrEmpty(sshPath))
            {
                sshClient = "OpenSSH";
            }
            else if (GitCommandHelpers.Plink())
            {
                sshClient = "PuTTY";
            }
            else
            {
                sshClient = "Other";
            }

            DiagnosticsClient.TrackEvent("AppStart",
                new Dictionary<string, string>
                {
                    { "Portable", AppSettings.IsPortable().ToString() },
                    { "Git", GitVersion.Current.ToString() },
                    { "SSH", sshClient },
                    { nameof(AppSettings.CurrentTranslation), AppSettings.CurrentTranslation },
                    { nameof(AppSettings.StartWithRecentWorkingDir), AppSettings.StartWithRecentWorkingDir.ToString() },
                });
        }
    }
}
