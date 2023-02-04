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
        /// A torchlite.BoolStorage is a contiguous, one-dimensional array of elements of the
        /// torchlite.bool.
        /// </summary>
        public unsafe sealed partial class BoolStorage : Storage, IList<bool>
        {

            /// <summary>
            /// Gets or sets the element at the specified index.
            /// </summary>
            public new bool this[int index]
            {

                get
                {
                    if((index < 0) || (index >= this.size))
                    {
                        throw new IndexOutOfRangeException(string.Format("Index {0} is out of range of a storage of size {1}.", index, this.size));
                    }
                    return *((bool*)this.data_ptr + index);
                }

                set
                {
                    if((index < 0) || (index >= this.size))
                    {
                        throw new IndexOutOfRangeException(string.Format("Index {0} is out of range of a storage of size {1}.", index, this.size));
                    }
                    *((bool*)this.data_ptr + index) = value;
                }

            }

            /// <summary>
            /// Gets the number of elements contained in the ICollection&lt;T&gt;.
            /// </summary>
            int ICollection<bool>.Count
            {

                get
                {
                    return this.size;
                }

            }

            /// <summary>
            /// Gets a value indicating whether the ICollection&lt;T&gt; is read-only.
            /// </summary>
            bool ICollection<bool>.IsReadOnly
            {

                get
                {
                    return false;
                }

            }

        }

    }

}