using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

/// <summary>
/// スキルデータの基底　
/// </summary>
public abstract class SkillDataBase : ScriptableObject {
    public enum TargetType {
        EnemySingle,
        EnemyAll,
        AllySingle,
        AllyAll,
        RandomEnemy,
        RangeEnemy
    }



    [Header("基本情報")]
    public string skillName;
    public string description;
    public Sprite icon;

    [Header("数値パラメータ")]
    [Tooltip("ID")]
    public int ID;
    [Tooltip("威力(%)")]
    public int power;
    [Tooltip("必要レベル"), Range(0, 10)]
    public int requiredLevel;
    [Tooltip("必要MP")]
    public int needMP;
    [Tooltip("対象の数")]
    public TargetType targetType;

    [System.NonSerialized]
    public const float RATIO = 0.01f;

    public abstract UniTask Execute(IEnumerable<BattleCharacterBase> _target,BattleCharacterBase _source,CancellationToken _token);
}
