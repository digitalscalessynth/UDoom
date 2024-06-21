using System;
using System.IO;

namespace AssetLoader.Common 
{
    /// <summary>
    /// Base class for all file types that require a stream for reading its contents.
    public abstract class StreamReadable
    {
        public StreamReadable(Stream stream) 
        {
            // Check if the stream is null
            if (stream == null) 
            {
                throw new ArgumentNullException(nameof(stream));
            }

            // Check if the stream is readable
            if (!stream.CanRead) 
            {
                throw new ArgumentException("The stream is not readable.", nameof(stream));
            }

            // Stream in the original position
            if (!stream.CanSeek) 
            {
                throw new ArgumentException("The stream is not seekable.", nameof(stream));
            }

            // Stream at the start
            stream.Seek(0, SeekOrigin.Begin);

            // Continue with the implementation in concrete classes
        }
    }
}
