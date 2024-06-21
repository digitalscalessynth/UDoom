
namespace AssetLoader.Common  
{
    /// <summary>
    /// Represents the header of an archive file.
    /// </summary>
    public abstract class Header 
    {
        public string Identification { get; protected set; }
        public uint NumEntries { get; protected set; }
        public uint DirectoryOffset { get; protected set; }

        protected Header(string identification, uint numEntries, uint directoryOffset)
        {
            Identification = identification;
            NumEntries = numEntries;
            DirectoryOffset = directoryOffset;
        }
    }
}
