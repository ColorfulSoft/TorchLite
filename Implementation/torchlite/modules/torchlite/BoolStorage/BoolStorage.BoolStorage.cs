//***************************************************************************************************
//* (C) ColorfulSoft corp., 2019-2023. All rights reserved.
//* The code is available under the Apache-2.0 license. Read the License for details.
//***************************************************************************************************

using System;

namespace System.AI.Experimental
{

    public static partial class torchlite
    {

        public partial class BoolStorage
        {

            /// <summary>
            /// Initializes the bool storage of the specified size.
            /// </summary>
            /// <param name="size">The number of elements in the storage.</param>
            public BoolStorage(int size) : base(size, torchlite.@bool)
            {
            }

            /// <summary>
            /// Initializes the BoolStorage object from the .NET array.
            /// </summary>
            /// <param name="array">.NET Array of float, int or bool data type.</param>
            public BoolStorage(Array array) : base(array, torchlite.@bool)
            {
            }

        }

    }

}