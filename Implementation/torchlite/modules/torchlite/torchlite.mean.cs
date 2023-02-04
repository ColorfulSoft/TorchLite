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

        public static Tensor mean(this Tensor input, bool keepdim = false)
        {
            var dim = new int[input.shape.ndim];
            for(int i = 0; i < dim.Length; ++i)
            {
                dim[i] = i;
            }
            return input.mean(dim, keepdim);
        }

        public static Tensor mean(this Tensor input, int dim, bool keepdim = false)
        {
            return input.mean(new []{dim}, keepdim);
        }

        public static Tensor mean(this Tensor input, IList<int> dim, bool keepdim = false)
        {
            if(dim == null)
            {
                throw new NullReferenceException("Null value is invalid for 'dim' argument.");
            }
            // Get number of dimensions and number of elements of input tensor.
            var x_ndim = input.shape.ndim;
            var x_numel = input.shape.numel();
            // Clean up the dimension list.
            // [1, 1] -> ArgumentException
            // [0, 1] -> [0, 1]
            // [0, -1] -> [0, <last dimension number>]
            // etc.
            var _dim = new List<int>();
            for(int i = 0; i < dim.Count; ++i)
            {
                var current = (dim[i] >= 0) ? dim[i] : (x_ndim - dim[i]);
                if(!_dim.Contains(current))
                {
                    _dim.Add(current);
                }
                else
                {
                    throw new ArgumentException(string.Format("Dim {0} appears multiple times in the list of dims.", current));
                }
            }
            var reduced_dims_count = _dim.Count;
            // Compute reduced shapes.
            // reduced_shape: with single dimensions instead of reduced.
            // reduced_shape: without single dimensions instead of reduced.
            var reduced_shape = new List<int>();
            var reduced_shape_squeezed = new List<int>();
            var reduced_elements = 1;
            var x_shape = input.shape.data_ptr;
            for(int i = 0; i < x_ndim; ++i)
            {
                if(_dim.Contains(i))
                {
                    reduced_shape.Add(1);
                    reduced_elements *= x_shape[i];
                    continue;
                }
                reduced_shape.Add(input.shape[i]);
                reduced_shape_squeezed.Add(input.shape[i]);
            }
            // compute strides of the input tensor.
            var x_strides = (int*)Marshal.AllocHGlobal(x_ndim * sizeof(int));
            if(x_ndim > 0)
            {
                x_strides[x_ndim - 1] = 1;
            }
            for(int i = x_ndim - 2; i >= 0; --i)
            {
                x_strides[i] = x_strides[i + 1] * x_shape[i + 1];
            }
            // Reduce
            switch(input.dtype)
            {
                case torchlite.float32:
                {
                    var result = torchlite.zeros(keepdim ? reduced_shape : reduced_shape_squeezed, dtype: input.dtype, requires_grad: input.requires_grad);
                    var y_shape = result.shape.data_ptr;
                    var x = (float*)input.storage.data_ptr;
                    var y = (float*)result.storage.data_ptr;
                    for(int i = 0; i < x_numel; ++i)
                    {
                        var y_i = 0;
                        var idx = 0;
                        var x_i = i;
                        for(int j = 0; j < x_ndim; ++j)
                        {
                            var loc = x_i / x_strides[j];
                            x_i -= loc * x_strides[j];
                            for(int k = 0; k < reduced_dims_count; ++k)
                            {
                                if(j == _dim[k])
                                {
                                    goto skip_dim;
                                }
                            }
                            y_i *= y_shape[idx++];
                            y_i += loc;
                          skip_dim:;
                        }
                        y[y_i] += x[i];
                    }
                    var y_numel = result.shape.numel();
                    for(int i = 0; i < y_numel; ++i)
                    {
                        y[i] /= reduced_elements;
                    }
                    if(result.requires_grad)
                    {
                        result.__parents = new []{input};
                        result.backward_fn = () =>
                        {
                            var y_grad = (float*)result.grad.storage.data_ptr;
                            var x_grad = (float*)input.grad.storage.data_ptr;
                            for(int i = 0; i < x_numel; ++i)
                            {
                                var y_i = 0;
                                var idx = 0;
                                var x_i = i;
                                for(int j = 0; j < x_ndim; ++j)
                                {
                                    var loc = x_i / x_strides[j];
                                    x_i -= loc * x_strides[j];
                                    for(int k = 0; k < reduced_dims_count; ++k)
                                    {
                                        if(j == _dim[k])
                                        {
                                            goto skip_dim;
                                        }
                                    }
                                    y_i *= y_shape[idx++];
                                    y_i += loc;
                                  skip_dim:;
                                }
                                x_grad[i] += y_grad[y_i] / reduced_elements;
                            }
                            Marshal.FreeHGlobal((IntPtr)x_strides);
                        };
                    }
                    else
                    {
                        Marshal.FreeHGlobal((IntPtr)x_strides);
                    }
                    return result;
                }
                case torchlite.int32:
                {
                    var result = torchlite.zeros(keepdim ? reduced_shape : reduced_shape_squeezed, dtype: input.dtype, requires_grad: input.requires_grad);
                    var y_shape = result.shape.data_ptr;
                    var x = (int*)input.storage.data_ptr;
                    var y = (int*)result.storage.data_ptr;
                    for(int i = 0; i < x_numel; ++i)
                    {
                        var y_i = 0;
                        var idx = 0;
                        var x_i = i;
                        for(int j = 0; j < x_ndim; ++j)
                        {
                            var loc = x_i / x_strides[j];
                            x_i -= loc * x_strides[j];
                            for(int k = 0; k < reduced_dims_count; ++k)
                            {
                                if(j == _dim[k])
                                {
                                    goto skip_dim;
                                }
                            }
                            y_i *= y_shape[idx++];
                            y_i += loc;
                          skip_dim:;
                        }
                        y[y_i] += x[i];
                    }
                    var y_numel = result.shape.numel();
                    for(int i = 0; i < y_numel; ++i)
                    {
                        y[i] /= reduced_elements;
                    }
                    Marshal.FreeHGlobal((IntPtr)x_strides);
                    return result;
                }
                case torchlite.@bool:
                {
                    throw new NotImplementedException();
                }
                default:
                {
                    throw new TypeAccessException();
                }
            }
        }

    }

}