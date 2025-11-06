using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using UnityEngine;

public class SkillDataManager : MonoBehaviour
{
    public static SkillDataManager instance = null;
    //使えるスキル
    public List<SkillDataBase> skills = null;
    void Start()
    {
        instance = this;

        skills = new List<SkillDataBase>();
        skills.Add(new SkillData001_Slash());
    }
    /// <summary>
    /// スキルを使用
    /// </summary>
    /// <param name="_id"></param>
    public async UniTask ExecuteSkill(int _id, List<BattleCharacterBase> _characterBases,BattleCharacterBase _useCharacter) {
        await skills[_id].UseSkill(_characterBases,_useCharacter);
    }
}
