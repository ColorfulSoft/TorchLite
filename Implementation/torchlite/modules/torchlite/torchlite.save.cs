//***************************************************************************************************
//* (C) ColorfulSoft corp., 2019-2023. All rights reserved.
//* The code is available under the Apache-2.0 license. Read the License for details.
//***************************************************************************************************

using System.IO;
using System.Collections.Generic;

namespace System.AI.Experimental
{

    public unsafe static partial class torchlite
    {

        /// <summary>
        /// Saves an object to a disk file.
        /// </summary>
        /// <param name="obj">Saved object.</param>
        /// <param name="f">A writable stream object or a string or Uri object containing a file name.</param>
        public static void save(Dictionary<string, Tensor> obj, Stream f)
        {
            var bw = new BinaryWriter(f);
            bw.Write("TorchLite");
            foreach(var parameter in obj)
            {
                bw.Write(parameter.Key);
                var tensor = parameter.Value;
                bw.Write((byte)tensor.dtype);
                var ndim = tensor.shape.ndim;
                bw.Write(ndim);
                var shape = tensor.shape.data_ptr;
                for(int i = 0; i < ndim; ++i)
                {
                    bw.Write(shape[i]);
                }
                var nbytes = tensor.shape.numel() * tensor.dtype.size();
                var bytes = (byte*)tensor.storage.data_ptr;
                for(int i = 0; i < nbytes; ++i)
                {
                    bw.Write(bytes[i]);
                }
            }
        }

        /// <summary>
        /// Saves an object to a disk file.
        /// </summary>
        /// <param name="obj">Saved object.</param>
        /// <param name="f">A writable stream object or a string or Uri object containing a file name.</param>
        public static void save(Dictionary<string, Tensor> obj, Uri f)
        {
            using(var s = File.Create(f.AbsolutePath))
            {
                torchlite.save(obj, s);
            }
        }

        /// <summary>
        /// Saves an object to a disk file.
        /// </summary>
        /// <param name="obj">Saved object.</param>
        /// <param name="f">A writable stream object or a string or Uri object containing a file name.</param>
        public static void save(Dictionary<string, Tensor> obj, string f)
        {
            using(var s = File.Create(f))
            {
                torchlite.save(obj, s);
            }
        }

    }

}