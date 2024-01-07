using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SoundFile", menuName = "Scriptable Object/SoundFile", order = -1)]
public class SoundFile : ScriptableObject
{
    public DataType voiceType;
    public List<AudioClip> datas = new List<AudioClip>();
}
