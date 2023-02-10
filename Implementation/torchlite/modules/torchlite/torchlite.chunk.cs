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
        /// Attempts to split a tensor into the specified number of chunks. Each chunk is a view of the input tensor.
        /// </summary>
        /// <param name="input">The tensor to split.</param>
        /// <param name="chunks">Number of chunks to return.</param>
        /// <param name="dim">Dimension along which to split the tensor.</param>
        /// <returns>Array of tensors.</returns>
        public static Tensor[] chunk(this Tensor input, int chunks, int dim = 0)
        {
            var chunk_size = input.shape.data_ptr[dim] / chunks + (((input.shape.data_ptr[dim] % chunks) != 0) ? 1 : 0);
            var n_chunks = input.shape.data_ptr[dim] / chunk_size + (((input.shape.data_ptr[dim] % chunk_size) != 0) ? 1 : 0);
            var output = new Tensor[n_chunks];
            var ndim = input.shape.ndim;
            var x_shape = input.shape.data_ptr;
            var shape = new int[ndim];
            for(int i = 0; i < ndim; ++i)
            {
                shape[i] = x_shape[i];
            }
            shape[dim] = chunk_size;
            for(int chunk = 0; chunk < n_chunks; ++chunk)
            {
                // Last chunk
                if((chunk + 1) == n_chunks)
                {
                    shape[dim] = x_shape[dim] - (n_chunks - 1) * chunk_size;
                }
                // Make tensor
                output[chunk] = new Tensor(shape, input.dtype, input.requires_grad);
                // Compute strides
                var y_shape = output[chunk].shape.data_ptr;
                var y_strides = (int*)Marshal.AllocHGlobal(ndim * sizeof(int));
                if(ndim > 0)
                {
                    y_strides[ndim - 1] = 1;
                }
                for(int i = ndim - 2; i >= 0; --i)
                {
                    y_strides[i] = y_strides[i + 1] * y_shape[i + 1];
                }
                var x_strides = (int*)Marshal.AllocHGlobal(ndim * sizeof(int));
                if(ndim > 0)
                {
                    x_strides[ndim - 1] = 1;
                }
                for(int i = ndim - 2; i >= 0; --i)
                {
                    x_strides[i] = x_strides[i + 1] * x_shape[i + 1];
                }
                var numel = output[chunk].shape.numel();
                // Measurement offset
                var dim_ = chunk * chunk_size;
                switch(input.dtype)
                {
                    case torchlite.float32:
                    {
                        var src = (float*)input.storage.data_ptr;
                        var dst = (float*)output[chunk].storage.data_ptr;
                        for(int i = 0; i < numel; ++i)
                        {
                            var y_loc = i;
                            var x_index = 0;
                            for(int j = 0; j < ndim; ++j)
                            {
                                if(j == dim)
                                {
                                    x_index += y_loc / y_strides[j] + dim_;
                                }
                                else
                                {
                                    x_index += y_loc / y_strides[j];
                                }
                                y_loc %= y_strides[j];
                            }
                            dst[i] = src[x_index];
                        }
                        if(output[chunk].requires_grad)
                        {
                            output[chunk].parents = new []{input};
                            // Make copy of common variables for lambda
                            var chunk_ = chunk;
                            var numel_ = numel;
                            var x_strides_ = x_strides;
                            var y_strides_ = y_strides;
                            bool backward_passed = false;
                            output[chunk].backward_fn = () =>
                            {
                                if(backward_passed)
                                {
                                    throw new Exception("Trying to backward through the graph a second time (or directly access saved tensors after they have already been freed).");
                                }
                                backward_passed = true;
                                var src_grad = (float*)input.grad.storage.data_ptr;
                                var dst_grad = (float*)output[chunk_].grad.storage.data_ptr;
                                for(int i = 0; i < numel_; ++i)
                                {
                                    var y_loc = i;
                                    var x_index = 0;
                                    for(int j = 0; j < ndim; ++j)
                                    {
                                        if(j == dim)
                                        {
                                            x_index += (y_loc / y_strides_[j] + dim_) * x_strides_[j];
                                        }
                                        else
                                        {
                                            x_index += y_loc / y_strides_[j] * x_strides_[j];
                                        }
                                        y_loc %= y_strides_[j];
                                    }
                                    src_grad[x_index] += dst_grad[i];
                                }
                                Marshal.FreeHGlobal((IntPtr)x_strides_);
                                Marshal.FreeHGlobal((IntPtr)y_strides_);
                            };
                        }
                        else
                        {
                            Marshal.FreeHGlobal((IntPtr)x_strides);
                            Marshal.FreeHGlobal((IntPtr)y_strides);
                        }
                        break;
                    }
                    case torchlite.int32:
                    {
                        var src = (int*)input.storage.data_ptr;
                        var dst = (int*)output[chunk].storage.data_ptr;
                        for(int i = 0; i < numel; ++i)
                        {
                            var y_loc = i;
                            var x_index = 0;
                            for(int j = 0; j < ndim; ++j)
                            {
                                if(j == dim)
                                {
                                    x_index += y_loc / y_strides[j] + dim_;
                                }
                                else
                                {
                                    x_index += y_loc / y_strides[j];
                                }
                                y_loc %= y_strides[j];
                            }
                            dst[i] = src[x_index];
                        }
                        Marshal.FreeHGlobal((IntPtr)x_strides);
                        Marshal.FreeHGlobal((IntPtr)y_strides);
                        break;
                    }
                    case torchlite.@bool:
                    {
                        var src = (bool*)input.storage.data_ptr;
                        var dst = (bool*)output[chunk].storage.data_ptr;
                        for(int i = 0; i < numel; ++i)
                        {
                            var y_loc = i;
                            var x_index = 0;
                            for(int j = 0; j < ndim; ++j)
                            {
                                if(j == dim)
                                {
                                    x_index += y_loc / y_strides[j] + dim_;
                                }
                                else
                                {
                                    x_index += y_loc / y_strides[j];
                                }
                                y_loc %= y_strides[j];
                            }
                            dst[i] = src[x_index];
                        }
                        Marshal.FreeHGlobal((IntPtr)x_strides);
                        Marshal.FreeHGlobal((IntPtr)y_strides);
                        break;
                    }
                }
            }
            return output;
        }

    }

}