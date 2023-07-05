using System;
using System.Threading;
using LibUvc.StrongTyping;

namespace LibUvc.Management
{
    public sealed class UvcStreamManager : IDisposable
    {
        public event Action<UvcStreamManager> postInvalidate = delegate { };

        public UvcStreamManager
        (
            UvcStreamHandlePtr              streamHandlePtr_, 
            Action<UvcFramePtr.Readonly>    capturingThreadCallback_
        )
        {
            Auxiliary.VerifyNotNull(streamHandlePtr_);
            streamHandlePtr_.VerifyIsValid();

            streamHandlePtr = streamHandlePtr_;

            capturingThreadCallback = capturingThreadCallback_;

            capturingThread = (capturingThreadCallback != null) ? new Thread(RunCallbackThread) : null;            

            isValid = true;

            if (capturingThread == null) return;

            isPendingCapturingThreadFinish = false;

            capturingThread.Start();
        }

        public void Dispose()
        {           
            if (!isValid) return;

            isValid = false;
         
            try
            {
                if (capturingThread != null)
                {
                    isPendingCapturingThreadFinish = true;
                    capturingThread.Join();
                }
                else
                {
                    // Do nothing.
                }

                postInvalidate.Invoke(this);
            }
            finally
            {              
                Uvc.StreamClose(streamHandlePtr.readOnly);
                streamHandlePtr.Reset();
            }
        }

        /// <param name="timeoutMicroseconds_">
        /// &gt; 0 : Wait at most N microseconds; 0: Wait indefinitely; -1 : return immediately     
        /// </param>
        public void PollFrame(int timeoutMicroseconds_, UvcFramePtr OUT_framePtr_)
        {
            VerifyIsValid();

            if (capturingThread != null)
            {
                throw new Exception("Can't poll frame when started whith capturingThreadCallback!");
            }

            Uvc.GetFrame
            (
                streamHandlePtr.readOnly,
                OUT_framePtr_,
                timeoutMicroseconds_
            );
        }

        /////////////////////////////////////////////
        // Private
        /////////////////////////////////////////////
        
        private void RunCallbackThread()
        {
            while (!isPendingCapturingThreadFinish)
            {
                Uvc.GetFrame
                (
                    streamHandlePtr.readOnly,
                    framePtr,
                    0
                );

                capturingThreadCallback.Invoke(framePtr.readOnly);
            }           
        }

        private void VerifyIsValid()
        {
            if (!isValid) throw new Exception("UvcStreamManager is not valid!");
        }

        /////////////////////////////////////////////
        // Private
        /////////////////////////////////////////////

        private readonly UvcStreamHandlePtr streamHandlePtr;

        private readonly Action<UvcFramePtr.Readonly> capturingThreadCallback;

        private readonly Thread capturingThread;

        private readonly UvcFramePtr framePtr = new UvcFramePtr();

        private bool isValid;

        private volatile bool isPendingCapturingThreadFinish = false;
    }
}
