using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AudioData", menuName = "DivoPOC/AudioData")]
public class AudioData : ScriptableObject
{
    [Serializable]
    public struct AudioEntry
    {
        public string clipName;
        public AudioClip clip;
    }

    public List<AudioEntry> audioEntries = new List<AudioEntry>();
}
