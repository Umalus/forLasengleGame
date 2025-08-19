using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class FieldPhase : BasePhase
{
    public override async UniTask Initialize() {
        await base.Initialize();
    }

    public override async UniTask Execute() {
        gameObject.SetActive(true);

        await UniTask.CompletedTask;
    }
}
