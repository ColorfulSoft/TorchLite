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

        public partial class FloatStorage
        {

            /// <summary>
            /// Removes the IList&lt;T&gt; item at the specified index.
            /// </summary>
            /// <param name="index">The zero-based index of the item to remove.</param>
            [Obsolete("IList<float>.RemoveAt(int) -> void method is not implemented for torchlite.FloatStorage.", true)]
            void IList<float>.RemoveAt(int index)
            {
                throw new NotSupportedException("IList<float>.RemoveAt(int) -> void method is not implemented for torchlite.FloatStorage.");
            }

        }

    }

}