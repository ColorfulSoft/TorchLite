//***************************************************************************************************
//* (C) ColorfulSoft corp., 2019-2023. All rights reserved.
//* The code is available under the Apache-2.0 license. Read the License for details.
//***************************************************************************************************

namespace System.AI.Experimental
{

    public static partial class torchlite
    {

        /// <summary>
        /// Constructs a tensor with no autograd history by copying data.
        /// </summary>
        /// <param name="data">Initial data for the tensor. Can be a .NET Array or scalar.</param>
        /// <param name="dtype">The desired data type of returned tensor. Default: if null, infers data type from data.</param>
        /// <param name="requires_grad">If autograd should record operations on the returned tensor. Default: false.</param>
        /// <returns>Tensor.</returns>
        public static Tensor tensor(object data, DType? dtype = null, bool requires_grad = false)
        {
            if(data is Array)
            {
                return Tensor.from_dotnet(data as Array, dtype, requires_grad);
            }
            if(data is float)
            {
                return (float)data;
            }
            if(data is int)
            {
                return (int)data;
            }
            if(data is bool)
            {
                return (bool)data;
            }
            throw new ArgumentException("data should be .NET Array, float, int or bool data type.");
        }

    }

}