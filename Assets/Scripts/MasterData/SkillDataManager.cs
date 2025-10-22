using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillDataManager : MonoBehaviour
{
    //使えるスキル
    public List<SkillDataBase> skills = null;
    void Start()
    {
        skills = new List<SkillDataBase>();
        //skills.Add(new );
    }
    /// <summary>
    /// スキルを使用
    /// </summary>
    /// <param name="_id"></param>
    public void ExecuteSkill(int _id,BattleCharacterBase _useCharacter) {
        skills[_id].UseSkill(_useCharacter);
    }
}
