using System.Collections;
using System.Collections.Generic;

namespace LibUvc.StrongTyping
{
    public enum UvcVideoStreamingDescriptorSubtype : int
    {
        UNDEFINED                = LowLevel.uvc_vs_desc_subtype.UVC_VS_UNDEFINED,
        INPUT_HEADER             = LowLevel.uvc_vs_desc_subtype.UVC_VS_INPUT_HEADER,
        OUTPUT_HEADER            = LowLevel.uvc_vs_desc_subtype.UVC_VS_OUTPUT_HEADER,
        STILL_IMAGE_FRAME        = LowLevel.uvc_vs_desc_subtype.UVC_VS_STILL_IMAGE_FRAME,
        FORMAT_UNCOMPRESSED      = LowLevel.uvc_vs_desc_subtype.UVC_VS_FORMAT_UNCOMPRESSED,
        FRAME_UNCOMPRESSED       = LowLevel.uvc_vs_desc_subtype.UVC_VS_FRAME_UNCOMPRESSED,
        FORMAT_MJPEG             = LowLevel.uvc_vs_desc_subtype.UVC_VS_FORMAT_MJPEG,
        FRAME_MJPEG              = LowLevel.uvc_vs_desc_subtype.UVC_VS_FRAME_MJPEG,
        FORMAT_MPEG2TS           = LowLevel.uvc_vs_desc_subtype.UVC_VS_FORMAT_MPEG2TS,
        FORMAT_DV                = LowLevel.uvc_vs_desc_subtype.UVC_VS_FORMAT_DV,
        COLORFORMAT              = LowLevel.uvc_vs_desc_subtype.UVC_VS_COLORFORMAT,
        FORMAT_FRAME_BASED       = LowLevel.uvc_vs_desc_subtype.UVC_VS_FORMAT_FRAME_BASED,
        FRAME_FRAME_BASED        = LowLevel.uvc_vs_desc_subtype.UVC_VS_FRAME_FRAME_BASED,
        FORMAT_STREAM_BASED      = LowLevel.uvc_vs_desc_subtype.UVC_VS_FORMAT_STREAM_BASED
    }
}
