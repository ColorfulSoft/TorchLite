//***************************************************************************************************
//* (C) ColorfulSoft corp., 2019-2023. All rights reserved.
//* The code is available under the Apache-2.0 license. Read the License for details.
//***************************************************************************************************

using System;
using System.Threading;

namespace System.AI.Experimental
{

    public static partial class torchlite
    {

        /// <summary>
        /// Sets the seed for generating random numbers to a non-deterministic random number.
        /// Returns a 32 bit number used to seed the RNG.
        /// </summary>
        /// <returns>New seed.</returns>
        public static int seed()
        {
            // If the random number generator is not initialized in this thread, initialize it with thread id.
            if(torchlite.__rand == null)
            {
                torchlite.__rand = new Random(Thread.CurrentThread.ManagedThreadId);
            }
            // Generate new seed, initialize a new System.Random object with it.
            int seed = torchlite.__rand.Next();
            torchlite.__rand = new Random(seed);
            // Return new seed.
            return seed;
        }

    }

}