//***************************************************************************************************
//* (C) ColorfulSoft corp., 2019-2023. All rights reserved.
//* The code is available under the Apache-2.0 license. Read the License for details.
//***************************************************************************************************

namespace System.AI.Experimental
{

    public static partial class torchlite
    {

        public partial class Size
        {

            /// <summary>
            /// Compares two sizes.
            /// </summary>
            /// <param name="left">Left size object.</param>
            /// <param name="right">Right size object.</param>
            /// <returns>True if sizes are not equal.</returns>
            public static bool operator !=(Size left, Size right)
            {
                return !(left == right);
            }

        }

    }

}