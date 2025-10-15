/*
 * @brief   戦闘用のプレイヤー
 */
using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Threading;

public class BattlePlayer : BattleCharacterBase
{
    public static Button normalAttackButton = null;
    public BattlePlayerAction playerAction = null;
    /// <summary>
    /// 初期化関数
    /// </summary>
    /// <param name="_ID"></param>
    /// <param name="_masterID"></param>
    public override void Initilized(int _ID, int _masterID) {
        base.Initilized(_ID, _masterID);
        playerAction = new BattlePlayerAction();
        HP *= 100;
        anim = GetComponentInChildren<Animator>();
    }

    /// <summary>
    /// プレイヤーかどうか
    /// </summary>
    /// <returns></returns>
    public override bool IsPlayer() {
        return true;
    }
    public void TakeDamageAnimation() {
        anim.SetTrigger("Damage");
    }
}
