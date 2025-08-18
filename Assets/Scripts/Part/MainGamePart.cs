using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using UnityEngine;

using static GameEnum;

public class MainGamePart : BasePart {

    private eGamePhase phase = eGamePhase.Invalid;
    [SerializeField]
    private List<BasePhase> phaseOrigin = null;
    public override async UniTask Init() {
        await base.Init();
        await UniTask.CompletedTask;
    }
    public override async UniTask Setup() {
        await base.Setup();
        await UniTask.CompletedTask;
    }

    public override async UniTask Execute() {
        await UniTask.CompletedTask;
    }

    public override async UniTask Teardown() {
        await base.Teardown();
        await UniTask.CompletedTask;
    }

    public async void ChangeGamePhase(eGamePhase _nextPhase) {
        phase = _nextPhase;
        switch (phase) {
            case eGamePhase.Field:
                await phaseOrigin[(int)eGamePhase.Battle].Teardown();
                await phaseOrigin[(int)eGamePhase.Field].Initialize();
                break;
            case eGamePhase.Battle:
                await phaseOrigin[(int)eGamePhase.Field].Teardown();
                await phaseOrigin[(int)eGamePhase.Battle].Initialize();
                break;
        }
    }
}
