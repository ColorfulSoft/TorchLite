//***************************************************************************************************
//* (C) ColorfulSoft corp., 2019-2023. All rights reserved.
//* The code is available under the Apache-2.0 license. Read the License for details.
//***************************************************************************************************

using System;
using System.Reflection;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace System.AI.Experimental
{

    /// <summary>
    /// The torchlite package contains data structures for multi-dimensional tensors and defines
    /// mathematical operations over these tensors. Additionally, it provides many utilities for
    /// efficient serializing of Tensors, and other useful utilities.
    /// </summary>
    public unsafe static partial class torchlite
    {

        #region constants

        /// <summary>
        /// 32-bit floating point. Alias for torchlite.float.
        /// </summary>
        public const DType float32 = DType.float32;

        /// <summary>
        /// 32-bit floating point. Alias for torchlite.float32.
        /// </summary>
        public const DType @float = DType.float32;

        /// <summary>
        /// 32-bit signed integer. Alias for torchlite.int.
        /// </summary>
        public const DType int32 = DType.int32;

        /// <summary>
        /// 32-bit signed integer. Alias for torchlite.int32.
        /// </summary>
        public const DType @int = DType.int32;

        /// <summary>
        /// Boolean.
        /// </summary>
        public const DType @bool = DType.@bool;

        #endregion

        #region fields

        /// <summary>
        /// Random number generator.
        /// </summary>
        [ThreadStatic]
        private static Random __rand;

        /// <summary>
        /// The default data type for tensors.
        /// </summary>
        [ThreadStatic]
        private static DType __default_dtype;

        /// <summary>
        /// Stores information about the existence of a no_grad manager in the current context.
        /// </summary>
        [ThreadStatic]
        private static bool __no_grad;

        #endregion

        #region properties

        /// <summary>
        /// Current version of torchlite.
        /// </summary>
        public static Version version
        {

            get;

            private set;

        }

        /// <summary>
        /// The default data type for tensors.
        /// </summary>
        public static DType default_dtype
        {

            get
            {
                return torchlite.__default_dtype;
            }

            set
            {
                torchlite.__default_dtype = value;
            }

        }

        #endregion

        #region constructors

        /// <summary>
        /// Initializes torchlite.
        /// </summary>
        static torchlite()
        {
            // fields
            torchlite.__rand = new Random();
            torchlite.__default_dtype = torchlite.float32;
            torchlite.__no_grad = false;
            // properties
            torchlite.version = Assembly.GetExecutingAssembly().GetName().Version;
        }

        #endregion

        #region methods

        #region reproducibility

        /// <summary>
        /// Sets the seed for generating random numbers to a non-deterministic random number.
        /// Returns a 32 bit number used to seed the RNG.
        /// </summary>
        /// <returns>New seed.</returns>
        public static int seed()
        {
            int seed = torchlite.__rand.Next();
            torchlite.__rand = new Random(seed);
            return seed;
        }

        /// <summary>
        /// Sets the seed for generating random numbers. Returns a System.Random object.
        /// </summary>
        /// <param name="seed">The desired seed.</param>
        /// <returns>System.Random object.</returns>
        public static Random manual_seed(int seed)
        {
            torchlite.__rand = new Random(seed);
            return torchlite.__rand;
        }

        #endregion

        #region empty(*shape, dtype, requires_grad)

        /// <summary>
        /// Returns an empty tensor of the specified size.
        /// </summary>
        /// <param name="shape">Tensor's dimensions.</param>
        /// <param name="dtype">The tensor data type.</param>
        /// <param name="requires_grad">Specifies whether to create a gradient for the tensor.</param>
        /// <returns>Tensor.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Tensor empty(IList<int> shape,
                                   DType? dtype = null,
                                   bool requires_grad = false)
        {
            return new Tensor(shape, dtype, requires_grad);
        }

        /// <summary>
        /// Initializes a 0-dimensional empty tensor.
        /// </summary>
        /// <param name="dtype">The tensor data type.</param>
        /// <param name="requires_grad">Specifies whether to create a gradient for the tensor.</param>
        /// <returns>Tensor.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Tensor empty(DType? dtype = null,
                                   bool requires_grad = false)
        {
            return new Tensor(dtype, requires_grad);
        }

        /// <summary>
        /// Initializes a 1-dimensional empty tensor.
        /// </summary>
        /// <param name="dim1">First dimension.</param>
        /// <param name="dtype">The tensor data type.</param>
        /// <param name="requires_grad">Specifies whether to create a gradient for the tensor.</param>
        /// <returns>Tensor.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Tensor empty(int dim1,
                                   DType? dtype = null,
                                   bool requires_grad = false)
        {
            return new Tensor(dim1, dtype, requires_grad);
        }

        /// <summary>
        /// Initializes a 2-dimensional empty tensor.
        /// </summary>
        /// <param name="dim1">First dimension.</param>
        /// <param name="dim2">Second dimension.</param>
        /// <param name="dtype">The tensor data type.</param>
        /// <param name="requires_grad">Specifies whether to create a gradient for the tensor.</param>
        /// <returns>Tensor.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Tensor empty(int dim1,
                                   int dim2,
                                   DType? dtype = null,
                                   bool requires_grad = false)
        {
            return new Tensor(dim1, dim2, dtype, requires_grad);
        }

        /// <summary>
        /// Initializes a 3-dimensional empty tensor.
        /// </summary>
        /// <param name="dim1">First dimension.</param>
        /// <param name="dim2">Second dimension.</param>
        /// <param name="dim3">Third dimension.</param>
        /// <param name="dtype">The tensor data type.</param>
        /// <param name="requires_grad">Specifies whether to create a gradient for the tensor.</param>
        /// <returns>Tensor.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Tensor empty(int dim1,
                                   int dim2,
                                   int dim3,
                                   DType? dtype = null,
                                   bool requires_grad = false)
        {
            return new Tensor(dim1, dim2, dim3, dtype, requires_grad);
        }

        /// <summary>
        /// Initializes a 4-dimensional empty tensor.
        /// </summary>
        /// <param name="dim1">First dimension.</param>
        /// <param name="dim2">Second dimension.</param>
        /// <param name="dim3">Third dimension.</param>
        /// <param name="dim4">Fourth dimension.</param>
        /// <param name="dtype">The tensor data type.</param>
        /// <param name="requires_grad">Specifies whether to create a gradient for the tensor.</param>
        /// <returns>Tensor.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Tensor empty(int dim1,
                                   int dim2,
                                   int dim3,
                                   int dim4,
                                   DType? dtype = null,
                                   bool requires_grad = false)
        {
            return new Tensor(dim1, dim2, dim3, dim4, dtype, requires_grad);
        }

        /// <summary>
        /// Initializes a 5-dimensional empty tensor.
        /// </summary>
        /// <param name="dim1">First dimension.</param>
        /// <param name="dim2">Second dimension.</param>
        /// <param name="dim3">Third dimension.</param>
        /// <param name="dim4">Fourth dimension.</param>
        /// <param name="dim5">Fifth dimension.</param>
        /// <param name="dtype">The tensor data type.</param>
        /// <param name="requires_grad">Specifies whether to create a gradient for the tensor.</param>
        /// <returns>Tensor.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Tensor empty(int dim1,
                                   int dim2,
                                   int dim3,
                                   int dim4,
                                   int dim5,
                                   DType? dtype = null,
                                   bool requires_grad = false)
        {
            return new Tensor(dim1, dim2, dim3, dim4, dim5, dtype, requires_grad);
        }

        /// <summary>
        /// Initializes a 6-dimensional empty tensor.
        /// </summary>
        /// <param name="dim1">First dimension.</param>
        /// <param name="dim2">Second dimension.</param>
        /// <param name="dim3">Third dimension.</param>
        /// <param name="dim4">Fourth dimension.</param>
        /// <param name="dim5">Fifth dimension.</param>
        /// <param name="dim6">Sixth dimension.</param>
        /// <param name="dtype">The tensor data type.</param>
        /// <param name="requires_grad">Specifies whether to create a gradient for the tensor.</param>
        /// <returns>Tensor.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Tensor empty(int dim1,
                                   int dim2,
                                   int dim3,
                                   int dim4,
                                   int dim5,
                                   int dim6,
                                   DType? dtype = null,
                                   bool requires_grad = false)
        {
            return new Tensor(dim1, dim2, dim3, dim4, dim5, dim6, dtype, requires_grad);
        }

        /// <summary>
        /// Initializes a 7-dimensional empty tensor.
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
        /// <returns>Tensor.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Tensor empty(int dim1,
                                   int dim2,
                                   int dim3,
                                   int dim4,
                                   int dim5,
                                   int dim6,
                                   int dim7,
                                   DType? dtype = null,
                                   bool requires_grad = false)
        {
            return new Tensor(dim1, dim2, dim3, dim4, dim5, dim6, dim7, dtype, requires_grad);
        }

        /// <summary>
        /// Initializes a 8-dimensional empty tensor.
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
        /// <returns>Tensor.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Tensor empty(int dim1,
                                   int dim2,
                                   int dim3,
                                   int dim4,
                                   int dim5,
                                   int dim6,
                                   int dim7,
                                   int dim8,
                                   DType? dtype = null,
                                   bool requires_grad = false)
        {
            return new Tensor(dim1, dim2, dim3, dim4, dim5, dim6, dim7, dim8, dtype, requires_grad);
        }

        #endregion

        #region empty_like(value, dtype, requires_grad)

        /// <summary>
        /// Returns a new empty tensor of same shape as value.
        /// </summary>
        /// <param name="value">Source tensor.</param>
        /// <param name="dtype">Data type of the new tensor.</param>
        /// <param name="requires_grad">Specifies whether to create a gradient for the tensor.</param>
        /// <returns>Tensor.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Tensor empty_like(Tensor value,
                                        DType? dtype = null,
                                        bool requires_grad = false)
        {
            return new Tensor(value.shape, dtype ?? value.dtype, requires_grad);
        }

        #endregion

        #region full(*shape, fill_value, dtype, requires_grad)

        /// <summary>
        /// Returns a tensor of the specified size filled with fill_value.
        /// </summary>
        /// <param name="shape">Tensor's dimensions.</param>
        /// <param name="fill_value">The value to fill the output tensor with.</param>
        /// <param name="dtype">The tensor data type.</param>
        /// <param name="requires_grad">Specifies whether to create a gradient for the tensor.</param>
        /// <returns>Tensor.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Tensor full(IList<int> shape,
                                  object fill_value,
                                  DType? dtype = null,
                                  bool requires_grad = false)
        {
            if(dtype == null)
            {
                if((fill_value is float) || (fill_value is double))
                {
                    dtype = torchlite.float32;
                }
                if((fill_value is byte) || (fill_value is sbyte) ||
                   (fill_value is ushort) || (fill_value is short) ||
                   (fill_value is uint) || (fill_value is int) ||
                   (fill_value is ulong) || (fill_value is long))
                {
                    dtype = torchlite.int32;
                }
                if(fill_value is bool)
                {
                    dtype = torchlite.@bool;
                }
            }
            var t = new Tensor(shape, dtype, requires_grad);
            switch(t.dtype)
            {
                case torchlite.float32:
                {
                    var value = Convert.ToSingle(fill_value);
                    var ptr = (float*)t.storage.data_ptr;
                    var elements = t.storage.size;
                    for(int i = 0; i < elements; ++i)
                    {
                        ptr[i] = value;
                    }
                    break;
                }
                case torchlite.int32:
                {
                    var value = Convert.ToInt32(fill_value);
                    var ptr = (int*)t.storage.data_ptr;
                    var elements = t.storage.size;
                    for(int i = 0; i < elements; ++i)
                    {
                        ptr[i] = value;
                    }
                    break;
                }
                case torchlite.@bool:
                {
                    var value = Convert.ToBoolean(fill_value);
                    var ptr = (bool*)t.storage.data_ptr;
                    var elements = t.storage.size;
                    for(int i = 0; i < elements; ++i)
                    {
                        ptr[i] = value;
                    }
                    break;
                }
            }
            return t;
        }

        /// <summary>
        /// Initializes a 0-dimensional tensor filled with fill_value.
        /// </summary>
        /// <param name="fill_value">The value to fill the output tensor with.</param>
        /// <param name="dtype">The tensor data type.</param>
        /// <param name="requires_grad">Specifies whether to create a gradient for the tensor.</param>
        /// <returns>Tensor.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Tensor full(object fill_value,
                                  DType? dtype = null,
                                  bool requires_grad = false)
        {
            return torchlite.full(new int[0], fill_value, dtype, requires_grad);
        }

        /// <summary>
        /// Initializes a 1-dimensional tensor filled with fill_value.
        /// </summary>
        /// <param name="dim1">First dimension.</param>
        /// <param name="fill_value">The value to fill the output tensor with.</param>
        /// <param name="dtype">The tensor data type.</param>
        /// <param name="requires_grad">Specifies whether to create a gradient for the tensor.</param>
        /// <returns>Tensor.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Tensor full(int dim1,
                                  object fill_value,
                                  DType? dtype = null,
                                  bool requires_grad = false)
        {
            return torchlite.full(new []{dim1}, fill_value, dtype, requires_grad);
        }

        /// <summary>
        /// Initializes a 2-dimensional tensor filled with fill_value.
        /// </summary>
        /// <param name="dim1">First dimension.</param>
        /// <param name="dim2">Second dimension.</param>
        /// <param name="fill_value">The value to fill the output tensor with.</param>
        /// <param name="dtype">The tensor data type.</param>
        /// <param name="requires_grad">Specifies whether to create a gradient for the tensor.</param>
        /// <returns>Tensor.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Tensor full(int dim1,
                                  int dim2,
                                  object fill_value,
                                  DType? dtype = null,
                                  bool requires_grad = false)
        {
            return torchlite.full(new []{dim1,
                                         dim2}, fill_value, dtype, requires_grad);
        }

        /// <summary>
        /// Initializes a 3-dimensional tensor filled with fill_value.
        /// </summary>
        /// <param name="dim1">First dimension.</param>
        /// <param name="dim2">Second dimension.</param>
        /// <param name="dim3">Third dimension.</param>
        /// <param name="fill_value">The value to fill the output tensor with.</param>
        /// <param name="dtype">The tensor data type.</param>
        /// <param name="requires_grad">Specifies whether to create a gradient for the tensor.</param>
        /// <returns>Tensor.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Tensor full(int dim1,
                                  int dim2,
                                  int dim3,
                                  object fill_value,
                                  DType? dtype = null,
                                  bool requires_grad = false)
        {
            return torchlite.full(new []{dim1,
                                         dim2,
                                         dim3}, fill_value, dtype, requires_grad);
        }

        /// <summary>
        /// Initializes a 4-dimensional tensor filled with fill_value.
        /// </summary>
        /// <param name="dim1">First dimension.</param>
        /// <param name="dim2">Second dimension.</param>
        /// <param name="dim3">Third dimension.</param>
        /// <param name="dim4">Fourth dimension.</param>
        /// <param name="fill_value">The value to fill the output tensor with.</param>
        /// <param name="dtype">The tensor data type.</param>
        /// <param name="requires_grad">Specifies whether to create a gradient for the tensor.</param>
        /// <returns>Tensor.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Tensor full(int dim1,
                                  int dim2,
                                  int dim3,
                                  int dim4,
                                  object fill_value,
                                  DType? dtype = null,
                                  bool requires_grad = false)
        {
            return torchlite.full(new []{dim1,
                                         dim2,
                                         dim3,
                                         dim4}, fill_value, dtype, requires_grad);
        }

        /// <summary>
        /// Initializes a 5-dimensional tensor filled with fill_value.
        /// </summary>
        /// <param name="dim1">First dimension.</param>
        /// <param name="dim2">Second dimension.</param>
        /// <param name="dim3">Third dimension.</param>
        /// <param name="dim4">Fourth dimension.</param>
        /// <param name="dim5">Fifth dimension.</param>
        /// <param name="fill_value">The value to fill the output tensor with.</param>
        /// <param name="dtype">The tensor data type.</param>
        /// <param name="requires_grad">Specifies whether to create a gradient for the tensor.</param>
        /// <returns>Tensor.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Tensor full(int dim1,
                                  int dim2,
                                  int dim3,
                                  int dim4,
                                  int dim5,
                                  object fill_value,
                                  DType? dtype = null,
                                  bool requires_grad = false)
        {
            return torchlite.full(new []{dim1,
                                         dim2,
                                         dim3,
                                         dim4,
                                         dim5}, fill_value, dtype, requires_grad);
        }

        /// <summary>
        /// Initializes a 6-dimensional tensor filled with fill_value.
        /// </summary>
        /// <param name="dim1">First dimension.</param>
        /// <param name="dim2">Second dimension.</param>
        /// <param name="dim3">Third dimension.</param>
        /// <param name="dim4">Fourth dimension.</param>
        /// <param name="dim5">Fifth dimension.</param>
        /// <param name="dim6">Sixth dimension.</param>
        /// <param name="fill_value">The value to fill the output tensor with.</param>
        /// <param name="dtype">The tensor data type.</param>
        /// <param name="requires_grad">Specifies whether to create a gradient for the tensor.</param>
        /// <returns>Tensor.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Tensor full(int dim1,
                                  int dim2,
                                  int dim3,
                                  int dim4,
                                  int dim5,
                                  int dim6,
                                  object fill_value,
                                  DType? dtype = null,
                                  bool requires_grad = false)
        {
            return torchlite.full(new []{dim1,
                                         dim2,
                                         dim3,
                                         dim4,
                                         dim5,
                                         dim6}, fill_value, dtype, requires_grad);
        }

        /// <summary>
        /// Initializes a 7-dimensional tensor filled with fill_value.
        /// </summary>
        /// <param name="dim1">First dimension.</param>
        /// <param name="dim2">Second dimension.</param>
        /// <param name="dim3">Third dimension.</param>
        /// <param name="dim4">Fourth dimension.</param>
        /// <param name="dim5">Fifth dimension.</param>
        /// <param name="dim6">Sixth dimension.</param>
        /// <param name="dim7">Seventh dimension.</param>
        /// <param name="fill_value">The value to fill the output tensor with.</param>
        /// <param name="dtype">The tensor data type.</param>
        /// <param name="requires_grad">Specifies whether to create a gradient for the tensor.</param>
        /// <returns>Tensor.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Tensor full(int dim1,
                                  int dim2,
                                  int dim3,
                                  int dim4,
                                  int dim5,
                                  int dim6,
                                  int dim7,
                                  object fill_value,
                                  DType? dtype = null,
                                  bool requires_grad = false)
        {
            return torchlite.full(new []{dim1,
                                         dim2,
                                         dim3,
                                         dim4,
                                         dim5,
                                         dim6,
                                         dim7}, fill_value, dtype, requires_grad);
        }

        /// <summary>
        /// Initializes a 8-dimensional tensor filled with fill_value.
        /// </summary>
        /// <param name="dim1">First dimension.</param>
        /// <param name="dim2">Second dimension.</param>
        /// <param name="dim3">Third dimension.</param>
        /// <param name="dim4">Fourth dimension.</param>
        /// <param name="dim5">Fifth dimension.</param>
        /// <param name="dim6">Sixth dimension.</param>
        /// <param name="dim7">Seventh dimension.</param>
        /// <param name="dim8">Eighth dimension.</param>
        /// <param name="fill_value">The value to fill the output tensor with.</param>
        /// <param name="dtype">The tensor data type.</param>
        /// <param name="requires_grad">Specifies whether to create a gradient for the tensor.</param>
        /// <returns>Tensor.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Tensor full(int dim1,
                                  int dim2,
                                  int dim3,
                                  int dim4,
                                  int dim5,
                                  int dim6,
                                  int dim7,
                                  int dim8,
                                  object fill_value,
                                  DType? dtype = null,
                                  bool requires_grad = false)
        {
            return torchlite.full(new []{dim1,
                                         dim2,
                                         dim3,
                                         dim4,
                                         dim5,
                                         dim6,
                                         dim7,
                                         dim8}, fill_value, dtype, requires_grad);
        }

        #endregion

        #region full_like(value, dtype, requires_grad)

        /// <summary>
        /// Returns a new tensor of same shape as value filled with fill_value.
        /// </summary>
        /// <param name="value">Source tensor.</param>
        /// <param name="fill_value">The value to fill the output tensor with.</param>
        /// <param name="dtype">Data type of the new tensor.</param>
        /// <param name="requires_grad">Specifies whether to create a gradient for the tensor.</param>
        /// <returns>Tensor.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Tensor full_like(Tensor value,
                                       object fill_value,
                                       DType? dtype = null,
                                       bool requires_grad = false)
        {
            return torchlite.full(value.shape, fill_value, dtype, requires_grad);
        }

        #endregion

        #region zeros(*shape, dtype, requires_grad)

        /// <summary>
        /// Returns a tensor of the specified size filled with zeros.
        /// </summary>
        /// <param name="shape">Tensor's dimensions.</param>
        /// <param name="dtype">The tensor data type.</param>
        /// <param name="requires_grad">Specifies whether to create a gradient for the tensor.</param>
        /// <returns>Tensor.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Tensor zeros(IList<int> shape,
                                   DType? dtype = null,
                                   bool requires_grad = false)
        {
            return torchlite.full(shape, 0, dtype ?? torchlite.default_dtype, requires_grad);
        }

        /// <summary>
        /// Initializes a 0-dimensional tensor with zeros.
        /// </summary>
        /// <param name="dtype">The tensor data type.</param>
        /// <param name="requires_grad">Specifies whether to create a gradient for the tensor.</param>
        /// <returns>Tensor.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Tensor zeros(DType? dtype = null,
                                   bool requires_grad = false)
        {
            return torchlite.full(new int[0], 0, dtype ?? torchlite.default_dtype, requires_grad);
        }

        /// <summary>
        /// Initializes a 1-dimensional tensor with zeros.
        /// </summary>
        /// <param name="dim1">First dimension.</param>
        /// <param name="dtype">The tensor data type.</param>
        /// <param name="requires_grad">Specifies whether to create a gradient for the tensor.</param>
        /// <returns>Tensor.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Tensor zeros(int dim1,
                                   DType? dtype = null,
                                   bool requires_grad = false)
        {
            return torchlite.full(new []{dim1}, 0, dtype ?? torchlite.default_dtype, requires_grad);
        }

        /// <summary>
        /// Initializes a 2-dimensional tensor with zeros.
        /// </summary>
        /// <param name="dim1">First dimension.</param>
        /// <param name="dim2">Second dimension.</param>
        /// <param name="dtype">The tensor data type.</param>
        /// <param name="requires_grad">Specifies whether to create a gradient for the tensor.</param>
        /// <returns>Tensor.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Tensor zeros(int dim1,
                                   int dim2,
                                   DType? dtype = null,
                                   bool requires_grad = false)
        {
            return torchlite.full(new []{dim1,
                                         dim2}, 0, dtype ?? torchlite.default_dtype, requires_grad);
        }

        /// <summary>
        /// Initializes a 3-dimensional tensor with zeros.
        /// </summary>
        /// <param name="dim1">First dimension.</param>
        /// <param name="dim2">Second dimension.</param>
        /// <param name="dim3">Third dimension.</param>
        /// <param name="dtype">The tensor data type.</param>
        /// <param name="requires_grad">Specifies whether to create a gradient for the tensor.</param>
        /// <returns>Tensor.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Tensor zeros(int dim1,
                                   int dim2,
                                   int dim3,
                                   DType? dtype = null,
                                   bool requires_grad = false)
        {
            return torchlite.full(new []{dim1,
                                         dim2,
                                         dim3}, 0, dtype ?? torchlite.default_dtype, requires_grad);
        }

        /// <summary>
        /// Initializes a 4-dimensional tensor with zeros.
        /// </summary>
        /// <param name="dim1">First dimension.</param>
        /// <param name="dim2">Second dimension.</param>
        /// <param name="dim3">Third dimension.</param>
        /// <param name="dim4">Fourth dimension.</param>
        /// <param name="dtype">The tensor data type.</param>
        /// <param name="requires_grad">Specifies whether to create a gradient for the tensor.</param>
        /// <returns>Tensor.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Tensor zeros(int dim1,
                                   int dim2,
                                   int dim3,
                                   int dim4,
                                   DType? dtype = null,
                                   bool requires_grad = false)
        {
            return torchlite.full(new []{dim1,
                                         dim2,
                                         dim3,
                                         dim4}, 0, dtype ?? torchlite.default_dtype, requires_grad);
        }

        /// <summary>
        /// Initializes a 5-dimensional tensor with zeros.
        /// </summary>
        /// <param name="dim1">First dimension.</param>
        /// <param name="dim2">Second dimension.</param>
        /// <param name="dim3">Third dimension.</param>
        /// <param name="dim4">Fourth dimension.</param>
        /// <param name="dim5">Fifth dimension.</param>
        /// <param name="dtype">The tensor data type.</param>
        /// <param name="requires_grad">Specifies whether to create a gradient for the tensor.</param>
        /// <returns>Tensor.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Tensor zeros(int dim1,
                                   int dim2,
                                   int dim3,
                                   int dim4,
                                   int dim5,
                                   DType? dtype = null,
                                   bool requires_grad = false)
        {
            return torchlite.full(new []{dim1,
                                         dim2,
                                         dim3,
                                         dim4,
                                         dim5}, 0, dtype ?? torchlite.default_dtype, requires_grad);
        }

        /// <summary>
        /// Initializes a 6-dimensional tensor with zeros.
        /// </summary>
        /// <param name="dim1">First dimension.</param>
        /// <param name="dim2">Second dimension.</param>
        /// <param name="dim3">Third dimension.</param>
        /// <param name="dim4">Fourth dimension.</param>
        /// <param name="dim5">Fifth dimension.</param>
        /// <param name="dim6">Sixth dimension.</param>
        /// <param name="dtype">The tensor data type.</param>
        /// <param name="requires_grad">Specifies whether to create a gradient for the tensor.</param>
        /// <returns>Tensor.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Tensor zeros(int dim1,
                                   int dim2,
                                   int dim3,
                                   int dim4,
                                   int dim5,
                                   int dim6,
                                   DType? dtype = null,
                                   bool requires_grad = false)
        {
            return torchlite.full(new []{dim1,
                                         dim2,
                                         dim3,
                                         dim4,
                                         dim5,
                                         dim6}, 0, dtype ?? torchlite.default_dtype, requires_grad);
        }

        /// <summary>
        /// Initializes a 7-dimensional tensor with zeros.
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
        /// <returns>Tensor.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Tensor zeros(int dim1,
                                   int dim2,
                                   int dim3,
                                   int dim4,
                                   int dim5,
                                   int dim6,
                                   int dim7,
                                   DType? dtype = null,
                                   bool requires_grad = false)
        {
            return torchlite.full(new []{dim1,
                                         dim2,
                                         dim3,
                                         dim4,
                                         dim5,
                                         dim6,
                                         dim7}, 0, dtype ?? torchlite.default_dtype, requires_grad);
        }

        /// <summary>
        /// Initializes a 8-dimensional tensor with zeros.
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
        /// <returns>Tensor.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Tensor zeros(int dim1,
                                   int dim2,
                                   int dim3,
                                   int dim4,
                                   int dim5,
                                   int dim6,
                                   int dim7,
                                   int dim8,
                                   DType? dtype = null,
                                   bool requires_grad = false)
        {
            return torchlite.full(new []{dim1,
                                         dim2,
                                         dim3,
                                         dim4,
                                         dim5,
                                         dim6,
                                         dim7,
                                         dim8}, 0, dtype ?? torchlite.default_dtype, requires_grad);
        }

        #endregion

        #region zeros_like(value, dtype, requires_grad)

        /// <summary>
        /// Returns a new tensor of same shape as value filled with zeros.
        /// </summary>
        /// <param name="value">Source tensor.</param>
        /// <param name="dtype">Data type of the new tensor.</param>
        /// <param name="requires_grad">Specifies whether to create a gradient for the tensor.</param>
        /// <returns>Tensor.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Tensor zeros_like(Tensor value,
                                        DType? dtype = null,
                                        bool requires_grad = false)
        {
            return torchlite.full(value.shape, 0, dtype ?? value.dtype, requires_grad);
        }

        #endregion

        #region ones(*shape, dtype, requires_grad)

        /// <summary>
        /// Returns a tensor of the specified size filled with ones.
        /// </summary>
        /// <param name="shape">Tensor's dimensions.</param>
        /// <param name="dtype">The tensor data type.</param>
        /// <param name="requires_grad">Specifies whether to create a gradient for the tensor.</param>
        /// <returns>Tensor.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Tensor ones(IList<int> shape,
                                  DType? dtype = null,
                                  bool requires_grad = false)
        {
            return torchlite.full(shape, 1, dtype ?? torchlite.default_dtype, requires_grad);
        }

        /// <summary>
        /// Initializes a 0-dimensional tensor with ones.
        /// </summary>
        /// <param name="dtype">The tensor data type.</param>
        /// <param name="requires_grad">Specifies whether to create a gradient for the tensor.</param>
        /// <returns>Tensor.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Tensor ones(DType? dtype = null,
                                  bool requires_grad = false)
        {
            return torchlite.full(new int[0], 1, dtype ?? torchlite.default_dtype, requires_grad);
        }

        /// <summary>
        /// Initializes a 1-dimensional tensor with ones.
        /// </summary>
        /// <param name="dim1">First dimension.</param>
        /// <param name="dtype">The tensor data type.</param>
        /// <param name="requires_grad">Specifies whether to create a gradient for the tensor.</param>
        /// <returns>Tensor.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Tensor ones(int dim1,
                                  DType? dtype = null,
                                  bool requires_grad = false)
        {
            return torchlite.full(new []{dim1}, 1, dtype ?? torchlite.default_dtype, requires_grad);
        }

        /// <summary>
        /// Initializes a 2-dimensional tensor with ones.
        /// </summary>
        /// <param name="dim1">First dimension.</param>
        /// <param name="dim2">Second dimension.</param>
        /// <param name="dtype">The tensor data type.</param>
        /// <param name="requires_grad">Specifies whether to create a gradient for the tensor.</param>
        /// <returns>Tensor.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Tensor ones(int dim1,
                                  int dim2,
                                  DType? dtype = null,
                                  bool requires_grad = false)
        {
            return torchlite.full(new []{dim1,
                                         dim2}, 1, dtype ?? torchlite.default_dtype, requires_grad);
        }

        /// <summary>
        /// Initializes a 3-dimensional tensor with ones.
        /// </summary>
        /// <param name="dim1">First dimension.</param>
        /// <param name="dim2">Second dimension.</param>
        /// <param name="dim3">Third dimension.</param>
        /// <param name="dtype">The tensor data type.</param>
        /// <param name="requires_grad">Specifies whether to create a gradient for the tensor.</param>
        /// <returns>Tensor.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Tensor ones(int dim1,
                                  int dim2,
                                  int dim3,
                                  DType? dtype = null,
                                  bool requires_grad = false)
        {
            return torchlite.full(new []{dim1,
                                         dim2,
                                         dim3}, 1, dtype ?? torchlite.default_dtype, requires_grad);
        }

        /// <summary>
        /// Initializes a 4-dimensional tensor with ones.
        /// </summary>
        /// <param name="dim1">First dimension.</param>
        /// <param name="dim2">Second dimension.</param>
        /// <param name="dim3">Third dimension.</param>
        /// <param name="dim4">Fourth dimension.</param>
        /// <param name="dtype">The tensor data type.</param>
        /// <param name="requires_grad">Specifies whether to create a gradient for the tensor.</param>
        /// <returns>Tensor.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Tensor ones(int dim1,
                                  int dim2,
                                  int dim3,
                                  int dim4,
                                  DType? dtype = null,
                                  bool requires_grad = false)
        {
            return torchlite.full(new []{dim1,
                                         dim2,
                                         dim3,
                                         dim4}, 1, dtype ?? torchlite.default_dtype, requires_grad);
        }

        /// <summary>
        /// Initializes a 5-dimensional tensor with ones.
        /// </summary>
        /// <param name="dim1">First dimension.</param>
        /// <param name="dim2">Second dimension.</param>
        /// <param name="dim3">Third dimension.</param>
        /// <param name="dim4">Fourth dimension.</param>
        /// <param name="dim5">Fifth dimension.</param>
        /// <param name="dtype">The tensor data type.</param>
        /// <param name="requires_grad">Specifies whether to create a gradient for the tensor.</param>
        /// <returns>Tensor.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Tensor ones(int dim1,
                                  int dim2,
                                  int dim3,
                                  int dim4,
                                  int dim5,
                                  DType? dtype = null,
                                  bool requires_grad = false)
        {
            return torchlite.full(new []{dim1,
                                         dim2,
                                         dim3,
                                         dim4,
                                         dim5}, 1, dtype ?? torchlite.default_dtype, requires_grad);
        }

        /// <summary>
        /// Initializes a 6-dimensional tensor with ones.
        /// </summary>
        /// <param name="dim1">First dimension.</param>
        /// <param name="dim2">Second dimension.</param>
        /// <param name="dim3">Third dimension.</param>
        /// <param name="dim4">Fourth dimension.</param>
        /// <param name="dim5">Fifth dimension.</param>
        /// <param name="dim6">Sixth dimension.</param>
        /// <param name="dtype">The tensor data type.</param>
        /// <param name="requires_grad">Specifies whether to create a gradient for the tensor.</param>
        /// <returns>Tensor.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Tensor ones(int dim1,
                                  int dim2,
                                  int dim3,
                                  int dim4,
                                  int dim5,
                                  int dim6,
                                  DType? dtype = null,
                                  bool requires_grad = false)
        {
            return torchlite.full(new []{dim1,
                                         dim2,
                                         dim3,
                                         dim4,
                                         dim5,
                                         dim6}, 1, dtype ?? torchlite.default_dtype, requires_grad);
        }

        /// <summary>
        /// Initializes a 7-dimensional tensor with ones.
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
        /// <returns>Tensor.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Tensor ones(int dim1,
                                  int dim2,
                                  int dim3,
                                  int dim4,
                                  int dim5,
                                  int dim6,
                                  int dim7,
                                  DType? dtype = null,
                                  bool requires_grad = false)
        {
            return torchlite.full(new []{dim1,
                                         dim2,
                                         dim3,
                                         dim4,
                                         dim5,
                                         dim6,
                                         dim7}, 1, dtype ?? torchlite.default_dtype, requires_grad);
        }

        /// <summary>
        /// Initializes a 8-dimensional tensor with ones.
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
        /// <returns>Tensor.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Tensor ones(int dim1,
                                  int dim2,
                                  int dim3,
                                  int dim4,
                                  int dim5,
                                  int dim6,
                                  int dim7,
                                  int dim8,
                                  DType? dtype = null,
                                  bool requires_grad = false)
        {
            return torchlite.full(new []{dim1,
                                         dim2,
                                         dim3,
                                         dim4,
                                         dim5,
                                         dim6,
                                         dim7,
                                         dim8}, 1, dtype ?? torchlite.default_dtype, requires_grad);
        }

        #endregion

        #region ones_like(value, dtype, requires_grad)

        /// <summary>
        /// Returns a new tensor of same shape as value filled with ones.
        /// </summary>
        /// <param name="value">Source tensor.</param>
        /// <param name="dtype">Data type of the new tensor.</param>
        /// <param name="requires_grad">Specifies whether to create a gradient for the tensor.</param>
        /// <returns>Tensor.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Tensor ones_like(Tensor value,
                                       DType? dtype = null,
                                       bool requires_grad = false)
        {
            return torchlite.full(value.shape, 1, dtype ?? value.dtype, requires_grad);
        }

        #endregion

        #region eye(*shape, dtype, requires_grad)

        /// <summary>
        /// Returns an unit matrix of size m by n.
        /// </summary>
        /// <param name="n">Matrix height.</param>
        /// <param name="m">Matrix width.</param>
        /// <param name="dtype">The tensor data type.</param>
        /// <param name="requires_grad">Specifies whether to create a gradient for the tensor.</param>
        /// <returns>Tensor.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Tensor eye(int n,
                                 int m,
                                 DType? dtype = null,
                                 bool requires_grad = false)
        {
            var t = torchlite.zeros(n, m, dtype, requires_grad);
            n = Math.Min(n, m);
            switch(t.dtype)
            {
                case torchlite.float32:
                {
                    var ptr = (float*)t.storage.data_ptr;
                    for(int i = 0; i < n; ++i)
                    {
                        ptr[i * m + i] = 1;
                    }
                    break;
                }
                case torchlite.int32:
                {
                    var ptr = (int*)t.storage.data_ptr;
                    for(int i = 0; i < n; ++i)
                    {
                        ptr[i * m + i] = 1;
                    }
                    break;
                }
                case torchlite.@bool:
                {
                    var ptr = (bool*)t.storage.data_ptr;
                    for(int i = 0; i < n; ++i)
                    {
                        ptr[i * m + i] = true;
                    }
                    break;
                }
            }
            return t;
        }

        /// <summary>
        /// Returns an unit matrix of size n by n.
        /// </summary>
        /// <param name="n">Matrix height.</param>
        /// <param name="dtype">The tensor data type.</param>
        /// <param name="requires_grad">Specifies whether to create a gradient for the tensor.</param>
        /// <returns>Tensor.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Tensor eye(int n,
                                 DType? dtype = null,
                                 bool requires_grad = false)
        {
            return torchlite.eye(n, n, dtype, requires_grad);
        }

        #endregion

        #region randn(*shape, dtype, requires_grad)

        /// <summary>
        /// Returns a tensor of the specified size filled with normal random numbers.
        /// </summary>
        /// <param name="shape">Tensor's dimensions.</param>
        /// <param name="dtype">The tensor data type.</param>
        /// <param name="requires_grad">Specifies whether to create a gradient for the tensor.</param>
        /// <returns>Tensor.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Tensor randn(IList<int> shape,
                                   DType? dtype = null,
                                   bool requires_grad = false)
        {
            var t = new Tensor(shape, dtype, requires_grad);
            switch(t.dtype)
            {
                case torchlite.float32:
                {
                    var ptr = (float*)t.storage.data_ptr;
                    var elements = t.storage.size;
                    for(int i = 0; i < elements; ++i)
                    {
                        var u1 = 1.0 - __rand.NextDouble();
                        var u2 = 1.0 - __rand.NextDouble();
                        var randStdNormal = Math.Sqrt(-2.0 * Math.Log(u1)) * Math.Sin(2.0 * Math.PI * u2);
                        ptr[i] = (float)randStdNormal;
                    }
                    break;
                }
                case torchlite.int32:
                case torchlite.@bool:
                {
                    throw new NotImplementedException("torchlite.randn is not implemented for int and bool tensors.");
                }
            }
            return t;
        }

        /// <summary>
        /// Initializes a 0-dimensional tensor with normal random numbers.
        /// </summary>
        /// <param name="dtype">The tensor data type.</param>
        /// <param name="requires_grad">Specifies whether to create a gradient for the tensor.</param>
        /// <returns>Tensor.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Tensor randn(DType? dtype = null,
                                   bool requires_grad = false)
        {
            return torchlite.randn(new int[0], dtype, requires_grad);
        }

        /// <summary>
        /// Initializes a 1-dimensional tensor with normal random numbers.
        /// </summary>
        /// <param name="dim1">First dimension.</param>
        /// <param name="dtype">The tensor data type.</param>
        /// <param name="requires_grad">Specifies whether to create a gradient for the tensor.</param>
        /// <returns>Tensor.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Tensor randn(int dim1,
                                   DType? dtype = null,
                                   bool requires_grad = false)
        {
            return torchlite.randn(new []{dim1}, dtype, requires_grad);
        }

        /// <summary>
        /// Initializes a 2-dimensional tensor with normal random numbers.
        /// </summary>
        /// <param name="dim1">First dimension.</param>
        /// <param name="dim2">Second dimension.</param>
        /// <param name="dtype">The tensor data type.</param>
        /// <param name="requires_grad">Specifies whether to create a gradient for the tensor.</param>
        /// <returns>Tensor.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Tensor randn(int dim1,
                                   int dim2,
                                   DType? dtype = null,
                                   bool requires_grad = false)
        {
            return torchlite.randn(new []{dim1,
                                          dim2}, dtype, requires_grad);
        }

        /// <summary>
        /// Initializes a 3-dimensional tensor with normal random numbers.
        /// </summary>
        /// <param name="dim1">First dimension.</param>
        /// <param name="dim2">Second dimension.</param>
        /// <param name="dim3">Third dimension.</param>
        /// <param name="dtype">The tensor data type.</param>
        /// <param name="requires_grad">Specifies whether to create a gradient for the tensor.</param>
        /// <returns>Tensor.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Tensor randn(int dim1,
                                   int dim2,
                                   int dim3,
                                   DType? dtype = null,
                                   bool requires_grad = false)
        {
            return torchlite.randn(new []{dim1,
                                          dim2,
                                          dim3}, dtype, requires_grad);
        }

        /// <summary>
        /// Initializes a 4-dimensional tensor with normal random numbers.
        /// </summary>
        /// <param name="dim1">First dimension.</param>
        /// <param name="dim2">Second dimension.</param>
        /// <param name="dim3">Third dimension.</param>
        /// <param name="dim4">Fourth dimension.</param>
        /// <param name="dtype">The tensor data type.</param>
        /// <param name="requires_grad">Specifies whether to create a gradient for the tensor.</param>
        /// <returns>Tensor.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Tensor randn(int dim1,
                                   int dim2,
                                   int dim3,
                                   int dim4,
                                   DType? dtype = null,
                                   bool requires_grad = false)
        {
            return torchlite.randn(new []{dim1,
                                          dim2,
                                          dim3,
                                          dim4}, dtype, requires_grad);
        }

        /// <summary>
        /// Initializes a 5-dimensional tensor with normal random numbers.
        /// </summary>
        /// <param name="dim1">First dimension.</param>
        /// <param name="dim2">Second dimension.</param>
        /// <param name="dim3">Third dimension.</param>
        /// <param name="dim4">Fourth dimension.</param>
        /// <param name="dim5">Fifth dimension.</param>
        /// <param name="dtype">The tensor data type.</param>
        /// <param name="requires_grad">Specifies whether to create a gradient for the tensor.</param>
        /// <returns>Tensor.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Tensor randn(int dim1,
                                   int dim2,
                                   int dim3,
                                   int dim4,
                                   int dim5,
                                   DType? dtype = null,
                                   bool requires_grad = false)
        {
            return torchlite.randn(new []{dim1,
                                          dim2,
                                          dim3,
                                          dim4,
                                          dim5}, dtype, requires_grad);
        }

        /// <summary>
        /// Initializes a 6-dimensional tensor with normal random numbers.
        /// </summary>
        /// <param name="dim1">First dimension.</param>
        /// <param name="dim2">Second dimension.</param>
        /// <param name="dim3">Third dimension.</param>
        /// <param name="dim4">Fourth dimension.</param>
        /// <param name="dim5">Fifth dimension.</param>
        /// <param name="dim6">Sixth dimension.</param>
        /// <param name="dtype">The tensor data type.</param>
        /// <param name="requires_grad">Specifies whether to create a gradient for the tensor.</param>
        /// <returns>Tensor.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Tensor randn(int dim1,
                                   int dim2,
                                   int dim3,
                                   int dim4,
                                   int dim5,
                                   int dim6,
                                   DType? dtype = null,
                                   bool requires_grad = false)
        {
            return torchlite.randn(new []{dim1,
                                          dim2,
                                          dim3,
                                          dim4,
                                          dim5,
                                          dim6}, dtype, requires_grad);
        }

        /// <summary>
        /// Initializes a 7-dimensional tensor with normal random numbers.
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
        /// <returns>Tensor.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Tensor randn(int dim1,
                                   int dim2,
                                   int dim3,
                                   int dim4,
                                   int dim5,
                                   int dim6,
                                   int dim7,
                                   DType? dtype = null,
                                   bool requires_grad = false)
        {
            return torchlite.randn(new []{dim1,
                                          dim2,
                                          dim3,
                                          dim4,
                                          dim5,
                                          dim6,
                                          dim7}, dtype, requires_grad);
        }

        /// <summary>
        /// Initializes a 8-dimensional tensor with normal random numbers.
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
        /// <returns>Tensor.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Tensor randn(int dim1,
                                   int dim2,
                                   int dim3,
                                   int dim4,
                                   int dim5,
                                   int dim6,
                                   int dim7,
                                   int dim8,
                                   DType? dtype = null,
                                   bool requires_grad = false)
        {
            return torchlite.randn(new []{dim1,
                                          dim2,
                                          dim3,
                                          dim4,
                                          dim5,
                                          dim6,
                                          dim7,
                                          dim8}, dtype, requires_grad);
        }

        #endregion

        #region randn_like(value, dtype, requires_grad)

        /// <summary>
        /// Returns a new tensor of same shape as value filled with the normal random numbers.
        /// </summary>
        /// <param name="value">Source tensor.</param>
        /// <param name="dtype">Data type of the new tensor.</param>
        /// <param name="requires_grad">Specifies whether to create a gradient for the tensor.</param>
        /// <returns>Tensor.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Tensor randn_like(Tensor value,
                                        DType? dtype = null,
                                        bool requires_grad = false)
        {
            return torchlite.randn(value.shape, dtype ?? value.dtype, requires_grad);
        }

        #endregion

        #region uniform(*shape, min, max, dtype, requires_grad)

        /// <summary>
        /// Returns a tensor of the specified size filled with uniform random numbers.
        /// </summary>
        /// <param name="shape">Tensor's dimensions.</param>
        /// <param name="min">Lower bound.</param>
        /// <param name="max">Upper bound.</param>
        /// <param name="dtype">The tensor data type.</param>
        /// <param name="requires_grad">Specifies whether to create a gradient for the tensor.</param>
        /// <returns>Tensor.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Tensor uniform(IList<int> shape,
                                     float min,
                                     float max,
                                     DType? dtype = null,
                                     bool requires_grad = false)
        {
            if(min >= max)
            {
                throw new ArgumentException(string.Format("Lower uniform bound should be less than upper, but [{0}, {1}] range given.", min, max));
            }
            var t = new Tensor(shape, dtype, requires_grad);
            switch(t.dtype)
            {
                case torchlite.float32:
                {
                    var ptr = (float*)t.storage.data_ptr;
                    var elements = t.storage.size;
                    for(int i = 0; i < elements; ++i)
                    {
                        ptr[i] = (float)(__rand.NextDouble() * (max - min) + min);
                    }
                    break;
                }
                case torchlite.int32:
                {
                    var ptr = (int*)t.storage.data_ptr;
                    var elements = t.storage.size;
                    for(int i = 0; i < elements; ++i)
                    {
                        ptr[i] = __rand.Next((int)min, (int)max);
                    }
                    break;
                }
                case torchlite.@bool:
                {
                    throw new NotImplementedException("torchlite.uniform is not implemented for bool tensors.");
                }
            }
            return t;
        }

        /// <summary>
        /// Initializes a 0-dimensional tensor with uniform random numbers.
        /// </summary>
        /// <param name="min">Bottom bound of uniform random sequence.</param>
        /// <param name="max">Upper bound of uniform random sequence.</param>
        /// <param name="dtype">The tensor data type.</param>
        /// <param name="requires_grad">Specifies whether to create a gradient for the tensor.</param>
        /// <returns>Tensor.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Tensor uniform(float min,
                                     float max,
                                     DType? dtype = null,
                                     bool requires_grad = false)
        {
            return torchlite.uniform(new int[0], min, max, dtype, requires_grad);
        }

        /// <summary>
        /// Initializes a 1-dimensional tensor with uniform random numbers.
        /// </summary>
        /// <param name="dim1">First dimension.</param>
        /// <param name="min">Bottom bound of uniform random sequence.</param>
        /// <param name="max">Upper bound of uniform random sequence.</param>
        /// <param name="dtype">The tensor data type.</param>
        /// <param name="requires_grad">Specifies whether to create a gradient for the tensor.</param>
        /// <returns>Tensor.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Tensor uniform(int dim1,
                                     float min,
                                     float max,
                                     DType? dtype = null,
                                     bool requires_grad = false)
        {
            return torchlite.uniform(new []{dim1}, min, max, dtype, requires_grad);
        }

        /// <summary>
        /// Initializes a 2-dimensional tensor with uniform random numbers.
        /// </summary>
        /// <param name="dim1">First dimension.</param>
        /// <param name="dim2">Second dimension.</param>
        /// <param name="min">Bottom bound of uniform random sequence.</param>
        /// <param name="max">Upper bound of uniform random sequence.</param>
        /// <param name="dtype">The tensor data type.</param>
        /// <param name="requires_grad">Specifies whether to create a gradient for the tensor.</param>
        /// <returns>Tensor.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Tensor uniform(int dim1,
                                     int dim2,
                                     float min,
                                     float max,
                                     DType? dtype = null,
                                     bool requires_grad = false)
        {
            return torchlite.uniform(new []{dim1,
                                            dim2}, min, max, dtype, requires_grad);
        }

        /// <summary>
        /// Initializes a 3-dimensional tensor with uniform random numbers.
        /// </summary>
        /// <param name="dim1">First dimension.</param>
        /// <param name="dim2">Second dimension.</param>
        /// <param name="dim3">Third dimension.</param>
        /// <param name="min">Bottom bound of uniform random sequence.</param>
        /// <param name="max">Upper bound of uniform random sequence.</param>
        /// <param name="dtype">The tensor data type.</param>
        /// <param name="requires_grad">Specifies whether to create a gradient for the tensor.</param>
        /// <returns>Tensor.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Tensor uniform(int dim1,
                                     int dim2,
                                     int dim3,
                                     float min,
                                     float max,
                                     DType? dtype = null,
                                     bool requires_grad = false)
        {
            return torchlite.uniform(new []{dim1,
                                            dim2,
                                            dim3}, min, max, dtype, requires_grad);
        }

        /// <summary>
        /// Initializes a 4-dimensional tensor with uniform random numbers.
        /// </summary>
        /// <param name="dim1">First dimension.</param>
        /// <param name="dim2">Second dimension.</param>
        /// <param name="dim3">Third dimension.</param>
        /// <param name="dim4">Fourth dimension.</param>
        /// <param name="min">Bottom bound of uniform random sequence.</param>
        /// <param name="max">Upper bound of uniform random sequence.</param>
        /// <param name="dtype">The tensor data type.</param>
        /// <param name="requires_grad">Specifies whether to create a gradient for the tensor.</param>
        /// <returns>Tensor.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Tensor uniform(int dim1,
                                     int dim2,
                                     int dim3,
                                     int dim4,
                                     float min,
                                     float max,
                                     DType? dtype = null,
                                     bool requires_grad = false)
        {
            return torchlite.uniform(new []{dim1,
                                            dim2,
                                            dim3,
                                            dim4}, min, max, dtype, requires_grad);
        }

        /// <summary>
        /// Initializes a 5-dimensional tensor with uniform random numbers.
        /// </summary>
        /// <param name="dim1">First dimension.</param>
        /// <param name="dim2">Second dimension.</param>
        /// <param name="dim3">Third dimension.</param>
        /// <param name="dim4">Fourth dimension.</param>
        /// <param name="dim5">Fifth dimension.</param>
        /// <param name="min">Bottom bound of uniform random sequence.</param>
        /// <param name="max">Upper bound of uniform random sequence.</param>
        /// <param name="dtype">The tensor data type.</param>
        /// <param name="requires_grad">Specifies whether to create a gradient for the tensor.</param>
        /// <returns>Tensor.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Tensor uniform(int dim1,
                                     int dim2,
                                     int dim3,
                                     int dim4,
                                     int dim5,
                                     float min,
                                     float max,
                                     DType? dtype = null,
                                     bool requires_grad = false)
        {
            return torchlite.uniform(new []{dim1,
                                            dim2,
                                            dim3,
                                            dim4,
                                            dim5}, min, max, dtype, requires_grad);
        }

        /// <summary>
        /// Initializes a 6-dimensional tensor with uniform random numbers.
        /// </summary>
        /// <param name="dim1">First dimension.</param>
        /// <param name="dim2">Second dimension.</param>
        /// <param name="dim3">Third dimension.</param>
        /// <param name="dim4">Fourth dimension.</param>
        /// <param name="dim5">Fifth dimension.</param>
        /// <param name="dim6">Sixth dimension.</param>
        /// <param name="min">Bottom bound of uniform random sequence.</param>
        /// <param name="max">Upper bound of uniform random sequence.</param>
        /// <param name="dtype">The tensor data type.</param>
        /// <param name="requires_grad">Specifies whether to create a gradient for the tensor.</param>
        /// <returns>Tensor.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Tensor uniform(int dim1,
                                     int dim2,
                                     int dim3,
                                     int dim4,
                                     int dim5,
                                     int dim6,
                                     float min,
                                     float max,
                                     DType? dtype = null,
                                     bool requires_grad = false)
        {
            return torchlite.uniform(new []{dim1,
                                            dim2,
                                            dim3,
                                            dim4,
                                            dim5,
                                            dim6}, min, max, dtype, requires_grad);
        }

        /// <summary>
        /// Initializes a 7-dimensional tensor with uniform random numbers.
        /// </summary>
        /// <param name="dim1">First dimension.</param>
        /// <param name="dim2">Second dimension.</param>
        /// <param name="dim3">Third dimension.</param>
        /// <param name="dim4">Fourth dimension.</param>
        /// <param name="dim5">Fifth dimension.</param>
        /// <param name="dim6">Sixth dimension.</param>
        /// <param name="dim7">Seventh dimension.</param>
        /// <param name="min">Bottom bound of uniform random sequence.</param>
        /// <param name="max">Upper bound of uniform random sequence.</param>
        /// <param name="dtype">The tensor data type.</param>
        /// <param name="requires_grad">Specifies whether to create a gradient for the tensor.</param>
        /// <returns>Tensor.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Tensor uniform(int dim1,
                                     int dim2,
                                     int dim3,
                                     int dim4,
                                     int dim5,
                                     int dim6,
                                     int dim7,
                                     float min,
                                     float max,
                                     DType? dtype = null,
                                     bool requires_grad = false)
        {
            return torchlite.uniform(new []{dim1,
                                            dim2,
                                            dim3,
                                            dim4,
                                            dim5,
                                            dim6,
                                            dim7}, min, max, dtype, requires_grad);
        }

        /// <summary>
        /// Initializes a 8-dimensional tensor with uniform random numbers.
        /// </summary>
        /// <param name="dim1">First dimension.</param>
        /// <param name="dim2">Second dimension.</param>
        /// <param name="dim3">Third dimension.</param>
        /// <param name="dim4">Fourth dimension.</param>
        /// <param name="dim5">Fifth dimension.</param>
        /// <param name="dim6">Sixth dimension.</param>
        /// <param name="dim7">Seventh dimension.</param>
        /// <param name="dim8">Eighth dimension.</param>
        /// <param name="min">Bottom bound of uniform random sequence.</param>
        /// <param name="max">Upper bound of uniform random sequence.</param>
        /// <param name="dtype">The tensor data type.</param>
        /// <param name="requires_grad">Specifies whether to create a gradient for the tensor.</param>
        /// <returns>Tensor.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Tensor uniform(int dim1,
                                     int dim2,
                                     int dim3,
                                     int dim4,
                                     int dim5,
                                     int dim6,
                                     int dim7,
                                     int dim8,
                                     float min,
                                     float max,
                                     DType? dtype = null,
                                     bool requires_grad = false)
        {
            return torchlite.uniform(new []{dim1,
                                            dim2,
                                            dim3,
                                            dim4,
                                            dim5,
                                            dim6,
                                            dim7,
                                            dim8}, min, max, dtype, requires_grad);
        }

        #endregion

        #region uniform_like(value, dtype, requires_grad)

        /// <summary>
        /// Returns a new tensor of same shape as value filled with ones.
        /// </summary>
        /// <param name="value">Source tensor.</param>
        /// <param name="min">Lower bound.</param>
        /// <param name="max">Upper bound.</param>
        /// <param name="dtype">Data type of the new tensor.</param>
        /// <param name="requires_grad">Specifies whether to create a gradient for the tensor.</param>
        /// <returns>Tensor.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Tensor uniform_like(Tensor value,
                                          float min,
                                          float max,
                                          DType? dtype = null,
                                          bool requires_grad = false)
        {
            return torchlite.uniform(value.shape, min, max, dtype ?? value.dtype, requires_grad);
        }

        #endregion

        #endregion

        #region extensionmethods

        /// <summary>
        /// Returns the size of the data type in bytes.
        /// </summary>
        /// <param name="dtype">Data type.</param>
        /// <returns>Size in bytes.</returns>
        public static int size(this DType dtype)
        {
            switch(dtype)
            {
                case torchlite.@bool:
                {
                    return 1;
                }
                case torchlite.float32:
                case torchlite.int32:
                {
                    return 4;
                }
                default:
                {
                    throw new TypeAccessException(string.Format("Invalid type code {0}.", (byte)dtype));
                }
            }
        }

        #endregion

    }

}