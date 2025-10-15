using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

using static CommonModule;

public class BattlePlayerAction{

    /// <summary>
    /// バトル中の行動選択
    /// </summary>
    /// <param name="_enemies"></param>
    /// <param name="_source"></param>
    /// <returns></returns>
    public async UniTask Order(List<BattleEnemy> _enemies,BattleCharacterBase _source) {
        if (IsEmpty(_enemies)) return;

        while (true) {
            if (await NormalAttack(_enemies,_source))
                break;
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
        for(int i = 0,max = _enemies.Count;i < max; i++) {
            BattleCharacterBase target = _enemies[i];
            if (!target.isSelect) continue;
            ParticleSystem ps = _source.GetComponentInChildren<ParticleSystem>();
            ps.Play();


            anim.SetTrigger("Attack");
            target.RemoveHP(damage);
            if (target.isDead)
                target.Dead();

            Debug.Log("敵のHP" + target.HP);
            //ダメージ表記
            await target.GetComponent<DamageEffect>().Damage(target.GetComponentInChildren<SphereCollider>(),damage);
        }

        return true;
    }
}
