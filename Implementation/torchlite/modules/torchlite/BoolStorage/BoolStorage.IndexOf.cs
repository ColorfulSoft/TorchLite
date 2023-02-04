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
            /// Determines the index of a specific item in the IList&lt;T&gt;.
            /// </summary>
            /// <param name="item">The object to locate in the IList&lt;T&gt;.</param>
            /// <returns>The index of item if found in the list; otherwise, -1.</returns>
            [Obsolete("IList<bool>.IndexOf(bool) -> int method is not implemented for torchlite.BoolStorage.", true)]
            int IList<bool>.IndexOf(bool item)
            {
                throw new NotSupportedException("IList<bool>.IndexOf(bool) -> int method is not implemented for torchlite.BoolStorage.");
            }

        }

    }

}