using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

using static CommonModule;

public class BattlePlayerAction {

    /// <summary>
    /// バトル中の行動選択
    /// </summary>
    /// <param name="_enemies"></param>
    /// <param name="_source"></param>
    /// <returns></returns>
    public async UniTask Order(List<BattleEnemy> _enemies, BattleCharacterBase _source) {
        if (IsEmpty(_enemies)) return;

        while (true) {
            var normalAttackTask = NormalAttack(_enemies, _source);
            var useSkillTask = UseSkill(_enemies, _source);

            var (index,_,_) = await UniTask.WhenAny(normalAttackTask, useSkillTask);

            if (index == 0) break; // NormalAttack が選ばれた
            if (index == 1) break; // UseSkill が選ばれた

        }
    }

    /// <summary>
    /// 通常攻撃(単体攻撃想定)
    /// </summary>
    /// <returns></returns>
    private async UniTask<bool> NormalAttack(List<BattleEnemy> _enemies, BattleCharacterBase _source) {
        //対象のボタンが押されたかどうか判定
        var buttonEvent = BattlePlayer.normalAttackButton.onClick.GetAsyncEventHandler(CancellationToken.None);
        await buttonEvent.OnInvokeAsync();

        int damage = Random.Range(_source.rawAttack - 5, _source.rawAttack);
        Animator anim = _source.GetComponentInChildren<Animator>();
        for (int i = 0, max = _enemies.Count; i < max; i++) {
            BattleCharacterBase target = _enemies[i];
            if (!target.isSelect) continue;
            if (_source.ID == 0) {
                ParticleSystem ps = _source.GetComponentInChildren<ParticleSystem>();
                ps.Play();
            }
            target.RemoveHP(damage);
            //ダメージ表記
            await target.GetComponent<DamageEffect>().Damage(target.GetComponentInChildren<SphereCollider>(), damage);
            anim.SetTrigger("Attack");
            if (target.isDead)
                target.Dead();
            float animLength = anim.GetCurrentAnimatorStateInfo(0).length;
            Debug.Log("敵のHP" + target.HP);
            float waitTime = animLength * 1000;
            await EffectManager.instance.ExecuteEffect(1, target.transform);
            await UniTask.DelayFrame((int)waitTime);
        }
        return true;
    }

    private async UniTask<bool> UseSkill(List<BattleEnemy> _enemies, BattleCharacterBase _source) {

        //対象のボタンが押されたかどうか判定
        var buttonEvent = BattlePlayer.skillButton.onClick.GetAsyncEventHandler(CancellationToken.None);
        await buttonEvent.OnInvokeAsync();

        for(int i = 0,max = _enemies.Count; i < max; i++) {
            SkillDataBase useSkill = _source.GetComponent<BattlePlayer>().usableSkill[0];
            //必要MPを確認
            if (useSkill.needMP > _source.MP) return false;
            _source.SetMP(useSkill.needMP);
            await useSkill.Execute(_enemies[i], _source);
        }

        return true;
    }
}
