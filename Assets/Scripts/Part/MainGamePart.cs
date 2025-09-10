using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using UnityEngine;

using static GameEnum;
using static CommonModule;

public class MainGamePart : BasePart {

    private static eGamePhase phase = eGamePhase.Invalid;
    [SerializeField]
    private List<BasePhase> phaseOrigin = null;

    private static BasePhase[] basePhases = null;
    public override async UniTask Init() {
        await base.Init();
        basePhases = new BasePhase[(int)eGamePhase.PhaseMax];

        for (int i = 0, max = phaseOrigin.Count; i < max; i++) {
            basePhases[i] = Instantiate(phaseOrigin[i], transform);
        }

        await UniTask.CompletedTask;
    }
    public override async UniTask Setup() {
        await base.Setup();
        await basePhases[(int)eGamePhase.Field].Setup();
        await ChangeGamePhase(eGamePhase.Field);
        
        await UniTask.CompletedTask;
    }

    public override async UniTask Execute() {
        await FadeManager.instance.FadeIn();
        await UniTask.CompletedTask;
    }

    public override async UniTask Teardown() {
        await base.Teardown();
        for (int i = 0, max = basePhases.Length; i < max; i++) {
            Destroy(basePhases[i]);
        }
        await UniTask.CompletedTask;
    }

    public static async UniTask ChangeGamePhase(eGamePhase _nextPhase) {
        phase = _nextPhase;
        switch (phase) {
            case eGamePhase.Field:
                await basePhases[(int)eGamePhase.Battle].Teardown();
                await basePhases[(int)eGamePhase.Field].Initialize();
                break;
            case eGamePhase.Battle:
                await basePhases[(int)eGamePhase.Field].Teardown();
                await basePhases[(int)eGamePhase.Battle].Initialize();
                break;
        }
        await FadeManager.instance.FadeOut();

        await basePhases[(int)phase].Execute();
    }
}
