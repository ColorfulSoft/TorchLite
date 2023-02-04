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

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            private string __get_format()
            {
                var elements = this.storage.size;
                switch(this.dtype)
                {
                    case torchlite.float32:
                    {
                        var max_power = float.MinValue;
                        var min_power = float.MaxValue;
                        var ptr = (float*)this.storage.data_ptr;
                        var sign = false;
                        var frac = false;
                        for(int i = 0; i < elements; ++i)
                        {
                            var v = ptr[i];
                            var p = ((v != 0) && !float.IsInfinity(v) && !float.IsNaN(v)) ? (int)Math.Floor(Math.Log10(Math.Abs(v))) : 0;
                            if(p > max_power)
                            {
                                max_power = p;
                            }
                            if(p < min_power)
                            {
                                min_power = p;
                            }
                        }
                        for(int i = 0; i < elements; ++i)
                        {
                            var v = ptr[i];
                            if(v < 0)
                            {
                                sign = true;
                                break;
                            }
                        }
                        for(int i = 0; i < elements; ++i)
                        {
                            var v = ptr[i];
                            if((v - Math.Floor(v)) != 0)
                            {
                                frac = true;
                                break;
                            }
                        }
                        var format = "{0,";
                        if((max_power < 4) && (min_power > -4))
                        {
                            format += ((frac ? 6 : 1) + (sign ? 1 : 0) + Math.Max(max_power, 0)).ToString() + (frac ? ":0.####}" : ":0}");
                        }
                        else
                        {
                            format += "11:0.0000e+00}";
                        }
                        return format;
                    }
                    case torchlite.int32:
                    {
                        var max = int.MinValue;
                        var min = int.MaxValue;
                        var ptr = (int*)this.storage.data_ptr;
                        for(int i = 0; i < elements; ++i)
                        {
                            var v = ptr[i];
                            if(v > max)
                            {
                                max = v;
                            }
                            if(v < min)
                            {
                                min = v;
                            }
                        }
                        var format = "{0,";
                        format += Math.Max(max.ToString().Length, min.ToString().Length).ToString() + '}';
                        return format;
                    }
                    case torchlite.@bool:
                    {
                        return "{0,5}";
                    }
                    default:
                    {
                        throw new NotImplementedException();
                    }
                }
            }

        }

    }

}