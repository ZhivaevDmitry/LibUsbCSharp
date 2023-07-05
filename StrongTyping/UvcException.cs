using System;

namespace LibUvc.StrongTyping
{
    public sealed class UvcException : Exception
    {
        public readonly UvcError uvcError;

        public UvcException(UvcError uvcError_) : base("Uvc error: " + uvcError_)
        {
            uvcError = uvcError_;
        }
    }
}

