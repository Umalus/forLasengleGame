using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BasePart : MonoBehaviour
{
    /// <summary>
    /// �Q�[���J�n���Ɉ�x�����Ă΂��
    /// </summary>
    /// <returns></returns>
    public virtual async UniTask Init() {
        gameObject.SetActive(false);
        await UniTask.CompletedTask;
    }

    /// <summary>
    /// �p�[�g�J�ڑO�ɌĂ΂��
    /// </summary>
    /// <returns></returns>
    public virtual async UniTask Setup() {
        gameObject.SetActive(true);
        await UniTask.CompletedTask;
    }

    /// <summary>
    /// �p�[�g���s���ɌĂ΂��
    /// </summary>
    /// <returns></returns>
    public abstract UniTask Execute();

    /// <summary>
    /// �p�[�g���I������ۂɌĂ΂��
    /// </summary>
    /// <returns></returns>
    public virtual async UniTask Teardown() {
        gameObject.SetActive(false);
        await UniTask.CompletedTask;
    }
}
