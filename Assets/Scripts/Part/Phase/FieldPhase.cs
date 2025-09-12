using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class FieldPhase : BasePhase
{
    
    [SerializeField]
    private FloorManager floorManager = null;

    public override async UniTask Setup() {
        await base.Setup();
        await floorManager.Setup();
    }


    public override async UniTask Initialize() {
        await base.Initialize();
        if (floorManager == null) return;
       
        await floorManager.Initialize();

    }

    public override async UniTask Execute() {
        gameObject.SetActive(true);
        await FadeManager.instance.FadeIn();
        await floorManager.Execute();

    }
    
}
