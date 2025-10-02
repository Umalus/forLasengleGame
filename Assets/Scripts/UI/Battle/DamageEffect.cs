using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;

public class DamageEffect : MonoBehaviour
{
    //�@DamageUI�v���n�u
    [SerializeField]
    private GameObject damageUI;

    public async Task Damage(Collider _col,float _damage) {
        var damageText = damageUI.GetComponentInChildren<TextMeshProUGUI>();
        damageText.text = _damage.ToString();
        //�@DamageUI���C���X�^���X���B�o��ʒu�͐ڐG�����R���C�_�̒��S����J�����̕����ɏ����񂹂��ʒu
        Instantiate(damageUI, _col.bounds.center - Camera.main.transform.forward * 1.0f, Quaternion.identity);
        await UniTask.DelayFrame(100);
    }
}
