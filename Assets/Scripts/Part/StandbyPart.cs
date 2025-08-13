using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class StandbyPart : BasePart
{
    public override async UniTask Execute() {

        UniTask task = PartManager.instance.TransitionPart(GameEnum.eGamePart.Title);
        await UniTask.CompletedTask;
    }
}
