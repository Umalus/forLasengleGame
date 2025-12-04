using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

[CreateAssetMenu(menuName = "Skill/Slash")]

public class Slash : SkillDataBase {

    /// <summary>
    /// スキル処理
    /// </summary>
    /// <returns></returns>
    public override async UniTask Execute(IEnumerable<BattleCharacterBase> _targets, BattleCharacterBase _source,CancellationToken _token ) {
        Animator anim = _source.GetComponentInChildren<Animator>();
        foreach (var target in _targets) {
            if (!target.isSelect) return;

            int damage = (int)(_source.rawAttack * power * RATIO);


            target.RemoveHP(damage);
            //ダメージ表記
            await target.GetComponent<DamageEffect>().Damage(target.GetComponentInChildren<SphereCollider>(), damage);
            anim.SetTrigger("Attack");
            if (target.isDead)
                target.Dead();
            float animLength = anim.GetCurrentAnimatorStateInfo(0).length;
            Debug.Log("敵のHP" + target.HP);
            float waitTime = animLength * 1000;
            EffectManager.instance.ExecuteEffect(1, target.transform);
            await UniTask.DelayFrame((int)waitTime, cancellationToken: _token);
        }

        
    }
}
