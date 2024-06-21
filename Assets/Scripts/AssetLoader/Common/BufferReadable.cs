using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AssetLoader.Common 
{
    public abstract class BufferReadable 
    {
        public BufferReadable(byte[] buffer) 
        {
            // Check if the buffer is null
            if (buffer == null) 
            {
                throw new System.ArgumentNullException(nameof(buffer));
            }

            // Check if the buffer is empty
            if (buffer.Length == 0) 
            {
                throw new System.ArgumentException("The buffer is empty.", nameof(buffer));
            }

            // Continue with the implementation in concrete classes
        }
    }
}
