namespace TypingCatStatistics
{
    [Command(PackageIds.MainToolWindowCommand)]
    internal sealed class MainToolWindowCommand : BaseCommand<MainToolWindowCommand>
    {
        protected override Task ExecuteAsync(OleMenuCmdEventArgs e)
        {
            return MainToolWindow.ShowAsync();
        }
    }
}
