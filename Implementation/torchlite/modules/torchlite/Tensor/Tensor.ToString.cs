//***************************************************************************************************
//* (C) ColorfulSoft corp., 2019-2023. All rights reserved.
//* The code is available under the Apache-2.0 license. Read the License for details.
//***************************************************************************************************

using System;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;

namespace System.AI.Experimental
{

    public static partial class torchlite
    {

        public unsafe partial class Tensor
        {

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public override string ToString()
            {
                var str = "tensor(";
                var loc = (int*)Marshal.AllocHGlobal(this.shape.ndim * sizeof(int)).ToPointer();
                for(int i = 0; i < this.shape.ndim; ++i)
                {
                    loc[i] = 0;
                }
                var format = this.__get_format();
                this.__print(ref str, loc, 0, format);
                str += string.Format(", dtype = torchlite.{0}, requires_grad = {1}", this.dtype.ToString(), this.requires_grad);
                return str + ")";
            }

        }

    }

}