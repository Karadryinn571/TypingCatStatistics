global using Community.VisualStudio.Toolkit;
global using Microsoft.VisualStudio.Shell;
global using System;
global using Task = System.Threading.Tasks.Task;
using System.Runtime.InteropServices;
using System.Threading;

namespace TypingCatStatistics
{
    [PackageRegistration(UseManagedResourcesOnly = true, AllowsBackgroundLoading = true)]
    [InstalledProductRegistration(Vsix.Name, Vsix.Description, Vsix.Version)]
    [ProvideToolWindow(typeof(MainToolWindow.Pane), PositionX = 400, PositionY = 400, Width = 360, Height = 265, Style = VsDockStyle.AlwaysFloat)]
    [ProvideMenuResource("Menus.ctmenu", 1)]
    [Guid(PackageGuids.TypingCatStatisticsString)]
    public sealed class TypingCatStatisticsPackage : ToolkitPackage
    {
        protected override async Task InitializeAsync(CancellationToken cancellationToken, IProgress<ServiceProgressData> progress)
        {
            await JoinableTaskFactory.SwitchToMainThreadAsync(cancellationToken);
            await this.RegisterCommandsAsync();
            this.RegisterToolWindows();
        }
    }
}
