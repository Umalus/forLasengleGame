using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class EndingPart : BasePart
{
    public override async UniTask Execute() {
        await UniTask.CompletedTask;
    }
}
