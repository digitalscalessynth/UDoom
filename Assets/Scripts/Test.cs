using System.IO;
using UnityEngine;
using AssetLoader.Archive.Wad;

public class Test : MonoBehaviour
{
    public string wadPath = "D:/Unity/UDoom/Resources/Wad/DOOM2.WAD";
    
    private void Start()
    {
        using FileStream fileStream = new FileStream(wadPath, FileMode.Open, FileAccess.Read);
        WadArchive wadArchive = new WadArchive(fileStream);
        
        foreach(WadLump entry in wadArchive.GetEntries())
        {
            Debug.Log(entry.Name);
        }
    }
}
