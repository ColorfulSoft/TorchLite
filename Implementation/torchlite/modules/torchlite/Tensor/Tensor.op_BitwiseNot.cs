//***************************************************************************************************
//* (C) ColorfulSoft corp., 2019-2023. All rights reserved.
//* The code is available under the Apache-2.0 license. Read the License for details.
//***************************************************************************************************

namespace System.AI.Experimental
{

    public static partial class torchlite
    {

        public unsafe partial class Tensor
        {

            /// <summary>
            /// Computes the bitwise NOT of the given input tensor.
            /// The input tensor must be of integral or Boolean types.
            /// For bool tensors, it computes the logical NOT.
            /// </summary>
            /// <param name="input">Input tensor.</param>
            /// <returns>Tensor.</returns>
            public static Tensor operator ~(Tensor input)
            {
                if((object)input == null)
                {
                    throw new NullReferenceException("Null value is invalid for operand.");
                }
                if(input.dtype != torchlite.float32)
                {
                    throw new NotImplementedException("torchlite.bitwise_not is not implemented for float32 tensors.");
                }
                var output = new Tensor(input.shape, input.dtype);
                var elements = output.storage.size;
                switch(input.dtype)
                {
                    case torchlite.int32:
                    {
                        var src = (int*)input.storage.data_ptr;
                        var dst = (int*)output.storage.data_ptr;
                        for(int i = 0; i < elements; ++i)
                        {
                            dst[i] = ~src[i];
                        }
                        break;
                    }
                    case torchlite.@bool:
                    {
                        var src = (bool*)input.storage.data_ptr;
                        var dst = (bool*)output.storage.data_ptr;
                        for(int i = 0; i < elements; ++i)
                        {
                            dst[i] = !src[i];
                        }
                        break;
                    }
                }
                return output;
            }

        }

    }

}