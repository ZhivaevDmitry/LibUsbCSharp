using System;

namespace LibUvc.StrongTyping
{
    public abstract class NativePtrReadonly<T_Interface, T_Descendant, T_DescendantReadonly>
        where T_Interface           : class
        where T_Descendant          : NativePtr<T_Interface, T_Descendant, T_DescendantReadonly>
        where T_DescendantReadonly  : NativePtrReadonly<T_Interface, T_Descendant, T_DescendantReadonly>
    {
        public NativePtrReadonly(T_Descendant editable_)
        {
            editable = editable_;
        }
        
        public bool IsValid
        {
            get { return editable.IsValid; }
        }
        
        public IntPtr Ptr
        {
            get { return editable.Ptr; }
        }
        
        public IntPtr VerifiedPtr
        {
            get { return editable.VerifiedPtr; }
        }
        
        public void VerifyIsValid()
        {
            editable.VerifyIsValid();
        }

        public T_Interface I 
        {
            get { return editable.I; }
        }

        private readonly T_Descendant editable;
    }

    public abstract class NativePtr<T_Interface, T_Descendant, T_DescendantReadonly>
        where T_Interface           : class
        where T_Descendant          : NativePtr<T_Interface, T_Descendant, T_DescendantReadonly>
        where T_DescendantReadonly  : NativePtrReadonly<T_Interface, T_Descendant, T_DescendantReadonly>
    {
        public NativePtr
        (
            Func<T_Descendant, T_DescendantReadonly>    ptrReadonlyFactoryMethod_,
            Func<T_DescendantReadonly, T_Interface>     objIntrerfaceFactoryMethod_
        )
        {
            T_Descendant descendant = (this as T_Descendant);

            Auxiliary.VerifyNotNull(descendant);

            T_DescendantReadonly readOnly = ptrReadonlyFactoryMethod_(descendant);

            Auxiliary.VerifyNotNull(readOnly);

            objInterface = objIntrerfaceFactoryMethod_(readOnly);

            Auxiliary.VerifyNotNull(objInterface);
        }

        public void Reset()
        {
            rawUnsafeMutablePtr = IntPtr.Zero;
        }

        public void Assign(T_Descendant other_)
        {
            rawUnsafeMutablePtr = other_.Ptr;
        }

        public void Assign(T_DescendantReadonly other_)
        {
            rawUnsafeMutablePtr = other_.Ptr;
        }

        public bool IsValid
        {
            get { return rawUnsafeMutablePtr != IntPtr.Zero; }
        }

        public IntPtr Ptr
        {
            get { return rawUnsafeMutablePtr; }
        }

        public IntPtr VerifiedPtr
        {
            get { VerifyIsValid(); return rawUnsafeMutablePtr; }
        }

        public void VerifyIsValid()
        {
            if (rawUnsafeMutablePtr == IntPtr.Zero) throw new Exception("Native ptr is invalid!");
        }

        public T_Interface I
        {
            get { return objInterface; }
        }

        public volatile IntPtr rawUnsafeMutablePtr = IntPtr.Zero;

        private readonly T_Interface objInterface;
    }
}