//***************************************************************************************************
//* (C) ColorfulSoft corp., 2019-2023. All rights reserved.
//* The code is available under the Apache-2.0 license. Read the License for details.
//***************************************************************************************************

namespace System.AI.Experimental
{

    public static partial class torchlite
    {

        public partial class Tensor
        {

            public static explicit operator float(Tensor value)
            {
                if(value.storage.size != 1)
                {
                    throw new ArgumentException("Float value of Tensor with more than one value is ambiguous.");
                }
                return Convert.ToSingle(value.storage[0]);
            }

            public static explicit operator int(Tensor value)
            {
                if(value.storage.size != 1)
                {
                    throw new ArgumentException("Integer value of Tensor with more than one value is ambiguous.");
                }
                return Convert.ToInt32(value.storage[0]);
            }

            public static explicit operator bool(Tensor value)
            {
                if(value.storage.size != 1)
                {
                    throw new ArgumentException("Boolean value of Tensor with more than one value is ambiguous.");
                }
                return Convert.ToBoolean(value.storage[0]);
            }

        }

    }

}