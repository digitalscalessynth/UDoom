using System;
using System.Collections;
using System.Collections.Generic;
using AssetLoader.Archive;
using AssetLoader.Archive.Grp;
using UnityEngine;

namespace AssetLoader.File.BuildEngine.Art 
{
    public class Palette : IByteReadable<Palette> 
    {
        public readonly byte[][] Colors;    
        public readonly short NumPalettes;
        public readonly byte[][] PaletteData;
        public readonly byte[] TranslucentData;

        public Palette(byte[][] colors, short numPalettes, byte[][] paletteData, byte[] translucentData) 
        {
            Colors = colors;
            NumPalettes = numPalettes;
            PaletteData = paletteData;
            TranslucentData = translucentData;
        } 

        #region IByteReadable<T> implementation

        public static Palette LoadFromBytes(byte[] bytes) 
        {
            byte[][] colors = new byte[256][];
            for (int i = 0; i < 256; i++) 
            {
                // Extract 6-bit RGB values to 8-bit
                byte r = (byte)((bytes[i * 3] & 0x3F) << 2);
                byte g = (byte)((bytes[i * 3 + 1] & 0x3F) << 2);
                byte b = (byte)((bytes[i * 3 + 2] & 0x3F) << 2);

                colors[i] = new byte[3] { r, g, b };
            }

            short numPalettes = BitConverter.ToInt16(bytes, 768);
            byte[][] paletteData = new byte[numPalettes][];
            for (int i = 0; i < numPalettes; i++) 
            {
                int pointer = 770 + i * 256;
                paletteData[i] = new byte[256];
                Array.Copy(bytes, pointer, paletteData[i], 0, 256);
            }

            return new Palette(colors, numPalettes, paletteData, null);
        }

        #endregion

        #region LookForPaletteData

        public static bool FindPaletteData(GrpEntry grpEntry) 
        {
            return grpEntry.Name == "PALETTE.DAT";
        }

        #endregion
    }
}
