using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class BattleCharacterBase : MonoBehaviour
{
    //キャラクターのID
    public int ID { get; private set; } = -1;
    //マスターID
    public int masterID { get; protected set; } = -1;
    //死亡判定
    public bool isDead { get; protected set; } = false;
    //対象に選ばれているか
    public bool isSelect { get; protected set; } = false;

    //マスターデータ依存のステータス
    public int HP { get; protected set; } = -1;
    public int MaxHP { get; protected set; } = -1;
    public int MP { get; protected set; } = -1;
    public int MaxMP { get; protected set; } = -1;
    public int rawAttack { get; protected set; } = -1;
    public int rawDefence { get; protected set; } = -1;
    public int criticalRatio { get; protected set; } = -1;
    public int criticalPower { get; protected set; } = -1;
    public int exp { get; protected set; } = 0;
    public int addExp { get; protected set; } = -1;
    public int lv { get; protected set; } = -1;


    public int speed { get; protected set; } = -1;
    protected enum CharacterState {
        Invalid = -1,
        Field,
        Battle,
    }
    public virtual void Initilized(int _ID,int _masterID) {
        ID = _ID;
        masterID = _masterID;
        var characterMaster = CharacterMasterUtility.GetCharacterMaster(ID);
        SetMaster(characterMaster);
    }

    private void SetMaster(Entity_CharacterStatus.Param _param) {

        SetMaxHP(_param.MaxMP);
        SetHP(_param.HP);
        SetMaxMP(_param.MP);
        SetMP(_param.MP);
        SetRawAttack(_param.Attack);
        SetRawDefence(_param.Defense);
        SetCriticalPower(_param.CriticalPower);
        SetCriticalRatio(_param.CriticalRatio);
        SetSpeed(_param.Speed);
        exp = 0;
        addExp = SetAddExp(_param.MaxAddExp);

        SetLv(_param.Lv);
    }
    public abstract bool IsPlayer();

    public void SetMaxHP(int _maxHP) {
        MaxHP = _maxHP;
    }
    public void SetHP(int _HP) {
        HP = _HP;
    }public void SetMaxMP(int _maxMP) {
        MaxMP = _maxMP;
    }

    public void RemoveHP(int _damage) {
        SetHP(HP - _damage);
    }
    public void SetMP(int _MP) {
        MP = _MP;
    }public void SetRawAttack(int _attack) {
        rawAttack = _attack;
    }
    public void SetRawDefence(int _defence) {
        rawDefence = _defence;
    }public void SetCriticalRatio(int _ratio) {
        criticalRatio = _ratio;
    }
    public void SetCriticalPower(int _power) {
        criticalPower = _power;
    }
    public void SetSpeed(int _speed) {
        speed = _speed;
    }
    public virtual async UniTask AddExp(int _addExp) {
        exp += _addExp;
        await UniTask.CompletedTask;
    }

    public int SetAddExp(int _exp) {
        int addExp = Random.Range(_exp - 3, _exp + 1);
        if (addExp <= 1)
            addExp = 1;

        return addExp;
    }

    public void SetLv(int _lv,int _ratio = 1) {
        lv = _lv * _ratio;
    }

    public int GetID() {
        return ID;
    }

    public int GetTarget() {
        return ID;
    }

    public void SetIsSelect(bool _isSelect) {
        isSelect = _isSelect;
        Debug.Log("select!! : " + isSelect);
    }
}
