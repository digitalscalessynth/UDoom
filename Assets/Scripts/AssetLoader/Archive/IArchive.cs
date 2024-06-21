using System.Collections.Generic;
using AssetLoader.Common;

namespace AssetLoader.Archive
{
    /// <summary>
    /// Interface for archive files, files that contain multiple entries. Such as WAD, PK3, GRP, etc.
    /// </summary>
    public interface IArchiveFile<out THeader, out TEntry> where THeader : Header where TEntry : Entry
    {
        THeader GetHeader();
        IEnumerable<TEntry> GetEntries();
    }
}
