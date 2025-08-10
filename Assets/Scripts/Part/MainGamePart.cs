using Cysharp.Threading.Tasks;
using UnityEngine;

public class MainGamePart : BasePart {

    [SerializeField]
    private static GameObject MainMap = null;

    public override async UniTask Init() {
        await base.Init();
        await UniTask.CompletedTask;
    }
    public override async UniTask Setup() {
        await base.Setup();
        await UniTask.CompletedTask;
    }

    public override async UniTask Execute() {
        await FadeManager.instance.FadeIn();
        await UniTask.CompletedTask;
    }

    public override async UniTask Teardown() {
        await base.Teardown();
        await UniTask.CompletedTask;
    }
}
