using System;
using System.Runtime.InteropServices;

namespace LibUvc.StrongTyping
{   
    public sealed class UvcFormatDescriptorPtr : 
        NativePtr<UvcFormatDescriptorPtr.Interface, UvcFormatDescriptorPtr, UvcFormatDescriptorPtr.Readonly>
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
                public void GetFrameDescriptor(UvcFrameDescriptorPtr OUT_uvcFrameDescriptorPtr_)
                {
                    OUT_uvcFrameDescriptorPtr_.rawUnsafeMutablePtr =
                        Marshal.ReadIntPtr
                        (
                            IntPtr.Add(nativePtrReadonly.VerifiedPtr, LowLevel.OffsetOf_uvc_format_desc_t.frame_descs)
                        );
                }

                public UvcVideoStreamingDescriptorSubtype GetDescriptorSubtype()
                {
                    return
                       (UvcVideoStreamingDescriptorSubtype)Marshal.ReadInt32
                       (
                           IntPtr.Add(nativePtrReadonly.VerifiedPtr, LowLevel.OffsetOf_uvc_format_desc_t.bDescriptorSubtype)
                       );
                }

                public Fields(Readonly nativePtrReadonly_) { nativePtrReadonly = nativePtrReadonly_; }
                private readonly Readonly nativePtrReadonly;
            }            
        }

        public sealed class Readonly : NativePtrReadonly<Interface, UvcFormatDescriptorPtr, Readonly>
        {
            public Readonly(UvcFormatDescriptorPtr editable_) : base(editable_) { }
        }

        public readonly Readonly readOnly;

        public UvcFormatDescriptorPtr() : base(o => new Readonly(o), o => new Interface(o)) 
        {
            readOnly = I.nativePtrReadonly;
        }
    }
}
