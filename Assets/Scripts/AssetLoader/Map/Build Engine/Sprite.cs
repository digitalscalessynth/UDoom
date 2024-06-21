using System;
using AssetLoader.Archive;
using AssetLoader.Common;
using AssetLoader.File;

namespace AssetLoader.Map 
{
    public class Sprite : IByteReadable<Sprite> 
    {
        private const int SpriteSize = 44;

        public readonly int X;
        public readonly int Y;
        public readonly int Z;
        public readonly short Cstat;
        public readonly short Picnum;
        public readonly sbyte Shade;
        public readonly byte Pal;
        public readonly byte Clipdist;
        public readonly byte Xrepeat;
        public readonly byte Yrepeat;
        public readonly byte Xoffset;
        public readonly byte Yoffset;
        public readonly short SectorNum;
        public readonly short Statnum;
        public readonly short Ang;
        public readonly short Owner;
        public readonly short Xvel;
        public readonly short Yvel;
        public readonly short Zvel;
        public readonly short Lotag;
        public readonly short Hitag;
        public readonly short Extra;

        public Sprite(int x, int y, int z, short cstat, short picnum, sbyte shade, byte pal, byte clipdist, byte xrepeat, byte yrepeat, byte xoffset, byte yoffset, short sectorNum, short statnum, short ang, short owner, short xvel, short yvel, short zvel, short lotag, short hitag, short extra) 
        {
            X = x;
            Y = y;
            Z = z;
            Cstat = cstat;
            Picnum = picnum;
            Shade = shade;
            Pal = pal;
            Clipdist = clipdist;
            Xrepeat = xrepeat;
            Yrepeat = yrepeat;
            Xoffset = xoffset;
            Yoffset = yoffset;
            SectorNum = sectorNum;
            Statnum = statnum;
            Ang = ang;
            Owner = owner;
            Xvel = xvel;
            Yvel = yvel;
            Zvel = zvel;
            Lotag = lotag;
            Hitag = hitag;
            Extra = extra;
        }

        #region IByteReadable<T> Implementation

        public static Sprite LoadFromBytes(byte[] bytes) 
        {
            int x = BitConverter.ToInt32(bytes, 0);
            int y = BitConverter.ToInt32(bytes, 4);
            int z = -BitConverter.ToInt32(bytes, 8) >> 4;
            short cstat = BitConverter.ToInt16(bytes, 12);
            short picnum = BitConverter.ToInt16(bytes, 14);
            sbyte shade = (sbyte)bytes[16];
            byte pal = bytes[17];
            byte clipdist = bytes[18];

            // Filler bytes[19]

            byte xrepeat = bytes[20];
            byte yrepeat = bytes[21];
            byte xoffset = bytes[22];
            byte yoffset = bytes[23];
            short sectorNum = BitConverter.ToInt16(bytes, 24);
            short statnum = BitConverter.ToInt16(bytes, 26);
            short ang = BitConverter.ToInt16(bytes, 28);
            short owner = BitConverter.ToInt16(bytes, 30);
            short xvel = BitConverter.ToInt16(bytes, 32);
            short yvel = BitConverter.ToInt16(bytes, 34);
            short zvel = BitConverter.ToInt16(bytes, 36);
            short lotag = BitConverter.ToInt16(bytes, 38);
            short hitag = BitConverter.ToInt16(bytes, 40);
            short extra = BitConverter.ToInt16(bytes, 42);

            return new Sprite(x, y, z, cstat, picnum, shade, pal, clipdist, xrepeat, yrepeat, xoffset, yoffset, sectorNum, statnum, ang, owner, xvel, yvel, zvel, lotag, hitag, extra);
        }

        public static int GetSize() => SpriteSize;
        
        #endregion
    }
}
