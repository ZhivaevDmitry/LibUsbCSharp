using System;
using System.Runtime.InteropServices;

namespace LibUvc.StrongTyping
{   
    public sealed class UvcFramePtr : NativePtr<UvcFramePtr.Interface, UvcFramePtr, UvcFramePtr.Readonly>
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
                public IntPtr GetData()
                {
                    return
                        Marshal.ReadIntPtr
                        (
                            IntPtr.Add(nativePtrReadonly.VerifiedPtr, LowLevel.OffsetOf_uvc_frame_t.data)
                        );
                }

                public long GetDataBytes()
                {
                    return
                        Marshal.ReadIntPtr
                        (
                            IntPtr.Add(nativePtrReadonly.VerifiedPtr, LowLevel.OffsetOf_uvc_frame_t.data_bytes)
                        )
                        .ToInt64();
                }

                public uint GetWidth()
                {
                    return
                        Convert.ToUInt32
                        (
                                Marshal.ReadInt32
                            (
                                IntPtr.Add(nativePtrReadonly.VerifiedPtr, LowLevel.OffsetOf_uvc_frame_t.width)
                            )
                        );
                }

                public uint GetHeight()
                {
                    return
                        Convert.ToUInt32
                        (
                            Marshal.ReadInt32
                            (
                                IntPtr.Add(nativePtrReadonly.VerifiedPtr, LowLevel.OffsetOf_uvc_frame_t.height)
                            )
                        );
                }

                public UvcFrameFormat GetFrameFormat()
                {
                    return
                        (UvcFrameFormat)Marshal.ReadInt32
                        (
                            IntPtr.Add(nativePtrReadonly.VerifiedPtr, LowLevel.OffsetOf_uvc_frame_t.frame_format)
                        );
                }

                public Fields(Readonly nativePtrReadonly_) { nativePtrReadonly = nativePtrReadonly_; }
                private readonly Readonly nativePtrReadonly;
            }            
        }

        public sealed class Readonly : NativePtrReadonly<Interface, UvcFramePtr, Readonly>
        {
            public Readonly(UvcFramePtr editable_) : base(editable_) { }
        }

        public readonly Readonly readOnly;

        public UvcFramePtr() : base(o => new Readonly(o), o => new Interface(o)) 
        {
            readOnly = I.nativePtrReadonly;
        }
    }
}
