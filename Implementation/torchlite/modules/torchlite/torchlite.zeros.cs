//***************************************************************************************************
//* (C) ColorfulSoft corp., 2019-2023. All rights reserved.
//* The code is available under the Apache-2.0 license. Read the License for details.
//***************************************************************************************************

using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace System.AI.Experimental
{

    public static partial class torchlite
    {

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

    }

}