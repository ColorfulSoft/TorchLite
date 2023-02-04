//***************************************************************************************************
//* (C) ColorfulSoft corp., 2019-2023. All rights reserved.
//* The code is available under the Apache-2.0 license. Read the License for details.
//***************************************************************************************************

using System.Runtime.InteropServices;
namespace System.AI.Experimental
{

    public static partial class torchlite
    {

        public unsafe partial class Storage
        {

            /// <summary>
            /// Implicitly converts .NET array to the Storage object.
            /// </summary>
            /// <param name="array">.NET array of float, int or bool data type.</param>
            /// <returns>Storage object.</returns>
            public static implicit operator Storage(Array array)
            {
                return new Storage(array);
            }

        }

    }

}