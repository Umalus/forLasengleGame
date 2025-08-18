using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEditor.PackageManager;
using UnityEngine;

public class InputSystemManager : SystemObject
{
    public static InputSystemManager instance = null;

    public Action inputActions = null;

    public override async UniTask Initialize() {
        instance = this;
        inputActions = new Action();

        await UniTask.CompletedTask;
    }

#if UNITY_EDITOR
    private void Awake() {
        instance = this;
        inputActions = new Action();
    }
#endif
}
