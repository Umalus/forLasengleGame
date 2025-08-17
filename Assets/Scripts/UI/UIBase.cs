using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    // ����
    public virtual async UniTask Close() {
        // ���j���|���\���ɂ���
        UIRoot?.SetActive(false);
        await UniTask.CompletedTask;
    }
}
