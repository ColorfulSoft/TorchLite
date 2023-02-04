//***************************************************************************************************
//* (C) ColorfulSoft corp., 2019-2023. All rights reserved.
//* The code is available under the Apache-2.0 license. Read the License for details.
//***************************************************************************************************

using System;

namespace System.AI.Experimental
{

    public static partial class torchlite
    {

        /// <summary>
        /// Sets the seed for generating random numbers. Returns a System.Random object.
        /// </summary>
        /// <param name="seed">The desired seed.</param>
        /// <returns>System.Random object.</returns>
        public static Random manual_seed(int seed)
        {
            torchlite.__rand = new Random(seed);
            return torchlite.__rand;
        }

    }

}