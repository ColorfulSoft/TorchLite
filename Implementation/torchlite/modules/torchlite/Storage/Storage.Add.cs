﻿//***************************************************************************************************
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
            /// Adds an item to the ICollection&lt;T&gt;.
            /// </summary>
            /// <param name="item">The object to add to the ICollection&lt;T&gt;.</param>
            [Obsolete("ICollection<object>.Add(object) -> void method is not implemented for torchlite.Storage.", true)]
            void ICollection<object>.Add(object item)
            {
                throw new NotSupportedException("ICollection<object>.Add(object) -> void method is not implemented for torchlite.Storage.");
            }

        }

    }

}