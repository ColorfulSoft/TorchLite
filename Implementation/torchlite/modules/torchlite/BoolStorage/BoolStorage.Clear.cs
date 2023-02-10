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
            /// Removes all items from the ICollection&lt;T&gt;.
            /// </summary>
            [Obsolete("ICollection<bool>.Clear() -> void method is not implemented for torchlite.BoolStorage.", true)]
            void ICollection<bool>.Clear()
            {
                throw new NotSupportedException("ICollection<bool>.Clear() -> void method is not implemented for torchlite.BoolStorage.");
            }

        }

    }

}