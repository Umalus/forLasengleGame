using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �X�L���f�[�^�̊��@
/// </summary>
public abstract class SkillDataBase : MonoBehaviour
{
    protected int id;
    protected int needMP;
    protected int rangeType;
    protected string skillName;
    protected int lv;


    //�}�X�^�[�f�[�^��������󂯎���ă����o��������
    public void Initialize(int _id) {
        Entity_SkillData.Param initParam = SkillDataUtility.GetSkillData(_id);
        id = initParam.ID;
        lv = initParam.Lv;
        rangeType = initParam.rangeType;
        needMP = initParam.NeedMP;
        skillName = initParam.name;
    }

    public abstract void UseSkill(BattleCharacterBase _useCharacter);
    
}
