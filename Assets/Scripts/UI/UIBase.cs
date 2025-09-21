using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIBase : MonoBehaviour{

    [SerializeField]
    private GameObject UIRoot = null;

    // ������
    public virtual async UniTask Initialize() {
        await UniTask.CompletedTask;
    }

    // �J��
    public virtual async UniTask Open() {
        // ���j���[��\������
        UIRoot?.SetActive(true);
        await UniTask.CompletedTask;

    }

    public virtual async UniTask OpenWithFadeIn() {
        // ���j���[��\������
        UIRoot?.SetActive(true);
        await ExecuteFade(1.0f, 0.3f);
    }

    // ����
    public virtual async UniTask Close() {
        // ���j���|���\���ɂ���
        UIRoot?.SetActive(false);
        await UniTask.CompletedTask;
    }
    public virtual async UniTask CloseWithFadeOut() {
        // ���j���[��\������
        await ExecuteFade(0.0f, 0.3f);
        UIRoot?.SetActive(false);
        
    }

    public async UniTask ExecuteFade(float _targetAlpha, float _duration) {
        Image fadeImage = UIRoot?.GetComponent<Image>();

        //�o�ߎ���
        float elapsedTime = 0.0f;
        float startAlpha = fadeImage.color.a;
        Color changeColor = fadeImage.color;
        while (elapsedTime < _duration) {
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
