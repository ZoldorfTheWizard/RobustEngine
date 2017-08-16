using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.Remoting.Messaging;
using glTFLoader.Schema;
using Newtonsoft.Json;

namespace RobustEngine.System.AssetLoading.TypeLoaders
{
    class GltfTypeLoader : ITypeLoader
    {
        const uint GLTF = 0x46546C67;
        const uint JSON = 0x4E4F534A;

        //This shit is complicated and 90% ripped off from the loader interface to take a stream instead of a path
        public object Load(Stream stream)
        {
            bool binaryFile = false;

            using (BinaryReader binaryReader = new BinaryReader(stream))
            {
                uint magic = binaryReader.ReadUInt32();
                if (magic == GLTF)
                {
                    binaryFile = true;
                }
            }

            string fileData;
            if(binaryFile)
            {
                fileData = parseBinary(stream);
            } else
            {
                fileData = parseTextStream(stream);
            }
            return JsonConvert.DeserializeObject<Gltf>(fileData);
        }

        private string parseTextStream(Stream stream)
        {
            MemoryStream ms = new MemoryStream();
            stream.CopyTo(ms);
            var byteArray = ms.ToArray();
            return Encoding.UTF8.GetString(byteArray);
        }

        private string parseBinary(Stream stream)
        {
            using (BinaryReader binaryReader = new BinaryReader(stream))
            {
                uint magic = binaryReader.ReadUInt32();
                if (magic != GLTF)
                {
                    throw new InvalidDataException($"Unexpected magic number: {magic}");
                }

                uint version = binaryReader.ReadUInt32();
                if (version != 2)
                {
                    throw new NotImplementedException($"Unkown Version Number: {version}");
                }

                uint length = binaryReader.ReadUInt32();
                var fs = (FileStream)stream;
                long fileLength = new FileInfo(fs.Name).Length;
                if(length != fileLength)
                {
                    throw new InvalidDataException($"The specified length of the file {length} is not equal to the length of the file {fileLength}");
                }

                uint chunkLength = binaryReader.ReadUInt32();
                uint chunkFormat = binaryReader.ReadUInt32();
                if(chunkFormat != JSON)
                {
                    throw new NotImplementedException($"The first chunk must be format 'JSON': {chunkFormat}");
                }

                return Encoding.UTF8.GetString(binaryReader.ReadBytes((int)chunkLength));
            }
        }
    }
}
