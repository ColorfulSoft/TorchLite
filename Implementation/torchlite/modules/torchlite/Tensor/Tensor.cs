//***************************************************************************************************
//* (C) ColorfulSoft corp., 2019-2023. All rights reserved.
//* The code is available under the Apache-2.0 license. Read the License for details.
//***************************************************************************************************

using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace System.AI.Experimental
{

    public static partial class torchlite
    {

        public unsafe partial class Tensor
        {

            #region fields

            /// <summary>
            /// Gradient tensor.
            /// </summary>
            internal Tensor __grad;

            #endregion

            #region properties

            /// <summary>
            /// The back propagation function.
            /// </summary>
            public Action backward_fn
            {

                get;

                set;

            }

            /// <summary>
            /// Parent tensors.
            /// </summary>
            public Tensor[] parents
            {

                get;

                set;

            }

            /// <summary>
            /// Storage for tensor's data.
            /// </summary>
            public Storage storage
            {

                get;

                internal set;

            }

            /// <summary>
            /// Tensor's shape.
            /// </summary>
            public Size shape
            {

                get;

                internal set;

            }

            /// <summary>
            /// Tensor's data type.
            /// </summary>
            public DType dtype
            {

                get
                {
                    return this.storage.dtype;
                }

            }

            /// <summary>
            /// Tensor's gradient.
            /// </summary>
            public Tensor grad
            {

                get
                {
                    return this.__grad;
                }

                set
                {
                    if(this.shape != value.shape)
                    {
                        throw new ArgumentException("Assigned grad has data of a different size.");
                    }
                    this.__grad = value;
                }

            }

            /// <summary>
            /// Indicates whether the tensor has a gradient.
            /// </summary>
            public bool requires_grad
            {

                get
                {
                    return (object)this.grad != null;
                }

            }

            public int ndim
            {

                get
                {
                    return this.shape.ndim;
                }

            }

            public Tensor data
            {

                get
                {
                    return new Tensor(this.storage, this.shape);
                }

                set
                {
                    if(this.shape != value.shape)
                    {
                        throw new ArgumentException("The size of the tensor and the data being set do not match.");
                    }
                    if(this.dtype != value.dtype)
                    {
                        throw new ArgumentException("The type of the current tensor and the data being set do not match.");
                    }
                    this.storage = value.storage;
                }

            }

            /// <summary>
            /// Provides tensor indexing by dimensions with slice support.
            /// </summary>
            public Tensor this[params object[] index]
            {

                get
                {
                    if(index.Length > this.shape.ndim)
                    {
                        throw new IndexOutOfRangeException(string.Format("Too many indexes for a {0}-dimensional tensor.", this.shape.ndim));
                    }
                    // Compute slices
                    int ndim = this.shape.ndim;
                    var begin = (int*)Marshal.AllocHGlobal(ndim * sizeof(int));
                    var x_shape = this.shape.data_ptr;
                    var y_shape = (int*)Marshal.AllocHGlobal(ndim * sizeof(int));
                    var shape = new List<int>();
                    for(int i = 0; i < index.Length; ++i)
                    {
                        var index_object = index[i];
                        if(index_object is int)
                        {
                            begin[i] = (int)index_object;
                            if(begin[i] < -x_shape[i])
                            {
                                var e = new IndexOutOfRangeException(string.Format("Index {0} is out of range of tensor of size {1} in {2} dimension.", begin[i], this.shape, i));
                                Marshal.FreeHGlobal((IntPtr)begin);
                                Marshal.FreeHGlobal((IntPtr)y_shape);
                                throw e;
                            }
                            if(begin[i] < 0)
                            {
                                begin[i] += x_shape[i];
                            }
                            if(begin[i] >= x_shape[i])
                            {
                                var e = new IndexOutOfRangeException(string.Format("Index {0} is out of range of tensor of size {1} in {2} dimension.", begin[i], this.shape, i));
                                Marshal.FreeHGlobal((IntPtr)begin);
                                Marshal.FreeHGlobal((IntPtr)y_shape);
                                throw e;
                            }
                            y_shape[i] = 1;
                            continue;
                        }
                        if(index_object is string)
                        {
                            var str = (string)index_object;
                            str = str.Replace(" ", "");
                            str = str.Replace("\t", "");
                            if(str.Length == 0)
                            {
                                var e = new ArgumentException("Empty string index is invalid.");
                                Marshal.FreeHGlobal((IntPtr)begin);
                                Marshal.FreeHGlobal((IntPtr)y_shape);
                                throw e;
                            }
                            if(str[0] == ':')
                            {
                                begin[i] = 0;
                                if(str.Length == 1)
                                {
                                    shape.Add(x_shape[i]);
                                    y_shape[i] = x_shape[i];
                                    continue;
                                }
                                var end = int.Parse(str.Substring(1));
                                if(end < -x_shape[i])
                                {
                                    var e = new IndexOutOfRangeException(string.Format("Index {0} is out of range of tensor of size {1} in {2} dimension.", end, this.shape, i));
                                    Marshal.FreeHGlobal((IntPtr)begin);
                                    Marshal.FreeHGlobal((IntPtr)y_shape);
                                    throw e;
                                }
                                if(end < 0)
                                {
                                    end += x_shape[i];
                                }
                                if(end <= begin[i])
                                {
                                    var e = new ArgumentException(string.Format("The index of the beginning of the slice must be less than the index of the end of the slice. Given [{0}, {1}) range.", begin[i], end));
                                    Marshal.FreeHGlobal((IntPtr)begin);
                                    Marshal.FreeHGlobal((IntPtr)y_shape);
                                    throw e;
                                }
                                if(end > x_shape[i])
                                {
                                    var e = new IndexOutOfRangeException(string.Format("Range [0, {0}) is out of range of tensor of size {1} in {2} dimension.", end, this.shape, i));
                                    Marshal.FreeHGlobal((IntPtr)begin);
                                    Marshal.FreeHGlobal((IntPtr)y_shape);
                                    throw e;
                                }
                                shape.Add(end - begin[i]);
                                y_shape[i] = end;
                                continue;
                            }
                            var pos = str.IndexOf(':');
                            if(pos > 0)
                            {
                                begin[i] = int.Parse(str.Substring(0, pos));
                                if(begin[i] < -x_shape[i])
                                {
                                    var e = new IndexOutOfRangeException(string.Format("Index {0} is out of range of tensor of size {1} in {2} dimension.", begin[i], this.shape, i));
                                    Marshal.FreeHGlobal((IntPtr)begin);
                                    Marshal.FreeHGlobal((IntPtr)y_shape);
                                    throw e;
                                }
                                if(begin[i] < 0)
                                {
                                    begin[i] += x_shape[i];
                                }
                                if((pos + 1) < str.Length)
                                {
                                    var end = int.Parse(str.Substring(pos + 1));
                                    if(end < -x_shape[i])
                                    {
                                        var e = new IndexOutOfRangeException(string.Format("Index {0} is out of range of tensor of size {1} in {2} dimension.", end, this.shape, i));
                                        Marshal.FreeHGlobal((IntPtr)begin);
                                        Marshal.FreeHGlobal((IntPtr)y_shape);
                                        throw e;
                                    }
                                    if(end < 0)
                                    {
                                        end += x_shape[i];
                                    }
                                    if(end <= begin[i])
                                    {
                                        var e = new ArgumentException(string.Format("The index of the beginning of the slice must be less than the index of the end of the slice. Given [{0}, {1}) range.", begin[i], end));
                                        Marshal.FreeHGlobal((IntPtr)begin);
                                        Marshal.FreeHGlobal((IntPtr)y_shape);
                                        throw e;
                                    }
                                    if((begin[i] >= x_shape[i]) || (end > x_shape[i]))
                                    {
                                        var e = new IndexOutOfRangeException(string.Format("Range [{0}, {1}) is out of range of tensor of size {2} in {3} dimension.", begin[i], end, this.shape, i));
                                        Marshal.FreeHGlobal((IntPtr)begin);
                                        Marshal.FreeHGlobal((IntPtr)y_shape);
                                        throw e;
                                    }
                                    shape.Add(end - begin[i]);
                                    y_shape[i] = end - begin[i];
                                    continue;
                                }
                                if(begin[i] >= x_shape[i])
                                {
                                    var e = new IndexOutOfRangeException(string.Format("Range [{0}, {1}) is out of range of tensor of size {2} in {3} dimension.", begin[i], x_shape[i], this.shape, i));
                                    Marshal.FreeHGlobal((IntPtr)begin);
                                    Marshal.FreeHGlobal((IntPtr)y_shape);
                                    throw e;
                                }
                                shape.Add(x_shape[i] - begin[i]);
                                y_shape[i] = x_shape[i] - begin[i];
                                continue;
                            }
                            begin[i] = int.Parse(str);
                            if(begin[i] < -x_shape[i])
                            {
                                var e = new IndexOutOfRangeException(string.Format("Index {0} is out of range of tensor of size {1} in {2} dimension.", begin[i], this.shape, i));
                                Marshal.FreeHGlobal((IntPtr)begin);
                                Marshal.FreeHGlobal((IntPtr)y_shape);
                                throw e;
                            }
                            if(begin[i] < 0)
                            {
                                begin[i] += x_shape[i];
                            }
                            if(begin[i] >= x_shape[i])
                            {
                                var e = new IndexOutOfRangeException(string.Format("Index {0} is out of range of tensor of size {1} in {2} dimension.", begin[i], this.shape, i));
                                Marshal.FreeHGlobal((IntPtr)begin);
                                Marshal.FreeHGlobal((IntPtr)y_shape);
                                throw e;
                            }
                            y_shape[i] = 1;
                            continue;
                        }
                        if(index_object is torchlite.Slice)
                        {
                            var slice = (torchlite.Slice)index_object;
                            begin[i] = slice.begin;
                            var end = slice.end;
                            if(begin[i] < -x_shape[i])
                            {
                                var e = new IndexOutOfRangeException(string.Format("Index {0} is out of range of tensor of size {1} in {2} dimension.", begin[i], this.shape, i));
                                Marshal.FreeHGlobal((IntPtr)begin);
                                Marshal.FreeHGlobal((IntPtr)y_shape);
                                throw e;
                            }
                            if(begin[i] < 0)
                            {
                                begin[i] += x_shape[i];
                            }
                            if(begin[i] >= x_shape[i])
                            {
                                var e = new IndexOutOfRangeException(string.Format("Index {0} is out of range of tensor of size {1} in {2} dimension.", begin[i], this.shape, i));
                                Marshal.FreeHGlobal((IntPtr)begin);
                                Marshal.FreeHGlobal((IntPtr)y_shape);
                                throw e;
                            }
                            if(end < -x_shape[i])
                            {
                                var e = new IndexOutOfRangeException(string.Format("Index {0} is out of range of tensor of size {1} in {2} dimension.", end, this.shape, i));
                                Marshal.FreeHGlobal((IntPtr)begin);
                                Marshal.FreeHGlobal((IntPtr)y_shape);
                                throw e;
                            }
                            if(end < 0)
                            {
                                end += x_shape[i];
                            }
                            if(end <= begin[i])
                            {
                                var e = new ArgumentException(string.Format("The index of the beginning of the slice must be less than the index of the end of the slice. Given [{0}, {1}) range.", begin[i], end));
                                Marshal.FreeHGlobal((IntPtr)begin);
                                Marshal.FreeHGlobal((IntPtr)y_shape);
                                throw e;
                            }
                            if((begin[i] >= x_shape[i]) || (end > x_shape[i]))
                            {
                                var e = new IndexOutOfRangeException(string.Format("Range [{0}, {1}) is out of range of tensor of size {2} in {3} dimension.", begin[i], end, this.shape, i));
                                Marshal.FreeHGlobal((IntPtr)begin);
                                Marshal.FreeHGlobal((IntPtr)y_shape);
                                throw e;
                            }
                            shape.Add(end - begin[i]);
                            continue;
                        }
                        Marshal.FreeHGlobal((IntPtr)begin);
                        Marshal.FreeHGlobal((IntPtr)y_shape);
                        throw new ArgumentException("Index may be of int, string or torchlite.Slice data type.");
                    }
                    for(int i = index.Length; i < ndim; ++i)
                    {
                        begin[i] = 0;
                        shape.Add(x_shape[i]);
                        y_shape[i] = x_shape[i];
                    }
                    // Compute strides
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
                    var y = new Tensor(shape, this.dtype, this.requires_grad);
                    var y_numel = y.numel();
                    switch(this.dtype)
                    {
                        case torchlite.float32:
                        {
                            var src = (float*)this.storage.data_ptr;
                            var dst = (float*)y.storage.data_ptr;
                            for(int i = 0; i < y_numel; ++i)
                            {
                                var y_loc = i;
                                var x_index = 0;
                                for(int j = 0; j < ndim; ++j)
                                {
                                    x_index += (y_loc / y_strides[j] + begin[j]) * x_strides[j];
                                    y_loc %= y_strides[j];
                                }
                                dst[i] = src[x_index];
                            }
                            if(y.requires_grad)
                            {
                                y.parents = new []{this};
                                y.backward_fn = () =>
                                {
                                    var src_grad = (float*)this.grad.storage.data_ptr;
                                    var dst_grad = (float*)y.grad.storage.data_ptr;
                                    for(int i = 0; i < y_numel; ++i)
                                    {
                                        var y_loc = i;
                                        var x_index = 0;
                                        for(int j = 0; j < ndim; ++j)
                                        {
                                            x_index += (y_loc / y_strides[j] + begin[j]) * x_strides[j];
                                            y_loc %= y_strides[j];
                                        }
                                        src_grad[x_index] += dst_grad[i];
                                    }
                                    Marshal.FreeHGlobal((IntPtr)begin);
                                    Marshal.FreeHGlobal((IntPtr)y_shape);
                                    Marshal.FreeHGlobal((IntPtr)x_strides);
                                    Marshal.FreeHGlobal((IntPtr)y_strides);
                                };
                            }
                            else
                            {
                                Marshal.FreeHGlobal((IntPtr)begin);
                                Marshal.FreeHGlobal((IntPtr)y_shape);
                                Marshal.FreeHGlobal((IntPtr)x_strides);
                                Marshal.FreeHGlobal((IntPtr)y_strides);
                            }
                            break;
                        }
                        case torchlite.int32:
                        {
                            var src = (int*)this.storage.data_ptr;
                            var dst = (int*)y.storage.data_ptr;
                            for(int i = 0; i < y_numel; ++i)
                            {
                                var y_loc = i;
                                var x_index = 0;
                                for(int j = 0; j < ndim; ++j)
                                {
                                    x_index += (y_loc / y_strides[j] + begin[j]) * x_strides[j];
                                    y_loc %= y_strides[j];
                                }
                                dst[i] = src[x_index];
                            }
                            Marshal.FreeHGlobal((IntPtr)begin);
                            Marshal.FreeHGlobal((IntPtr)y_shape);
                            Marshal.FreeHGlobal((IntPtr)x_strides);
                            Marshal.FreeHGlobal((IntPtr)y_strides);
                            break;
                        }
                        case torchlite.@bool:
                        {
                            var src = (bool*)this.storage.data_ptr;
                            var dst = (bool*)y.storage.data_ptr;
                            for(int i = 0; i < y_numel; ++i)
                            {
                                var y_loc = i;
                                var x_index = 0;
                                for(int j = 0; j < ndim; ++j)
                                {
                                    x_index += (y_loc / y_strides[j] + begin[j]) * x_strides[j];
                                    y_loc %= y_strides[j];
                                }
                                dst[i] = src[x_index];
                            }
                            Marshal.FreeHGlobal((IntPtr)begin);
                            Marshal.FreeHGlobal((IntPtr)y_shape);
                            Marshal.FreeHGlobal((IntPtr)x_strides);
                            Marshal.FreeHGlobal((IntPtr)y_strides);
                            break;
                        }
                    }
                    return y;
                }

                set
                {
                    if(index.Length > this.shape.ndim)
                    {
                        throw new IndexOutOfRangeException(string.Format("Too many indexes for a {0}-dimensional tensor.", this.shape.ndim));
                    }
                    if(this.requires_grad)
                    {
                        throw new InvalidProgramException("A view of a leaf Variable that requires grad is being used in an in-place operation.");
                    }
                    // Compute slices
                    int ndim = this.shape.ndim;
                    var begin = (int*)Marshal.AllocHGlobal(ndim * sizeof(int));
                    var x_shape = this.shape.data_ptr;
                    var y_shape = (int*)Marshal.AllocHGlobal(ndim * sizeof(int));
                    for(int i = 0; i < index.Length; ++i)
                    {
                        var index_object = index[i];
                        if(index_object is int)
                        {
                            begin[i] = (int)index_object;
                            if(begin[i] < -x_shape[i])
                            {
                                var e = new IndexOutOfRangeException(string.Format("Index {0} is out of range of tensor of size {1} in {2} dimension.", begin[i], this.shape, i));
                                Marshal.FreeHGlobal((IntPtr)begin);
                                Marshal.FreeHGlobal((IntPtr)y_shape);
                                throw e;
                            }
                            if(begin[i] < 0)
                            {
                                begin[i] += x_shape[i];
                            }
                            if(begin[i] >= x_shape[i])
                            {
                                var e = new IndexOutOfRangeException(string.Format("Index {0} is out of range of tensor of size {1} in {2} dimension.", begin[i], this.shape, i));
                                Marshal.FreeHGlobal((IntPtr)begin);
                                Marshal.FreeHGlobal((IntPtr)y_shape);
                                throw e;
                            }
                            y_shape[i] = 1;
                            continue;
                        }
                        if(index_object is string)
                        {
                            var str = (string)index_object;
                            str = str.Replace(" ", "");
                            str = str.Replace("\t", "");
                            if(str.Length == 0)
                            {
                                var e = new ArgumentException("Empty string index is invalid.");
                                Marshal.FreeHGlobal((IntPtr)begin);
                                Marshal.FreeHGlobal((IntPtr)y_shape);
                                throw e;
                            }
                            if(str[0] == ':')
                            {
                                begin[i] = 0;
                                if(str.Length == 1)
                                {
                                    y_shape[i] = x_shape[i];
                                    continue;
                                }
                                var end = int.Parse(str.Substring(1));
                                if(end < -x_shape[i])
                                {
                                    var e = new IndexOutOfRangeException(string.Format("Index {0} is out of range of tensor of size {1} in {2} dimension.", end, this.shape, i));
                                    Marshal.FreeHGlobal((IntPtr)begin);
                                    Marshal.FreeHGlobal((IntPtr)y_shape);
                                    throw e;
                                }
                                if(end < 0)
                                {
                                    end += x_shape[i];
                                }
                                if(end <= begin[i])
                                {
                                    var e = new ArgumentException(string.Format("The index of the beginning of the slice must be less than the index of the end of the slice. Given [{0}, {1}) range.", begin[i], end));
                                    Marshal.FreeHGlobal((IntPtr)begin);
                                    Marshal.FreeHGlobal((IntPtr)y_shape);
                                    throw e;
                                }
                                if(end > x_shape[i])
                                {
                                    var e = new IndexOutOfRangeException(string.Format("Range [0, {0}) is out of range of tensor of size {1} in {2} dimension.", end, this.shape, i));
                                    Marshal.FreeHGlobal((IntPtr)begin);
                                    Marshal.FreeHGlobal((IntPtr)y_shape);
                                    throw e;
                                }
                                y_shape[i] = end;
                                continue;
                            }
                            var pos = str.IndexOf(':');
                            if(pos > 0)
                            {
                                begin[i] = int.Parse(str.Substring(0, pos));
                                if(begin[i] < -x_shape[i])
                                {
                                    var e = new IndexOutOfRangeException(string.Format("Index {0} is out of range of tensor of size {1} in {2} dimension.", begin[i], this.shape, i));
                                    Marshal.FreeHGlobal((IntPtr)begin);
                                    Marshal.FreeHGlobal((IntPtr)y_shape);
                                    throw e;
                                }
                                if(begin[i] < 0)
                                {
                                    begin[i] += x_shape[i];
                                }
                                if((pos + 1) < str.Length)
                                {
                                    var end = int.Parse(str.Substring(pos + 1));
                                    if(end < -x_shape[i])
                                    {
                                        var e = new IndexOutOfRangeException(string.Format("Index {0} is out of range of tensor of size {1} in {2} dimension.", end, this.shape, i));
                                        Marshal.FreeHGlobal((IntPtr)begin);
                                        Marshal.FreeHGlobal((IntPtr)y_shape);
                                        throw e;
                                    }
                                    if(end < 0)
                                    {
                                        end += x_shape[i];
                                    }
                                    if(end <= begin[i])
                                    {
                                        var e = new ArgumentException(string.Format("The index of the beginning of the slice must be less than the index of the end of the slice. Given [{0}, {1}) range.", begin[i], end));
                                        Marshal.FreeHGlobal((IntPtr)begin);
                                        Marshal.FreeHGlobal((IntPtr)y_shape);
                                        throw e;
                                    }
                                    if((begin[i] >= x_shape[i]) || (end > x_shape[i]))
                                    {
                                        var e = new IndexOutOfRangeException(string.Format("Range [{0}, {1}) is out of range of tensor of size {2} in {3} dimension.", begin[i], end, this.shape, i));
                                        Marshal.FreeHGlobal((IntPtr)begin);
                                        Marshal.FreeHGlobal((IntPtr)y_shape);
                                        throw e;
                                    }
                                    y_shape[i] = end - begin[i];
                                    continue;
                                }
                                if(begin[i] >= x_shape[i])
                                {
                                    var e = new IndexOutOfRangeException(string.Format("Range [{0}, {1}) is out of range of tensor of size {2} in {3} dimension.", begin[i], x_shape[i], this.shape, i));
                                    Marshal.FreeHGlobal((IntPtr)begin);
                                    Marshal.FreeHGlobal((IntPtr)y_shape);
                                    throw e;
                                }
                                y_shape[i] = x_shape[i] - begin[i];
                                continue;
                            }
                            begin[i] = int.Parse(str);
                            if(begin[i] < -x_shape[i])
                            {
                                var e = new IndexOutOfRangeException(string.Format("Index {0} is out of range of tensor of size {1} in {2} dimension.", begin[i], this.shape, i));
                                Marshal.FreeHGlobal((IntPtr)begin);
                                Marshal.FreeHGlobal((IntPtr)y_shape);
                                throw e;
                            }
                            if(begin[i] < 0)
                            {
                                begin[i] += x_shape[i];
                            }
                            if(begin[i] >= x_shape[i])
                            {
                                var e = new IndexOutOfRangeException(string.Format("Index {0} is out of range of tensor of size {1} in {2} dimension.", begin[i], this.shape, i));
                                Marshal.FreeHGlobal((IntPtr)begin);
                                Marshal.FreeHGlobal((IntPtr)y_shape);
                                throw e;
                            }
                            y_shape[i] = 1;
                        }
                        if(index_object is torchlite.Slice)
                        {
                            var slice = (torchlite.Slice)index_object;
                            begin[i] = slice.begin;
                            var end = slice.end;
                            if(begin[i] < -x_shape[i])
                            {
                                var e = new IndexOutOfRangeException(string.Format("Index {0} is out of range of tensor of size {1} in {2} dimension.", begin[i], this.shape, i));
                                Marshal.FreeHGlobal((IntPtr)begin);
                                Marshal.FreeHGlobal((IntPtr)y_shape);
                                throw e;
                            }
                            if(begin[i] < 0)
                            {
                                begin[i] += x_shape[i];
                            }
                            if(begin[i] >= x_shape[i])
                            {
                                var e = new IndexOutOfRangeException(string.Format("Index {0} is out of range of tensor of size {1} in {2} dimension.", begin[i], this.shape, i));
                                Marshal.FreeHGlobal((IntPtr)begin);
                                Marshal.FreeHGlobal((IntPtr)y_shape);
                                throw e;
                            }
                            if(end < -x_shape[i])
                            {
                                var e = new IndexOutOfRangeException(string.Format("Index {0} is out of range of tensor of size {1} in {2} dimension.", end, this.shape, i));
                                Marshal.FreeHGlobal((IntPtr)begin);
                                Marshal.FreeHGlobal((IntPtr)y_shape);
                                throw e;
                            }
                            if(end < 0)
                            {
                                end += x_shape[i];
                            }
                            if(end <= begin[i])
                            {
                                var e = new ArgumentException(string.Format("The index of the beginning of the slice must be less than the index of the end of the slice. Given [{0}, {1}) range.", begin[i], end));
                                Marshal.FreeHGlobal((IntPtr)begin);
                                Marshal.FreeHGlobal((IntPtr)y_shape);
                                throw e;
                            }
                            if((begin[i] >= x_shape[i]) || (end > x_shape[i]))
                            {
                                var e = new IndexOutOfRangeException(string.Format("Range [{0}, {1}) is out of range of tensor of size {2} in {3} dimension.", begin[i], end, this.shape, i));
                                Marshal.FreeHGlobal((IntPtr)begin);
                                Marshal.FreeHGlobal((IntPtr)y_shape);
                                throw e;
                            }
                            continue;
                        }
                        Marshal.FreeHGlobal((IntPtr)begin);
                        Marshal.FreeHGlobal((IntPtr)y_shape);
                        throw new ArgumentException("Index may be of int, string or torchlite.Slice data type.");
                    }
                    for(int i = index.Length; i < ndim; ++i)
                    {
                        begin[i] = 0;
                        y_shape[i] = x_shape[i];
                    }
                    // Compute compatible shape of v
                    var v_shape = value.shape.data_ptr;
                    if(value.shape.ndim < ndim)
                    {
                        var v_shape_ = (int*)Marshal.AllocHGlobal(ndim * sizeof(int));
                        var bias = ndim - value.shape.ndim;
                        for(int i = 0; i < bias; ++i)
                        {
                            v_shape_[i] = 1;
                        }
                        for(int i = bias; i < ndim; ++i)
                        {
                            v_shape_[i] = v_shape[i - bias];
                        }
                        v_shape = v_shape_;
                    }
                    else
                    {
                        var v_shape_ = (int*)Marshal.AllocHGlobal(ndim * sizeof(int));
                        var bias = value.shape.ndim - ndim;
                        for(int i = 0; i < bias; ++i)
                        {
                            if(v_shape[i] != 1)
                            {
                                var e = new ArgumentException(string.Format("Sizes of slice and assignable tensor are incompatible in dimension {0}.", i - bias));
                                Marshal.FreeHGlobal((IntPtr)begin);
                                Marshal.FreeHGlobal((IntPtr)y_shape);
                                Marshal.FreeHGlobal((IntPtr)v_shape_);
                                throw e;
                            }
                        }
                        for(int i = bias; i < ndim + bias; ++i)
                        {
                            v_shape_[i - bias] = v_shape[i];
                        }
                        v_shape = v_shape_;
                    }
                    // Check v_shape
                    for(int i = 0; i < ndim; ++i)
                    {
                        if((v_shape[i] != 1) && (v_shape[i] != y_shape[i]))
                        {
                            var e = new ArgumentException(string.Format("Sizes of slice and assignable tensor are incompatible in dimension {0}.", i));
                            Marshal.FreeHGlobal((IntPtr)begin);
                            Marshal.FreeHGlobal((IntPtr)y_shape);
                            Marshal.FreeHGlobal((IntPtr)v_shape);
                            throw e;
                        }
                    }
                    // Compute strides
                    var y_strides = (int*)Marshal.AllocHGlobal(ndim * sizeof(int));
                    var y_numel = 1;
                    if(ndim > 0)
                    {
                        y_strides[ndim - 1] = 1;
                        y_numel = y_shape[ndim - 1];
                    }
                    for(int i = ndim - 2; i >= 0; --i)
                    {
                        y_strides[i] = y_strides[i + 1] * y_shape[i + 1];
                        y_numel *= y_shape[i];
                    }
                    var v_strides = (int*)Marshal.AllocHGlobal(ndim * sizeof(int));
                    if(ndim > 0)
                    {
                        v_strides[ndim - 1] = 1;
                    }
                    for(int i = ndim - 2; i >= 0; --i)
                    {
                        v_strides[i] = v_strides[i + 1] * v_shape[i + 1];
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
                    switch(this.dtype)
                    {
                        case torchlite.float32:
                        {
                            switch(value.dtype)
                            {
                                case torchlite.float32:
                                {
                                    var src = (float*)value.storage.data_ptr;
                                    var dst = (float*)this.storage.data_ptr;
                                    for(int i = 0; i < y_numel; ++i)
                                    {
                                        var y_loc = i;
                                        var x_index = 0;
                                        var v_index = 0;
                                        for(int j = 0; j < ndim; ++j)
                                        {
                                            x_index += (y_loc / y_strides[j] + begin[j]) * x_strides[j];
                                            v_index += (Math.Min(y_loc / y_strides[j] + 1, v_shape[j]) - 1) * v_strides[j];
                                            y_loc %= y_strides[j];
                                        }
                                        dst[x_index] = src[v_index];
                                    }
                                    if(value.requires_grad)
                                    {
                                        this.grad = torchlite.zeros(this.shape);
                                        this.parents = new []{value};
                                        this.backward_fn = () =>
                                        {
                                            var src_grad = (float*)value.grad.storage.data_ptr;
                                            var dst_grad = (float*)this.grad.storage.data_ptr;
                                            for(int i = 0; i < y_numel; ++i)
                                            {
                                                var y_loc = i;
                                                var x_index = 0;
                                                var v_index = 0;
                                                for(int j = 0; j < ndim; ++j)
                                                {
                                                    x_index += (y_loc / y_strides[j] + begin[j]) * x_strides[j];
                                                    v_index += (Math.Min(y_loc / y_strides[j] + 1, v_shape[j]) - 1) * v_strides[j];
                                                    y_loc %= y_strides[j];
                                                }
                                                src_grad[v_index] += dst_grad[x_index];
                                                dst_grad[x_index] = 0;
                                            }
                                            Marshal.FreeHGlobal((IntPtr)begin);
                                            Marshal.FreeHGlobal((IntPtr)x_strides);
                                            Marshal.FreeHGlobal((IntPtr)v_strides);
                                            Marshal.FreeHGlobal((IntPtr)y_strides);
                                            Marshal.FreeHGlobal((IntPtr)y_shape);
                                            Marshal.FreeHGlobal((IntPtr)v_shape);
                                        };
                                    }
                                    else
                                    {
                                        Marshal.FreeHGlobal((IntPtr)begin);
                                        Marshal.FreeHGlobal((IntPtr)x_strides);
                                        Marshal.FreeHGlobal((IntPtr)v_strides);
                                        Marshal.FreeHGlobal((IntPtr)y_strides);
                                        Marshal.FreeHGlobal((IntPtr)y_shape);
                                        Marshal.FreeHGlobal((IntPtr)v_shape);
                                    }
                                    break;
                                }
                                case torchlite.int32:
                                {
                                    var src = (int*)value.storage.data_ptr;
                                    var dst = (float*)this.storage.data_ptr;
                                    for(int i = 0; i < y_numel; ++i)
                                    {
                                        var y_loc = i;
                                        var x_index = 0;
                                        var v_index = 0;
                                        for(int j = 0; j < ndim; ++j)
                                        {
                                            x_index += (y_loc / y_strides[j] + begin[j]) * x_strides[j];
                                            v_index += (Math.Min(y_loc / y_strides[j] + 1, v_shape[j]) - 1) * v_strides[j];
                                            y_loc %= y_strides[j];
                                        }
                                        dst[x_index] = src[v_index];
                                    }
                                    Marshal.FreeHGlobal((IntPtr)begin);
                                    Marshal.FreeHGlobal((IntPtr)x_strides);
                                    Marshal.FreeHGlobal((IntPtr)v_strides);
                                    Marshal.FreeHGlobal((IntPtr)y_strides);
                                    Marshal.FreeHGlobal((IntPtr)y_shape);
                                    Marshal.FreeHGlobal((IntPtr)v_shape);
                                    break;
                                }
                                case torchlite.@bool:
                                {
                                    var src = (byte*)value.storage.data_ptr;
                                    var dst = (float*)this.storage.data_ptr;
                                    for(int i = 0; i < y_numel; ++i)
                                    {
                                        var y_loc = i;
                                        var x_index = 0;
                                        var v_index = 0;
                                        for(int j = 0; j < ndim; ++j)
                                        {
                                            x_index += (y_loc / y_strides[j] + begin[j]) * x_strides[j];
                                            v_index += (Math.Min(y_loc / y_strides[j] + 1, v_shape[j]) - 1) * v_strides[j];
                                            y_loc %= y_strides[j];
                                        }
                                        dst[x_index] = src[v_index];
                                    }
                                    Marshal.FreeHGlobal((IntPtr)begin);
                                    Marshal.FreeHGlobal((IntPtr)x_strides);
                                    Marshal.FreeHGlobal((IntPtr)v_strides);
                                    Marshal.FreeHGlobal((IntPtr)y_strides);
                                    Marshal.FreeHGlobal((IntPtr)y_shape);
                                    Marshal.FreeHGlobal((IntPtr)v_shape);
                                    break;
                                }
                            }
                            break;
                        }
                        case torchlite.int32:
                        {
                            switch(value.dtype)
                            {
                                case torchlite.float32:
                                {
                                    var src = (float*)value.storage.data_ptr;
                                    var dst = (int*)this.storage.data_ptr;
                                    for(int i = 0; i < y_numel; ++i)
                                    {
                                        var y_loc = i;
                                        var x_index = 0;
                                        var v_index = 0;
                                        for(int j = 0; j < ndim; ++j)
                                        {
                                            x_index += (y_loc / y_strides[j] + begin[j]) * x_strides[j];
                                            v_index += (Math.Min(y_loc / y_strides[j] + 1, v_shape[j]) - 1) * v_strides[j];
                                            y_loc %= y_strides[j];
                                        }
                                        dst[x_index] = (int)src[v_index];
                                    }
                                    Marshal.FreeHGlobal((IntPtr)begin);
                                    Marshal.FreeHGlobal((IntPtr)x_strides);
                                    Marshal.FreeHGlobal((IntPtr)v_strides);
                                    Marshal.FreeHGlobal((IntPtr)y_strides);
                                    Marshal.FreeHGlobal((IntPtr)y_shape);
                                    Marshal.FreeHGlobal((IntPtr)v_shape);
                                    break;
                                }
                                case torchlite.int32:
                                {
                                    var src = (int*)value.storage.data_ptr;
                                    var dst = (int*)this.storage.data_ptr;
                                    for(int i = 0; i < y_numel; ++i)
                                    {
                                        var y_loc = i;
                                        var x_index = 0;
                                        var v_index = 0;
                                        for(int j = 0; j < ndim; ++j)
                                        {
                                            x_index += (y_loc / y_strides[j] + begin[j]) * x_strides[j];
                                            v_index += (Math.Min(y_loc / y_strides[j] + 1, v_shape[j]) - 1) * v_strides[j];
                                            y_loc %= y_strides[j];
                                        }
                                        dst[x_index] = src[v_index];
                                    }
                                    Marshal.FreeHGlobal((IntPtr)begin);
                                    Marshal.FreeHGlobal((IntPtr)x_strides);
                                    Marshal.FreeHGlobal((IntPtr)v_strides);
                                    Marshal.FreeHGlobal((IntPtr)y_strides);
                                    Marshal.FreeHGlobal((IntPtr)y_shape);
                                    Marshal.FreeHGlobal((IntPtr)v_shape);
                                    break;
                                }
                                case torchlite.@bool:
                                {
                                    var src = (byte*)value.storage.data_ptr;
                                    var dst = (int*)this.storage.data_ptr;
                                    for(int i = 0; i < y_numel; ++i)
                                    {
                                        var y_loc = i;
                                        var x_index = 0;
                                        var v_index = 0;
                                        for(int j = 0; j < ndim; ++j)
                                        {
                                            x_index += (y_loc / y_strides[j] + begin[j]) * x_strides[j];
                                            v_index += (Math.Min(y_loc / y_strides[j] + 1, v_shape[j]) - 1) * v_strides[j];
                                            y_loc %= y_strides[j];
                                        }
                                        dst[x_index] = src[v_index];
                                    }
                                    Marshal.FreeHGlobal((IntPtr)begin);
                                    Marshal.FreeHGlobal((IntPtr)x_strides);
                                    Marshal.FreeHGlobal((IntPtr)v_strides);
                                    Marshal.FreeHGlobal((IntPtr)y_strides);
                                    Marshal.FreeHGlobal((IntPtr)y_shape);
                                    Marshal.FreeHGlobal((IntPtr)v_shape);
                                    break;
                                }
                            }
                            break;
                        }
                        case torchlite.@bool:
                        {
                            switch(value.dtype)
                            {
                                case torchlite.float32:
                                {
                                    var src = (float*)value.storage.data_ptr;
                                    var dst = (bool*)this.storage.data_ptr;
                                    for(int i = 0; i < y_numel; ++i)
                                    {
                                        var y_loc = i;
                                        var x_index = 0;
                                        var v_index = 0;
                                        for(int j = 0; j < ndim; ++j)
                                        {
                                            x_index += (y_loc / y_strides[j] + begin[j]) * x_strides[j];
                                            v_index += (Math.Min(y_loc / y_strides[j] + 1, v_shape[j]) - 1) * v_strides[j];
                                            y_loc %= y_strides[j];
                                        }
                                        dst[x_index] = src[v_index] != 0;
                                    }
                                    Marshal.FreeHGlobal((IntPtr)begin);
                                    Marshal.FreeHGlobal((IntPtr)x_strides);
                                    Marshal.FreeHGlobal((IntPtr)v_strides);
                                    Marshal.FreeHGlobal((IntPtr)y_strides);
                                    Marshal.FreeHGlobal((IntPtr)y_shape);
                                    Marshal.FreeHGlobal((IntPtr)v_shape);
                                    break;
                                }
                                case torchlite.int32:
                                {
                                    var src = (int*)value.storage.data_ptr;
                                    var dst = (bool*)this.storage.data_ptr;
                                    for(int i = 0; i < y_numel; ++i)
                                    {
                                        var y_loc = i;
                                        var x_index = 0;
                                        var v_index = 0;
                                        for(int j = 0; j < ndim; ++j)
                                        {
                                            x_index += (y_loc / y_strides[j] + begin[j]) * x_strides[j];
                                            v_index += (Math.Min(y_loc / y_strides[j] + 1, v_shape[j]) - 1) * v_strides[j];
                                            y_loc %= y_strides[j];
                                        }
                                        dst[x_index] = src[v_index] != 0;
                                    }
                                    Marshal.FreeHGlobal((IntPtr)begin);
                                    Marshal.FreeHGlobal((IntPtr)x_strides);
                                    Marshal.FreeHGlobal((IntPtr)v_strides);
                                    Marshal.FreeHGlobal((IntPtr)y_strides);
                                    Marshal.FreeHGlobal((IntPtr)y_shape);
                                    Marshal.FreeHGlobal((IntPtr)v_shape);
                                    break;
                                }
                                case torchlite.@bool:
                                {
                                    var src = (bool*)value.storage.data_ptr;
                                    var dst = (bool*)this.storage.data_ptr;
                                    for(int i = 0; i < y_numel; ++i)
                                    {
                                        var y_loc = i;
                                        var x_index = 0;
                                        var v_index = 0;
                                        for(int j = 0; j < ndim; ++j)
                                        {
                                            x_index += (y_loc / y_strides[j] + begin[j]) * x_strides[j];
                                            v_index += (Math.Min(y_loc / y_strides[j] + 1, v_shape[j]) - 1) * v_strides[j];
                                            y_loc %= y_strides[j];
                                        }
                                        dst[x_index] = src[v_index];
                                    }
                                    Marshal.FreeHGlobal((IntPtr)begin);
                                    Marshal.FreeHGlobal((IntPtr)x_strides);
                                    Marshal.FreeHGlobal((IntPtr)v_strides);
                                    Marshal.FreeHGlobal((IntPtr)y_strides);
                                    Marshal.FreeHGlobal((IntPtr)y_shape);
                                    Marshal.FreeHGlobal((IntPtr)v_shape);
                                    break;
                                }
                            }
                            break;
                        }
                    }
                }

            }

            #endregion

        }

    }

}