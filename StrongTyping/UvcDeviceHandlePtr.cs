using System;
using System.Runtime.InteropServices;

namespace LibUvc.StrongTyping
{
    public sealed class UvcDeviceHandlePtr : NativePtr<UvcDeviceHandlePtr.Interface, UvcDeviceHandlePtr, UvcDeviceHandlePtr.Readonly>
    {
        public sealed class Interface
        {
            public readonly Readonly nativePtrReadonly;

            public Interface(Readonly nativePtrReadonly_)
            {
                nativePtrReadonly = nativePtrReadonly_;

                fields = new Fields(nativePtrReadonly);
            }

            public readonly Fields fields;

            public sealed class Fields
            {
                public void GetStreams(UvcStreamHandlePtr OUT_uvcStreamHandlePtr_)
                {
                    OUT_uvcStreamHandlePtr_.rawUnsafeMutablePtr =
                        Marshal.ReadIntPtr
                        (
                            IntPtr.Add(nativePtrReadonly.VerifiedPtr, LowLevel.OffsetsOf_uvc_device_handle_t.streams)
                        );
                }

                public Fields(Readonly nativePtrReadonly_) { nativePtrReadonly = nativePtrReadonly_; }
                private readonly Readonly nativePtrReadonly;
            }            
        }

        public sealed class Readonly : NativePtrReadonly<Interface, UvcDeviceHandlePtr, Readonly>
        {
            public Readonly(UvcDeviceHandlePtr editable_) : base(editable_) { }
        }

        public readonly Readonly readOnly;

        public UvcDeviceHandlePtr() : base(o => new Readonly(o), o => new Interface(o)) 
        {
            readOnly = I.nativePtrReadonly;
        }
    }
}
