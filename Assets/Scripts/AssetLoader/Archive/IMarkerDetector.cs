using System.Collections;
using System.Collections.Generic;
using AssetLoader.Common;
using UnityEngine;

namespace AssetLoader.Archive 
{
    public interface IMarkerDetector<T> where T : Entry
    {
        public static int GetMarkerType(T entry) 
        {
            throw new System.NotImplementedException();
        }
    }
}
