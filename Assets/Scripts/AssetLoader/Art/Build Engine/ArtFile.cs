using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using AssetLoader.Archive.Grp;
using AssetLoader.Common;

namespace AssetLoader.Art 
{
    public class ArtFileException : System.Exception 
    {
        public ArtFileException(string message) : base(message) { }
        public ArtFileException(string message, System.Exception innerException) : base(message, innerException) { }
    }

    public class ArtFile : BufferReadable
    {
        public readonly int Version;
        public readonly int NumTiles;
        public readonly int TileStart;
        public readonly int TileEnd;
        public readonly short[] TileXOffsets;
        public readonly short[] TileYOffsets;
        public readonly int[] TileAttributes;

        public ArtFile(byte[] buffer) : base(buffer)
        {
            try 
            {
                Version = BitConverter.ToInt32(buffer, 0);
                
                // Check if the header is valid
                bool isValid = Version == 1;
                if (!isValid) 
                {
                    throw new ArtFileException($"Invalid BuildEngine art file: {Version} is not a valid BuildEngine version.");
                }

                NumTiles = BitConverter.ToInt32(buffer, 4);
                TileStart = BitConverter.ToInt32(buffer, 8);
                TileEnd = BitConverter.ToInt32(buffer, 12);

                TileXOffsets = new short[TileEnd - TileStart + 1];
                for (int i = 0; i < TileXOffsets.Length; i++) 
                {
                    TileXOffsets[i] = BitConverter.ToInt16(buffer, 16 + i * 2);
                } 

                TileYOffsets = new short[TileEnd - TileStart + 1];
                for (int i = 0; i < TileYOffsets.Length; i++) 
                {
                    TileYOffsets[i] = BitConverter.ToInt16(buffer, 16 + TileXOffsets.Length * 2 + i * 2);
                }

                TileAttributes = new int[TileEnd - TileStart + 1];
                for (int i = 0; i < TileAttributes.Length; i++) 
                {
                    TileAttributes[i] = BitConverter.ToInt32(buffer, 16 + TileXOffsets.Length * 2 + TileYOffsets.Length * 2 + i * 4);
                }
            }
            catch (IOException ex)
            {
                throw new IOException("The file couldn't be read", ex);
            }
            catch (Exception ex)
            {
                throw new ArtFileException("The BuildEngine map file couldn't be read", ex);
            }
        }

        #region Find Art Data

        public static IEnumerable<GrpEntry> FindArtData(GrpArchive grpArchive) 
        {
            return grpArchive.GetEntries().Where(entry => entry.Name.EndsWith(".ART"));
        }

        #endregion
    }
}
