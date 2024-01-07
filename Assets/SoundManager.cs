using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    AudioSource voice;

    [SerializeField] List<SoundFile> SoundFiles = new List<SoundFile>();

    Dictionary<string, AudioClip> clips = new Dictionary<string, AudioClip>();

    private void Awake()
    {
        Init();
    }
    public void Init()
    {
        GameObject go = new GameObject("voice");
        voice = go.AddComponent<AudioSource>();
        voice.spatialBlend = 0f;
        voice.loop = false;


        foreach (var item in SoundFiles)
        {
            foreach (var clip in item.datas)
            {
                clips.Add(item.voiceType + clip.name.Substring(0,2), clip);
            }
        }
    }
    public void Play(AudioClip clip)
    {
        if (clip == null) return;

        if (voice.isPlaying)
            voice.Stop();

        voice.pitch = 1.0f;
        voice.clip = clip;
        voice.Play();
    }
    public void Play(string name,DataType voiceType)
    {
        AudioClip clip = GetOrAddAudioClip(voiceType+name);

        Play(clip);
    }


    AudioClip GetOrAddAudioClip(string name)
    {
        AudioClip clip = null;
        clips.TryGetValue(name, out clip);

        if (clip == null)
        {
            Debug.LogError($"Clip missing : {name}");
        }

        return clip;
    }
}
