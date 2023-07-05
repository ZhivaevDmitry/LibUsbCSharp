using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using LibUvc.StrongTyping;

namespace LibUvc.Management
{
    public sealed class UvcDeviceManager : IDisposable
    {
        /////////////////////////////////////////////
        // Public
        /////////////////////////////////////////////

        public event Action<UvcDeviceManager> postInvalidate = delegate { };

        /////////////////////////////////////////////
        // Public
        /////////////////////////////////////////////

        public UvcDeviceManager(UvcDeviceHandlePtr deviceHandlePtr_)
        {
            Auxiliary.VerifyNotNull(deviceHandlePtr_);
            deviceHandlePtr_.VerifyIsValid();

            deviceHandlePtr = deviceHandlePtr_;

            UvcFormatDescriptorPtr firstFormatDescriptorPtr = new UvcFormatDescriptorPtr();

            Uvc.GetFormatDescs(deviceHandlePtr.readOnly, firstFormatDescriptorPtr);

            UvcFormatDescriptor firstFormatDescriptor = new UvcFormatDescriptor(this, firstFormatDescriptorPtr.readOnly);

            listOfFormatDescriptors.Add(firstFormatDescriptor);
            listOfFormatDescriptorPtrs.Add(firstFormatDescriptorPtr);            

            isValid = true;
        }

        public void Dispose()
        {
            if (!isValid) return;

            isValid = false;

            try
            {
                foreach (var streamManageAndStreamHandlePtr in streamManageToStreamHandlePtr)
                {
                    streamManageAndStreamHandlePtr.Key.postInvalidate -= HandleStreamManagerPostInvalidate;
                }

                foreach (var streamManageAndStreamHandlePtr in streamManageToStreamHandlePtr)
                {
                    streamManageAndStreamHandlePtr.Key.Dispose();
                }

                streamManageToStreamHandlePtr.Clear();

                postInvalidate.Invoke(this);
            }
            finally
            {
                Uvc.Close(deviceHandlePtr.readOnly);
                deviceHandlePtr.Reset();
            }
        }

        public ReadOnlyCollection<UvcFormatDescriptor> formatDescriptors
        {
            get { VerifyIsValid(); return listOfFormatDescriptors.AsReadOnly(); }
        }

        public void FillInStreamControl
        (
            UvcStreamControlPtr.Readonly    uvcStreamControlPtr_,
            UvcFrameFormat                  desiredFrameFormat_,
            int                             desiredWidth_,
            int                             desiredHeight_,
            int                             desiredFps_            
        )
        {
            VerifyIsValid();

            Uvc.GetStreamCtrlFormatSize
            (
                deviceHandlePtr.readOnly,
                uvcStreamControlPtr_,
                desiredFrameFormat_,
                desiredWidth_,
                desiredHeight_,
                desiredFps_
            );
        }

        public UvcStreamManager StartStream
        (
            UvcStreamControlPtr.Readonly uvcStreamControlPtr_,
            Action<UvcFramePtr.Readonly> capturingThreadCallback_ = null
        )
        {
            VerifyIsValid();

            UvcStreamHandlePtr streamHandlePtr = new UvcStreamHandlePtr();

            Uvc.StreamOpenCtrl(deviceHandlePtr.readOnly, streamHandlePtr, uvcStreamControlPtr_);

            try
            {
                Uvc.StreamStart
                (
                    streamHandlePtr.readOnly
                );
            }
            catch (UvcException e_)
            {
                Uvc.StreamClose(streamHandlePtr.readOnly);
                streamHandlePtr.Reset();

                throw e_;
            }

            UvcStreamManager streamManager = new UvcStreamManager(streamHandlePtr, capturingThreadCallback_);

            streamManager.postInvalidate += HandleStreamManagerPostInvalidate;

            streamManageToStreamHandlePtr.Add(streamManager, streamHandlePtr);

            return streamManager;
        }

        /////////////////////////////////////////////
        // Private
        /////////////////////////////////////////////

        private void HandleStreamManagerPostInvalidate(UvcStreamManager sender_)
        {
            sender_.postInvalidate -= HandleStreamManagerPostInvalidate;

            streamManageToStreamHandlePtr.Remove(sender_);
        }

        private void VerifyIsValid()
        {
            if (!isValid) throw new Exception("UvcDeviceManager is not valid!");
        }

        /////////////////////////////////////////////
        // Private
        /////////////////////////////////////////////

        public readonly UvcDeviceHandlePtr deviceHandlePtr;

        private readonly List<UvcFormatDescriptor>      listOfFormatDescriptors     = new List<UvcFormatDescriptor>();
        private readonly List<UvcFormatDescriptorPtr>   listOfFormatDescriptorPtrs  = new List<UvcFormatDescriptorPtr>();


        private readonly Dictionary<UvcStreamManager, UvcStreamHandlePtr> streamManageToStreamHandlePtr =
           new Dictionary<UvcStreamManager, UvcStreamHandlePtr>();

        private bool isValid;
    }
}

