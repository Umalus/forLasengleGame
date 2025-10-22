using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillData001_Slash : SkillDataBase
{
    public override void UseSkill(BattleCharacterBase _useCharacter) {
        //もし、使用者のレベルが必要なレベルを満たしていなければ、処理しない
        //※UIで表示しないようにはするが念のため
        if (_useCharacter.lv < this.lv) return;
        //MPが足らない時はログを表示し処理しない
        if(_useCharacter.MP < needMP) {
            Debug.Log("MPがたりません!");
            return;
        }
        for(int i = 0 ,max = rangeType; i < max; i++) {

        }

    }
}
