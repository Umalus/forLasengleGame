using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// スキルデータの基底　
/// </summary>
public abstract class SkillDataBase : MonoBehaviour
{
    protected int id;
    protected int needMP;
    protected int rangeType;
    protected string skillName;
    protected int lv;

    //マスターデータから情報を受け取ってメンバを初期化
    public void Initialize(int _id) {
        Entity_SkillData.Param initParam = SkillDataUtility.GetSkillData(_id);
        id = initParam.ID;
        lv = initParam.Lv;
        rangeType = initParam.rangeType;
        needMP = initParam.NeedMP;
        skillName = initParam.name;
    }

    public abstract UniTask UseSkill(List<BattleCharacterBase> _enemies, BattleCharacterBase _useCharacter);
    
}
