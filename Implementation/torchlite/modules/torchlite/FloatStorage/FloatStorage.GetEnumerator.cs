//***************************************************************************************************
//* (C) ColorfulSoft corp., 2019-2023. All rights reserved.
//* The code is available under the Apache-2.0 license. Read the License for details.
//***************************************************************************************************

using System;
using System.Collections;
using System.Collections.Generic;

namespace System.AI.Experimental
{

    public static partial class torchlite
    {

        public partial class FloatStorage
        {

            /// <summary>
            /// Returns an enumerator that iterates through a collection.
            /// </summary>
            /// <returns>An IEnumerator object that can be used to iterate through the collection.</returns>
            IEnumerator IEnumerable.GetEnumerator()
            {
                for(int i = 0; i < this.size; ++i)
                {
                    yield return this[i];
                }
            }

            /// <summary>
            /// Returns an enumerator that iterates through a collection.
            /// </summary>
            /// <returns>An IEnumerator object that can be used to iterate through the collection.</returns>
            IEnumerator<float> IEnumerable<float>.GetEnumerator()
            {
                for(int i = 0; i < this.size; ++i)
                {
                    yield return this[i];
                }
            }

        }

    }

}