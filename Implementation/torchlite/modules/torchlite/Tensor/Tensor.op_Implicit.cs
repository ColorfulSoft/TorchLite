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

            public static implicit operator Tensor(float value)
            {
                var t = new Tensor(torchlite.float32);
                *((float*)t.storage.data_ptr) = value;
                return t;
            }

            public static implicit operator Tensor(int value)
            {
                var t = new Tensor(torchlite.int32);
                *((int*)t.storage.data_ptr) = value;
                return t;
            }

            public static implicit operator Tensor(bool value)
            {
                var t = new Tensor(torchlite.@bool);
                *((bool*)t.storage.data_ptr) = value;
                return t;
            }

            public static implicit operator Tensor(Array value)
            {
                return from_dotnet(value);
            }

        }

    }

}