//***************************************************************************************************
//* (C) ColorfulSoft corp., 2019-2023. All rights reserved.
//* The code is available under the Apache-2.0 license. Read the License for details.
//***************************************************************************************************

using System;
using System.Collections;
using System.Globalization;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;

namespace System.AI.Experimental
{

    public static partial class torchlite
    {

        public unsafe class Tensor
        {

            internal delegate void _backward_fn();

            #region fields

            private Tensor __grad;

            internal Tensor[] _parent;

            internal _backward_fn backward_fn;

            #endregion

            #region properties

            public Storage storage
            {

                get;

                private set;

            }

            public Size shape
            {

                get;

                private set;

            }

            public DType dtype
            {

                get
                {
                    return this.storage.dtype;
                }

            }

            public bool requires_grad
            {

                get
                {
                    return this.__grad != null;
                }

            }

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

            #endregion

            #region constructors

            public Tensor(IList<int> shape,
                          DType? dtype = null,
                          bool requires_grad = false)
            {
                dtype = dtype ?? torchlite.default_dtype;
                if(requires_grad && !dtype.Value.is_floating_point())
                {
                    throw new ArgumentException("Only floating point tensors may requires_grad.");
                }
                this.shape = new Size(shape);
                this.storage = new Storage(this.shape.numel(), dtype);
                if(requires_grad)
                {
                    this.grad = torchlite.zeros(shape, dtype);
                }
            }

            /// <summary>
            /// Initializes a 0-dimensional tensor of a dtype type.
            /// </summary>
            /// <param name="dtype">The tensor data type.</param>
            /// <param name="requires_grad">Specifies whether to create a gradient for the tensor.</param>
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public Tensor(DType? dtype = null,
                          bool requires_grad = false) : this(null,
                                                             dtype,
                                                             requires_grad)
            {
            }

            /// <summary>
            /// Initializes a 1-dimensional tensor of a dtype type.
            /// </summary>
            /// <param name="dim1">First dimension.</param>
            /// <param name="dtype">The tensor data type.</param>
            /// <param name="requires_grad">Specifies whether to create a gradient for the tensor.</param>
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public Tensor(int dim1,
                          DType? dtype = null,
                          bool requires_grad = false) : this(new []{dim1},
                                                             dtype,
                                                             requires_grad)
            {
            }

            /// <summary>
            /// Initializes a 2-dimensional tensor of a dtype type.
            /// </summary>
            /// <param name="dim1">First dimension.</param>
            /// <param name="dim2">Second dimension.</param>
            /// <param name="dtype">The tensor data type.</param>
            /// <param name="requires_grad">Specifies whether to create a gradient for the tensor.</param>
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public Tensor(int dim1,
                          int dim2,
                          DType? dtype = null,
                          bool requires_grad = false) : this(new []{dim1,
                                                                    dim2},
                                                             dtype,
                                                             requires_grad)
            {
            }

            /// <summary>
            /// Initializes a 3-dimensional tensor of a dtype type.
            /// </summary>
            /// <param name="dim1">First dimension.</param>
            /// <param name="dim2">Second dimension.</param>
            /// <param name="dim3">Third dimension.</param>
            /// <param name="dtype">The tensor data type.</param>
            /// <param name="requires_grad">Specifies whether to create a gradient for the tensor.</param>
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public Tensor(int dim1,
                          int dim2,
                          int dim3,
                          DType? dtype = null,
                          bool requires_grad = false) : this(new []{dim1,
                                                                    dim2,
                                                                    dim3},
                                                             dtype,
                                                             requires_grad)
            {
            }

            /// <summary>
            /// Initializes a 4-dimensional tensor of a dtype type.
            /// </summary>
            /// <param name="dim1">First dimension.</param>
            /// <param name="dim2">Second dimension.</param>
            /// <param name="dim3">Third dimension.</param>
            /// <param name="dim4">Fourth dimension.</param>
            /// <param name="dtype">The tensor data type.</param>
            /// <param name="requires_grad">Specifies whether to create a gradient for the tensor.</param>
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public Tensor(int dim1,
                          int dim2,
                          int dim3,
                          int dim4,
                          DType? dtype = null,
                          bool requires_grad = false) : this(new []{dim1,
                                                                    dim2,
                                                                    dim3,
                                                                    dim4},
                                                             dtype,
                                                             requires_grad)
            {
            }

            /// <summary>
            /// Initializes a 5-dimensional tensor of a dtype type.
            /// </summary>
            /// <param name="dim1">First dimension.</param>
            /// <param name="dim2">Second dimension.</param>
            /// <param name="dim3">Third dimension.</param>
            /// <param name="dim4">Fourth dimension.</param>
            /// <param name="dim5">Fifth dimension.</param>
            /// <param name="dtype">The tensor data type.</param>
            /// <param name="requires_grad">Specifies whether to create a gradient for the tensor.</param>
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public Tensor(int dim1,
                          int dim2,
                          int dim3,
                          int dim4,
                          int dim5,
                          DType? dtype = null,
                          bool requires_grad = false) : this(new []{dim1,
                                                                    dim2,
                                                                    dim3,
                                                                    dim4,
                                                                    dim5},
                                                             dtype,
                                                             requires_grad)
            {
            }

            /// <summary>
            /// Initializes a 6-dimensional tensor of a dtype type.
            /// </summary>
            /// <param name="dim1">First dimension.</param>
            /// <param name="dim2">Second dimension.</param>
            /// <param name="dim3">Third dimension.</param>
            /// <param name="dim4">Fourth dimension.</param>
            /// <param name="dim5">Fifth dimension.</param>
            /// <param name="dim6">Sixth dimension.</param>
            /// <param name="dtype">The tensor data type.</param>
            /// <param name="requires_grad">Specifies whether to create a gradient for the tensor.</param>
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public Tensor(int dim1,
                          int dim2,
                          int dim3,
                          int dim4,
                          int dim5,
                          int dim6,
                          DType? dtype = null,
                          bool requires_grad = false) : this(new []{dim1,
                                                                    dim2,
                                                                    dim3,
                                                                    dim4,
                                                                    dim5,
                                                                    dim6},
                                                             dtype,
                                                             requires_grad)
            {
            }

            /// <summary>
            /// Initializes a 7-dimensional tensor of a dtype type.
            /// </summary>
            /// <param name="dim1">First dimension.</param>
            /// <param name="dim2">Second dimension.</param>
            /// <param name="dim3">Third dimension.</param>
            /// <param name="dim4">Fourth dimension.</param>
            /// <param name="dim5">Fifth dimension.</param>
            /// <param name="dim6">Sixth dimension.</param>
            /// <param name="dim7">Seventh dimension.</param>
            /// <param name="dtype">The tensor data type.</param>
            /// <param name="requires_grad">Specifies whether to create a gradient for the tensor.</param>
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public Tensor(int dim1,
                          int dim2,
                          int dim3,
                          int dim4,
                          int dim5,
                          int dim6,
                          int dim7,
                          DType? dtype = null,
                          bool requires_grad = false) : this(new []{dim1,
                                                                    dim2,
                                                                    dim3,
                                                                    dim4,
                                                                    dim5,
                                                                    dim6,
                                                                    dim7},
                                                             dtype,
                                                             requires_grad)
            {
            }

            /// <summary>
            /// Initializes a 8-dimensional tensor of a dtype type.
            /// </summary>
            /// <param name="dim1">First dimension.</param>
            /// <param name="dim2">Second dimension.</param>
            /// <param name="dim3">Third dimension.</param>
            /// <param name="dim4">Fourth dimension.</param>
            /// <param name="dim5">Fifth dimension.</param>
            /// <param name="dim6">Sixth dimension.</param>
            /// <param name="dim7">Seventh dimension.</param>
            /// <param name="dim8">Eighth dimension.</param>
            /// <param name="dtype">The tensor data type.</param>
            /// <param name="requires_grad">Specifies whether to create a gradient for the tensor.</param>
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public Tensor(int dim1,
                          int dim2,
                          int dim3,
                          int dim4,
                          int dim5,
                          int dim6,
                          int dim7,
                          int dim8,
                          DType? dtype = null,
                          bool requires_grad = false) : this(new []{dim1,
                                                                    dim2,
                                                                    dim3,
                                                                    dim4,
                                                                    dim5,
                                                                    dim6,
                                                                    dim7,
                                                                    dim8},
                                                             dtype,
                                                             requires_grad)
            {
            }

            #endregion

            #region methods

            public void backward(Tensor grad = null)
            {
                if(!this.requires_grad)
                {
                    throw new Exception("");
                }
                if(this.shape.ndim != 0)
                {
                    if(grad == null)
                    {
                        throw new Exception("");
                    }
                    if(grad.shape != this.shape)
                    {
                        throw new Exception("");
                    }
                }
                // topological order all of the children in the graph
                var topo = new List<Tensor>();
                var visited = new HashSet<Tensor>();
                Action<Tensor> build_topo = null;
                build_topo = (Tensor v) =>
                {
                    if(!visited.Contains(v))
                    {
                        visited.Add(v);
                        if(v._parent != null)
                        {
                            foreach(var child in v._parent)
                            {
                                build_topo(child);
                            }
                        }
                        topo.Add(v);
                    }
                };
                build_topo(this);
                // go one variable at a time and apply the chain rule to get its gradient
                this.grad = grad ?? 1f;
                topo.Reverse();
                foreach(var v in topo)
                {
                    if(v.backward_fn != null)
                    {
                        v.backward_fn();
                    }
                }
            }

            #region _copy(source, destination, count)

            private static void _copy(float* source, float* destination, int count)
            {
                for(int i = 0; i < count; ++i)
                {
                    destination[i] = source[i];
                }
            }

            private static void _copy(int* source, float* destination, int count)
            {
                for(int i = 0; i < count; ++i)
                {
                    destination[i] = source[i];
                }
            }

            private static void _copy(bool* source, float* destination, int count)
            {
                for(int i = 0; i < count; ++i)
                {
                    destination[i] = source[i] ? 1 : 0;
                }
            }

            private static void _copy(float* source, int* destination, int count)
            {
                for(int i = 0; i < count; ++i)
                {
                    destination[i] = (int)source[i];
                }
            }

            private static void _copy(int* source, int* destination, int count)
            {
                for(int i = 0; i < count; ++i)
                {
                    destination[i] = source[i];
                }
            }

            private static void _copy(bool* source, int* destination, int count)
            {
                for(int i = 0; i < count; ++i)
                {
                    destination[i] = source[i] ? 1 : 0;
                }
            }

            private static void _copy(float* source, bool* destination, int count)
            {
                for(int i = 0; i < count; ++i)
                {
                    destination[i] = source[i] != 0;
                }
            }

            private static void _copy(int* source, bool* destination, int count)
            {
                for(int i = 0; i < count; ++i)
                {
                    destination[i] = source[i] != 0;
                }
            }

            private static void _copy(bool* source, bool* destination, int count)
            {
                for(int i = 0; i < count; ++i)
                {
                    destination[i] = source[i];
                }
            }

            #endregion

            /// <summary>
            /// 
            /// </summary>
            /// <param name="array"></param>
            /// <param name="dtype"></param>
            /// <returns></returns>
            public static Tensor from_dotnet(Array array, DType? dtype = null, bool requires_grad = false)
            {
                var handle = GCHandle.Alloc(array, GCHandleType.Pinned);
                if(array.GetType().GetElementType() == typeof(float))
                {
                    dtype = dtype ?? torchlite.float32;
                    var shape = new int[array.Rank];
                    for(int i = 0; i < shape.Length; ++i)
                    {
                        shape[i] = array.GetLength(i);
                    }
                    var t = new Tensor(shape, dtype, requires_grad);
                    var source = (float*)handle.AddrOfPinnedObject();
                    switch(dtype)
                    {
                        case torchlite.float32:
                        {
                            _copy(source, (float*)t.storage.data_ptr, t.shape.numel());
                            break;
                        }
                        case torchlite.int32:
                        {
                            _copy(source, (int*)t.storage.data_ptr, t.shape.numel());
                            break;
                        }
                        case torchlite.@bool:
                        {
                            _copy(source, (bool*)t.storage.data_ptr, t.shape.numel());
                            break;
                        }
                    }
                    handle.Free();
                    return t;
                }
                if(array.GetType().GetElementType() == typeof(int))
                {
                    dtype = dtype ?? torchlite.int32;
                    var shape = new int[array.Rank];
                    for(int i = 0; i < shape.Length; ++i)
                    {
                        shape[i] = array.GetLength(i);
                    }
                    var t = new Tensor(shape, dtype, requires_grad);
                    var source = (int*)handle.AddrOfPinnedObject();
                    switch(dtype)
                    {
                        case torchlite.float32:
                        {
                            _copy(source, (float*)t.storage.data_ptr, t.shape.numel());
                            break;
                        }
                        case torchlite.int32:
                        {
                            _copy(source, (int*)t.storage.data_ptr, t.shape.numel());
                            break;
                        }
                        case torchlite.@bool:
                        {
                            _copy(source, (bool*)t.storage.data_ptr, t.shape.numel());
                            break;
                        }
                    }
                    handle.Free();
                    return t;
                }
                if(array.GetType().GetElementType() == typeof(bool))
                {
                    dtype = dtype ?? torchlite.@bool;
                    var shape = new int[array.Rank];
                    for(int i = 0; i < shape.Length; ++i)
                    {
                        shape[i] = array.GetLength(i);
                    }
                    var t = new Tensor(shape, dtype, requires_grad);
                    var source = (bool*)handle.AddrOfPinnedObject();
                    switch(dtype)
                    {
                        case torchlite.float32:
                        {
                            _copy(source, (float*)t.storage.data_ptr, t.shape.numel());
                            break;
                        }
                        case torchlite.int32:
                        {
                            _copy(source, (int*)t.storage.data_ptr, t.shape.numel());
                            break;
                        }
                        case torchlite.@bool:
                        {
                            _copy(source, (bool*)t.storage.data_ptr, t.shape.numel());
                            break;
                        }
                    }
                    handle.Free();
                    return t;
                }
                handle.Free();
                throw new ArgumentException("Only float, int and bool arrays are supported.");
            }

            private void _print(ref string str, int* loc, int dim, string format)
            {
                if((this.shape.ndim - dim) != 0)
                {
                    if(dim > 0)
                    {
                        if(loc[dim - 1] > 0)
                        {
                            for(int i = 0; i < (7 + dim); ++i)
                            {
                                str += ' ';
                            }
                        }
                    }
                    str += '[';
                    var size = this.shape[dim];
                    for(int i = 0; i < size; ++i)
                    {
                        _print(ref str, loc, dim + 1, format);
                        ++loc[dim];
                    }
                    loc[dim] = 0;
                    str += ']';
                    if(dim > 0)
                    {
                        if((loc[dim - 1] + 1) != this.shape[dim - 1])
                        {
                            str += ',';
                            for(int j = 0; j < (this.shape.ndim - dim); ++j)
                            {
                                str += '\n';
                            }
                        }
                    }
                }
                else
                {
                    var n = this.shape.ndim;
                    var shape = this.shape.data_ptr;
                    var index = 0;
                    for(int i = 0; i < n; ++i)
                    {
                        index *= shape[i];
                        index += loc[i];
                    }
                    switch(this.dtype)
                    {
                        case torchlite.float32:
                        {
                            str += string.Format(new CultureInfo("en-US"), format, ((float*)this.storage.data_ptr)[index]);
                            break;
                        }
                        case torchlite.int32:
                        {
                            str += string.Format(new CultureInfo("en-US"), format, ((int*)this.storage.data_ptr)[index]);
                            break;
                        }
                        case torchlite.@bool:
                        {
                            str += string.Format(new CultureInfo("en-US"), format, ((bool*)this.storage.data_ptr)[index]);
                            break;
                        }
                    }
                    if((dim - 1) >= 0)
                    {
                        if((loc[dim - 1] + 1) != this.shape.data_ptr[dim - 1])
                        {
                            str += ", ";
                        }
                    }
                }
            }

            private string get_format()
            {
                var elements = this.storage.size;
                switch(this.dtype)
                {
                    case torchlite.float32:
                    {
                        var max_power = float.MinValue;
                        var min_power = float.MaxValue;
                        var ptr = (float*)this.storage.data_ptr;
                        var sign = false;
                        var frac = false;
                        for(int i = 0; i < elements; ++i)
                        {
                            var v = ptr[i];
                            var p = ((v != 0) && !float.IsInfinity(v) && !float.IsNaN(v)) ? (int)Math.Floor(Math.Log10(Math.Abs(v))) : 0;
                            if(p > max_power)
                            {
                                max_power = p;
                            }
                            if(p < min_power)
                            {
                                min_power = p;
                            }
                        }
                        for(int i = 0; i < elements; ++i)
                        {
                            var v = ptr[i];
                            if(v < 0)
                            {
                                sign = true;
                                break;
                            }
                        }
                        for(int i = 0; i < elements; ++i)
                        {
                            var v = ptr[i];
                            if((v - Math.Floor(v)) != 0)
                            {
                                frac = true;
                                break;
                            }
                        }
                        var format = "{0,";
                        if((max_power < 4) && (min_power > -4))
                        {
                            format += ((frac ? 6 : 1) + (sign ? 1 : 0) + Math.Max(max_power, 0)).ToString() + (frac ? ":0.####}" : ":0}");
                        }
                        else
                        {
                            format += "11:0.0000e+00}";
                        }
                        return format;
                    }
                    case torchlite.int32:
                    {
                        var max = int.MinValue;
                        var min = int.MaxValue;
                        var ptr = (int*)this.storage.data_ptr;
                        for(int i = 0; i < elements; ++i)
                        {
                            var v = ptr[i];
                            if(v > max)
                            {
                                max = v;
                            }
                            if(v < min)
                            {
                                min = v;
                            }
                        }
                        var format = "{0,";
                        format += Math.Max(max.ToString().Length, min.ToString().Length).ToString() + '}';
                        return format;
                    }
                    case torchlite.@bool:
                    {
                        return "{0,5}";
                    }
                    default:
                    {
                        throw new NotImplementedException();
                    }
                }
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public override string ToString()
            {
                var str = "tensor(";
                var loc = (int*)Marshal.AllocHGlobal(this.shape.ndim * sizeof(int)).ToPointer();
                for(int i = 0; i < this.shape.ndim; ++i)
                {
                    loc[i] = 0;
                }
                var format = this.get_format();
                _print(ref str, loc, 0, format);
                str += string.Format(", dtype = torchlite.{0}, requires_grad = {1}", this.dtype.ToString(), this.requires_grad);
                return str + ")";
            }

            #endregion

            #region operators

            public static implicit operator Tensor(float value)
            {
                var t = new Tensor();
                _copy(&value, (float*)t.storage.data_ptr, 1);
                return t;
            }

            public static implicit operator Tensor(int value)
            {
                var t = new Tensor(torchlite.@int);
                _copy(&value, (int*)t.storage.data_ptr, 1);
                return t;
            }

            public static implicit operator Tensor(bool value)
            {
                var t = new Tensor(torchlite.@bool);
                _copy(&value, (bool*)t.storage.data_ptr, 1);
                return t;
            }

            public static implicit operator Tensor(Array value)
            {
                return from_dotnet(value);
            }

            #endregion

        }

    }

}