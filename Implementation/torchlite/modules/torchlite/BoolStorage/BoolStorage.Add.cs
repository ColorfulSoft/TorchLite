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
            /// Adds an item to the ICollection&lt;T&gt;.
            /// </summary>
            /// <param name="item">The object to add to the ICollection&lt;T&gt;.</param>
            [Obsolete("ICollection<bool>.Add(bool) -> void method is not implemented for torchlite.BoolStorage.", true)]
            void ICollection<bool>.Add(bool item)
            {
                throw new NotSupportedException("ICollection<bool>.Add(bool) -> void method is not implemented for torchlite.BoolStorage.");
            }

        }

    }

}