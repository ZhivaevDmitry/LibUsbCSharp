using System;

using LibUvc.StrongTyping;

namespace LibUvc.Management
{
    public sealed class UvcFrameDescriptor
    {
        /////////////////////////////////////////////
        // Public
        /////////////////////////////////////////////

        public event Action<UvcFrameDescriptor> postInvalidate = delegate { };

        /////////////////////////////////////////////
        // Public
        /////////////////////////////////////////////

        public UvcFrameDescriptor
        (
            UvcFormatDescriptor             formatDescriptor_, 
            UvcFrameDescriptorPtr.Readonly  frameDescriptorPtr_
        )
        {
            Auxiliary.VerifyNotNull(formatDescriptor_);
            Auxiliary.VerifyNotNull(frameDescriptorPtr_);
            frameDescriptorPtr_.VerifyIsValid();

            formatDescriptor    = formatDescriptor_;
            frameDescriptorPtr  = frameDescriptorPtr_;

            formatDescriptor.postInvalidate += HandleDFormatDescriptorPostInvalidate;

            isValid = true;
        }

        public byte FrameIndex
        {
            get
            {
                VerifyIsValid(); return frameDescriptorPtr.I.fields.GetFrameIndex();
            }            
        }

        public ushort Width
        {
            get
            {
                VerifyIsValid(); return frameDescriptorPtr.I.fields.GetWidth();
            }
        }

        public ushort Height
        {
            get
            {
                VerifyIsValid(); return frameDescriptorPtr.I.fields.GetHeight();
            }
        }

        /// <summary>
        /// Default frame interval (in 100ns units)
        /// </summary>
        public uint DefaultFrameInterval
        {
            get
            {
                VerifyIsValid(); return frameDescriptorPtr.I.fields.GetDefaultFrameInterval();
            }
        }

        /////////////////////////////////////////////
        // Private
        /////////////////////////////////////////////

        private void HandleDFormatDescriptorPostInvalidate(UvcFormatDescriptor sender_)
        {
            VerifyIsValid();

            isValid = false;

            formatDescriptor.postInvalidate -= HandleDFormatDescriptorPostInvalidate;

            postInvalidate.Invoke(this);
        }

        private void VerifyIsValid()
        {
            if (!isValid) throw new Exception("UvcFrameDescriptor is not valid!");
        }

        /////////////////////////////////////////////
        // Private
        /////////////////////////////////////////////

        private readonly UvcFormatDescriptor            formatDescriptor;
        private readonly UvcFrameDescriptorPtr.Readonly frameDescriptorPtr;

        private bool isValid;
    }
}
