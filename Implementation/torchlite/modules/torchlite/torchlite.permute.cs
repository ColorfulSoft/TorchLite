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
        /// Returns a view of the original tensor input with its dimensions permuted.
        /// </summary>
        /// <param name="input">The input tensor.</param>
        /// <param name="dims">The desired ordering of dimensions.</param>
        /// <returns>Tensor.</returns>
        public static Tensor permute(this Tensor input, int[] dims)
        {
            // Compute shape
            var ndim = input.shape.ndim;
            if(dims.Length != ndim)
            {
                throw new ArgumentException("Number of dims don't match in permute.");
            }
            var x_shape = input.shape.data_ptr;
            var shape = new int[ndim];
            for(int i = 0; i < ndim; ++i)
            {
                for(int j = 0; j < ndim; ++j)
                {
                    if(i == j)
                    {
                        continue;
                    }
                    if(dims[j] == dims[i])
                    {
                        throw new ArgumentException("Repeated dim in permute.");
                    }
                }
                if((dims[i] < -ndim) && (dims[i] >= ndim))
                {
                    throw new ArgumentException(string.Format("Dimension out of range (expected to be in range of [-{0}, {1}], but got {2})", ndim, ndim - 1, dims[i]));
                }
                if(dims[i] < 0)
                {
                    dims[i] += ndim;
                }
                shape[i] = x_shape[dims[i]];
            }
            var numel = input.shape.numel();
            var output = new Tensor(shape, input.dtype, input.requires_grad);
            var y_shape = output.shape.data_ptr;
            // Copy dims
            var dim = (int*)Marshal.AllocHGlobal(ndim * sizeof(int));
            for(int i = 0; i < ndim; ++i)
            {
                for(int j = 0; j < ndim; ++j)
                {
                    if(i == dims[j])
                    {
                        dim[i] = j;
                    }
                }
            }
            // Compute strides
            var x_strides = (int*)Marshal.AllocHGlobal(ndim * sizeof(int));
            var y_strides = (int*)Marshal.AllocHGlobal(ndim * sizeof(int));
            if(ndim > 0)
            {
                x_strides[ndim - 1] = 1;
                y_strides[ndim - 1] = 1;
            }
            for(int i = ndim - 2; i >= 0; --i)
            {
                x_strides[i] = x_strides[i + 1] * x_shape[i + 1];
                y_strides[i] = y_strides[i + 1] * y_shape[i + 1];
            }
            switch(input.dtype)
            {
                case torchlite.float32:
                {
                    var src = (float*)input.storage.data_ptr;
                    var dst = (float*)output.storage.data_ptr;
                    for(int i = 0; i < numel; ++i)
                    {
                        var x_loc = i;
                        var y_index = 0;
                        for(int j = 0; j < ndim; ++j)
                        {
                            y_index += x_loc / x_strides[j] * y_strides[dim[j]];
                            x_loc %= x_strides[j];
                        }
                        dst[y_index] = src[i];
                    }
                    if(output.requires_grad)
                    {
                        output.__parents = new []{input};
                        output.backward_fn = () =>
                        {
                            var src_grad = (float*)input.grad.storage.data_ptr;
                            var dst_grad = (float*)output.grad.storage.data_ptr;
                            for(int i = 0; i < numel; ++i)
                            {
                                var x_loc = i;
                                var y_index = 0;
                                for(int j = 0; j < ndim; ++j)
                                {
                                    y_index += x_loc / x_strides[j] * y_strides[dim[j]];
                                    x_loc %= x_strides[j];
                                }
                                src_grad[i] += dst_grad[y_index];
                            }
                            Marshal.FreeHGlobal((IntPtr)dim);
                            Marshal.FreeHGlobal((IntPtr)x_strides);
                            Marshal.FreeHGlobal((IntPtr)y_strides);
                        };
                    }
                    else
                    {
                        Marshal.FreeHGlobal((IntPtr)dim);
                        Marshal.FreeHGlobal((IntPtr)x_strides);
                        Marshal.FreeHGlobal((IntPtr)y_strides);
                    }
                    break;
                }
                case torchlite.int32:
                {
                    var src = (int*)input.storage.data_ptr;
                    var dst = (int*)output.storage.data_ptr;
                    for(int i = 0; i < numel; ++i)
                    {
                        var x_loc = i;
                        var y_index = 0;
                        for(int j = 0; j < ndim; ++j)
                        {
                            y_index += x_loc / x_strides[j] * y_strides[dim[j]];
                            x_loc %= x_strides[j];
                        }
                        dst[y_index] = src[i];
                    }
                    Marshal.FreeHGlobal((IntPtr)dim);
                    Marshal.FreeHGlobal((IntPtr)x_strides);
                    Marshal.FreeHGlobal((IntPtr)y_strides);
                    break;
                }
                case torchlite.@bool:
                {
                    var src = (bool*)input.storage.data_ptr;
                    var dst = (bool*)output.storage.data_ptr;
                    for(int i = 0; i < numel; ++i)
                    {
                        var x_loc = i;
                        var y_index = 0;
                        for(int j = 0; j < ndim; ++j)
                        {
                            y_index += x_loc / x_strides[j] * y_strides[dim[j]];
                            x_loc %= x_strides[j];
                        }
                        dst[y_index] = src[i];
                    }
                    Marshal.FreeHGlobal((IntPtr)dim);
                    Marshal.FreeHGlobal((IntPtr)x_strides);
                    Marshal.FreeHGlobal((IntPtr)y_strides);
                    break;
                }
            }
            return output;
        }

    }

}