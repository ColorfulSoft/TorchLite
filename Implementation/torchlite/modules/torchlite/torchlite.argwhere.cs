//***************************************************************************************************
//* (C) ColorfulSoft corp., 2019-2023. All rights reserved.
//* The code is available under the Apache-2.0 license. Read the License for details.
//***************************************************************************************************

using System.Runtime.InteropServices;

namespace System.AI.Experimental
{

    public unsafe static partial class torchlite
    {

        /// <summary>
        /// Returns a tensor containing the indices of all non-zero elements of input.
        /// Each row in the result contains the indices of a non-zero element in input.
        /// The result is sorted lexicographically, with the last index changing the fastest (C-style).
        /// If input has nn dimensions, then the resulting indices tensor out is of size (z x n),
        /// where z is the total number of non-zero elements in the input tensor.
        /// </summary>
        /// <param name="input">Input tensor.</param>
        /// <returns>Index tensor.</returns>
        public static Tensor argwhere(this Tensor input)
        {
            var ndim = input.shape.ndim;
            if(ndim == 0)
            {
                throw new ArgumentException("torchlite.argwhere is not implemented for 0-dim tensors.");
            }
            var shape = input.shape.data_ptr;
            var numel = input.shape.numel();
            var strides = (int*)Marshal.AllocHGlobal(ndim * sizeof(int));
            if(ndim > 0)
            {
                strides[ndim - 1] = 1;
            }
            for(int i = ndim - 2; i >= 0; --i)
            {
                strides[i] = strides[i + 1] * shape[i + 1];
            }
            Tensor output = null;
            switch(input.dtype)
            {
                case torchlite.float32:
                {
                    var input_data = (float*)input.storage.data_ptr;
                    var nonzero = 0;
                    for(int i = 0; i < numel; ++i)
                    {
                        if(input_data[i] != 0)
                        {
                            ++nonzero;
                        }
                    }
                    output = new Tensor(nonzero, ndim, torchlite.int32);
                    var output_data = (int*)output.storage.data_ptr;
                    for(int i = 0; i < numel; ++i)
                    {
                        if(input_data[i] != 0)
                        {
                            var loc = i;
                            for(int j = 0; j < ndim; ++j)
                            {
                                *output_data++ = loc / strides[j];
                                loc %= strides[j];
                            }
                        }
                    }
                    break;
                }
                case torchlite.int32:
                {
                    var input_data = (int*)input.storage.data_ptr;
                    var nonzero = 0;
                    for(int i = 0; i < numel; ++i)
                    {
                        if(input_data[i] != 0)
                        {
                            ++nonzero;
                        }
                    }
                    output = new Tensor(nonzero, ndim, torchlite.int32);
                    var output_data = (int*)output.storage.data_ptr;
                    for(int i = 0; i < numel; ++i)
                    {
                        if(input_data[i] != 0)
                        {
                            var loc = i;
                            for(int j = 0; j < ndim; ++j)
                            {
                                *output_data++ = loc / strides[j];
                                loc %= strides[j];
                            }
                        }
                    }
                    break;
                }
                case torchlite.@bool:
                {
                    var input_data = (byte*)input.storage.data_ptr;
                    var nonzero = 0;
                    for(int i = 0; i < numel; ++i)
                    {
                        if(input_data[i] != 0)
                        {
                            ++nonzero;
                        }
                    }
                    output = new Tensor(nonzero, ndim, torchlite.int32);
                    var output_data = (int*)output.storage.data_ptr;
                    for(int i = 0; i < numel; ++i)
                    {
                        if(input_data[i] != 0)
                        {
                            var loc = i;
                            for(int j = 0; j < ndim; ++j)
                            {
                                *output_data++ = loc / strides[j];
                                loc %= strides[j];
                            }
                        }
                    }
                    break;
                }
            }
            Marshal.FreeHGlobal((IntPtr)strides);
            return output;
        }

    }

}