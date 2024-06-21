using System;
using AssetLoader.Archive;
using AssetLoader.Common;
using AssetLoader.File;

namespace AssetLoader.Map 
{
    public class Wall : IByteReadable<Wall>
    {
        private const int WallSize = 32;

        public readonly int X;
        public readonly int Y;
        public readonly short Point2;
        public readonly short NextWall;
        public readonly short NextSector;
        public readonly short Cstat;
        public readonly short Picnum;
        public readonly short OverPicnum;
        public readonly sbyte Shade;
        public readonly byte Pal;
        public readonly byte Xrepeat;
        public readonly byte Yrepeat;
        public readonly byte Xpanning;
        public readonly byte Ypanning;
        public readonly short Lotag;
        public readonly short Hitag;
        public readonly short Extra;

        public Wall(int x, int y, short point2, short nextWall, short nextSector, short cstat, short picnum, short overPicnum, sbyte shade, byte pal, byte xrepeat, byte yrepeat, byte xpanning, byte ypanning, short lotag, short hitag, short extra)
        {
            X = x;
            Y = y;
            Point2 = point2;
            NextWall = nextWall;
            NextSector = nextSector;
            Cstat = cstat;
            Picnum = picnum;
            OverPicnum = overPicnum;
            Shade = shade;
            Pal = pal;
            Xrepeat = xrepeat;
            Yrepeat = yrepeat;
            Xpanning = xpanning;
            Ypanning = ypanning;
            Lotag = lotag;
            Hitag = hitag;
            Extra = extra;
        }

        #region IByteReadable<T> Implementation

        public static Wall LoadFromBytes(byte[] bytes) 
        {
            int x = BitConverter.ToInt32(bytes, 0);
            int y = BitConverter.ToInt32(bytes, 4);
            short point2 = BitConverter.ToInt16(bytes, 8);
            short nextWall = BitConverter.ToInt16(bytes, 10);
            short nextSector = BitConverter.ToInt16(bytes, 12);
            short cstat = BitConverter.ToInt16(bytes, 14);
            short picnum = BitConverter.ToInt16(bytes, 16);
            short overPicnum = BitConverter.ToInt16(bytes, 18);
            sbyte shade = (sbyte)bytes[20];
            byte pal = bytes[21];
            byte xrepeat = bytes[22];
            byte yrepeat = bytes[23];
            byte xpanning = bytes[24];
            byte ypanning = bytes[25];
            short lotag = BitConverter.ToInt16(bytes, 26);
            short hitag = BitConverter.ToInt16(bytes, 28);
            short extra = BitConverter.ToInt16(bytes, 30);

            return new Wall(x, y, point2, nextWall, nextSector, cstat, picnum, overPicnum, shade, pal, xrepeat, yrepeat, xpanning, ypanning, lotag, hitag, extra);
        }

        public static int GetSize() => WallSize;

        #endregion
    }
}
