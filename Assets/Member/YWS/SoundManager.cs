using UnityEngine;
using System;
using System.Collections.Generic;

// 音量管理クラス
[Serializable]
public class SoundVolume
{
    public float BGM = 1.0f;
    public float SE = 1.0f;
    public float Narration = 1.0f;
    public bool Mute = false;

    public void Init()
    {
        BGM = 1.0f;
        SE = 1.0f;
        Narration = 1.0f;
        Mute = false;
    }
}

// 音管理クラス
public class SoundManager : MonoBehaviour
{
    protected static SoundManager instance;

    public static SoundManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = (SoundManager)FindObjectOfType(typeof(SoundManager));

                if (instance == null)
                {
                    Debug.LogError("SoundManager Instance Error");
                }
            }

            return instance;
        }
    }

    // 音量
    public SoundVolume volume = new SoundVolume();

    // === AudioSource ===
    // BGM
    private AudioSource BGMsource;
    // SE
    private AudioSource[] SEsources = new AudioSource[16];
    // Narration
    private AudioSource NarrationSource;

    // === AudioClip ===
    // BGM
    public AudioClip[] BGM;
    // SE
    public AudioClip[] SE;
    // Narration
    public List<AudioClip> Narration = new List<AudioClip>();
    private bool IsNarrationPlaying = false;
    private int NarrationPlayingNum = 0;
    
    // 
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    static void OnBoot()
    {
        Debug.Log("<color=red>サウンドマネージャー運転中</color>");
    }

    private void Awake()
    {
        // 音管理はシーン遷移では破棄させない
        DontDestroyOnLoad(gameObject);

        // 全てのAudioSourceコンポーネントを追加する
        // BGM AudioSource
        BGMsource = gameObject.AddComponent<AudioSource>();
        // BGMはループを有効にする
        BGMsource.loop = true;

        // SE AudioSource
        for (int i = 0; i < SEsources.Length; i++)
        {
            SEsources[i] = gameObject.AddComponent<AudioSource>();
        }

        // Narration AudioSource
        NarrationSource = gameObject.AddComponent<AudioSource>();
    }

    private void Update()
    {
        // ミュート設定
        BGMsource.mute = volume.Mute;
        foreach (AudioSource source in SEsources)
        {
            source.mute = volume.Mute;
        }
        NarrationSource.mute = volume.Mute;

        // ボリューム設定
        BGMsource.volume = volume.BGM;
        foreach (AudioSource source in SEsources)
        {
            source.volume = volume.SE;
        }
        NarrationSource.volume = volume.Narration;

        if (IsNarrationPlaying)
        {
            if (!NarrationSource.isPlaying)
            {
                Debug.Log("End");
                SwitchNextNarration();
            }
        }
    }

    // ***** BGM再生 *****
    // BGM再生
    public void PlayBGM(int index)
    {
        if (0 > index || BGM.Length <= index)
        {
            return;
        }
        // 同じBGMの場合は何もしない
        if (BGMsource.clip == BGM[index])
        {
            return;
        }
        BGMsource.Stop();
        BGMsource.clip = BGM[index];
        BGMsource.loop = true;
        BGMsource.Play();
    }

    // BGM停止
    public void StopBGM()
    {
        BGMsource.Stop();
        BGMsource.clip = null;
    }

    // ***** SE再生 *****
    // SE再生
    public void PlaySE(int index)
    {
        if (0 > index || SE.Length <= index)
        {
            return;
        }

        // 再生中で無いAudioSouceで鳴らす
        foreach (AudioSource source in SEsources)
        {
            if (false == source.isPlaying)
            {
                source.clip = SE[index];
                source.Play();
                return;
            }
        }
    }

    // SE停止
    public void StopSE()
    {
        // 全てのSE用のAudioSouceを停止する
        foreach (AudioSource source in SEsources)
        {
            source.Stop();
            source.clip = null;
        }
    }

    public void PlayNarration()
    {
        if (Narration.Count == 0)
        {
            return;
        }

        NarrationSource.clip = Narration[0];
        NarrationSource.Play();
        IsNarrationPlaying = true;
    }

    private void SwitchNextNarration()
    {
        if (!IsNarrationPlaying)
        {
            return;
        }

        NarrationPlayingNum++;
        if (NarrationPlayingNum >= Narration.Count)
        {
            IsNarrationPlaying = false;
            SoundManager.Instance.Narration = new List<AudioClip>();
            NarrationPlayingNum = 0;
            return;
        }

        NarrationSource.Stop();
        NarrationSource.clip = Narration[NarrationPlayingNum];
        NarrationSource.Play();
    }

    public void StopNarration()
    {
        NarrationSource.Stop();
        NarrationSource.clip = null;
        NarrationPlayingNum = 0;
    }
}