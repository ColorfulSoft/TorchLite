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

        public partial class Storage
        {

            /// <summary>
            /// Determines whether the ICollection&lt;T&gt; contains a specific value.
            /// </summary>
            /// <param name="item">The object to locate in the ICollection&lt;T&gt;.</param>
            /// <returns>true if item is found in the ICollection&lt;T&gt;; otherwise, false.</returns>
            [Obsolete("ICollection<object>.Contains(object) -> bool method is not implemented for torchlite.Storage.", true)]
            bool ICollection<object>.Contains(object item)
            {
                throw new NotSupportedException("ICollection<object>.Contains(object) -> bool method is not implemented for torchlite.Storage.");
            }

        }

    }

}