using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using UnityEngine;
using System;

using static CommonModule;
using static SkillDataBase;

public class BattlePlayerAction {

    /// <summary>
    /// バトル中の行動選択
    /// </summary>
    /// <param name="_enemies"></param>
    /// <param name="_source"></param>
    /// <returns></returns>
    public async UniTask Order(List<BattleEnemy> _enemies, List<BattlePlayer> _partyMember, BattleCharacterBase _source) {
        if (IsEmpty(_enemies)) return;

        //while (true) {
        //    var normalAttackTask = NormalAttack(_enemies, _source, _cts.Token);
        //    var useSkillTask = UseSkill(_enemies, _partyMember, _source, _cts.Token);

        //    var (index, _, _) = await UniTask.WhenAny(normalAttackTask, useSkillTask);

        //    //勝利タスクが決まったため選ばれなかったタスクをキャンセル
        //    _cts.Cancel();

        //    if (index == 0) break; // NormalAttack が選ばれた
        //    if (index == 1) break; // UseSkill が選ばれた

        //}

        var _cts = new CancellationTokenSource();

        try {
            var normalAttackTask = NormalAttack(_enemies, _source, _cts.Token).SuppressCancellationThrow();
            var useSkillTask = UseSkill(_enemies, _partyMember, _source, _cts.Token).SuppressCancellationThrow();

            var (index, _, _) = await UniTask.WhenAny(normalAttackTask, useSkillTask);

            // 勝者が決まったので残りをキャンセル
            _cts.Cancel();

            if (index == 0) {
                // NormalAttack が選ばれた
            }
            else if (index == 1) {
                // UseSkill が選ばれた
            }
        }
        catch (OperationCanceledException) {
            // 敗者側のキャンセルでここに来る → 無視してOK
        }

    }

    /// <summary>
    /// 通常攻撃(単体攻撃想定)
    /// </summary>
    /// <returns></returns>
    private async UniTask<bool> NormalAttack(List<BattleEnemy> _enemies, BattleCharacterBase _source,CancellationToken _token) {
        //対象のボタンが押されたかどうか判定
        var buttonEvent = BattlePlayer.normalAttackButton.onClick.GetAsyncEventHandler(_token);
        await buttonEvent.OnInvokeAsync();

        await BattlePhase.battleCanvas.Close();
        int damage = UnityEngine.Random.Range(_source.rawAttack - 5, _source.rawAttack);
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
            EffectManager.instance.ExecuteEffect(1, target.transform);
            await UniTask.DelayFrame((int)waitTime,cancellationToken:_token);
        }
        return true;
    }

    private async UniTask<bool> UseSkill(List<BattleEnemy> _enemies, List<BattlePlayer> _partyMember, BattleCharacterBase _source, CancellationToken _token) {

        //対象のボタンが押されたかどうか判定
        var buttonEvent = BattlePlayer.skillButton.onClick.GetAsyncEventHandler(_token);
        await buttonEvent.OnInvokeAsync();
        await BattlePhase.battleCanvas.Close();
        //使うスキルを選択(まだ一個目のみ対応)
        SkillDataBase useSkill = _source.GetComponent<BattlePlayer>().usableSkill[0];

        //必要MPを確認
        if (useSkill.needMP > _source.currentMP) {
            Debug.Log("MPがたりません");
            return false;
        }
        _source.SetMP(_source.currentMP - useSkill.needMP);
        var targets = SelectTargets(useSkill, _enemies, _partyMember);
        await useSkill.Execute(targets, _source,_token);

        return true;
    }

    IEnumerable<BattleCharacterBase> SelectTargets(
    SkillDataBase skill,
    List<BattleEnemy> enemies,
    List<BattlePlayer> allies) {
        return skill.targetType switch {
            TargetType.EnemySingle => new[] { enemies.First(e => e.isSelect) },
            TargetType.EnemyAll => enemies,
            TargetType.AllySingle => new[] { allies.First(a => a.isSelect) },
            TargetType.AllyAll => allies,
            TargetType.RandomEnemy => new[] { enemies[UnityEngine.Random.Range(0, enemies.Count)] },
            _ => Enumerable.Empty<BattleCharacterBase>()
        };
    }

}
