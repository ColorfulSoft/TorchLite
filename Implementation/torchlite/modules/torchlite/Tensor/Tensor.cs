//***************************************************************************************************
//* (C) ColorfulSoft corp., 2019-2023. All rights reserved.
//* The code is available under the Apache-2.0 license. Read the License for details.
//***************************************************************************************************

using System;

namespace System.AI.Experimental
{

    public static partial class torchlite
    {

        public partial class Tensor
        {

            #region delegates

            /// <summary>
            /// Back propagation function template.
            /// </summary>
            internal delegate void __backward_fn();

            #endregion

            #region fields

            /// <summary>
            /// Parent tensors.
            /// </summary>
            internal Tensor[] __parents;

            /// <summary>
            /// The back propagation function.
            /// </summary>
            internal __backward_fn backward_fn;

            /// <summary>
            /// Gradient tensor.
            /// </summary>
            private Tensor __grad;

            #endregion

            #region properties

            /// <summary>
            /// Storage for tensor's data.
            /// </summary>
            public Storage storage
            {

                get;

                private set;

            }

            /// <summary>
            /// Tensor's shape.
            /// </summary>
            public Size shape
            {

                get;

                private set;

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

            #endregion

        }

    }

}