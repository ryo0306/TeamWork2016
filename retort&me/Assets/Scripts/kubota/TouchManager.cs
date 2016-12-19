using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;

//必要な操作
//    スワイプ
//    フリック
//    タップ



public class TouchManager : SingletonMonoBehaviour<TouchManager>
{
    enum FrickDirection 
    {
        Up,
        Down,
        Right,
        Left,
    }


    Touch[] touchs;

    int touchsNum;


    void Start()
    {
        if (Input.touchSupported)
        {
            Debug.Log("このタッチ入力に対応しています。");
        }
        Input.multiTouchEnabled = false;
        Input.simulateMouseWithTouches = false;
    }

    Touch GetTouch(int num_)
    {
        if (touchs.Length < num_)
        {
            Debug.LogError("そのタッチは検出されませんでした。");
            Debug.Log("よってからのTouchを返します。");
            return new Touch();
        }
        return touchs[num_];
    }

    Touch SearchIdGetTouch(int id)
    {
        foreach (var touch in touchs)
        {
            if (touch.fingerId == id)
            {
                return touch;
            }
        }

        Debug.LogError("見当するTouchはみつかりませんでした。");
        Debug.Log("よってからのTouchを返します。");
        return new Touch();
    }

    Vector2 GetPos(int num_ = 0)
    {
#if UNITY_STANDALONE
        return Input.mousePosition;
#else
        return touchs[num_].position;
#endif
    }


    /// <summary>
    /// 現在タップされているすべてを調べます。
    /// </summary>
    /// <returns></returns>
    bool IsBegin()
    {
        bool isBegin = false;
#if UNITY_STANDALONE

#else
        foreach (var touch in touchs)
        {
            if (touch.phase == TouchPhase.Began)
            {
                isBegin = true;
                break;
            }
        }

#endif
        return isBegin;
    }

    /// <summary>
    /// 指定されたTouchだけ調べます
    /// </summary>
    /// <param name="num_"></param>
    /// <returns></returns>
    bool IsBegin(int num_)
    {

        return (touchs[num_].phase == TouchPhase.Began);
    }


    /// <summary>
    /// 現在タップされているすべてを調べます。
    /// </summary>
    /// <returns></returns>
    bool isMove()
    {
        bool isMove = false;
        foreach (var touch in touchs)
        {
            if (touch.phase == TouchPhase.Moved)
            {
                isMove = true;
                break;
            }
        }
        return isMove;
    }

    /// <summary>
    /// 指定されたTouchだけ調べます
    /// </summary>
    /// <param name="num_"></param>
    /// <returns></returns>
    bool IsMove(int num_)
    {
        return (touchs[num_].phase == TouchPhase.Moved);
    }

    /// <summary>
    /// 現在タップされているすべてを調べます。
    /// </summary>
    /// <returns></returns>
    bool IsEnd()
    {
        bool isEnd = false;
        foreach (var touch in touchs)
        {
            if (touch.phase == TouchPhase.Ended)
            {
                isEnd = true;
                break;
            }
        }
        return isEnd;
    }

    /// <summary>
    /// 指定されたTouchだけ調べます
    /// </summary>
    /// <param name="num_"></param>
    /// <returns></returns>
    bool IsEnd(int num_)
    {
        return (touchs[num_].phase == TouchPhase.Ended);
    }


    void FixedUpdate()
    {
       touchs =  Input.touches;
       touchsNum = Input.touchCount;
    }

    bool Frick(FrickDirection direction, float distance = 0.5f)
    {
        if (touchs.Length <= 0) return false;
        foreach (var touch in touchs)
        {
            switch (direction)
            {
                case FrickDirection.Up:
                    return (touch.deltaPosition.y > distance);

                case FrickDirection.Down:
                    return (touch.deltaPosition.y < -distance);

                case FrickDirection.Right:
                    return (touch.deltaPosition.x > distance);

                case FrickDirection.Left:
                    return (touch.deltaPosition.x < -distance);
                default:
                    return false;
            }
        }
        return false;
    }
}