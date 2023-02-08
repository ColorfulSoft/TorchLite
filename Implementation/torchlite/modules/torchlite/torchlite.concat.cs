//***************************************************************************************************
//* (C) ColorfulSoft corp., 2019-2023. All rights reserved.
//* The code is available under the Apache-2.0 license. Read the License for details.
//***************************************************************************************************

using System.Collections.Generic;

namespace System.AI.Experimental
{

    public static partial class torchlite
    {

        /// <summary>
        /// Alias of torchlite.cat().
        /// </summary>
        /// <param name="tensors">Any .NET sequence of tensors of the same type. Non-empty tensors provided must have the same shape, except in the cat dimension.</param>
        /// <param name="dim">The dimension over which the tensors are concatenated.</param>
        /// <returns>The output tensor.</returns>
        public static Tensor concat(IList<Tensor> tensors, int dim = 0)
        {
            return torchlite.cat(tensors, dim);
        }

    }

}