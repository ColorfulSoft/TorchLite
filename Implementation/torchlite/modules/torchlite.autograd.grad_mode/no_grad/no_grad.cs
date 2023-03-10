//***************************************************************************************************
//* (C) ColorfulSoft corp., 2019-2023. All rights reserved.
//* The code is available under the Apache-2.0 license. Read the License for details.
//***************************************************************************************************

using System;

namespace System.AI.Experimental
{

    public static partial class torchlite
    {

        public static partial class autograd
        {

            public static partial class grad_mode
            {

                /// <summary>
                /// no_grad context manager. Disables gradient computing.
                /// </summary>
                [AttributeUsage(AttributeTargets.Assembly |
                                AttributeTargets.Class |
                                AttributeTargets.Constructor |
                                AttributeTargets.Method |
                                AttributeTargets.Struct)]
                public sealed partial class no_grad : Attribute, IDisposable
                {
                }

            }

        }

    }

}