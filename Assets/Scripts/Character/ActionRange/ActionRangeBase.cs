using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ActionRangeBase{
    public List<int> targetIDList = null;

    public abstract void Execute(BattleCharacterBase _sourceCharacter);
}
