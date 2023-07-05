using System;
using System.Runtime.InteropServices;

namespace LibUvc
{
    public static class LowLevel
    {
        private const string DLL_NAME = "uvc";
       
        //////////////////////////////////////////////////////////
        // Structs
        //////////////////////////////////////////////////////////

        public enum uvc_vs_desc_subtype : int
        {
            UVC_VS_UNDEFINED            = 0x00,
            UVC_VS_INPUT_HEADER         = 0x01,
            UVC_VS_OUTPUT_HEADER        = 0x02,
            UVC_VS_STILL_IMAGE_FRAME    = 0x03,
            UVC_VS_FORMAT_UNCOMPRESSED  = 0x04,
            UVC_VS_FRAME_UNCOMPRESSED   = 0x05,
            UVC_VS_FORMAT_MJPEG         = 0x06,
            UVC_VS_FRAME_MJPEG          = 0x07,
            UVC_VS_FORMAT_MPEG2TS       = 0x0a,
            UVC_VS_FORMAT_DV            = 0x0c,
            UVC_VS_COLORFORMAT          = 0x0d,
            UVC_VS_FORMAT_FRAME_BASED   = 0x10,
            UVC_VS_FRAME_FRAME_BASED    = 0x11,
            UVC_VS_FORMAT_STREAM_BASED  = 0x12
        };

        public enum uvc_frame_format : int
        {
            UVC_FRAME_FORMAT_UNKNOWN = 0,
            /** Any supported format */
            UVC_FRAME_FORMAT_ANY = 0,
            UVC_FRAME_FORMAT_UNCOMPRESSED,
            UVC_FRAME_FORMAT_COMPRESSED,
            /** YUYV/YUV2/YUV422: YUV encoding with one luminance value per pixel and
             * one UV (chrominance) pair for every two pixels.
             */
            UVC_FRAME_FORMAT_YUYV,
            UVC_FRAME_FORMAT_UYVY,
            /** 24-bit RGB */
            UVC_FRAME_FORMAT_RGB,
            UVC_FRAME_FORMAT_BGR,
            /** Motion-JPEG (or JPEG) encoded images */
            UVC_FRAME_FORMAT_MJPEG,
            UVC_FRAME_FORMAT_H264,
            /** Greyscale images */
            UVC_FRAME_FORMAT_GRAY8,
            UVC_FRAME_FORMAT_GRAY16,
            /* Raw colour mosaic images */
            UVC_FRAME_FORMAT_BY8,
            UVC_FRAME_FORMAT_BA81,
            UVC_FRAME_FORMAT_SGRBG8,
            UVC_FRAME_FORMAT_SGBRG8,
            UVC_FRAME_FORMAT_SRGGB8,
            UVC_FRAME_FORMAT_SBGGR8,
            /** YUV420: NV12 */
            UVC_FRAME_FORMAT_NV12,
            /** YUV: P010 */
            UVC_FRAME_FORMAT_P010,
            /** Number of formats understood */
            UVC_FRAME_FORMAT_COUNT,
        };

        [StructLayout(LayoutKind.Sequential)]
        public struct uvc_device_handle_t
        {
            //struct uvc_device *dev;
            public IntPtr dev;

            //struct uvc_device_handle *prev, *next;
            public IntPtr prev, next;

            ///** Underlying USB device handle */
            //libusb_device_handle* usb_devh;
            public IntPtr usb_devh;

            //struct uvc_device_info *info;
            public IntPtr info;

            //struct libusb_transfer *status_xfer;
            public IntPtr status_xfer;

            //uint8_t status_buf[32];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 32)]
            public byte[] status_buf;

            ///** Function to call when we receive status updates from the camera */
            //uvc_status_callback_t* status_cb;
            public IntPtr status_cb;

            //void* status_user_ptr;
            public IntPtr status_user_ptr;

            ///** Function to call when we receive button events from the camera */
            //uvc_button_callback_t* button_cb;
            public IntPtr button_cb;

            //void* button_user_ptr;
            public IntPtr button_user_ptr;

            //uvc_stream_handle_t* streams;
            public IntPtr streams;

            ///** Whether the camera is an iSight that sends one header per frame */
            //uint8_t is_isight;
            public byte is_isight;

            //uint32_t claimed;
            public uint claimed;
        };

        public static class OffsetsOf_uvc_device_handle_t
        {
            public static readonly int dev              = Marshal.OffsetOf<uvc_device_handle_t>("dev").ToInt32();
            public static readonly int prev             = Marshal.OffsetOf<uvc_device_handle_t>("prev").ToInt32();
            public static readonly int next             = Marshal.OffsetOf<uvc_device_handle_t>("next").ToInt32();
            public static readonly int usb_devh         = Marshal.OffsetOf<uvc_device_handle_t>("usb_devh").ToInt32();
            public static readonly int info             = Marshal.OffsetOf<uvc_device_handle_t>("info").ToInt32();
            public static readonly int status_xfer      = Marshal.OffsetOf<uvc_device_handle_t>("status_xfer").ToInt32();
            public static readonly int status_buf       = Marshal.OffsetOf<uvc_device_handle_t>("status_buf").ToInt32();
            public static readonly int status_cb        = Marshal.OffsetOf<uvc_device_handle_t>("status_cb").ToInt32();
            public static readonly int status_user_ptr  = Marshal.OffsetOf<uvc_device_handle_t>("status_user_ptr").ToInt32();
            public static readonly int button_cb        = Marshal.OffsetOf<uvc_device_handle_t>("button_cb").ToInt32();
            public static readonly int button_user_ptr  = Marshal.OffsetOf<uvc_device_handle_t>("button_user_ptr").ToInt32();
            public static readonly int streams          = Marshal.OffsetOf<uvc_device_handle_t>("streams").ToInt32();
            public static readonly int is_isight        = Marshal.OffsetOf<uvc_device_handle_t>("is_isight").ToInt32();
            public static readonly int claimed          = Marshal.OffsetOf<uvc_device_handle_t>("claimed").ToInt32();
        };


        [StructLayout(LayoutKind.Sequential)]
        public struct uvc_frame_desc_t
        {
            //  struct uvc_format_desc *parent;
            public IntPtr parent;
            //  struct uvc_frame_desc *prev, *next;
            public IntPtr prev, next;
            //  /** Type of frame, such as JPEG frame or uncompressed frme */
            //  enum uvc_vs_desc_subtype bDescriptorSubtype;
            public uvc_vs_desc_subtype bDescriptorSubtype;
            //  /** Index of the frame within the list of specs available for this format */
            //  uint8_t bFrameIndex;
            public byte bFrameIndex;
            //  uint8_t bmCapabilities;
            public byte bmCapabilities;
            //  /** Image width */
            //  uint16_t wWidth;
            public ushort wWidth;
            //  /** Image height */
            //  uint16_t wHeight;
            public ushort wHeight;
            //  /** Bitrate of corresponding stream at minimal frame rate */
            //  uint32_t dwMinBitRate;
            public uint dwMinBitRate;
            //  /** Bitrate of corresponding stream at maximal frame rate */
            //  uint32_t dwMaxBitRate;
            public uint dwMaxBitRate;
            //  /** Maximum number of bytes for a video frame */
            //  uint32_t dwMaxVideoFrameBufferSize;
            public uint dwMaxVideoFrameBufferSize;
            //  /** Default frame interval (in 100ns units) */
            //  uint32_t dwDefaultFrameInterval;
            public uint dwDefaultFrameInterval;
            //  /** Minimum frame interval for continuous mode (100ns units) */
            //  uint32_t dwMinFrameInterval;
            public uint dwMinFrameInterval;
            //  /** Maximum frame interval for continuous mode (100ns units) */
            //  uint32_t dwMaxFrameInterval;
            public uint dwMaxFrameInterval;
            //  /** Granularity of frame interval range for continuous mode (100ns) */
            //  uint32_t dwFrameIntervalStep;
            public uint dwFrameIntervalStep;
            //  /** Frame intervals */
            //  uint8_t bFrameIntervalType;
            public byte bFrameIntervalType;
            //  /** number of bytes per line */
            //  uint32_t dwBytesPerLine;
            public uint dwBytesPerLine;
            //  /** Available frame rates, zero-terminated (in 100ns units) */
            //  uint32_t *intervals;
            public IntPtr intervals;
        };

        public static class OffsetsOf_uvc_frame_desc_t
        {
            public static readonly int parent                    = Marshal.OffsetOf<uvc_frame_desc_t>("parent").ToInt32();
            public static readonly int prev                      = Marshal.OffsetOf<uvc_frame_desc_t>("prev").ToInt32();
            public static readonly int next                      = Marshal.OffsetOf<uvc_frame_desc_t>("next").ToInt32();
            public static readonly int bDescriptorSubtype        = Marshal.OffsetOf<uvc_frame_desc_t>("bDescriptorSubtype").ToInt32();
            public static readonly int bFrameIndex               = Marshal.OffsetOf<uvc_frame_desc_t>("bFrameIndex").ToInt32();
            public static readonly int bmCapabilities            = Marshal.OffsetOf<uvc_frame_desc_t>("bmCapabilities").ToInt32();
            public static readonly int wWidth                    = Marshal.OffsetOf<uvc_frame_desc_t>("wWidth").ToInt32();
            public static readonly int wHeight                   = Marshal.OffsetOf<uvc_frame_desc_t>("wHeight").ToInt32();
            public static readonly int dwMinBitRate              = Marshal.OffsetOf<uvc_frame_desc_t>("dwMinBitRate").ToInt32();
            public static readonly int dwMaxBitRate              = Marshal.OffsetOf<uvc_frame_desc_t>("dwMaxBitRate").ToInt32();
            public static readonly int dwMaxVideoFrameBufferSize = Marshal.OffsetOf<uvc_frame_desc_t>("dwMaxVideoFrameBufferSize").ToInt32();
            public static readonly int dwDefaultFrameInterval    = Marshal.OffsetOf<uvc_frame_desc_t>("dwDefaultFrameInterval").ToInt32();
            public static readonly int dwMinFrameInterval        = Marshal.OffsetOf<uvc_frame_desc_t>("dwMinFrameInterval").ToInt32();
            public static readonly int dwMaxFrameInterval        = Marshal.OffsetOf<uvc_frame_desc_t>("dwMaxFrameInterval").ToInt32();
            public static readonly int dwFrameIntervalStep       = Marshal.OffsetOf<uvc_frame_desc_t>("dwFrameIntervalStep").ToInt32();
            public static readonly int bFrameIntervalType        = Marshal.OffsetOf<uvc_frame_desc_t>("bFrameIntervalType").ToInt32();
            public static readonly int dwBytesPerLine            = Marshal.OffsetOf<uvc_frame_desc_t>("dwBytesPerLine").ToInt32();
            public static readonly int intervals                 = Marshal.OffsetOf<uvc_frame_desc_t>("intervals").ToInt32();
        }


        [StructLayout(LayoutKind.Sequential)]
        public struct uvc_format_desc_t
        {
            //  struct uvc_streaming_interface *parent;
            public IntPtr parent;

            //  struct uvc_format_desc *prev, *next;
            public IntPtr prev, next;

            //  /** Type of image stream, such as JPEG or uncompressed. */
            //  enum uvc_vs_desc_subtype bDescriptorSubtype;
            public uvc_vs_desc_subtype bDescriptorSubtype;

            //  /** Identifier of this format within the VS interface's format list */
            //  uint8_t bFormatIndex;
            public byte bFormatIndex;

            //  uint8_t bNumFrameDescriptors;
            public byte bNumFrameDescriptors;

            //  /** Format specifier */
            //  union {
            //      uint8_t guidFormat[16];
            //      uint8_t fourccFormat[4];
            //  };

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
            public byte[] format_specifier;

            //  /** Format-specific data */
            //  union {
            //    /** BPP for uncompressed stream */
            //    uint8_t bBitsPerPixel;
            //    /** Flags for JPEG stream */
            //    uint8_t bmFlags;
            //  };
            [StructLayout(LayoutKind.Explicit)]
            public struct format_specific_data_t
            {
                [FieldOffset(0)]
                public byte bBitsPerPixel;

                [FieldOffset(0)]
                public byte bmFlags;
            };

            public format_specific_data_t format_specific_data;

            //  /** Default {uvc_frame_desc} to choose given this format */

            //  uint8_t bDefaultFrameIndex;
            public byte bDefaultFrameIndex;

            //  uint8_t bAspectRatioX;
            public byte bAspectRatioX;

            //  uint8_t bAspectRatioY;
            public byte bAspectRatioY;

            //  uint8_t bmInterlaceFlags;
            public byte bmInterlaceFlags;

            //  uint8_t bCopyProtect;
            public byte bCopyProtect;

            //  uint8_t bVariableSize;
            public byte bVariableSize;

            //  /** Available frame specifications for this format */

            //  struct uvc_frame_desc *frame_descs;
            public IntPtr frame_descs;

            //  struct uvc_still_frame_desc *still_frame_desc;
            public IntPtr still_frame_desc;
        }

        public static class OffsetOf_uvc_format_desc_t
        {           
            public static readonly int parent                = Marshal.OffsetOf<uvc_format_desc_t>("parent").ToInt32();
            public static readonly int prev                  = Marshal.OffsetOf<uvc_format_desc_t>("prev").ToInt32();
            public static readonly int next                  = Marshal.OffsetOf<uvc_format_desc_t>("next").ToInt32();
            public static readonly int bDescriptorSubtype    = Marshal.OffsetOf<uvc_format_desc_t>("bDescriptorSubtype").ToInt32();
            public static readonly int bFormatIndex          = Marshal.OffsetOf<uvc_format_desc_t>("bFormatIndex").ToInt32();
            public static readonly int bNumFrameDescriptors  = Marshal.OffsetOf<uvc_format_desc_t>("bNumFrameDescriptors").ToInt32();
            public static readonly int format_specifier      = Marshal.OffsetOf<uvc_format_desc_t>("format_specifier").ToInt32();
            public static readonly int format_specific_data  = Marshal.OffsetOf<uvc_format_desc_t>("format_specific_data").ToInt32();
            public static readonly int bDefaultFrameIndex    = Marshal.OffsetOf<uvc_format_desc_t>("bDefaultFrameIndex").ToInt32();
            public static readonly int bAspectRatioX         = Marshal.OffsetOf<uvc_format_desc_t>("bAspectRatioX").ToInt32();
            public static readonly int bAspectRatioY         = Marshal.OffsetOf<uvc_format_desc_t>("bAspectRatioY").ToInt32();
            public static readonly int bmInterlaceFlags      = Marshal.OffsetOf<uvc_format_desc_t>("bmInterlaceFlags").ToInt32();
            public static readonly int bCopyProtect          = Marshal.OffsetOf<uvc_format_desc_t>("bCopyProtect").ToInt32();
            public static readonly int bVariableSize         = Marshal.OffsetOf<uvc_format_desc_t>("bVariableSize").ToInt32();
            public static readonly int frame_descs           = Marshal.OffsetOf<uvc_format_desc_t>("frame_descs").ToInt32();
            public static readonly int still_frame_desc      = Marshal.OffsetOf<uvc_format_desc_t>("still_frame_desc").ToInt32();
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct uvc_stream_ctrl_t
        {
            //  uint16_t bmHint;
            public ushort bmHint;
            //  uint8_t bFormatIndex;
            public byte bFormatIndex;
            //  uint8_t bFrameIndex;
            public byte bFrameIndex;
            //  uint32_t dwFrameInterval;
            public uint dwFrameInterval;
            //  uint16_t wKeyFrameRate;
            public ushort wKeyFrameRate;
            //  uint16_t wPFrameRate;
            public ushort wPFrameRate;
            //  uint16_t wCompQuality;
            public ushort wCompQuality;
            //  uint16_t wCompWindowSize;
            public ushort wCompWindowSize;
            //  uint16_t wDelay;
            public ushort wDelay;
            //  uint32_t dwMaxVideoFrameSize;
            public uint dwMaxVideoFrameSize;
            //  uint32_t dwMaxPayloadTransferSize;
            public uint dwMaxPayloadTransferSize;
            //  uint32_t dwClockFrequency;
            public uint dwClockFrequency;
            //  uint8_t bmFramingInfo;
            public uint bmFramingInfo;
            //  uint8_t bPreferredVersion;
            public uint bPreferredVersion;
            //  uint8_t bMinVersion;
            public uint bMinVersion;
            //  uint8_t bMaxVersion;
            public uint bMaxVersion;
            //  uint8_t bInterfaceNumber;
            public uint bInterfaceNumber;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct timeval
        {
            // __kernel_time_t tv_sec;
            public int tv_sec;

            // __kernel_suseconds_t tv_usec;
            public int tv_usec;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct timespec
        {
            // __kernel_time_t tv_sec;
            public int tv_sec;

            // long tv_nsec;
            public int tv_nsec;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct uvc_frame_t
        {
            ///** Image data for this frame */
            //void *data;
            public IntPtr data;

            ///** Size of image data buffer */
            //size_t data_bytes;
            public UIntPtr data_bytes;

            ///** Width of image in pixels */
            //uint32_t width;
            public uint width;

            ///** Height of image in pixels */
            //uint32_t height;
            public uint height;

            ///** Pixel data format */
            //enum uvc_frame_format frame_format;
            public int frame_format;

            ///** Number of bytes per horizontal line (undefined for compressed format) */
            //size_t step;
            public UIntPtr step;

            ///** Frame number (may skip, but is strictly monotonically increasing) */
            //uint32_t sequence;
            public uint sequence;

            ///** Estimate of system time when the device started capturing the image */
            //struct timeval capture_time;
            public timeval capture_time;

            ///** Estimate of system time when the device finished receiving the image */
            //struct timespec capture_time_finished;
            public timespec capture_time_finished;

            ///** Handle on the device that produced the image.
            //* @warning You must not call any uvc_* functions during a callback. */
            //uvc_device_handle_t *source;
            public IntPtr source;

            ///** Is the data buffer owned by the library?
            //* If 1, the data buffer can be arbitrarily reallocated by frame conversion
            //* functions.
            //* If 0, the data buffer will not be reallocated or freed by the library.
            //* Set this field to zero if you are supplying the buffer.
            //*/
            //uint8_t library_owns_data;
            public byte library_owns_data;

            ///** Metadata for this frame if available */
            //void *metadata;
            public IntPtr metadata;

            ///** Size of metadata buffer */
            //size_t metadata_bytes;
            public UIntPtr metadata_bytes;

        };

        public static class OffsetOf_uvc_frame_t
        {
            public static readonly int data                     = Marshal.OffsetOf<uvc_frame_t>("data").ToInt32();
            public static readonly int data_bytes               = Marshal.OffsetOf<uvc_frame_t>("data_bytes").ToInt32();
            public static readonly int width                    = Marshal.OffsetOf<uvc_frame_t>("width").ToInt32();
            public static readonly int height                   = Marshal.OffsetOf<uvc_frame_t>("height").ToInt32();
            public static readonly int frame_format             = Marshal.OffsetOf<uvc_frame_t>("frame_format").ToInt32();
            public static readonly int step                     = Marshal.OffsetOf<uvc_frame_t>("step").ToInt32();
            public static readonly int sequence                 = Marshal.OffsetOf<uvc_frame_t>("sequence").ToInt32();      
            public static readonly int capture_time             = Marshal.OffsetOf<uvc_frame_t>("capture_time").ToInt32();            
            public static readonly int capture_time_finished    = Marshal.OffsetOf<uvc_frame_t>("capture_time_finished").ToInt32();
            public static readonly int source                   = Marshal.OffsetOf<uvc_frame_t>("source").ToInt32();
            public static readonly int library_owns_data        = Marshal.OffsetOf<uvc_frame_t>("library_owns_data").ToInt32();
            public static readonly int metadata                 = Marshal.OffsetOf<uvc_frame_t>("metadata").ToInt32();
            public static readonly int metadata_bytes           = Marshal.OffsetOf<uvc_frame_t>("metadata_bytes").ToInt32();
        };

        //////////////////////////////////////////////////////////
        // Functions
        //////////////////////////////////////////////////////////

        //uvc_error_t uvc_init(uvc_context_t** pctx, struct libusb_context *usb_ctx)

        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        public static extern int uvc_init(ref IntPtr pctx, IntPtr usb_ctx);

        //void uvc_exit(uvc_context_t* ctx);

        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        public static extern void uvc_exit(IntPtr ctx);

        //uvc_error_t uvc_wrap(int sys_dev, uvc_context_t* context, uvc_device_handle_t** devh)

        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        public static extern int uvc_wrap(int sys_dev, IntPtr context, ref IntPtr devh);

        //void uvc_close(uvc_device_handle_t *devh)
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        public static extern void uvc_close(IntPtr devh);

        //const uvc_format_desc_t* uvc_get_format_descs(uvc_device_handle_t * devh)
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr uvc_get_format_descs(IntPtr devh);

        //uvc_error_t uvc_get_stream_ctrl_format_size
        //(
        //    uvc_device_handle_t* devh,
        //    uvc_stream_ctrl_t* ctrl,
        //    enum uvc_frame_format cf,
        //    int width, int height,
        //    int fps
        //)
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        public static extern int uvc_get_stream_ctrl_format_size
        (
            IntPtr  devh,
            IntPtr  ctrl,
            int     cf,
            int     width, 
            int     height,
            int     fps
        );

        //uvc_error_t uvc_start_streaming
        //(
        //    uvc_device_handle_t* devh,
        //    uvc_stream_ctrl_t* ctrl,
        //    uvc_frame_callback_t* cb,
        //    void* user_ptr,
        //    uint8_t flags
        //)
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        public static extern int uvc_start_streaming
        (
            IntPtr  devh,
            IntPtr  ctrl,
            IntPtr  cb,
            IntPtr  user_ptr,
            byte    flags
        );

        //void uvc_stop_streaming(uvc_device_handle_t* devh)
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        public static extern void uvc_stop_streaming(IntPtr devh);

        //uvc_error_t uvc_stream_open_ctrl
        //(
        //    uvc_device_handle_t*  devh,
        //    uvc_stream_handle_t** strmhp,
        //    uvc_stream_ctrl_t*    ctrl
        //);
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        public static extern int uvc_stream_open_ctrl
        (
            IntPtr      devh, 
            ref IntPtr  strmhp, 
            IntPtr      ctrl
        );

        //uvc_error_t uvc_stream_start
        //(
        //    uvc_stream_handle_t*    strmh,
        //    uvc_frame_callback_t*   cb,
        //    void*                   user_ptr,
        //    uint8_t                 flags
        //)
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        public static extern int uvc_stream_start
        (
            IntPtr  strmh, 
            IntPtr  cb, 
            IntPtr  user_ptr, 
            byte    flags
        );

        //void uvc_stream_close(uvc_stream_handle_t* strmh)
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        public static extern void uvc_stream_close(IntPtr strmh);

        //uvc_error_t uvc_stream_get_frame
        //(
        //    uvc_stream_handle_t* strmh,
        //    uvc_frame_t** frame,
        //    int32_t timeout_us
        //)
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        public static extern int uvc_stream_get_frame
        (
            IntPtr      strmh,
            ref IntPtr  frame,
            int         timeout_us
        );


        //uvc_frame_t* uvc_allocate_frame(size_t data_bytes)
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr uvc_allocate_frame
        (
           UIntPtr data_bytes
        );


        //void uvc_free_frame(uvc_frame_t* frame)
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        public static extern void uvc_free_frame(IntPtr frame);

        //uvc_error_t uvc_any2rgb(uvc_frame_t*in, uvc_frame_t*out)
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        public static extern int uvc_any2rgb(IntPtr fin, IntPtr fout);

        //uvc_error_t uvc_any2bgr(uvc_frame_t*in, uvc_frame_t*out)
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        public static extern int uvc_any2bgr(IntPtr fin, IntPtr fout);
    }
}