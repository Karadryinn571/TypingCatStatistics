using Microsoft.VisualStudio.Imaging;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace TypingCatStatistics
{
    public class MainToolWindow : BaseToolWindow<MainToolWindow>
    {
        public override string GetTitle(int toolWindowId) => "Typing Cat Statistics";

        public override Type PaneType => typeof(Pane);

        public override Task<FrameworkElement> CreateAsync(int toolWindowId, CancellationToken cancellationToken)
        {
            return Task.FromResult<FrameworkElement>(new MainToolWindowControl());
        }

        [Guid("f57d5d12-45d9-4268-aa5f-6d41952e59d0")]
        internal class Pane : ToolkitToolWindowPane
        {
            public Pane()
            {
                BitmapImageMoniker = KnownMonikers.ApplicationEnvironment;
            }
        }
    }
}
