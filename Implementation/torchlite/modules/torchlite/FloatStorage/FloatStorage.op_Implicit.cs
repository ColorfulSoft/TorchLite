//***************************************************************************************************
//* (C) ColorfulSoft corp., 2019-2023. All rights reserved.
//* The code is available under the Apache-2.0 license. Read the License for details.
//***************************************************************************************************

using System.Runtime.InteropServices;
namespace System.AI.Experimental
{

    public static partial class torchlite
    {

        public partial class FloatStorage
        {

            /// <summary>
            /// Implicitly converts .NET array to the FloatStorage object.
            /// </summary>
            /// <param name="array">.NET array of float, int or bool data type.</param>
            /// <returns>FloatStorage object.</returns>
            public static implicit operator FloatStorage(Array array)
            {
                return new FloatStorage(array);
            }

        }

    }

}