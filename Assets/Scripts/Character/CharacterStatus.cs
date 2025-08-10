/*
 * @class   CharacterStatus
 * @brief   キャラクターのデータ(ステータス)
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStatus{
    private int HP = -1;
    private int MaxHP = -1;
    private int MP = -1;
    private int MaxMp = -1;
    private float power = -1.0f;
    private float defence = -1.0f;

    public void SetHP(int _value) {
        HP = _value;
    }
    public void SetMP(int _value) {
        MP = _value;
    }
}
