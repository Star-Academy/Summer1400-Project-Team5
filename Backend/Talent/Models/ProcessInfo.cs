using System.Threading;

namespace Talent.Models
{
    public class ProcessInfo
    {
        public ProcessInfo()
        {
            CancellationTokenSource = new CancellationTokenSource();
            CurrentProcessIndex = 0;
        }

        public CancellationTokenSource CancellationTokenSource
        {
            get;
        }
        
        public CancellationToken CancellationToken => CancellationTokenSource.Token;
        public int CurrentProcessIndex { get; set; }
        
    }
}