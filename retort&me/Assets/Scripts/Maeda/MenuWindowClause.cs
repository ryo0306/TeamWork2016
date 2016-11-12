using UnityEngine;
using System.Collections;

public class MenuWindowClause : MonoBehaviour {
    [SerializeField]
    GameObject Menuwindow;

    public void OnClick()
    {
        Debug.Log("クリックされたよ");
        // 非表示にする
        Menuwindow.SetActive(false);
    }
}
