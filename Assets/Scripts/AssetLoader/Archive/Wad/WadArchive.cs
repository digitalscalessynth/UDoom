using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using AssetLoader.Archive.Wad.Marker;
using AssetLoader.Common;

namespace AssetLoader.Archive.Wad 
{
    public class WadFileException : Exception 
    {
        public WadFileException(string message) : base(message) { }
        public WadFileException(string message, Exception innerException) : base(message, innerException) { }
    }
    
    public class WadArchive : StreamReadable, IArchiveFile<WadHeader, WadLump>
    {
        #region Constants

        private const string IwadMagic = "IWAD";
        private const string PwadMagic = "PWAD";

        #endregion

        private readonly WadHeader _header;
        private readonly WadLump[] _lumps;

        public WadArchive(Stream stream) : base(stream)
        {
            // Check if the stream is bigger than 12 bytes
            if (stream.Length < 12) 
            {
                throw new WadFileException("The stream is too small to be a WAD file.");
            }

            try 
            {
                // Read the header
                byte[] headerBytes = new byte[WadHeader.GetSize()];
                if (stream.Read(headerBytes, 0, WadHeader.GetSize()) < WadHeader.GetSize())
                {
                    throw new WadFileException("The header couldn't be read.");
                }

                _header = WadHeader.LoadFromBytes(headerBytes);

                // Check if the header is valid
                bool isValid = _header.Identification is IwadMagic or PwadMagic;
                if (!isValid)
                {
                    throw new WadFileException($"Invalid WAD file: {_header.Identification} is not a valid WAD magic number.");
                }

                // Check if there are any entries
                bool isEmpty = _header.NumEntries == 0;
                if (isEmpty)
                {
                    throw new WadFileException("The WAD file doesn't contain any entries.");
                }

                // Go to the start of the entries
                stream.Seek(_header.DirectoryOffset, SeekOrigin.Begin);

                // Read the entries
                _lumps = new WadLump[_header.NumEntries];

                string marker = string.Empty;
                for (int i = 0; i < _header.NumEntries; i++)
                {
                    byte[] entryBytes = new byte[WadLump.GetSize()];
                    if(stream.Read(entryBytes, 0, WadLump.GetSize()) < WadLump.GetSize())
                    {
                        throw new WadFileException("An entry couldn't be read.");
                    }

                    WadLump lump = WadLump.LoadFromBytes(entryBytes);

                    // Detect the type of marker, only change name if marker != Other
                    WadMarker markerType = WadMarkerDetector.GetMarkerType(lump);

                    switch (markerType) 
                    {
                        case WadMarker.Map:
                            marker = "MAP";
                            break;
                    }

                    _lumps[i] = lump;
                }
            }         
            catch (IOException ex) 
            {
                throw new IOException("The file couldn't be read", ex);
            }
            catch (Exception ex) 
            {
                throw new WadFileException("An error occurred while reading the WAD file.", ex);
            }
        }

        #region IArchive<WadLump> implementation

        public IEnumerable<WadLump> GetEntries() => _lumps;

        #endregion

        #region IHeaderContainer<WadHeader> implementation

        public WadHeader GetHeader() => _header;

        #endregion
    }
}
