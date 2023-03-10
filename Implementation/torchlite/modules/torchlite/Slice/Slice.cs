//***************************************************************************************************
//* (C) ColorfulSoft corp., 2019-2023. All rights reserved.
//* The code is available under the Apache-2.0 license. Read the License for details.
//***************************************************************************************************

namespace System.AI.Experimental
{

    public static partial class torchlite
    {

        /// <summary>
        /// Represents information about the slice (start and end indexes).
        /// </summary>
        public partial struct Slice
        {

            /// <summary>
            /// The beginning of the slice.
            /// </summary>
            public readonly int begin;

            /// <summary>
            /// End of the slice.
            /// </summary>
            public readonly int end;

        }

    }

}