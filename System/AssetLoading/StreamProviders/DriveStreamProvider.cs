using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace RobustEngine.System.AssetLoading.StreamProviders
{
    class DriveStreamProvider : IStreamProvider
    {
        public Stream GetStream(string type, string key)
        {
            string targetFileDir = "";
            Stream outStream;
            var files = Directory.GetFiles($"..\\..\\..\\Content\\Assets\\{type}", $"{key}.*", SearchOption.AllDirectories);
            //If the resulting length of files array is 1, that means that it found the file it wanted
            if (files.Length == 1)
            {
                targetFileDir = files[0];
            } else if (files.Length == 0) //If it's 0, it didn't, so get the missing asset
            {
                targetFileDir = AssetLoader.GetMissingAssetDefault(type);
            } else //If something else happened, it's fucked
            {
                //error here?
            }
            try
            {
                outStream = File.OpenRead(targetFileDir);
            } catch(FileNotFoundException e)
            {
                outStream = File.OpenRead(AssetLoader.GetMissingAssetDefault(type));;
            }
            return outStream;
        }
    }
}
