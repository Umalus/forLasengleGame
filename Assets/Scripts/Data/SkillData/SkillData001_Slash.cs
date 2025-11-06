using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillData001_Slash : SkillDataBase
{
    public override async UniTask UseSkill(List<BattleCharacterBase> _enemies, BattleCharacterBase _useCharacter) {
        //もし、使用者のレベルが必要なレベルを満たしていなければ、処理しない
        //※UIで表示しないようにはするが念のため
        if (_useCharacter.lv < lv) return;
        //MPが足らない時はログを表示し処理しない
        if(_useCharacter.MP < needMP) {
            Debug.Log("MPがたりません!");
            return;
        }
        for(int i = 0 ,max = rangeType; i < max; i++) {
            var target = _enemies[i];
            //選ばれていなければその敵は処理しない
            if (!target.isSelect) continue;
            float damage = _useCharacter.rawAttack * 1.5f;
            //ダメージ処理
            target.RemoveHP((int)damage);
            await target.GetComponent<DamageEffect>().Damage(target.GetComponentInChildren<SphereCollider>(), (int)damage);


        }

    }
}
