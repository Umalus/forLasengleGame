using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIBase : MonoBehaviour{

    [SerializeField]
    private GameObject UIRoot = null;

    // 初期化
    public virtual async UniTask Initialize() {
        await UniTask.CompletedTask;
    }

    // 開く
    public virtual async UniTask Open() {
        // メニューを表示する
        UIRoot?.SetActive(true);
        await UniTask.CompletedTask;

    }

    // 閉じる
    public virtual async UniTask Close() {
        // メニュ－を非表示にする
        UIRoot?.SetActive(false);
        await UniTask.CompletedTask;
    }
}
