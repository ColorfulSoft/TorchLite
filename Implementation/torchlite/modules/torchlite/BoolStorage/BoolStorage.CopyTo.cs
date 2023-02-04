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

        public unsafe partial class BoolStorage
        {

            /// <summary>
            /// Copies the elements of the ICollection&lt;T&gt; to an Array, starting at a particular Array index.
            /// </summary>
            /// <param name="array">The one-dimensional Array that is the destination of the elements copied from ICollection&lt;T&gt;. The Array must have zero-based indexing.</param>
            /// <param name="arrayIndex">The zero-based index in array at which copying begins.</param>
            void ICollection<bool>.CopyTo(bool[] array, int arrayIndex)
            {
                if(array == null)
                {
                    throw new ArgumentNullException("array is null.");
                }
                if(arrayIndex < 0)
                {
                    throw new ArgumentOutOfRangeException("arrayIndex is less than 0.");
                }
                if((this.size + arrayIndex) > array.Length)
                {
                    throw new ArgumentException("The number of elements in the source ICollection<T> is greater than the available space from arrayIndex to the end of the destination array.");
                }
                var ptr = (bool*)this.data_ptr;
                for(int i = 0; i < this.size; ++i)
                {
                    array[arrayIndex + i] = ptr[i];
                }
            }

        }

    }

}