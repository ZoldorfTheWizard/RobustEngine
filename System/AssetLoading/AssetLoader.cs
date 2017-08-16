using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RobustEngine.System.AssetLoading
{
    //stream providers are anything that can provide a data stream.  I only have hard drive file reading for now but this potentially could be streamed from the internet
    interface IStreamProvider
    {
        Stream GetStream(string type, string name);
    }

    //type loaders convert the raw data stream to actual usable objects, with one per type of loaded asset
    interface ITypeLoader
    {
        object Load(Stream stream);
    }

    class AssetLoader
    {
        IStreamProvider provider;
        Dictionary<string, ITypeLoader> loaders = new Dictionary<string, ITypeLoader>();

        public AssetLoader(IStreamProvider streamProvider)
        {
            provider = streamProvider;
        }

        object LoadAsset(string type, string name)
        {
            var loader = loaders[type];
            var stream = provider.GetStream(type, name);

            return loader.Load(stream);
        }

        public void RegisterType(string type, ITypeLoader loader)
        {
            loaders[type] = loader;
        } 

        //this means the default "missing asset" sprites, models, sound effects, etc. all need to be in their respective type folder named 'missing.<filetype>'
        public static string GetMissingAssetDefault(string type)
        {
            return $"..\\..\\Content\\Assets\\{type}\\missing.*";
        }
    }
}
