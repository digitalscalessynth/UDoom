using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

namespace AssetLoader.Archive.Wad.Marker 
{
    // Enum to represent the different types of markers inside a WAD file
    public enum WadMarker
    {
        Map, 
        Patch,
        Flat,
        Sprite,
        Texture,    // Used in console ports
        Dialoge,
        Music,
        Sound,
        Zscript,
        Other
    }
    
    public class WadMarkerDetector : IMarkerDetector<WadLump>
    {
        #region Marker Detection

        // Map
        private static Regex _mapRegex = new Regex
        (
            @"^E\dM\d$ | ^MAP\d\d$ | THINGS | LINEDEFS | SIDEDEFS | VERTEXES | SEGS | SSECTORS | NODES | SECTORS | REJECT | BLOCKMAP | BEHAVIOR | SCRIPTS | TEXTMAP | ZNODES | ZSCRIPTS", 
            RegexOptions.Compiled | RegexOptions.IgnoreCase
        );
        
        // Patch
        private static Regex _patchRegex = new Regex(@"^P\d\d$", RegexOptions.Compiled | RegexOptions.IgnoreCase);

        // Flat
        private static Regex _flatRegex = new Regex(@"^F\d\d\d\d$", RegexOptions.Compiled | RegexOptions.IgnoreCase);

        // Sprite
        private static Regex _spriteRegex = new Regex(@"^S\d\d\d\d$", RegexOptions.Compiled | RegexOptions.IgnoreCase);

        // Texture
        private static Regex _textureRegex = new Regex(@"^T\d\d\d\d$", RegexOptions.Compiled | RegexOptions.IgnoreCase);

        // Dialogue
        private static Regex _dialogueRegex = new Regex(@"^SCRIPT\d\d$", RegexOptions.Compiled | RegexOptions.IgnoreCase);

        // Sound
        private static Regex _soundRegex = new Regex(@"^D\d\d\d\d$", RegexOptions.Compiled | RegexOptions.IgnoreCase);

        // Music
        private static Regex _musicRegex = new Regex(@"^D_.*$", RegexOptions.Compiled | RegexOptions.IgnoreCase);

        #endregion

        public static WadMarker GetMarkerType(WadLump lump)
        {
            return lump.Name switch
            {
                var name when _mapRegex.IsMatch(name) => WadMarker.Map,
                var name when _patchRegex.IsMatch(name) => WadMarker.Patch,
                var name when _flatRegex.IsMatch(name) => WadMarker.Flat,
                var name when _spriteRegex.IsMatch(name) => WadMarker.Sprite,
                var name when _textureRegex.IsMatch(name) => WadMarker.Texture,
                var name when _dialogueRegex.IsMatch(name) => WadMarker.Dialoge,
                var name when _soundRegex.IsMatch(name) => WadMarker.Sound,
                var name when _musicRegex.IsMatch(name) => WadMarker.Music,
                _ => WadMarker.Other,
            };
        }
    }
}
