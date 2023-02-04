//***************************************************************************************************
//* (C) ColorfulSoft corp., 2019-2023. All rights reserved.
//* The code is available under the Apache-2.0 license. Read the License for details.
//***************************************************************************************************

using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace System.AI.Experimental
{

    public unsafe static partial class torchlite
    {

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

    }

}