using UnityEngine;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

//https://github.com/naichilab/Unity-BgmManager/blob/master/Assets/naichilab/BgmManager/Scripts/BgmManager.cs
//を参照

public class BgmManager : MonoBehaviour
{

    //#region Singleton
    private static BgmManager instance;

    public static BgmManager Instance
    {
        get
        {
            instance = (BgmManager)FindObjectOfType(typeof(BgmManager));

            //例外処理
            if (instance == null)
            {
                Debug.LogError(typeof(BgmManager) + "is nothing");
            }
            return instance;
        }
    }
    /// <summary>
    /// デバックモード
    /// </summary>
    public bool DebugMode = true;

    /// <summary>
    /// BGM再生音量
    /// 次回のフェイドインから実行。
    /// 再生中の音量を変更するならCurrentAudioSource.Volumeで変更
    /// </summary>
    [Range(0f, 1f)]
    public float TargetVolume = 1.0f;

    /// <summary>
    /// フェイドイン、フェイドアウトにかかる時間。
    /// </summary>
    public float TimeToFade = 2.0f;

    /// <summary>
    /// フェイドインとフェイドアウトの実行を重ねる割合。
    /// 0を指定すると、完全にフェイドアウトしてからフェイドインを開始。
    /// 1を指定すると、フェイドアウトと同時にフェイドインを開始
    /// </summary>
    [Range(0f, 1f)]
    public float CrossFadeRaito = 1.0f;

    /// <summary>
    /// 現在再生中のAudioSourece
    /// FadeOut中のものは除く
    /// </summary>
    [NonSerialized]
    public AudioSource CurrentAudioSouce = null;


    public AudioSource SubAudioSource
    {
        get
        {
            if (this.AudioSources == null) return null;
            foreach (AudioSource s in this.AudioSources)
            {
                if (s != this.CurrentAudioSouce)
                {
                    return s;
                }
            }
            return null;
        }
    }
    /// <summary>
    /// BGMを再生するためのAudioSource
    /// クロスフェードを実現するための2つの要素を持つ
    /// </summary>
    private List<AudioSource> AudioSources = null;

    /// <summary>
    /// 再生可能なBGM(AudioClip)のリスト
    /// 実行時に Resources/Audio/BGMのフォルダから自動的に読み込まれる。
    /// </summary>
    private Dictionary<string, AudioClip> AudioClipDict = null;

    /// <summary>
    /// コルーチン中断に使用
    /// </summary>
    private IEnumerator fadeOutCoroutine;

    /// <summary>
    /// コルーチン中断に使用
    /// </summary>
    private IEnumerator fadeInCoroutine;

    public void Awake()
    {
        //シングルトン用
        if (this != Instance)
        {
            Destroy(this.gameObject);
            return;
        }
        //FIXME:後で調べる
        DontDestroyOnLoad(this.gameObject);

        //AudioSourceを2つ用意、クロスフェード時に同時に再生するために2つ用意する。
        this.AudioSources = new List<AudioSource>();
        this.AudioSources.Add(this.gameObject.AddComponent<AudioSource>());
        this.AudioSources.Add(this.gameObject.AddComponent<AudioSource>());
        foreach (AudioSource s in this.AudioSources)
        {
            s.playOnAwake = false;
            s.volume = 0f;
            s.loop = true;
        }

        //[Resources/Audio/BGM]フォルダからBGMを探す
        this.AudioClipDict = new Dictionary<string, AudioClip>();
        foreach (AudioClip bgm in Resources.LoadAll<AudioClip>("Audio/BGM"))
        {
            this.AudioClipDict.Add(bgm.name, bgm);
        }

        //有効なAudioListenerが一つも無い場合は生成する。（大体はMainCameraについている）
        if (FindObjectsOfType(typeof(AudioListener)).All(o => !((AudioListener)o).enabled))
        {
            this.gameObject.AddComponent<AudioListener>();
        }
    }


    /// <summary>
    /// デバッグ用操作パネル表示
    /// </summary>
    public void OnGUI()
    {
        if (this.DebugMode)
        {
            //AudioClipが見つからなかった場合
            if (this.AudioClipDict.Count == 0)
            {
                GUI.Box(new Rect(10, 10, 200, 50), "BGM Manager(DebugMode)");
                GUI.Label(new Rect(10, 35, 80, 20), "Audio clips not found.");
                return;
            }

            GUI.Box(new Rect(10, 10, 200, 150 + this.AudioClipDict.Count * 25), "BGM Manager(Debug Mode)");
            int i = 0;
            GUI.Label(new Rect(20, 30 + i++ * 20, 180, 20), "Target Volume : " + this.TargetVolume.ToString("0.00"));
            GUI.Label(new Rect(20, 30 + i++ * 20, 180, 20), "Time to Fade : " + this.TimeToFade.ToString("0.00"));
            GUI.Label(new Rect(20, 30 + i++ * 20, 180, 20), "Crossfade Ratio : " + this.TargetVolume.ToString("0.00"));

            i = 0;
            //再生ボタン
            foreach (AudioClip bgm in this.AudioClipDict.Values)
            {
                bool currentBgm = (this.CurrentAudioSouce != null && this.CurrentAudioSouce.clip == this.AudioClipDict[bgm.name]);

                if (GUI.Button(new Rect(20, 100 + i * 25, 40, 20), "Play"))
                {
                    this.Play(bgm.name);
                }
                string txt = string.Format("[{0}] {1}", currentBgm ? "X" : "_", bgm.name);
                GUI.Label(new Rect(70, 100 + i * 25, 1000, 20), txt);

                i++;
            }

            //停止ボタン
            if (GUI.Button(new Rect(20, 100 + i++ * 25, 100, 20), "Stop"))
            {
                this.Stop();
            }
            if (GUI.Button(new Rect(20, 100 + i++ * 25, 100, 20), "Stop Immedlately"))
            {
                this.StopImmediately();
            }
        }
    }

    /// <summary>
    /// 曲はResource/Audio/BGMに入っているもの
    /// </summary>
    /// <param name="bgmName"></param>
    public void Play(string bgmName)
    {
        if (!this.AudioClipDict.ContainsKey(bgmName))
        {
            Debug.LogError(string.Format("BGM名[{0}]が見つかりません。", bgmName));
            return;
        }

        if ((this.CurrentAudioSouce != null)
            && (this.CurrentAudioSouce.clip == this.AudioClipDict[bgmName]))
        {
            //すでに指定されたBGMを再生中
            return;
        }

        //クロスフェード中なら中止
        stopFadeOut();
        stopFadeIn();

        //再生中のBGMをフェードアウト開始
        this.Stop();

        float fadeInStarDelay = this.TimeToFade * (1.0f - this.CrossFadeRaito);

        //BGM再生開始
        this.CurrentAudioSouce = this.SubAudioSource;
        this.CurrentAudioSouce.clip = this.AudioClipDict[bgmName];
        this.fadeInCoroutine = fadeIn(this.CurrentAudioSouce, this.TimeToFade, this.CurrentAudioSouce.volume, this.TargetVolume, fadeInStarDelay);
        StartCoroutine(this.fadeInCoroutine);
    }

    /// <summary>
    /// BGMを停止します。
    /// </summary>
    public void Stop()
    {
        if (this.CurrentAudioSouce != null)
        {
            this.fadeOutCoroutine = fadeOut(this.CurrentAudioSouce, this.TimeToFade, this.CurrentAudioSouce.volume, 0f);
            StartCoroutine(this.fadeOutCoroutine);
        }
    }

    /// <summary>
    //BGMをただちに停止します 
    /// </summary>
    public void StopImmediately()
    {
        this.fadeInCoroutine = null;
        this.fadeOutCoroutine = null;
        foreach (AudioSource s in this.AudioSources)
        {
            s.Stop();
        }
        this.CurrentAudioSouce = null;
    }

    private IEnumerator fadeIn(AudioSource bgm, float timeToFade, float fromVolume, float toVolume, float delay)
    {
        if (delay > 0)
        {
            yield return new WaitForSeconds(delay);
        }

        float StartTime = Time.timeScale;
        bgm.Play();
        while (true)
        {
            float spentTime = Time.time - StartTime;
            if (spentTime > TimeToFade)
            {
                bgm.volume = toVolume;
                this.fadeInCoroutine = null;
                break;
            }

            float rate = spentTime / timeToFade;
            float vol = Mathf.Lerp(fromVolume, toVolume, rate);
            bgm.volume = vol;
            yield return null;
        }
    }

    private IEnumerator fadeOut(AudioSource bgm, float timeToFade, float fromVolume, float toVolume)
    {
        float startTime = Time.time;
        while (true)
        {
            float spentTime = Time.time - startTime;
            if (spentTime > TimeToFade)
            {
                bgm.volume = toVolume;
                bgm.Stop();
                this.fadeOutCoroutine = null;
                break;
            }

            float rate = spentTime / timeToFade;
            float vol = Mathf.Lerp(fromVolume, toVolume, rate);
            bgm.volume = vol;
            yield return null;
        }
    }


    /// <summary>
    /// フェードイン処理を中断します。
    /// </summary>
    private void stopFadeIn()
    {
        if (this.fadeInCoroutine != null) StopCoroutine(this.fadeInCoroutine);

        this.fadeInCoroutine = null;
    }


    /// <summary>
    /// フェードアウト処理を中断します。
    /// </summary>
    private void stopFadeOut()
    {
        if (this.fadeOutCoroutine != null) StopCoroutine(this.fadeOutCoroutine);

        this.fadeOutCoroutine = null;
    }

}
