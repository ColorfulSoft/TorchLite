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
            /// Converts the current Slice object to a string representation.
            /// </summary>
            /// <returns>String representation of Slice object.</returns>
            public override string ToString()
            {
                return string.Format("torchlite.Slice(begin={0}, end={1})", this.begin, this.end);
            }

        }

    }

}