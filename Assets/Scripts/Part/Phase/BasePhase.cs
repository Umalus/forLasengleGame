using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BasePhase : MonoBehaviour
{
    public virtual async UniTask Initialize() {

        await UniTask.CompletedTask;
    }

    public abstract UniTask Execute();

    public virtual async UniTask Teardown() {

        await UniTask.CompletedTask;
    }
}
