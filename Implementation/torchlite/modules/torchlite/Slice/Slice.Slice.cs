//***************************************************************************************************
//* (C) ColorfulSoft corp., 2019-2023. All rights reserved.
//* The code is available under the Apache-2.0 license. Read the License for details.
//***************************************************************************************************

namespace System.AI.Experimental
{

    public static partial class torchlite
    {

        public partial struct Slice
        {

            /// <summary>
            /// Initializes the slice structure.
            /// </summary>
            /// <param name="begin">The beginning of the slice.</param>
            /// <param name="end">End of the slice.</param>
            public Slice(int begin, int? end = null)
            {
                this.begin = begin;
                this.end = end ?? begin + 1;
            }

        }

    }

}