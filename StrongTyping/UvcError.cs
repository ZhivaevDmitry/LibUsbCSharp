using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LibUvc.StrongTyping
{
    public enum UvcError : int
    {
        /** Success (no error) */
        //UVC_SUCCESS = 0,
        /** Input/output error */
        UVC_ERROR_IO = -1,
        /** Invalid parameter */
        UVC_ERROR_INVALID_PARAM = -2,
        /** Access denied */
        UVC_ERROR_ACCESS = -3,
        /** No such device */
        UVC_ERROR_NO_DEVICE = -4,
        /** Entity not found */
        UVC_ERROR_NOT_FOUND = -5,
        /** Resource busy */
        UVC_ERROR_BUSY = -6,
        /** Operation timed out */
        UVC_ERROR_TIMEOUT = -7,
        /** Overflow */
        UVC_ERROR_OVERFLOW = -8,
        /** Pipe error */
        UVC_ERROR_PIPE = -9,
        /** System call interrupted */
        UVC_ERROR_INTERRUPTED = -10,
        /** Insufficient memory */
        UVC_ERROR_NO_MEM = -11,
        /** Operation not supported */
        UVC_ERROR_NOT_SUPPORTED = -12,
        /** Device is not UVC-compliant */
        UVC_ERROR_INVALID_DEVICE = -50,
        /** Mode not supported */
        UVC_ERROR_INVALID_MODE = -51,
        /** Resource has a callback (can't use polling and async) */
        UVC_ERROR_CALLBACK_EXISTS = -52,
        /** Undefined error */
        UVC_ERROR_OTHER = -99
    }
}
