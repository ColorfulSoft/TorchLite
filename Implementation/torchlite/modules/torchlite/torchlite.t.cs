//***************************************************************************************************
//* (C) ColorfulSoft corp., 2019-2023. All rights reserved.
//* The code is available under the Apache-2.0 license. Read the License for details.
//***************************************************************************************************

namespace System.AI.Experimental
{

    public static partial class torchlite
    {

        /// <summary>
        /// Expects input to be &lt;= 2-D tensor and transposes dimensions 0 and 1.
        /// 0-D and 1-D tensors are returned as is.
        /// When input is a 2-D tensor this is equivalent to transpose(input, 0, 1).
        /// </summary>
        /// <param name="input">The input tensor.</param>
        /// <returns>Tensor.</returns>
        public static Tensor t(this Tensor input)
        {
            if((input.shape.ndim == 0) || (input.shape.ndim == 1))
            {
                return input;
            }
            if(input.shape.ndim > 2)
            {
                throw new ArgumentException(string.Format("t() expects a tensor with <= 2 dimensions, but self is {0}D", input.shape.ndim));
            }
            return torchlite.permute(input, new []{1, 0});
        }

    }

}