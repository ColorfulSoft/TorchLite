//***************************************************************************************************
//* (C) ColorfulSoft corp., 2019-2023. All rights reserved.
//* The code is available under the Apache-2.0 license. Read the License for details.
//***************************************************************************************************

using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;

namespace System.AI.Experimental
{

    public static partial class torchlite
    {

        public unsafe partial class Size
        {

            /// <summary>
            /// Creates a size with dimensions from an IList&lt;int&gt; object.
            /// </summary>
            /// <param name="shape">Dimensions.</param>
            public Size(IList<int> shape)
            {
                if(shape == null)
                {
                    this.ndim = 0;
                    this.data_ptr = null;
                    return;
                }
                if(shape.Count > 8)
                {
                    throw new ArgumentException("TorchLite does not support tensors with more than 8 dimensions. Use Torch.NET instead.");
                }
                this.ndim = shape.Count;
                this.data_ptr = (int*)Marshal.AllocHGlobal(shape.Count * sizeof(int));
                for(int i = 0; i < this.ndim; ++i)
                {
                    if(shape[i] <= 0)
                    {
                        throw new ArgumentException(string.Format("Value {0} is invalid for {1} dimension.", shape[i], i));
                    }
                    this.data_ptr[i] = shape[i];
                }
            }

            /// <summary>
            /// Creates a size with specified dimensions.
            /// </summary>
            /// <param name="shape">Dimensions.</param>
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public Size(params int[] shape) : this((IList<int>)shape)
            {
            }

        }

    }

}