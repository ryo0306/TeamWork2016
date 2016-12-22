using UnityEngine;
using System;
using System.Collections;

public class SingletonMonoBehaviour<T> : MonoBehaviour where T : MonoBehaviour {

    private static T instance;
    public static T Instace
    {
        get
        {
            if (instance == null)
            {
                Type t = typeof(T);

                instance = (T)FindObjectOfType(t);
                if (instance == null)
                {
                    Debug.LogWarning(t + "をアタッチしているGameObjectはありません");
                }
            }

            return instance;
        }
    }

    /// <summary>
    /// 子クラスでAwakeを使う場合は
    /// 必ず親クラスのAwakeをCallして
    /// 複数のGameObjectにアタッチされないようにします.
    /// base.Awake();
    /// </summary>
    virtual protected void Awake()
    {
        if (this != Instace)
        {
            Destroy(this);
            Debug.LogWarning(typeof(T) +
                           "はすでに他のGameObjectにアタッチされているため、コンポーネントを破棄しました。" + 
                           "アタッチされているGameObjectは" + Instace.gameObject.name + "です。");
            return;
        }
    }
}
