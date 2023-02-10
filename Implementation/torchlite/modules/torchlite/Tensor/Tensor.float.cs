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
            /// Converts the current tensor to the torchlite.float32 data type.
            /// </summary>
            /// <returns>FloatTensor.</returns>
            public Tensor @float()
            {
                var t = new Tensor(this.shape, torchlite.float32, this.requires_grad);
                var dst = (float*)t.storage.data_ptr;
                var n = this.storage.size;
                switch(this.dtype)
                {
                    case torchlite.float32:
                    {
                        var src = (float*)this.storage.data_ptr;
                        for(int i = 0; i < n; ++i)
                        {
                            dst[i] = src[i];
                        }
                        break;
                    }
                    case torchlite.int32:
                    {
                        var src = (int*)this.storage.data_ptr;
                        for(int i = 0; i < n; ++i)
                        {
                            dst[i] = src[i];
                        }
                        break;
                    }
                    case torchlite.@bool:
                    {
                        var src = (bool*)this.storage.data_ptr;
                        for(int i = 0; i < n; ++i)
                        {
                            dst[i] = src[i] ? 1 : 0;
                        }
                        break;
                    }
                }
                if(t.requires_grad)
                {
                    t.parents = new []{this};
                    t.backward_fn = () =>
                    {
                        var src_g = (float*)this.__grad.storage.data_ptr;
                        var dst_g = (float*)t.__grad.storage.data_ptr;
                        for(int i = 0; i < n; ++i)
                        {
                            src_g[i] += dst_g[i];
                        }
                    };
                }
                return t;
            }

        }

    }

}