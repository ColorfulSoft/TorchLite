//***************************************************************************************************
//* (C) ColorfulSoft corp., 2019-2023. All rights reserved.
//* The code is available under the Apache-2.0 license. Read the License for details.
//***************************************************************************************************

using System;

namespace System.AI.Experimental
{

    public static partial class torchlite
    {

        public partial class FloatStorage
        {

            /// <summary>
            /// Initializes the floating point storage of the specified size.
            /// </summary>
            /// <param name="size">The number of elements in the storage.</param>
            public FloatStorage(int size) : base(size, torchlite.float32)
            {
            }

            /// <summary>
            /// Initializes the FloatStorage object from the .NET array.
            /// </summary>
            /// <param name="array">.NET Array of float, int or bool data type.</param>
            public FloatStorage(Array array) : base(array, torchlite.float32)
            {
            }

        }

    }

}