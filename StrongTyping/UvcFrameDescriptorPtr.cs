using System;
using System.Runtime.InteropServices;

namespace LibUvc.StrongTyping
{
    public sealed class UvcFrameDescriptorPtr : 
        NativePtr<UvcFrameDescriptorPtr.Interface, UvcFrameDescriptorPtr, UvcFrameDescriptorPtr.Readonly>
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
                public byte GetFrameIndex()
                {
                    return
                        Marshal.ReadByte
                        (
                            IntPtr.Add(nativePtrReadonly.VerifiedPtr, LowLevel.OffsetsOf_uvc_frame_desc_t.bFrameIndex)
                        );
                }

                public ushort GetWidth()
                {
                    return
                        Convert.ToUInt16
                        (
                            Marshal.ReadInt16
                            (
                                IntPtr.Add(nativePtrReadonly.VerifiedPtr, LowLevel.OffsetsOf_uvc_frame_desc_t.wWidth)
                            )
                        );
                }

                public ushort GetHeight()
                {
                    return
                        Convert.ToUInt16
                        (
                            Marshal.ReadInt16
                            (
                                IntPtr.Add(nativePtrReadonly.VerifiedPtr, LowLevel.OffsetsOf_uvc_frame_desc_t.wHeight)
                            )
                        );
                }

                public uint GetDefaultFrameInterval()
                {
                    return
                        Convert.ToUInt32
                        (
                            Marshal.ReadInt32
                            (
                                IntPtr.Add(nativePtrReadonly.VerifiedPtr, LowLevel.OffsetsOf_uvc_frame_desc_t.dwDefaultFrameInterval)
                            )
                        );
                }

                public Fields(Readonly nativePtrReadonly_) { nativePtrReadonly = nativePtrReadonly_; }
                private readonly Readonly nativePtrReadonly;
            }            
        }

        public sealed class Readonly : NativePtrReadonly<Interface, UvcFrameDescriptorPtr, Readonly>
        {
            public Readonly(UvcFrameDescriptorPtr editable_) : base(editable_) { }
        }

        public readonly Readonly readOnly;

        public UvcFrameDescriptorPtr() : base(o => new Readonly(o), o => new Interface(o)) 
        {
            readOnly = I.nativePtrReadonly;
        }
    }
}
