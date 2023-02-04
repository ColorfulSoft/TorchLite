//***************************************************************************************************
//* (C) ColorfulSoft corp., 2019-2023. All rights reserved.
//* The code is available under the Apache-2.0 license. Read the License for details.
//***************************************************************************************************

using System;
using System.Runtime.InteropServices;

namespace System.AI.Experimental
{

    public static partial class torchlite
    {

        public partial class IntStorage
        {

            /// <summary>
            /// Initializes the integer storage of the specified size.
            /// </summary>
            /// <param name="size">The number of elements in the storage.</param>
            public IntStorage(int size) : base(size, torchlite.int32)
            {
            }

            /// <summary>
            /// Initializes the IntStorage object from the .NET array.
            /// </summary>
            /// <param name="array">.NET Array of float, int or bool data type.</param>
            public IntStorage(Array array) : base(array, torchlite.int32)
            {
            }

        }

    }

}