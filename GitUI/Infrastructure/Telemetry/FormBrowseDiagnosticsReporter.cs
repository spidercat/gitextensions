using System.Collections.Generic;
using GitCommands;
using GitUI.CommandsDialogs;

namespace GitUI.Infrastructure.Telemetry
{
    internal class FormBrowseDiagnosticsReporter
    {
        private readonly FormBrowse _owner;

        public FormBrowseDiagnosticsReporter(FormBrowse owner)
        {
            _owner = owner;
        }

        public void Report()
        {
            DiagnosticsClient.TrackEvent($"{_owner.GetType().Name}Start",
                new Dictionary<string, string>
                {
                    // layout
                    { "ShowLeftPanel", _owner.MainSplitContainer.Panel1Collapsed.ToString() },
                    { nameof(AppSettings.ShowSplitViewLayout), AppSettings.ShowSplitViewLayout.ToString() },
                    { nameof(AppSettings.CommitInfoPosition), AppSettings.CommitInfoPosition.ToString() },

                    // revision grid
                    { nameof(AppSettings.ShowAuthorAvatarColumn), AppSettings.ShowAuthorAvatarColumn.ToString() },
                    { nameof(AppSettings.ShowAuthorNameColumn), AppSettings.ShowAuthorNameColumn.ToString() },
                    { nameof(AppSettings.ShowBuildStatusIconColumn), AppSettings.ShowBuildStatusIconColumn.ToString() },
                    { nameof(AppSettings.ShowBuildStatusTextColumn), AppSettings.ShowBuildStatusTextColumn.ToString() },

                    // commit info panel
                    { nameof(AppSettings.ShowAuthorAvatarInCommitInfo), AppSettings.ShowAuthorAvatarInCommitInfo.ToString() },
                    { nameof(AppSettings.ShowGpgInformation), AppSettings.ShowGpgInformation.ValueOrDefault.ToString() },

                    // other
                    { nameof(AppSettings.ShowAheadBehindData), AppSettings.ShowAheadBehindData.ToString() },
                    { nameof(AppSettings.CurrentTranslation), AppSettings.CurrentTranslation },
                    { nameof(AppSettings.ShowGitStatusInBrowseToolbar), AppSettings.ShowGitStatusInBrowseToolbar.ToString() },
                    { nameof(AppSettings.ShowGitStatusForArtificialCommits), AppSettings.ShowGitStatusForArtificialCommits.ToString() },
                    { nameof(AppSettings.RevisionGraphShowWorkingDirChanges), AppSettings.RevisionGraphShowWorkingDirChanges.ToString() },
                });
        }
    }
}