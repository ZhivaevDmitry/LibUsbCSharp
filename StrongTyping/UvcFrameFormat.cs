using System.Collections;
using System.Collections.Generic;

namespace LibUvc.StrongTyping
{
    public enum UvcFrameFormat : int
    {
        UNKNOWN        = LowLevel.uvc_frame_format.UVC_FRAME_FORMAT_UNKNOWN,
        /** Any supported format */
        ANY            = LowLevel.uvc_frame_format.UVC_FRAME_FORMAT_ANY,
        UNCOMPRESSED   = LowLevel.uvc_frame_format.UVC_FRAME_FORMAT_UNCOMPRESSED,
        COMPRESSED     = LowLevel.uvc_frame_format.UVC_FRAME_FORMAT_COMPRESSED,
        /** YUYV/YUV2/YUV422: YUV encoding with one luminance value per pixel and
        * one UV (chrominance) pair for every two pixels.
        */
        YUYV           = LowLevel.uvc_frame_format.UVC_FRAME_FORMAT_YUYV,
        UYVY           = LowLevel.uvc_frame_format.UVC_FRAME_FORMAT_UYVY,
        /** 24-bit RGB */
        RGB            = LowLevel.uvc_frame_format.UVC_FRAME_FORMAT_RGB,
        BGR            = LowLevel.uvc_frame_format.UVC_FRAME_FORMAT_BGR,
        /** Motion-JPEG (or JPEG) encoded images */
        MJPEG          = LowLevel.uvc_frame_format.UVC_FRAME_FORMAT_MJPEG,
        H264           = LowLevel.uvc_frame_format.UVC_FRAME_FORMAT_H264,
        /** Greyscale images */
        GRAY8          = LowLevel.uvc_frame_format.UVC_FRAME_FORMAT_GRAY8,
        GRAY16         = LowLevel.uvc_frame_format.UVC_FRAME_FORMAT_GRAY16,
        /* Raw colour mosaic images */
        BY8            = LowLevel.uvc_frame_format.UVC_FRAME_FORMAT_BY8,
        BA81           = LowLevel.uvc_frame_format.UVC_FRAME_FORMAT_BA81,
        SGRBG8         = LowLevel.uvc_frame_format.UVC_FRAME_FORMAT_SGRBG8,
        SGBRG8         = LowLevel.uvc_frame_format.UVC_FRAME_FORMAT_SGBRG8,
        SRGGB8         = LowLevel.uvc_frame_format.UVC_FRAME_FORMAT_SRGGB8,
        SBGGR8         = LowLevel.uvc_frame_format.UVC_FRAME_FORMAT_SBGGR8,
        /** YUV420: NV12 */
        NV12           = LowLevel.uvc_frame_format.UVC_FRAME_FORMAT_NV12,
        /** YUV: P010 */
        P010           = LowLevel.uvc_frame_format.UVC_FRAME_FORMAT_P010,
        /** Number of formats understood */
        COUNT          = LowLevel.uvc_frame_format.UVC_FRAME_FORMAT_COUNT,
    }
}
