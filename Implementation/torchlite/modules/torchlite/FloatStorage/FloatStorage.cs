//***************************************************************************************************
//* (C) ColorfulSoft corp., 2019-2023. All rights reserved.
//* The code is available under the Apache-2.0 license. Read the License for details.
//***************************************************************************************************

using System.Collections.Generic;

namespace System.AI.Experimental
{

    public static partial class torchlite
    {

        /// <summary>
        /// A torchlite.FloatStorage is a contiguous, one-dimensional array of elements of the
        /// torchlite.float32.
        /// </summary>
        public unsafe sealed partial class FloatStorage : Storage, IList<float>
        {

            /// <summary>
            /// Gets or sets the element at the specified index.
            /// </summary>
            public new float this[int index]
            {

                get
                {
                    if((index < 0) || (index >= this.size))
                    {
                        throw new IndexOutOfRangeException(string.Format("Index {0} is out of range of a storage of size {1}.", index, this.size));
                    }
                    return *((float*)this.data_ptr + index);
                }

                set
                {
                    if((index < 0) || (index >= this.size))
                    {
                        throw new IndexOutOfRangeException(string.Format("Index {0} is out of range of a storage of size {1}.", index, this.size));
                    }
                    *((float*)this.data_ptr + index) = value;
                }

            }

            /// <summary>
            /// Gets the number of elements contained in the ICollection&lt;T&gt;.
            /// </summary>
            int ICollection<float>.Count
            {

                get
                {
                    return this.size;
                }

            }

            /// <summary>
            /// Gets a value indicating whether the ICollection&lt;T&gt; is read-only.
            /// </summary>
            bool ICollection<float>.IsReadOnly
            {

                get
                {
                    return false;
                }

            }

        }

    }

}