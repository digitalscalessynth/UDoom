using System;
using AssetLoader.Common;
using AssetLoader.File;

namespace AssetLoader.Archive.Grp
{
    public class GrpEntry : Entry, IByteReadable<GrpEntry>
    {
        private const int FileEntrySize = 16;

        public GrpEntry(string name, uint size) : base(name, 0, size) {}

        public void SetOffset(uint offset) => Offset = offset;

        #region Implementation of IByteReadable<T>

        public static GrpEntry LoadFromBytes(byte[] bytes)
        {
            string fileName = System.Text.Encoding.ASCII.GetString(bytes, 0, 12).TrimEnd('\0');
            uint fileSize = BitConverter.ToUInt32(bytes, 12);

            return new GrpEntry(fileName, fileSize);
        }

        public static int GetSize() => FileEntrySize;

        #endregion

    }
}
