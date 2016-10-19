using UnityEngine;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;


public class SEManager : MonoBehaviour
{


    private static SEManager instance;

    public static SEManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = (SEManager)FindObjectOfType(typeof(SEManager));

                //例外処理
                if (instance == null)
                {
                    Debug.LogError(typeof(SEManager) + "is nothing");
                }
            }

            return instance;
        }
    }


    /// <summary>
    /// デバッグモード
    /// </summary>
    public bool debugMode = false;

    /// <summary>
    /// 最大同時再生数
    /// </summary>
    [SerializeField]
    private int maxSESource = 10;

    /// <summary>
    /// SE再生音量
    /// </summary>
    [Range(0f, 1f)]
    public float targetVolume = 1.0f;


    private List<AudioSource> AudioSources = null;

    /// <summary>
    /// 再生可能なSE(AudioClip)のリストです。
    /// 実行時に Resources/Audio/SE フォルダから自動読み込みされます。
    /// </summary>
    private Dictionary<string, AudioClip> AudioClipDict = null;

    public void Awake()
    {
        //シングルトン用
        if (this != Instance)
        {
            Destroy(this.gameObject);
            return;
        }
        DontDestroyOnLoad(this.gameObject);

        this.AudioSources = new List<AudioSource>();

        //[Resources/Audio/SE]フォルダからSEを探す
        this.AudioClipDict = new Dictionary<string, AudioClip>();
        foreach (AudioClip bgm in Resources.LoadAll<AudioClip>("Audio/SE"))
        {
            this.AudioClipDict.Add(bgm.name, bgm);
        }

        //有効なAudioListenerが一つも無い場合は生成する。（大体はMainCameraについてる）
        if (FindObjectsOfType(typeof(AudioListener)).All(o => !((AudioListener)o).enabled))
        {
            this.gameObject.AddComponent<AudioListener>();
        }
    }

    /// <summary>
    /// デバッグ用操作パネルを表示
    /// </summary>
    public void OnGUI()
    {
        if (this.debugMode)
        {
            //AudioClipが見つからなかった場合
            if (this.AudioClipDict.Count == 0)
            {
                GUI.Box(new Rect(10, 10, 200, 50), "SE Manager(Debug Mode)");
                GUI.Label(new Rect(10, 35, 1000, 20), "Audio clips not found.");
                return;
            }

            //枠
            GUI.Box(new Rect(10, 10, 200, 120 + this.AudioClipDict.Count * 25), "SE Manager(Debug Mode)");
            int i = 0;
            GUI.Label(new Rect(20, 30 + i++ * 20, 180, 20), "targetVolume : " + this.targetVolume.ToString("0.00"));
            GUI.Label(new Rect(20, 30 + i++ * 20, 180, 20), "Max Play : " + this.maxSESource.ToString("0"));

            i = 0;
            //再生ボタン
            foreach (AudioClip se in this.AudioClipDict.Values)
            {
                if (GUI.Button(new Rect(20, 80 + i * 25, 40, 20), "Play"))
                {
                    this.Play(se.name);
                }
                string txt = string.Format("{0}", se.name);
                GUI.Label(new Rect(70, 80 + i * 25, 1000, 20), txt);
                i++;
            }

            //停止ボタン
            if (GUI.Button(new Rect(20, 80 + i++ * 25, 180, 20), "Stop"))
            {
                this.StopImmediately();
            }

            int playingSources = this.AudioSources.Count(s => s.isPlaying);
            if (playingSources == 1)
            {
                GUI.Label(new Rect(20, 80 + i * 25, 1000, 20), string.Format("{0} audio source is playing.", playingSources));
            }
            else if (playingSources > 1)
            {
                GUI.Label(new Rect(20, 80 + i * 25, 1000, 20), string.Format("{0} audio sources are playing.", playingSources));
            }





        }
    }

    /// <summary>
    /// SEを再生します。
    /// </summary>
    /// <param name="seName">SE名</param>
    public void Play(string seName)
    {
        this.Play(seName, this.targetVolume);
    }

    /// <summary>
    /// SEを再生します。
    /// </summary>
    /// <param name="seName">SE名</param>
    /// <param name="targetVolume">再生音量</param>
    public void Play(string seName, float targetVolume)
    {
        if (!this.AudioClipDict.ContainsKey(seName))
        {
            Debug.LogError(string.Format("SE名[{0}]が見つかりません。", seName));
            return;
        }


        //空いているAudioSourceを探す
        AudioSource source = this.AudioSources.FirstOrDefault(s => !s.isPlaying);
        if (source == null)
        {
            if (this.AudioSources.Count >= this.maxSESource)
            {
                Debug.Log("最大再生数を超えました。");
                return;
            }

            source = this.gameObject.AddComponent<AudioSource>();
            source.playOnAwake = false;
            source.loop = false;
            this.AudioSources.Add(source);
        }

        source.clip = this.AudioClipDict[seName];
        source.volume = targetVolume;
        source.Play();
    }

    /// <summary>
    /// 全てのSEを停止。
    /// </summary>
    public void StopImmediately()
    {
        foreach (AudioSource source in this.AudioSources)
        {
            source.Stop();
        }
    }

    /// <summary>
    /// 特定のSEを停止。
    /// </summary>
    public void StopImmediately(string seName)
    {
        foreach (AudioSource source in this.AudioSources)
        {
            if (source.clip.name == seName)
            {
                source.Stop();
            }
        }
    }
}