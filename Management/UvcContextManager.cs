using System;
using System.Collections.Generic;
using LibUvc.StrongTyping;

namespace LibUvc.Management
{
    public sealed class UvcContextManager : IDisposable
    {
        /////////////////////////////////////////////
        // Public
        /////////////////////////////////////////////

        public event Action<UvcContextManager> postInvalidate = delegate { };

        /////////////////////////////////////////////
        // Public
        /////////////////////////////////////////////

        public UvcContextManager()
        {
            contextPtr = new UvcContextPtr();

            Uvc.Init(contextPtr);

            isValid = true;
        }

        public UvcDeviceManager WrapSystemDevice(int systemDeviceHandle_)
        {
            VerifyIsValid();

            UvcDeviceHandlePtr deviceHandlePtr = new UvcDeviceHandlePtr();

            Uvc.Wrap(systemDeviceHandle_, contextPtr.readOnly, deviceHandlePtr);

            UvcDeviceManager deviceManager = new UvcDeviceManager(deviceHandlePtr);

            deviceManager.postInvalidate += HandleDeviceManagerPostInvalidate;

            deviceManageToDeviceHandlePtr.Add(deviceManager, deviceHandlePtr);

            return deviceManager;
        }

        public void Dispose()
        {            
            if (!isValid) return;

            isValid = false;
          
            try
            {
                foreach (var deviceManageAndDeviceHandlePtr in deviceManageToDeviceHandlePtr)
                {
                    deviceManageAndDeviceHandlePtr.Key.postInvalidate -= HandleDeviceManagerPostInvalidate;                    
                }

                foreach (var deviceManageAndDeviceHandlePtr in deviceManageToDeviceHandlePtr)
                {
                    deviceManageAndDeviceHandlePtr.Key.Dispose();
                }                

                deviceManageToDeviceHandlePtr.Clear();

                postInvalidate.Invoke(this);
            }
            finally
            {
                Uvc.Exit(contextPtr.readOnly);
                contextPtr.Reset();
            }
        }

        /////////////////////////////////////////////
        // Private
        /////////////////////////////////////////////

        private void HandleDeviceManagerPostInvalidate(UvcDeviceManager sender_)
        {
            sender_.postInvalidate -= HandleDeviceManagerPostInvalidate;

            deviceManageToDeviceHandlePtr.Remove(sender_);
        }

        private void VerifyIsValid()
        {
            if (!isValid) throw new Exception("UvcContextManager is not valid!");
        }

        /////////////////////////////////////////////
        // Private
        /////////////////////////////////////////////

        private readonly UvcContextPtr contextPtr;

        private readonly Dictionary<UvcDeviceManager, UvcDeviceHandlePtr> deviceManageToDeviceHandlePtr = 
            new Dictionary<UvcDeviceManager, UvcDeviceHandlePtr>();

        private bool isValid;
    }
}
