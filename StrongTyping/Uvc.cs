using System;
using System.Runtime.InteropServices;

namespace LibUvc.StrongTyping
{
    public static class Uvc
    {
        public static void ThrowIfUvcError(int uvcError_)
        {
            if (uvcError_ == 0) return;

            throw new UvcException((UvcError)uvcError_);
        }

        public static void Init(UvcContextPtr OUT_uvcContextPtr_)
        {
            IntPtr ptr = OUT_uvcContextPtr_.rawUnsafeMutablePtr;

            ThrowIfUvcError
            (
                LowLevel.uvc_init(ref ptr, IntPtr.Zero)
            );

            OUT_uvcContextPtr_.rawUnsafeMutablePtr = ptr;
        }

        public static void Exit(UvcContextPtr.Readonly uvcContextPtr_)
        {
            LowLevel.uvc_exit(uvcContextPtr_.VerifiedPtr);
        }

        public static void Wrap
        (
            int                     systemDevice_,
            UvcContextPtr.Readonly  uvcContextPtr_,             
            UvcDeviceHandlePtr      OUT_uvcDeviceHandlePtr_
        )
        {
            IntPtr outPtr = IntPtr.Zero;

            ThrowIfUvcError
            (
                LowLevel.uvc_wrap(systemDevice_, uvcContextPtr_.Ptr, ref outPtr)
            );

            OUT_uvcDeviceHandlePtr_.rawUnsafeMutablePtr = outPtr;
        }

        public static void Close(UvcDeviceHandlePtr.Readonly uvcDeviceHandlePtr_)
        {
            LowLevel.uvc_close(uvcDeviceHandlePtr_.Ptr);
        }

        public static void GetFormatDescs
        (
            UvcDeviceHandlePtr.Readonly uvcDeviceHandlePtr_, 
            UvcFormatDescriptorPtr      OUT_uvcFormatDescriptorPtr
        )
        {
            OUT_uvcFormatDescriptorPtr.rawUnsafeMutablePtr =
                LowLevel.uvc_get_format_descs(uvcDeviceHandlePtr_.VerifiedPtr);
        }

        public static void AllocateStreamCtrl(UvcStreamControlPtr OUT_uvcStreamControlPtr_)
        {
            OUT_uvcStreamControlPtr_.rawUnsafeMutablePtr =
                Marshal.AllocHGlobal(Marshal.SizeOf(typeof(LowLevel.uvc_stream_ctrl_t)));
        }

        public static void FreeStreamCtrl(UvcStreamControlPtr.Readonly uvcStreamControlPtr_)
        {
            Marshal.FreeHGlobal(uvcStreamControlPtr_.VerifiedPtr);
        }

        public static void GetStreamCtrlFormatSize
        (
            UvcDeviceHandlePtr.Readonly     uvcDeviceHandlePtr_,
            UvcStreamControlPtr.Readonly    uvcStreamControlPtr_,
            UvcFrameFormat                  frameFormat_,
            int                             width_,
            int                             height_,
            int                             fps_
        )
        {
            ThrowIfUvcError
            (
                LowLevel.uvc_get_stream_ctrl_format_size
                (
                    uvcDeviceHandlePtr_.VerifiedPtr,
                    uvcStreamControlPtr_.VerifiedPtr,
                    (int)frameFormat_,
                    width_,
                    height_,
                    fps_
                )
            );
        }

        public static void StartStreaming
        (
            UvcDeviceHandlePtr.Readonly     uvcDeviceHandlePtr_,
            UvcStreamControlPtr.Readonly    uvcStreamControlPtr_,
            byte                            flags = 0
        )
        {
            ThrowIfUvcError
            (
                LowLevel.uvc_start_streaming
                (
                    uvcDeviceHandlePtr_.VerifiedPtr,
                    uvcStreamControlPtr_.VerifiedPtr,
                    IntPtr.Zero,
                    IntPtr.Zero,
                    flags
                )
            );
        }

        public static void StopStreaming(UvcDeviceHandlePtr.Readonly uvcDeviceHandlePtr_)
        {
            LowLevel.uvc_stop_streaming(uvcDeviceHandlePtr_.VerifiedPtr);
        }

        public static void StreamOpenCtrl
        (
            UvcDeviceHandlePtr.Readonly     uvcDeviceHandlePtr_,                        
            UvcStreamHandlePtr              OUT_uvcStreamHandlePtr_,
            UvcStreamControlPtr.Readonly    uvcStreamControlPtr_
        )
        {
            IntPtr outPtr = OUT_uvcStreamHandlePtr_.rawUnsafeMutablePtr;

            ThrowIfUvcError
            (
                LowLevel.uvc_stream_open_ctrl
                (
                    uvcDeviceHandlePtr_.VerifiedPtr,
                    ref outPtr,
                    uvcStreamControlPtr_.VerifiedPtr
                )
            );

            OUT_uvcStreamHandlePtr_.rawUnsafeMutablePtr = outPtr;
        }

        public static void StreamStart
        (
            UvcStreamHandlePtr.Readonly uvcStreamHandlePtr_,
            byte flags = 0
        )
        {
            ThrowIfUvcError
            (
                LowLevel.uvc_stream_start
                (
                    uvcStreamHandlePtr_.VerifiedPtr,
                    IntPtr.Zero,
                    IntPtr.Zero,
                    flags
                )
            );
        }

        public static void StreamClose(UvcStreamHandlePtr.Readonly uvcStreamHandlePtr_)
        {
            LowLevel.uvc_stream_close(uvcStreamHandlePtr_.VerifiedPtr);
        }

        public static void GetFrame
        (
            UvcStreamHandlePtr.Readonly uvcStreamHandlePtr_,
            UvcFramePtr                 OUT_uvcFramePtr_,
            int                         timeoutUs_            
        )
        {
            IntPtr outPtr = OUT_uvcFramePtr_.rawUnsafeMutablePtr;

            ThrowIfUvcError
            (
                LowLevel.uvc_stream_get_frame
                (
                    uvcStreamHandlePtr_.VerifiedPtr,
                    ref outPtr,
                    timeoutUs_
                )
            );

            OUT_uvcFramePtr_.rawUnsafeMutablePtr = outPtr;
        }

        public static void AllocateFrame(uint dataBytes_, UvcFramePtr OUT_uvcFramePtr_)
        {
            OUT_uvcFramePtr_.rawUnsafeMutablePtr =
                LowLevel.uvc_allocate_frame(new UIntPtr(dataBytes_));
        }

        public static void FreeFrame(UvcFramePtr.Readonly uvcFramePtr_)
        {
            LowLevel.uvc_free_frame(uvcFramePtr_.VerifiedPtr);
        }
       
        public static void ConvertAnyToRgb(UvcFramePtr.Readonly srcUvcFramePtr_, UvcFramePtr.Readonly dstUvcFramePtr_)
        {
            ThrowIfUvcError
            (
                LowLevel.uvc_any2rgb(srcUvcFramePtr_.VerifiedPtr, dstUvcFramePtr_.VerifiedPtr)
            );
        }
    }
}

