/*
 * @brief   戦闘用のプレイヤー
 */
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattlePlayer : BattleCharacterBase
{
    public static Button normalAttackButton = null;
    public static Button skillButton = null;
    public BattlePlayerAction playerAction = null;
    public List<SkillDataBase> skillPool = null;
    
    [System.NonSerialized]
    public List<SkillDataBase> usableSkill = null;

    /// <summary>
    /// 初期化関数
    /// </summary>
    /// <param name="_ID"></param>
    /// <param name="_masterID"></param>
    public override void Initilized(int _ID, int _masterID) {
        base.Initilized(_ID, _masterID);
        playerAction = new BattlePlayerAction();
#if UNITY_EDITOR
        HP *= 100;
#endif
        anim = GetComponentInChildren<Animator>();
        usableSkill = new List<SkillDataBase>();
        CheckAddNewSkills();
    }

    /// <summary>
    /// プレイヤーかどうか
    /// </summary>
    /// <returns></returns>
    public override bool IsPlayer() {
        return true;
    }

    /// <summary>
    /// レベルアップ処理
    /// </summary>
    public void LevelUp() {
        lv++;
        CheckAddNewSkills();
    }

    /// <summary>
    /// ダメージを食らったアニメーション発火
    /// </summary>
    public void TakeDamageAnimation() {
        anim.SetTrigger("Damage");
    }

    /// <summary>
    /// レベルに応じてスキルを追加
    /// </summary>
    private void CheckAddNewSkills() {
        foreach(var addSkill in skillPool) {
            if (addSkill.requiredLevel > lv) continue;
            usableSkill.Add(addSkill);
        }
    }
}
