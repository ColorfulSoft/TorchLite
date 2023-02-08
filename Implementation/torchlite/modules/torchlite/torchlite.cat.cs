//***************************************************************************************************
//* (C) ColorfulSoft corp., 2019-2023. All rights reserved.
//* The code is available under the Apache-2.0 license. Read the License for details.
//***************************************************************************************************

using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace System.AI.Experimental
{

    public unsafe static partial class torchlite
    {

        /// <summary>
        /// Concatenates the given sequence of seq tensors in the given dimension.
        /// All tensors must either have the same shape (except in the concatenating dimension).
        /// </summary>
        /// <param name="tensors">Any .NET sequence of tensors of the same type. Non-empty tensors provided must have the same shape, except in the cat dimension.</param>
        /// <param name="dim">The dimension over which the tensors are concatenated.</param>
        /// <returns>The output tensor.</returns>
        public static Tensor cat(IList<Tensor> tensors, int dim = 0)
        {
            if(tensors.Count == 0)
            {
                throw new ArgumentException("torchlite.cat should receive at least 1 tensor.");
            }
            var shape = new int[tensors[0].shape.ndim];
            ((IList<int>)tensors[0].shape).CopyTo(shape, 0);
            var dtype = tensors[0].dtype;
            var requires_grad = tensors[0].requires_grad;
            for(int i = 1; i < tensors.Count; ++i)
            {
                var t = tensors[i];
                if((dtype == torchlite.@bool) && (t.dtype != torchlite.@bool))
                {
                    dtype = t.dtype;
                }
                else
                {
                    if((dtype == torchlite.int32) && (t.dtype == torchlite.float32))
                    {
                        dtype = t.dtype;
                    }
                }
                requires_grad |= t.requires_grad;
                if(t.shape.ndim != shape.Length)
                {
                    throw new ArgumentException(string.Format("Sizes of tensors must match except in dimension {0}. Expected {1}-dim tensor but got {2}-dim tensor number {3} in the list.", dim, shape.Length, t.shape.ndim, i));
                }
                for(int j = 0; j < shape.Length; ++j)
                {
                    if(j == dim)
                    {
                        shape[dim] += t.shape.data_ptr[dim];
                        continue;
                    }
                    if(shape[j] != t.shape.data_ptr[j])
                    {
                        throw new ArgumentException(string.Format("Sizes of tensors must match except in dimension {0}. Expected size {1} but got size {2} for tensor number {3} in the list.", j, shape[j], t.shape.data_ptr[j], i));
                    }
                }
            }
            var output = new Tensor(shape, dtype, requires_grad);
            var y_shape = output.shape.data_ptr;
            var ndim = output.shape.ndim;
            var y_strides = (int*)Marshal.AllocHGlobal(ndim * sizeof(int));
            if(ndim > 0)
            {
                y_strides[ndim - 1] = 1;
            }
            for(int i = ndim - 2; i >= 0; --i)
            {
                y_strides[i] = y_strides[i + 1] * y_shape[i + 1];
            }
            int dim_ = 0;
            switch(dtype)
            {
                case torchlite.float32:
                {
                    var dst = (float*)output.storage.data_ptr;
                    foreach(var tensor in tensors)
                    {
                        var x_shape = tensor.shape.data_ptr;
                        var x_strides = (int*)Marshal.AllocHGlobal(ndim * sizeof(int));
                        var numel = tensor.shape.numel();
                        if(ndim > 0)
                        {
                            x_strides[ndim - 1] = 1;
                        }
                        for(int i = ndim - 2; i >= 0; --i)
                        {
                            x_strides[i] = x_strides[i + 1] * x_shape[i + 1];
                        }
                        switch(tensor.dtype)
                        {
                            case torchlite.float32:
                            {
                                var src = (float*)tensor.storage.data_ptr;
                                for(int i = 0; i < numel; ++i)
                                {
                                    var loc = i;
                                    var index = 0;
                                    for(int j = 0; j < ndim; ++j)
                                    {
                                        if(j == dim)
                                        {
                                            index += (loc / x_strides[j] + dim_) * y_strides[j];
                                            loc %= x_strides[j];
                                            continue;
                                        }
                                        index += loc / x_strides[j] * y_strides[j];
                                        loc %= x_strides[j];
                                    }
                                    dst[index] = src[i];
                                }
                                break;
                            }
                            case torchlite.int32:
                            {
                                var src = (int*)tensor.storage.data_ptr;
                                for(int i = 0; i < numel; ++i)
                                {
                                    var loc = i;
                                    var index = 0;
                                    for(int j = 0; j < ndim; ++j)
                                    {
                                        if(j == dim)
                                        {
                                            index += (loc / x_strides[j] + dim_) * y_strides[j];
                                            loc %= x_strides[j];
                                            continue;
                                        }
                                        index += loc / x_strides[j] * y_strides[j];
                                        loc %= x_strides[j];
                                    }
                                    dst[index] = src[i];
                                }
                                break;
                            }
                            case torchlite.@bool:
                            {
                                var src = (byte*)tensor.storage.data_ptr;
                                for(int i = 0; i < numel; ++i)
                                {
                                    var loc = i;
                                    var index = 0;
                                    for(int j = 0; j < ndim; ++j)
                                    {
                                        if(j == dim)
                                        {
                                            index += (loc / x_strides[j] + dim_) * y_strides[j];
                                            loc %= x_strides[j];
                                            continue;
                                        }
                                        index += loc / x_strides[j] * y_strides[j];
                                        loc %= x_strides[j];
                                    }
                                    dst[index] = src[i];
                                }
                                break;
                            }
                        }
                        dim_ += tensor.shape.data_ptr[dim];
                        Marshal.FreeHGlobal((IntPtr)x_strides);
                    }
                    break;
                }
                case torchlite.int32:
                {
                    var dst = (int*)output.storage.data_ptr;
                    foreach(var tensor in tensors)
                    {
                        var x_shape = tensor.shape.data_ptr;
                        var x_strides = (int*)Marshal.AllocHGlobal(ndim * sizeof(int));
                        var numel = tensor.shape.numel();
                        if(ndim > 0)
                        {
                            x_strides[ndim - 1] = 1;
                        }
                        for(int i = ndim - 2; i >= 0; --i)
                        {
                            x_strides[i] = x_strides[i + 1] * x_shape[i + 1];
                        }
                        switch(tensor.dtype)
                        {
                            case torchlite.int32:
                            {
                                var src = (int*)tensor.storage.data_ptr;
                                for(int i = 0; i < numel; ++i)
                                {
                                    var loc = i;
                                    var index = 0;
                                    for(int j = 0; j < ndim; ++j)
                                    {
                                        if(j == dim)
                                        {
                                            index += (loc / x_strides[j] + dim_) * y_strides[j];
                                            loc %= x_strides[j];
                                            continue;
                                        }
                                        index += loc / x_strides[j] * y_strides[j];
                                        loc %= x_strides[j];
                                    }
                                    dst[index] = src[i];
                                }
                                break;
                            }
                            case torchlite.@bool:
                            {
                                var src = (byte*)tensor.storage.data_ptr;
                                for(int i = 0; i < numel; ++i)
                                {
                                    var loc = i;
                                    var index = 0;
                                    for(int j = 0; j < ndim; ++j)
                                    {
                                        if(j == dim)
                                        {
                                            index += (loc / x_strides[j] + dim_) * y_strides[j];
                                            loc %= x_strides[j];
                                            continue;
                                        }
                                        index += loc / x_strides[j] * y_strides[j];
                                        loc %= x_strides[j];
                                    }
                                    dst[index] = src[i];
                                }
                                break;
                            }
                        }
                        dim_ += tensor.shape.data_ptr[dim];
                        Marshal.FreeHGlobal((IntPtr)x_strides);
                    }
                    break;
                }
                case torchlite.@bool:
                {
                    var dst = (byte*)output.storage.data_ptr;
                    foreach(var tensor in tensors)
                    {
                        var x_shape = tensor.shape.data_ptr;
                        var x_strides = (int*)Marshal.AllocHGlobal(ndim * sizeof(int));
                        var numel = tensor.shape.numel();
                        if(ndim > 0)
                        {
                            x_strides[ndim - 1] = 1;
                        }
                        for(int i = ndim - 2; i >= 0; --i)
                        {
                            x_strides[i] = x_strides[i + 1] * x_shape[i + 1];
                        }
                        var src = (byte*)tensor.storage.data_ptr;
                        for(int i = 0; i < numel; ++i)
                        {
                            var loc = i;
                            var index = 0;
                            for(int j = 0; j < ndim; ++j)
                            {
                                if(j == dim)
                                {
                                    index += (loc / x_strides[j] + dim_) * y_strides[j];
                                    loc %= x_strides[j];
                                    continue;
                                }
                                index += loc / x_strides[j] * y_strides[j];
                                loc %= x_strides[j];
                            }
                            dst[index] = src[i];
                        }
                        dim_ += tensor.shape.data_ptr[dim];
                        Marshal.FreeHGlobal((IntPtr)x_strides);
                    }
                    break;
                }
            }
            if(output.requires_grad)
            {
                var parents = new List<Tensor>();
                foreach(var tensor in tensors)
                {
                    if(tensor.requires_grad)
                    {
                        parents.Add(tensor);
                    }
                }
                output.__parents = parents.ToArray();
                output.backward_fn = () =>
                {
                    var dst_grad = (float*)output.grad.storage.data_ptr;
                    dim_ = 0;
                    foreach(var tensor in tensors)
                    {
                        if(!tensor.requires_grad)
                        {
                            dim_ += tensor.shape.data_ptr[dim];
                            continue;
                        }
                        var x_shape = tensor.shape.data_ptr;
                        var x_strides = (int*)Marshal.AllocHGlobal(ndim * sizeof(int));
                        var numel = tensor.shape.numel();
                        if(ndim > 0)
                        {
                            x_strides[ndim - 1] = 1;
                        }
                        for(int i = ndim - 2; i >= 0; --i)
                        {
                            x_strides[i] = x_strides[i + 1] * x_shape[i + 1];
                        }
                        var src_grad = (float*)tensor.grad.storage.data_ptr;
                        for(int i = 0; i < numel; ++i)
                        {
                            var loc = i;
                            var index = 0;
                            for(int j = 0; j < ndim; ++j)
                            {
                                if(j == dim)
                                {
                                    index += (loc / x_strides[j] + dim_) * y_strides[j];
                                    loc %= x_strides[j];
                                    continue;
                                }
                                index += loc / x_strides[j] * y_strides[j];
                                loc %= x_strides[j];
                            }
                            src_grad[i] += dst_grad[index];
                        }
                        dim_ += tensor.shape.data_ptr[dim];
                        Marshal.FreeHGlobal((IntPtr)x_strides);
                    }
                    Marshal.FreeHGlobal((IntPtr)y_strides);
                };
            }
            else
            {
                Marshal.FreeHGlobal((IntPtr)y_strides);
            }
            return output;
        }

    }

}