

namespace Talent.Models
{
    public class ProcessInfoModel
    {
        public int CurrentProcessIndex { get; }

        public ProcessInfoModel(ProcessInfo? processInfo)
        {
            CurrentProcessIndex = -1;
            if (processInfo != null) CurrentProcessIndex = processInfo.CurrentProcessIndex;
        }
    }
}