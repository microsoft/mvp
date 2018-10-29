using System;
using System.Net.Http;
using Newtonsoft.Json;
using System.IO.Compression;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using System.Net.Http.Headers;
using System.Text;

namespace Microsoft.Mvp.Helpers
{
    public class JsonContent : HttpContent
    {
        const string gzipEncoding = "gzip";
        
        static readonly MediaTypeHeaderValue jsonMediaTypeHeaderValue = new MediaTypeHeaderValue("application/json");
        static readonly UTF8Encoding _encoding = new UTF8Encoding(false);
        
        readonly JsonSerializer _serializer;
        readonly object _value;
        readonly bool _isGzip;
        readonly int _bufferSize;

        public JsonContent(object value, bool isGzip, int bufferSize, JsonSerializer serializer = null)
        {
            _value = value;
            
            _serializer = serializer ?? new JsonSerializer();
            
            _isGzip = isGzip;

            _bufferSize = bufferSize;
            
            Headers.ContentType = jsonMediaTypeHeaderValue;
            
            if(isGzip)
                Headers.ContentEncoding.Add(gzipEncoding);
        }

        protected override bool TryComputeLength(out long length)
        {
            length = -1;
            return false;
        }

        protected override Task SerializeToStreamAsync(Stream stream, TransportContext context)
        {
            return Task.Run(
                () =>
                {
                    if(_isGzip)
                    {
                        using (var gzip = new GZipStream(stream, CompressionLevel.Fastest, true))
                        using (var sw = new StreamWriter(gzip, _encoding, _bufferSize, true))
                        using (var jtw = new JsonTextWriter(sw) { Formatting = Formatting.None })
                        {
                            _serializer.Serialize(jtw, _value);
                            jtw.Flush();
                        }
                    }
                    else
                    {
                        using (var sw = new StreamWriter(stream, _encoding, _bufferSize, true))
                        using (var jtw = new JsonTextWriter(sw) { Formatting = Formatting.None })
                        {
                            _serializer.Serialize(jtw, _value);
                            jtw.Flush();
                        }
                    }
                });
        }
    }
}
