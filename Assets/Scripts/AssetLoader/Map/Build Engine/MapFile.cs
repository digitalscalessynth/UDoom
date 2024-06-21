using System;
using System.IO;
using AssetLoader.Common;

namespace AssetLoader.Map
{
    public class BuildEngineExpetion : Exception
    {
        public BuildEngineExpetion(string message) : base(message) { }
        public BuildEngineExpetion(string message, Exception innerException) : base(message, innerException) { }
    }

    public class MapFile : BufferReadable
    {
        public readonly int Version;
        public readonly int X;
        public readonly int Y;
        public readonly int Z;
        public readonly short Angle;
        public readonly short CurrentSectorIndex;
        public readonly ushort SectorCount;
        public readonly Sector[] Sectors;
        public readonly ushort WallCount;
        public readonly Wall[] Walls;
        public readonly ushort SpriteCount;
        public readonly Sprite[] Sprites;

        public MapFile(byte[] buffer) : base(buffer)
        {
            try
            {
                // Read the header
                Version = BitConverter.ToInt32(buffer, 0);

                // Check if the header is valid
                bool isValid = Version >= 7;
                if (!isValid)
                {
                    throw new BuildEngineExpetion($"Invalid BuildEngine map file: {Version} is not a valid BuildEngine version.");
                }

                X = BitConverter.ToInt32(buffer, 4);
                Y = BitConverter.ToInt32(buffer, 8);
                Z = -BitConverter.ToInt32(buffer, 12) << 4;   // All Z values are inverted and shifted by 4

                Angle = BitConverter.ToInt16(buffer, 16);
                CurrentSectorIndex = BitConverter.ToInt16(buffer, 18);

                SectorCount = BitConverter.ToUInt16(buffer, 20);

                // Check if there are any sectors
                if (SectorCount == 0)
                {
                    throw new BuildEngineExpetion("The BuildEngine map file doesn't contain any sectors.");
                }

               // Byte pointer
                int bytePointer = 22;

                // Read the sectors
                Sectors = new Sector[SectorCount];
                for(int i = 0; i < SectorCount; i++)
                {
                    byte[] bytes = new byte[Sector.GetSize()];
                    Array.Copy(buffer, bytePointer, bytes, 0, Sector.GetSize());

                    Sectors[i] = Sector.LoadFromBytes(bytes);

                    // Advance the byte pointer
                    bytePointer += Sector.GetSize();
                }

                WallCount = BitConverter.ToUInt16(buffer, bytePointer);
                bytePointer += 2;

                // Check if there are any walls
                if (WallCount == 0)
                {
                    throw new BuildEngineExpetion("The BuildEngine map file doesn't contain any walls.");
                }

                // Read the walls
                Walls = new Wall[WallCount];
                for(int i = 0; i < WallCount; i++)
                {
                    byte[] bytes = new byte[Wall.GetSize()];
                    Array.Copy(buffer, bytePointer, bytes, 0, Wall.GetSize());

                    Walls[i] = Wall.LoadFromBytes(bytes);

                    // Advance the byte pointer
                    bytePointer += Wall.GetSize();
                }

                // Read the sprites
                SpriteCount = BitConverter.ToUInt16(buffer, bytePointer);
                bytePointer += 2;

                // Check if there are any sprites
                if (SpriteCount == 0)
                {
                    throw new BuildEngineExpetion("The BuildEngine map file doesn't contain any sprites.");
                }

                Sprites = new Sprite[SpriteCount];
                for(int i = 0; i < SpriteCount; i++)
                {
                    byte[] bytes = new byte[Sprite.GetSize()];
                    Array.Copy(buffer, bytePointer, bytes, 0, Sprite.GetSize());

                    Sprites[i] = Sprite.LoadFromBytes(bytes);

                    // Advance the byte pointer
                    bytePointer += Sprite.GetSize();
                }

            }
            catch (IOException ex)
            {
                throw new IOException("The file couldn't be read", ex);
            }
            catch (Exception ex)
            {
                throw new BuildEngineExpetion("The BuildEngine map file couldn't be read", ex);
            }
        }
    }
}
