using System;
using System.Collections.Generic;
using System.IO;
using AssetLoader.Common;

namespace AssetLoader.Archive.Grp 
{
    public class GrpFileException : Exception
    {
        public GrpFileException(string message) : base(message) { }
        public GrpFileException(string message, Exception innerException) : base(message, innerException) { }
    }

    /// <summary>
    /// Represents a Build Engine GRP file. A GRP file is a container for various types of assets used by Build Engine games.
    /// </summary>
    public class GrpArchive : StreamReadable, IArchiveFile<GrpHeader, GrpEntry>
    {
        private const string Signature = "KenSilverman";

        private readonly GrpHeader _header;
        private readonly GrpEntry[] _entries;

        public GrpArchive(Stream stream) : base(stream)
        {
            byte[] headerBytes = new byte[GrpHeader.GetSize()];
            if(stream.Read(headerBytes, 0, GrpHeader.GetSize()) < GrpHeader.GetSize())
            {
                throw new GrpFileException("The header couldn't be read.");
            }

            _header = GrpHeader.LoadFromBytes(headerBytes);

            bool isValid = _header.Identification == Signature;
            if (!isValid)
            {
                throw new GrpFileException($"Invalid GRP file: {_header.Identification} is not a valid Build Engine GRP signature.");
            }

            uint directoryOffset = (uint)((_header.NumEntries * GrpEntry.GetSize()) + GrpHeader.GetSize());

            _entries = new GrpEntry[_header.NumEntries];
            _header.SetDirectoryOffset(directoryOffset);  // Set the directory offset to the current position in the stream

            for (int i = 0; i < _header.NumEntries; i++)
            {
                byte[] entryBytes = new byte[GrpEntry.GetSize()];
                if(stream.Read(entryBytes, 0, GrpEntry.GetSize()) < GrpEntry.GetSize())
                {
                    throw new GrpFileException("An entry couldn't be read.");
                }

                GrpEntry entry = GrpEntry.LoadFromBytes(entryBytes);
                entry.SetOffset(directoryOffset);  // Set the offset to the current position in the stream

                _entries[i] = entry;

                directoryOffset += entry.Size;
            }
        }

        #region IArchive<GrpEntry> Implementation

        public IEnumerable<GrpEntry> GetEntries() => _entries;

        #endregion

        #region IHeaderContainer<GrpHeader> Implementation

        public GrpHeader GetHeader() => _header;

        #endregion
    }
}
