using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillDataManager : MonoBehaviour
{
    //�g����X�L��
    public List<SkillDataBase> skills = null;
    void Start()
    {
        skills = new List<SkillDataBase>();
        //skills.Add(new );
    }
    /// <summary>
    /// �X�L�����g�p
    /// </summary>
    /// <param name="_id"></param>
    public void ExecuteSkill(int _id,BattleCharacterBase _useCharacter) {
        skills[_id].UseSkill(_useCharacter);
    }
}
