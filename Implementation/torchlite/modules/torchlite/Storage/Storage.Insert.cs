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
            /// Inserts an item to the IList&lt;T&gt; at the specified index.
            /// </summary>
            /// <param name="index">The zero-based index at which item should be inserted.</param>
            /// <param name="item">The object to insert into the IList&lt;T&gt;.</param>
            [Obsolete("IList<object>.Insert(int, object) -> void method is not implemented for torchlite.Storage.", true)]
            void IList<object>.Insert(int index, object item)
            {
                throw new NotSupportedException("IList<object>.Insert(int, object) -> void method is not implemented for torchlite.Storage.");
            }

        }

    }

}