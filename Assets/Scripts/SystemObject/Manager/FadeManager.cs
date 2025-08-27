using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeManager : SystemObject
{
    //インスタンス(これを取得してこのクラスの関数等を使用する)
    public static FadeManager instance = null;
    //フェード画像
    [SerializeField]
    private Image fadeImage = null;
    //どのくらいの時間をかけてフェードするか
    private const float DURTION_TIME = 1.0f;

    /// <summary>
    /// 初期化関数
    /// </summary>
    /// <returns></returns>
    public override async UniTask Initialize() {
        instance = this;
        await UniTask.CompletedTask;
    }
    /// <summary>
    /// フェードイン処理
    /// </summary>
    /// <param name="_duration"></param>
    /// <returns></returns>
    public async UniTask FadeIn(float _duration = DURTION_TIME) {
        await ExecuteFade(0.0f, _duration);
        fadeImage.enabled = false;
    }
    /// <summary>
    /// フェードアウト処理
    /// </summary>
    /// <param name="_duration"></param>
    /// <returns></returns>
    public async UniTask FadeOut(float _duration = DURTION_TIME) {
        fadeImage.enabled = true;
        await ExecuteFade(1.0f, _duration);
    }
    /// <summary>
    /// フェードのアルファ値を変更
    /// </summary>
    /// <param name="_targetAlpha"></param>
    /// <param name="_duration"></param>
    /// <returns></returns>
    public async UniTask ExecuteFade(float _targetAlpha , float _duration) {
        //経過時間
        float elapsedTime = 0.0f;
        float startAlpha = fadeImage.color.a;
        Color changeColor = fadeImage.color;
        while(elapsedTime < _duration) {
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
