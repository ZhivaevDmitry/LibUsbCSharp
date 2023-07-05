using System;

namespace LibUvc.StrongTyping
{
    public sealed class UvcContextPtr : NativePtr<UvcContextPtr.Interface, UvcContextPtr, UvcContextPtr.Readonly>
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
                public Fields(Readonly nativePtrReadonly_) { nativePtrReadonly = nativePtrReadonly_; }
                private readonly Readonly nativePtrReadonly;
            }            
        }

        public sealed class Readonly : NativePtrReadonly<Interface, UvcContextPtr, Readonly>
        {
            public Readonly(UvcContextPtr editable_) : base(editable_) { }
        }

        public readonly Readonly readOnly;

        public UvcContextPtr() : base(o => new Readonly(o), o => new Interface(o)) 
        {
            readOnly = I.nativePtrReadonly;
        }
    }
}
