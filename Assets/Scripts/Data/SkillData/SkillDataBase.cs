using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// スキルデータの基底　
/// </summary>
[CreateAssetMenu(fileName = "SkillData", menuName = "RPG/Skill")]
public class SkillDataBase : ScriptableObject
{
    [Header("基本情報")]
    public string skillName;
    public string description;
    public Sprite icon;

    [Header("数値パラメータ(威力は%換算される)")]
    public int power;
    public int needMP;
    public int range;
}
