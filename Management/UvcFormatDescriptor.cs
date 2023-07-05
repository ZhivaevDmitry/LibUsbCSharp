using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using LibUvc.StrongTyping;

namespace LibUvc.Management
{

    public sealed class UvcFormatDescriptor
    {
        /////////////////////////////////////////////
        // Public
        /////////////////////////////////////////////

        public event Action<UvcFormatDescriptor> postInvalidate = delegate { };

        /////////////////////////////////////////////
        // Public
        /////////////////////////////////////////////

        public UvcFormatDescriptor
        (
            UvcDeviceManager                deviceManager_, 
            UvcFormatDescriptorPtr.Readonly formatDescriptorPtr_
        )
        {
            Auxiliary.VerifyNotNull(deviceManager_);
            Auxiliary.VerifyNotNull(formatDescriptorPtr_);
            formatDescriptorPtr_.VerifyIsValid();

            deviceManager       = deviceManager_;
            formatDescriptorPtr = formatDescriptorPtr_;

            deviceManager.postInvalidate += HandleDeviceManagerPostInvalidate;

            UvcFrameDescriptorPtr firstFrameDescriptorPtr = new UvcFrameDescriptorPtr();

            formatDescriptorPtr.I.fields.GetFrameDescriptor(firstFrameDescriptorPtr);

            UvcFrameDescriptor firstFrameDescriptor = new UvcFrameDescriptor(this, firstFrameDescriptorPtr.readOnly);

            listOfFrameDescriptors.Add(firstFrameDescriptor);
            listOfFrameDescriptorPtrs.Add(firstFrameDescriptorPtr);

            isValid = true;
        }
         
        public ReadOnlyCollection<UvcFrameDescriptor> FrameDescriptors
        {
            get { VerifyIsValid(); return listOfFrameDescriptors.AsReadOnly(); }
        }

        public UvcVideoStreamingDescriptorSubtype DescriptorSubtype
        {
            get { VerifyIsValid(); return formatDescriptorPtr.I.fields.GetDescriptorSubtype(); }
        }        

        /////////////////////////////////////////////
        // Private
        /////////////////////////////////////////////

        private void HandleDeviceManagerPostInvalidate(UvcDeviceManager sender_)
        {
            VerifyIsValid();

            isValid = false;

            deviceManager.postInvalidate -= HandleDeviceManagerPostInvalidate;

            postInvalidate.Invoke(this);
        }

        private void VerifyIsValid()
        {
            if (!isValid) throw new Exception("UvcFormatDescriptor is not valid!");
        }

        /////////////////////////////////////////////
        // Private
        /////////////////////////////////////////////

        private readonly UvcDeviceManager                   deviceManager;
        private readonly UvcFormatDescriptorPtr.Readonly    formatDescriptorPtr;

        private readonly List<UvcFrameDescriptor>       listOfFrameDescriptors      = new List<UvcFrameDescriptor>();
        private readonly List<UvcFrameDescriptorPtr>    listOfFrameDescriptorPtrs   = new List<UvcFrameDescriptorPtr>();

        private bool isValid;
    }
}
