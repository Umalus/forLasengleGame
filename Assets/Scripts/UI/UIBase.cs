using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    public virtual async UniTask OpenWithFadeIn() {
        // メニューを表示する
        UIRoot?.SetActive(true);
        await ExecuteFade(1.0f, 0.3f);
    }

    // 閉じる
    public virtual async UniTask Close() {
        // メニュ－を非表示にする
        UIRoot?.SetActive(false);
        await UniTask.CompletedTask;
    }
    public virtual async UniTask CloseWithFadeOut() {
        // メニューを表示する
        await ExecuteFade(0.0f, 0.3f);
        UIRoot?.SetActive(false);
        
    }

    public async UniTask ExecuteFade(float _targetAlpha, float _duration) {
        Image fadeImage = UIRoot?.GetComponent<Image>();

        //経過時間
        float elapsedTime = 0.0f;
        float startAlpha = fadeImage.color.a;
        Color changeColor = fadeImage.color;
        while (elapsedTime < _duration) {
            elapsedTime += Time.deltaTime;

            float t = elapsedTime / _duration;
            //透明度を補完
            changeColor.a = Mathf.Lerp(startAlpha, _targetAlpha, t);
            fadeImage.color = changeColor;

            await UniTask.DelayFrame(1);
        }
        changeColor.a = _targetAlpha;
        fadeImage.color = changeColor;
    }
}
