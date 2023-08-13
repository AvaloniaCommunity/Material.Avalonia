using System;

namespace Numerge
{
    public class MergeAbortedException : Exception
    {
        public MergeAbortedException(string message) : base(message)
        {
            
        }
    }
}