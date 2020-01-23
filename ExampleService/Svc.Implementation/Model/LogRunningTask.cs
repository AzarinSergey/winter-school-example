using System;

namespace Svc.Implementation.Model
{
    public class LongRunningTask
    {
        public int Id { get; set; }

        public string TaskName { get; set; }

        public string Result { get; set; }

        public int RunningTimeSeconds { get; set; }

        public string Status { get; set; }
    }

    public enum LongRunningTaskStatus
    {
        InProgress = 1,
        Completed = 2
    }
}
