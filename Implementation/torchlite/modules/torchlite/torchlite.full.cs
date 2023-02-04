//***************************************************************************************************
//* (C) ColorfulSoft corp., 2019-2023. All rights reserved.
//* The code is available under the Apache-2.0 license. Read the License for details.
//***************************************************************************************************

using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace System.AI.Experimental
{

    public unsafe static partial class torchlite
    {

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

    }

}