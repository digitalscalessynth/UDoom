using System;
using AssetLoader.Archive;
using AssetLoader.Common;
using AssetLoader.File;

namespace AssetLoader.Map 
{
    public class Sector : IByteReadable<Sector>
    {
        private const int SectorSize = 40;

        public readonly short WallPtr;
        public readonly short WallCount;
        public readonly int CeilingZ;
        public readonly int FloorZ;
        public readonly short CeilingStat;
        public readonly short FloorStat;
        public readonly short CeilingPicNum;
        public readonly short CeilingHeinum;
        public readonly sbyte CeilingShade;
        public readonly byte CeilingPal;
        public readonly byte CeilingXpanning;
        public readonly byte CeilingYpanning;
        public readonly short FloorPicNum;
        public readonly short FloorHeinum;
        public readonly sbyte FloorShade;
        public readonly byte FloorPal;
        public readonly byte FloorXpanning;
        public readonly byte FloorYpanning;
        public readonly byte Visibility;
        public readonly short LoTag;
        public readonly short HiTag;
        public readonly short Extra;

        public Sector(short wallPtr, short wallCount, int ceilingZ, int floorZ, short ceilingStat, short floorStat, short ceilingPicNum, short ceilingHeinum, sbyte ceilingShade, byte ceilingPal, byte ceilingXpanning, byte ceilingYpanning, short floorPicNum, short floorHeinum, sbyte floorShade, byte floorPal, byte floorXpanning, byte floorYpanning, byte visibility, short loTag, short hiTag, short extra)
        {
            WallPtr = wallPtr;
            WallCount = wallCount;
            CeilingZ = ceilingZ;
            FloorZ = floorZ;
            CeilingStat = ceilingStat;
            FloorStat = floorStat;
            CeilingPicNum = ceilingPicNum;
            CeilingHeinum = ceilingHeinum;
            CeilingShade = ceilingShade;
            CeilingPal = ceilingPal;
            CeilingXpanning = ceilingXpanning;
            CeilingYpanning = ceilingYpanning;
            FloorPicNum = floorPicNum;
            FloorHeinum = floorHeinum;
            FloorShade = floorShade;
            FloorPal = floorPal;
            FloorXpanning = floorXpanning;
            FloorYpanning = floorYpanning;
            Visibility = visibility;
            LoTag = loTag;
            HiTag = hiTag;
            Extra = extra;
        }

        #region IBinaryReadable<T> implementation

        public static Sector LoadFromBytes(byte[] bytes) 
        {
            short wallPtr = BitConverter.ToInt16(bytes, 0);
            short wallCount = BitConverter.ToInt16(bytes, 2);
            int ceilingZ = -BitConverter.ToInt32(bytes, 4) << 4;    // All Z values are inverted and shifted by 4
            int floorZ = -BitConverter.ToInt32(bytes, 8) << 4;  // All Z values are inverted and shifted by 4
            short ceilingStat = BitConverter.ToInt16(bytes, 12);
            short floorStat = BitConverter.ToInt16(bytes, 14);
            short ceilingPicNum = BitConverter.ToInt16(bytes, 16);
            short ceilingHeinum = BitConverter.ToInt16(bytes, 18);
            sbyte ceilingShade = (sbyte)bytes[20];
            byte ceilingPal = bytes[21];
            byte ceilingXpanning = bytes[22];
            byte ceilingYpanning = bytes[23];
            short floorPicNum = BitConverter.ToInt16(bytes, 24);
            short floorHeinum = BitConverter.ToInt16(bytes, 26);
            sbyte floorShade = (sbyte)bytes[28];
            byte floorPal = bytes[29];
            byte floorXpanning = bytes[30];
            byte floorYpanning = bytes[31];
            byte visibility = bytes[32];

            // Filler bytes[33]

            short loTag = BitConverter.ToInt16(bytes, 34);
            short hiTag = BitConverter.ToInt16(bytes, 36);
            short extra = BitConverter.ToInt16(bytes, 38);

            return new Sector(wallPtr, wallCount, ceilingZ, floorZ, ceilingStat, floorStat, ceilingPicNum, ceilingHeinum, ceilingShade, ceilingPal, ceilingXpanning, ceilingYpanning, floorPicNum, floorHeinum, floorShade, floorPal, floorXpanning, floorYpanning, visibility, loTag, hiTag, extra);
        }

        public static int GetSize() => SectorSize;

        #endregion
    }
}
