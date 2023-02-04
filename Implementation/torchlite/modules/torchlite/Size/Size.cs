//***************************************************************************************************
//* (C) ColorfulSoft corp., 2019-2023. All rights reserved.
//* The code is available under the Apache-2.0 license. Read the License for details.
//***************************************************************************************************

using System;
using System.Collections.Generic;

namespace System.AI.Experimental
{

    public static partial class torchlite
    {

        /// <summary>
        /// Represents the dimensions of the tensor.
        /// </summary>
        public unsafe sealed partial class Size : IList<int>
        {

            /// <summary>
            /// A pointer to dimensions array.
            /// </summary>
            public int* data_ptr
            {

                get;

                private set;

            }

            /// <summary>
            /// The number of dimensions.
            /// </summary>
            public int ndim
            {

                get;

                private set;

            }

            /// <summary>
            /// Gets or sets the element at the specified index.
            /// </summary>
            public int this[int index]
            {

                get
                {
                    if((index < 0) || (index >= this.ndim))
                    {
                        throw new ArgumentOutOfRangeException(string.Format("Index {0} is out of range of a {1}-dimensional size.", index, this.ndim));
                    }
                    return this.data_ptr[index];
                }

                set
                {
                    throw new NotSupportedException("torchlite.Size is immutable.");
                }

            }

            /// <summary>
            /// Gets the number of elements contained in the ICollection&lt;T&gt;.
            /// </summary>
            int ICollection<int>.Count
            {

                get
                {
                    return this.ndim;
                }

            }

            /// <summary>
            /// Gets a value indicating whether the ICollection&lt;T&gt; is read-only.
            /// </summary>
            bool ICollection<int>.IsReadOnly
            {

                get
                {
                    return true;
                }

            }

        }

    }

}