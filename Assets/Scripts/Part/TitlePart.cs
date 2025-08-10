using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class TitlePart : BasePart {
    public override async UniTask Execute() {
        UniTask task = PartManager.instance.TransitionPart(GameEnum.eGamePart.Select);
        await UniTask.CompletedTask;
    }
}
