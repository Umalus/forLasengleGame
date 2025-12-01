using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Skill/Arclight")]
public class Arclight : SkillDataBase
{
    public override async UniTask Execute(BattleCharacterBase _target, BattleCharacterBase _source) {
        if (!_target.isSelect) return;

        int damage = (int)(_source.rawAttack * power * RATIO);
        Animator anim = _source.GetComponentInChildren<Animator>();

        _target.RemoveHP(damage);
        //ダメージ表記
        await _target.GetComponent<DamageEffect>().Damage(_target.GetComponentInChildren<SphereCollider>(), damage);
        anim.SetTrigger("Attack");
        if (_target.isDead)
            _target.Dead();
        float animLength = anim.GetCurrentAnimatorStateInfo(0).length;
        Debug.Log("敵のHP" + _target.HP);
        float waitTime = animLength * 1000;
        await EffectManager.instance.ExecuteEffect(1, _target.transform);
        await UniTask.DelayFrame((int)waitTime);
        await UniTask.CompletedTask;
    }
}
