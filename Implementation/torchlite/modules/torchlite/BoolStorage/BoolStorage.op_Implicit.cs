//***************************************************************************************************
//* (C) ColorfulSoft corp., 2019-2023. All rights reserved.
//* The code is available under the Apache-2.0 license. Read the License for details.
//***************************************************************************************************

using System.Runtime.InteropServices;
namespace System.AI.Experimental
{

    public static partial class torchlite
    {

        public partial class BoolStorage
        {

            /// <summary>
            /// Implicitly converts .NET array to the BoolStorage object.
            /// </summary>
            /// <param name="array">.NET array of float, int or bool data type.</param>
            /// <returns>BoolStorage object.</returns>
            public static implicit operator BoolStorage(Array array)
            {
                return new BoolStorage(array);
            }

        }

    }

}