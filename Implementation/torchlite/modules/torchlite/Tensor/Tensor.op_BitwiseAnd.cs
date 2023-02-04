//***************************************************************************************************
//* (C) ColorfulSoft corp., 2019-2023. All rights reserved.
//* The code is available under the Apache-2.0 license. Read the License for details.
//***************************************************************************************************

using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace System.AI.Experimental
{

    public static partial class torchlite
    {

        public unsafe partial class Tensor
        {

            /// <summary>
            /// Performs elementwise and of left and right tensors with broadcasting.
            /// </summary>
            /// <param name="left">Left operand.</param>
            /// <param name="right">Right operand.</param>
            /// <returns>Tensor.</returns>
            public static Tensor operator &(Tensor left, Tensor right)
            {
                if(((object)left == null) || ((object)right == null))
                {
                    throw new NullReferenceException("Null value is invalid for operand.");
                }
                if(left.dtype.is_floating_point() || right.dtype.is_floating_point())
                {
                    throw new ArgumentException("The & operator is not implemented for floating point tensors.");
                }
                // Get unmanaged shapes and compute strides
                var left_ndim = left.shape.ndim;
                var left_shape = left.shape.data_ptr;
                var left_strides = (int*)Marshal.AllocHGlobal(left_ndim * sizeof(int));
                if(left_ndim > 0)
                {
                    left_strides[left_ndim - 1] = 1;
                }
                for(int i = left_ndim - 2; i >= 0; --i)
                {
                    left_strides[i] = left_strides[i + 1] * left_shape[i + 1];
                }
                var right_ndim = right.shape.ndim;
                var right_shape = right.shape.data_ptr;
                var right_strides = (int*)Marshal.AllocHGlobal(right_ndim * sizeof(int));
                if(right_ndim > 0)
                {
                    right_strides[right_ndim - 1] = 1;
                }
                for(int i = right_ndim - 2; i >= 0; --i)
                {
                    right_strides[i] = right_strides[i + 1] * right_shape[i + 1];
                }
                // Compute result shape and broadcasting flags
                var result_ndim = Math.Max(left_ndim, right_ndim);
                var result_shape_ = new int[result_ndim];
                var left_broadcast_dims = (int*)Marshal.AllocHGlobal(left_ndim * sizeof(int));
                var right_broadcast_dims = (int*)Marshal.AllocHGlobal(right_ndim * sizeof(int));
                for(int i = 0; i < result_ndim; ++i)
                {
                    int az = (((left_ndim - i - 1) < left_ndim) && ((left_ndim - i - 1) > -1)) ? left_shape[left_ndim - i - 1] : 1;
                    int bz = (((right_ndim - i - 1) < right_ndim) && ((right_ndim - i - 1) > -1)) ? right_shape[right_ndim - i - 1] : 1;
                    if((az > 1) && (bz > 1) && (az != bz))
                    {
                        throw new ArgumentException("Tensors are not broadcastable.");
                    }
                    result_shape_[result_ndim - 1 - i] = Math.Max(az, bz);
                    if(i < left_ndim)
                    {
                        var dimA = left_ndim - 1 - i;
                        var azA = ((dimA > -1) && (dimA < left_ndim)) ? left_shape[dimA] : 1;
                        var bzA = (((result_ndim - i - 1) > -1) && ((result_ndim - 1 - i) < result_ndim)) ? result_shape_[result_ndim - 1 - i] : 1;
                        if((bzA > 1) && (azA == 1))
                        {
                            left_broadcast_dims[(left_ndim - 1) - i] = 1;
                        }
                        else
                        {
                            left_broadcast_dims[(left_ndim - 1) - i] = 0;
                        }
                    }
                    if(i < right_ndim)
                    {
                        var dimB = right_ndim - 1 - i;
                        var azB = ((dimB > -1) && (dimB < right_ndim)) ? right_shape[dimB] : 1;
                        var bzB = (((result_ndim - i - 1) > -1) && ((result_ndim - 1 - i) < result_ndim)) ? result_shape_[result_ndim - 1 - i] : 1;
                        if((bzB > 1) && (azB == 1))
                        {
                            right_broadcast_dims[(right_ndim - 1) - i] = 1;
                        }
                        else
                        {
                            right_broadcast_dims[(right_ndim - 1) - i] = 0;
                        }
                    }
                }
                var result_strides = (int*)Marshal.AllocHGlobal(result_ndim * sizeof(int));
                var result_numel = 1;
                if(result_ndim > 0)
                {
                    result_strides[result_ndim - 1] = 1;
                    result_numel *= result_shape_[result_ndim - 1];
                }
                for(int i = result_ndim - 2; i >= 0; --i)
                {
                    result_strides[i] = result_strides[i + 1] * result_shape_[i + 1];
                    result_numel *= result_shape_[i];
                }
                // Get parents for result tensor
                var parents = new List<Tensor>();
                if(left.requires_grad)
                {
                    parents.Add(left);
                }
                if(right.requires_grad)
                {
                    parents.Add(right);
                }
                // Compute!
                switch(left.dtype)
                {
                    case torchlite.int32:
                    {
                        var left_ptr = (int*)left.storage.data_ptr;
                        switch(right.dtype)
                        {
                            case torchlite.int32:
                            {
                                var result = new Tensor(result_shape_, torchlite.int32);
                                var result_shape = result.shape.data_ptr;
                                var right_ptr = (int*)right.storage.data_ptr;
                                var result_ptr = (int*)result.storage.data_ptr;
                                for(int i = 0; i < result_numel; ++i)
                                {
                                    var index = i;
                                    int li = 0;
                                    int ldim = 0;
                                    int ri = 0;
                                    int rdim = 0;
                                    for(int j = 0; j < result_ndim; ++j)
                                    {
                                        int loc = index / result_strides[j];
                                        index -= loc * result_strides[j];
                                        if(j >= (result_ndim - left_ndim))
                                        {
                                            int temp = loc;
                                            if(left_broadcast_dims[ldim] > 0)
                                            {
                                                temp = 0;
                                            }
                                            li += left_strides[ldim++] * temp;
                                        }
                                        if(j >= (result_ndim - right_ndim))
                                        {
                                            int temp = loc;
                                            if(right_broadcast_dims[rdim] > 0)
                                            {
                                                temp = 0;
                                            }
                                            ri += right_strides[rdim++] * temp;
                                        }
                                    }
                                    result_ptr[i] = left_ptr[li] & right_ptr[ri];
                                }
                                Marshal.FreeHGlobal((IntPtr)left_strides);
                                Marshal.FreeHGlobal((IntPtr)right_strides);
                                Marshal.FreeHGlobal((IntPtr)result_strides);
                                Marshal.FreeHGlobal((IntPtr)left_broadcast_dims);
                                Marshal.FreeHGlobal((IntPtr)right_broadcast_dims);
                                return result;
                            }
                            case torchlite.@bool:
                            {
                                var result = new Tensor(result_shape_, torchlite.int32);
                                var result_shape = result.shape.data_ptr;
                                var right_ptr = (byte*)right.storage.data_ptr;
                                var result_ptr = (int*)result.storage.data_ptr;
                                for(int i = 0; i < result_numel; ++i)
                                {
                                    var index = i;
                                    int li = 0;
                                    int ldim = 0;
                                    int ri = 0;
                                    int rdim = 0;
                                    for(int j = 0; j < result_ndim; ++j)
                                    {
                                        int loc = index / result_strides[j];
                                        index -= loc * result_strides[j];
                                        if(j >= (result_ndim - left_ndim))
                                        {
                                            int temp = loc;
                                            if(left_broadcast_dims[ldim] > 0)
                                            {
                                                temp = 0;
                                            }
                                            li += left_strides[ldim++] * temp;
                                        }
                                        if(j >= (result_ndim - right_ndim))
                                        {
                                            int temp = loc;
                                            if(right_broadcast_dims[rdim] > 0)
                                            {
                                                temp = 0;
                                            }
                                            ri += right_strides[rdim++] * temp;
                                        }
                                    }
                                    result_ptr[i] = left_ptr[li] & right_ptr[ri];
                                }
                                Marshal.FreeHGlobal((IntPtr)left_strides);
                                Marshal.FreeHGlobal((IntPtr)right_strides);
                                Marshal.FreeHGlobal((IntPtr)result_strides);
                                Marshal.FreeHGlobal((IntPtr)left_broadcast_dims);
                                Marshal.FreeHGlobal((IntPtr)right_broadcast_dims);
                                return result;
                            }
                            default:
                            {
                                Marshal.FreeHGlobal((IntPtr)left_strides);
                                Marshal.FreeHGlobal((IntPtr)right_strides);
                                Marshal.FreeHGlobal((IntPtr)result_strides);
                                Marshal.FreeHGlobal((IntPtr)left_broadcast_dims);
                                Marshal.FreeHGlobal((IntPtr)right_broadcast_dims);
                                throw new NotImplementedException();
                            }
                        }
                    }
                    case torchlite.@bool:
                    {
                        var left_ptr = (byte*)left.storage.data_ptr;
                        switch(right.dtype)
                        {
                            case torchlite.int32:
                            {
                                var result = new Tensor(result_shape_, torchlite.int32);
                                var result_shape = result.shape.data_ptr;
                                var right_ptr = (int*)right.storage.data_ptr;
                                var result_ptr = (int*)result.storage.data_ptr;
                                for(int i = 0; i < result_numel; ++i)
                                {
                                    var index = i;
                                    int li = 0;
                                    int ldim = 0;
                                    int ri = 0;
                                    int rdim = 0;
                                    for(int j = 0; j < result_ndim; ++j)
                                    {
                                        int loc = index / result_strides[j];
                                        index -= loc * result_strides[j];
                                        if(j >= (result_ndim - left_ndim))
                                        {
                                            int temp = loc;
                                            if(left_broadcast_dims[ldim] > 0)
                                            {
                                                temp = 0;
                                            }
                                            li += left_strides[ldim++] * temp;
                                        }
                                        if(j >= (result_ndim - right_ndim))
                                        {
                                            int temp = loc;
                                            if(right_broadcast_dims[rdim] > 0)
                                            {
                                                temp = 0;
                                            }
                                            ri += right_strides[rdim++] * temp;
                                        }
                                    }
                                    result_ptr[i] = left_ptr[li] & right_ptr[ri];
                                }
                                Marshal.FreeHGlobal((IntPtr)left_strides);
                                Marshal.FreeHGlobal((IntPtr)right_strides);
                                Marshal.FreeHGlobal((IntPtr)result_strides);
                                Marshal.FreeHGlobal((IntPtr)left_broadcast_dims);
                                Marshal.FreeHGlobal((IntPtr)right_broadcast_dims);
                                return result;
                            }
                            case torchlite.@bool:
                            {
                                var result = new Tensor(result_shape_, torchlite.@bool);
                                var result_shape = result.shape.data_ptr;
                                var left_ptr_ = (bool*)left_ptr;
                                var right_ptr = (bool*)right.storage.data_ptr;
                                var result_ptr = (bool*)result.storage.data_ptr;
                                for(int i = 0; i < result_numel; ++i)
                                {
                                    var index = i;
                                    int li = 0;
                                    int ldim = 0;
                                    int ri = 0;
                                    int rdim = 0;
                                    for(int j = 0; j < result_ndim; ++j)
                                    {
                                        int loc = index / result_strides[j];
                                        index -= loc * result_strides[j];
                                        if(j >= (result_ndim - left_ndim))
                                        {
                                            int temp = loc;
                                            if(left_broadcast_dims[ldim] > 0)
                                            {
                                                temp = 0;
                                            }
                                            li += left_strides[ldim++] * temp;
                                        }
                                        if(j >= (result_ndim - right_ndim))
                                        {
                                            int temp = loc;
                                            if(right_broadcast_dims[rdim] > 0)
                                            {
                                                temp = 0;
                                            }
                                            ri += right_strides[rdim++] * temp;
                                        }
                                    }
                                    result_ptr[i] = left_ptr_[li] && right_ptr[ri];
                                }
                                Marshal.FreeHGlobal((IntPtr)left_strides);
                                Marshal.FreeHGlobal((IntPtr)right_strides);
                                Marshal.FreeHGlobal((IntPtr)result_strides);
                                Marshal.FreeHGlobal((IntPtr)left_broadcast_dims);
                                Marshal.FreeHGlobal((IntPtr)right_broadcast_dims);
                                return result;
                            }
                            default:
                            {
                                Marshal.FreeHGlobal((IntPtr)left_strides);
                                Marshal.FreeHGlobal((IntPtr)right_strides);
                                Marshal.FreeHGlobal((IntPtr)result_strides);
                                Marshal.FreeHGlobal((IntPtr)left_broadcast_dims);
                                Marshal.FreeHGlobal((IntPtr)right_broadcast_dims);
                                throw new NotImplementedException();
                            }
                        }
                    }
                    default:
                    {
                        Marshal.FreeHGlobal((IntPtr)left_strides);
                        Marshal.FreeHGlobal((IntPtr)right_strides);
                        Marshal.FreeHGlobal((IntPtr)result_strides);
                        Marshal.FreeHGlobal((IntPtr)left_broadcast_dims);
                        Marshal.FreeHGlobal((IntPtr)right_broadcast_dims);
                        throw new NotImplementedException();
                    }
                }
            }

        }

    }

}