//***************************************************************************************************
//* (C) ColorfulSoft corp., 2019-2023. All rights reserved.
//* The code is available under the Apache-2.0 license. Read the License for details.
//***************************************************************************************************

namespace System.AI.Experimental
{

    public unsafe static partial class torchlite
    {

        /// <summary>
        /// Applies the rectified linear unit function element-wise.
        /// </summary>
        /// <param name="input">Input tensor.</param>
        /// <returns>Tensor.</returns>
        public static Tensor relu(this Tensor input)
        {
            if(input.dtype == torchlite.@bool)
            {
                throw new NotImplementedException("torchlite.relu is not implemented for bool tensors.");
            }
            var output = new Tensor(input.shape, input.dtype, input.requires_grad);
            var elements = output.storage.size;
            switch(input.dtype)
            {
                case torchlite.float32:
                {
                    var src = (float*)input.storage.data_ptr;
                    var dst = (float*)output.storage.data_ptr;
                    for(int i = 0; i < elements; ++i)
                    {
                        dst[i] = (src[i] > 0) ? src[i] : 0;
                    }
                    if(input.requires_grad)
                    {
                        output.parents = new []{input};
                        output.backward_fn = () =>
                        {
                            var dst_grad = (float*)output.grad.storage.data_ptr;
                            var src_grad = (float*)input.grad.storage.data_ptr;
                            for(int i = 0; i < elements; ++i)
                            {
                                src_grad[i] += (src[i] > 0) ? dst_grad[i] : 0;
                            }
                        };
                    }
                    break;
                }
                case torchlite.int32:
                {
                    var src = (int*)input.storage.data_ptr;
                    var dst = (int*)output.storage.data_ptr;
                    for(int i = 0; i < elements; ++i)
                    {
                        dst[i] = (src[i] > 0) ? src[i] : 0;
                    }
                    break;
                }
            }
            return output;
        }

    }

}