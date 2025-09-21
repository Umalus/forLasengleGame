using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIBattle : UIBase{
    public Button normalAttackButton = null;

    public override async UniTask Open() {
        await base.Open();

        BattlePlayer.normalAttackButton = normalAttackButton;
    }

    public override async UniTask Close() {
        await base.Close();
    }
}
