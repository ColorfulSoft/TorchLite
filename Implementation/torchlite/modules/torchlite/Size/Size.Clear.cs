//***************************************************************************************************
//* (C) ColorfulSoft corp., 2019-2023. All rights reserved.
//* The code is available under the Apache-2.0 license. Read the License for details.
//***************************************************************************************************

using System.Collections.Generic;

namespace System.AI.Experimental
{

    public static partial class torchlite
    {

        public partial class Size
        {

            /// <summary>
            /// Removes all items from the ICollection&ltT&gt.
            /// </summary>
            [Obsolete("ICollection<int>.Clear() -> void method is not implemented for torchlite.Size.", true)]
            void ICollection<int>.Clear()
            {
                throw new NotSupportedException("ICollection<int>.Clear() -> void method is not implemented for torchlite.Size.");
            }

        }

    }

}