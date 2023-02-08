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

        public partial class Tensor
        {

            internal Tensor()
            {
            }

            /// <summary>
            /// Initializes a new tensor with the storage of another tensor.
            /// </summary>
            /// <param name="other">Other tensor.</param>
            /// <param name="requires_grad">Specifies whether to create a gradient for the tensor.</param>
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            internal Tensor(Tensor other, bool requires_grad = false)
            {
                if(other == null)
                {
                    throw new NullReferenceException("Null value is invalid for 'other' parameter.");
                }
                if(!other.dtype.is_floating_point() && requires_grad)
                {
                    throw new ArgumentException("Only floating point tensors may requires_grad.");
                }
                this.shape = other.shape;
                this.storage = other.storage;
                if(requires_grad && torchlite.grad_enabled)
                {
                    this.__grad = torchlite.zeros(this.shape, this.dtype);
                }
            }

            /// <summary>
            /// Initializes a tensor of the specified size.
            /// </summary>
            /// <param name="shape">Tensor's dimensions.</param>
            /// <param name="dtype">The tensor data type.</param>
            /// <param name="requires_grad">Specifies whether to create a gradient for the tensor.</param>
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public Tensor(IList<int> shape,
                          DType? dtype = null,
                          bool requires_grad = false)
            {
                dtype = dtype ?? torchlite.default_dtype;
                if(!dtype.Value.is_floating_point() && requires_grad)
                {
                    throw new ArgumentException("Only floating point tensors may requires_grad.");
                }
                this.shape = new Size(shape);
                this.storage = new Storage(this.shape.numel(), dtype);
                if(requires_grad && torchlite.grad_enabled)
                {
                    this.__grad = torchlite.zeros(shape, dtype);
                }
            }

            /// <summary>
            /// Initializes a 0-dimensional tensor of a dtype type.
            /// </summary>
            /// <param name="dtype">The tensor data type.</param>
            /// <param name="requires_grad">Specifies whether to create a gradient for the tensor.</param>
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public Tensor(DType? dtype = null,
                          bool requires_grad = false) : this(null, dtype, requires_grad)
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

        }

    }

}