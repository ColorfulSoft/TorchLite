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

        public partial class BoolStorage
        {

            /// <summary>
            /// Removes the first occurrence of a specific object from the ICollection&lt;T&gt;.
            /// </summary>
            /// <param name="item">The object to remove from the ICollection&lt;T&gt;.</param>
            /// <returns>true if item was successfully removed from the ICollection&lt;T&gt;; otherwise, false. This method also returns false if item is not found in the original ICollection&lt;T&gt;.</returns>
            [Obsolete("ICollection<bool>.Remove(bool) -> bool method is not implemented for torchlite.BoolStorage.", true)]
            bool ICollection<bool>.Remove(bool item)
            {
                throw new NotSupportedException("ICollection<bool>.Remove(bool) -> bool method is not implemented for torchlite.BoolStorage.");
            }

        }

    }

}