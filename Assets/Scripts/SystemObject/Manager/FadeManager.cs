using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeManager : SystemObject
{
    //�C���X�^���X(������擾���Ă��̃N���X�̊֐������g�p����)
    public static FadeManager instance = null;
    //�t�F�[�h�摜
    [SerializeField]
    private Image fadeImage = null;
    //�ǂ̂��炢�̎��Ԃ������ăt�F�[�h���邩
    private const float DURTION_TIME = 1.0f;

    /// <summary>
    /// �������֐�
    /// </summary>
    /// <returns></returns>
    public override async UniTask Initialize() {
        instance = this;
        await UniTask.CompletedTask;
    }
    /// <summary>
    /// �t�F�[�h�C������
    /// </summary>
    /// <param name="_duration"></param>
    /// <returns></returns>
    public async UniTask FadeIn(float _duration = DURTION_TIME) {
        await ExecuteFade(0.0f, _duration);
        fadeImage.enabled = false;
    }
    /// <summary>
    /// �t�F�[�h�A�E�g����
    /// </summary>
    /// <param name="_duration"></param>
    /// <returns></returns>
    public async UniTask FadeOut(float _duration = DURTION_TIME) {
        fadeImage.enabled = true;
        await ExecuteFade(1.0f, _duration);
    }
    /// <summary>
    /// �t�F�[�h�̃A���t�@�l��ύX
    /// </summary>
    /// <param name="_targetAlpha"></param>
    /// <param name="_duration"></param>
    /// <returns></returns>
    public async UniTask ExecuteFade(float _targetAlpha , float _duration) {
        //�o�ߎ���
        float elapsedTime = 0.0f;
        float startAlpha = fadeImage.color.a;
        Color changeColor = fadeImage.color;
        while(elapsedTime < _duration) {
            elapsedTime += Time.deltaTime;

            float t = elapsedTime / _duration;
            //�����x��⊮
            changeColor.a = Mathf.Lerp(startAlpha, _targetAlpha, t);
            fadeImage.color = changeColor;

            await UniTask.DelayFrame(1);
        }
        changeColor.a = _targetAlpha;
        fadeImage.color = changeColor;
    }
}
