using System;

namespace LibUvc.StrongTyping
{
    public static class Auxiliary
    {      
        public static void VerifyNotNull(object obj_)
        {
            if (obj_ == null) throw new NullReferenceException();
        }        
    }
}
    
