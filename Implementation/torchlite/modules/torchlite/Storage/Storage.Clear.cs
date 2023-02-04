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
            /// Removes all items from the ICollection&ltT&gt.
            /// </summary>
            [Obsolete("ICollection<object>.Clear() -> void method is not implemented for torchlite.Storage.", true)]
            void ICollection<object>.Clear()
            {
                throw new NotSupportedException("ICollection<object>.Clear() -> void method is not implemented for torchlite.Storage.");
            }

        }

    }

}