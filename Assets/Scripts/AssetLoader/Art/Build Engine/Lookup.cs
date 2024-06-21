using System.Collections;
using System.Collections.Generic;
using AssetLoader.Archive;
using AssetLoader.Archive.Grp;
using UnityEngine;

namespace AssetLoader.File.BuildEngine.Art 
{
    public class Lookup : IByteReadable<Palette> 
    {
        #region LookForPaletteData

        public static bool FindTableData(GrpEntry grpEntry) 
        {
            return grpEntry.Name == "LOOKUP.DAT";
        }

        #endregion
    }
}
