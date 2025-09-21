using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class DamageEffect : MonoBehaviour
{
    //�@DamageUI�v���n�u
    [SerializeField]
    private GameObject damageUI;

    public async Task Damage(Collider col) {
        //�@DamageUI���C���X�^���X���B�o��ʒu�͐ڐG�����R���C�_�̒��S����J�����̕����ɏ����񂹂��ʒu
        var obj = Instantiate<GameObject>(damageUI, col.bounds.center - Camera.main.transform.forward * 0.2f, Quaternion.identity);
        await UniTask.DelayFrame(100);
    }
}
