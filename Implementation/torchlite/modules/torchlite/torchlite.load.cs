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
        /// Loads an object saved with torch.save() from a file.
        /// </summary>
        /// <param name="f">A readable stream object or a string or Uri object containing a file name.</param>
        /// <returns>Dictionary&lr;string, Tensor&gt;.</returns>
        public static Dictionary<string, Tensor> load(Stream f)
        {
            var br = new BinaryReader(f);
            var state_dict = new Dictionary<string, Tensor>();
            try
            {
                if(br.ReadString() != "TorchLite")
                {
                    throw new Exception();
                }
                while((br.BaseStream.Position + 1) < br.BaseStream.Length)
                {
                    var name = br.ReadString();
                    var dtype = (DType)br.ReadByte();
                    var shape = new int[br.ReadInt32()];
                    for(int i = 0; i < shape.Length; ++i)
                    {
                        shape[i] = br.ReadInt32();
                    }
                    var tensor = new Tensor(shape, dtype);
                    var nbytes = tensor.shape.numel() * dtype.size();
                    var bytes = (byte*)tensor.storage.data_ptr;
                    for(int i = 0; i < nbytes; ++i)
                    {
                        bytes[i] = br.ReadByte();
                    }
                    state_dict.Add(name, tensor);
                }
            }
            catch(Exception e)
            {
                throw new IOException("f is not valid TorchLite file.");
            }
            return state_dict;
        }

        /// <summary>
        /// Loads an object saved with torch.save() from a file.
        /// </summary>
        /// <param name="f">A readable stream object or a string or Uri object containing a file name.</param>
        /// <returns>Dictionary&lr;string, Tensor&gt;.</returns>
        public static Dictionary<string, Tensor> load(Uri f)
        {
            Dictionary<string, Tensor> state_dict;
            using(var s = File.OpenRead(f.AbsolutePath))
            {
                state_dict = torchlite.load(s);
            }
            return state_dict;
        }

        /// <summary>
        /// Loads an object saved with torch.save() from a file.
        /// </summary>
        /// <param name="f">A readable stream object or a string or Uri object containing a file name.</param>
        /// <returns>Dictionary&lr;string, Tensor&gt;.</returns>
        public static Dictionary<string, Tensor> load(string f)
        {
            Dictionary<string, Tensor> state_dict;
            using(var s = File.OpenRead(f))
            {
                state_dict = torchlite.load(s);
            }
            return state_dict;
        }

    }

}