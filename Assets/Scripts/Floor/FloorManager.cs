using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using static CommonModule;
using static GameEnum;

public class FloorManager : MonoBehaviour{
    public static FloorManager instance = null;

    private eFloorEndReason endReason = eFloorEndReason.Invalid;
    [SerializeField]
    private List<StageObject> stageObject = null;
    [SerializeField]
    private Transform ParentRoot = null;

    public async UniTask Initialize() {
        instance = this;

        //ステージ上のオブジェクトが無ければ
        if (IsEmpty(stageObject))
            return;
        List<UniTask> tasks = new List<UniTask>();
        for (int i = 0, max = stageObject.Count; i < max; i++) {
            //もし壊されている(倒されている)なら処理を続ける
            if (stageObject[i].isBreak) continue;

            //ステージ上のオブジェクトを生成
            StageObject createObject = Instantiate(stageObject[i]);
            tasks.Add(createObject.SetUp(ParentRoot));
        }
        await WaitTask(tasks);
    }

    public async UniTask Execute() {
        while (endReason == eFloorEndReason.Door) {
            await UniTask.CompletedTask;
        }

        await UniTask.CompletedTask;
    }
}
