//***************************************************************************************************
//* (C) ColorfulSoft corp., 2019-2023. All rights reserved.
//* The code is available under the Apache-2.0 license. Read the License for details.
//***************************************************************************************************

using System;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;

namespace System.AI.Experimental
{

    public static partial class torchlite
    {

        public unsafe partial class Tensor
        {

            private void __print(ref string str, int* loc, int dim, string format)
            {
                if((this.shape.ndim - dim) != 0)
                {
                    if(dim > 0)
                    {
                        if(loc[dim - 1] > 0)
                        {
                            for(int i = 0; i < (7 + dim); ++i)
                            {
                                str += ' ';
                            }
                        }
                    }
                    str += '[';
                    var size = this.shape[dim];
                    for(int i = 0; i < size; ++i)
                    {
                        __print(ref str, loc, dim + 1, format);
                        ++loc[dim];
                    }
                    loc[dim] = 0;
                    str += ']';
                    if(dim > 0)
                    {
                        if((loc[dim - 1] + 1) != this.shape[dim - 1])
                        {
                            str += ',';
                            for(int j = 0; j < (this.shape.ndim - dim); ++j)
                            {
                                str += '\n';
                            }
                        }
                    }
                }
                else
                {
                    var n = this.shape.ndim;
                    var shape = this.shape.data_ptr;
                    var index = 0;
                    for(int i = 0; i < n; ++i)
                    {
                        index *= shape[i];
                        index += loc[i];
                    }
                    switch(this.dtype)
                    {
                        case torchlite.float32:
                        {
                            str += string.Format(new CultureInfo("en-US"), format, ((float*)this.storage.data_ptr)[index]);
                            break;
                        }
                        case torchlite.int32:
                        {
                            str += string.Format(new CultureInfo("en-US"), format, ((int*)this.storage.data_ptr)[index]);
                            break;
                        }
                        case torchlite.@bool:
                        {
                            str += string.Format(new CultureInfo("en-US"), format, ((bool*)this.storage.data_ptr)[index]);
                            break;
                        }
                    }
                    if((dim - 1) >= 0)
                    {
                        if((loc[dim - 1] + 1) != this.shape.data_ptr[dim - 1])
                        {
                            str += ", ";
                        }
                    }
                }
            }

        }

    }

}