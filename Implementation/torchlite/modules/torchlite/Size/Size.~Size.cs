//***************************************************************************************************
//* (C) ColorfulSoft corp., 2019-2023. All rights reserved.
//* The code is available under the Apache-2.0 license. Read the License for details.
//***************************************************************************************************

using System;
using System.Runtime.InteropServices;

namespace System.AI.Experimental
{

    public static partial class torchlite
    {

        public unsafe partial class Size
        {

            /// <summary>
            /// Releases unmanaged resources.
            /// </summary>
            ~Size()
            {
                if(this.data_ptr != null)
                {
                    Marshal.FreeHGlobal((IntPtr)this.data_ptr);
                    this.data_ptr = null;
                }
            }

        }

    }

}