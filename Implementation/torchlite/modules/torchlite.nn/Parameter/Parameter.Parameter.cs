//***************************************************************************************************
//* (C) ColorfulSoft corp., 2019-2022. All rights reserved.
//* The code is available under the Apache-2.0 license. Read the License for details.
//***************************************************************************************************

using System;

namespace System.AI.Experimental
{

    public static partial class torchlite
    {

        public static partial class nn
        {

            public partial class Parameter
            {

                /// <summary>
                /// Initializes the Parameter with specified Tensor.
                /// </summary>
                /// <param name="value">Tensor object.</param>
                /// <param name="requires_grad">Specifies whether to create a gradient for the parameter.</param>
                public Parameter(Tensor value, bool requires_grad = true) : base(value, requires_grad)
                {
                }

            }

        }

    }

}