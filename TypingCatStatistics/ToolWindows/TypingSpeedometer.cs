using System.Windows.Threading;
using Microsoft.VisualStudio.Text;

namespace TypingCatStatistics
{
    public class TypingSpeedometer
    {
        public bool Typing => _typing;
        public double Speed => _speed;
        public double TopSpeed => _topSpeed;

        private bool _typing;
        private double _speed;
        private double _topSpeed;
        private int _changes;
        private readonly DispatcherTimer _timer;
        private readonly RollingQueue _speedQueue;
        private readonly RollingQueue _typingQueue;
        private DocumentView _document;

        public event EventHandler Change;

        public TypingSpeedometer(DispatcherTimer timer, int speedBuffer, int typingBuffer)
        {
            _timer = timer;
            _timer.Tick += OnTick;
            _speedQueue = new(speedBuffer);
            _typingQueue = new(typingBuffer);

            VS.Events.DocumentEvents.BeforeDocumentWindowShow += RegisterDocument;
            RegisterDocument(GetActiveDocument());
        }

        private DocumentView GetActiveDocument()
        {
            DocumentView document = null;
            ThreadHelper.JoinableTaskFactory.Run(async () => document = await VS.Documents.GetActiveDocumentViewAsync());
            return document;
        }

        private void RegisterDocument(DocumentView document)
        {
            if (document?.TextBuffer == null) return;
            if (_document != null) _document.TextBuffer.Changed -= OnChange;
            _document = document;
            _document.TextBuffer.Changed += OnChange;
        }

        private void OnChange(object sender, TextContentChangedEventArgs args)
        {
            _changes++;
            Change?.Invoke(null, null);
        }

        private void OnTick(object sender, EventArgs args)
        {
            _typingQueue.Enqueue(_changes);
            _typing = _typingQueue.Sum != 0;

            _speedQueue.Enqueue(_changes);
            _changes = 0;

            _speed = _speedQueue.Average;

            _topSpeed = Math.Max(_speed, _topSpeed);
        }
    }
}
