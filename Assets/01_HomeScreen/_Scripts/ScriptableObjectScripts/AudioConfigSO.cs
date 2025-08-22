using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AudioConfig", menuName = "DivoPOC/AudioConfig")]
[SerializeField]
public class AudioConfigSO : ScriptableObject
{
     public List<AudioData> audioDatas;
}
