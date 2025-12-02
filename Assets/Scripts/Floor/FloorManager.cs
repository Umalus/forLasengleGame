using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using UnityEngine;
using static CommonModule;
using static GameEnum;

public class FloorManager : MonoBehaviour{
    public static FloorManager instance = null;

    private eFloorEndReason endReason = eFloorEndReason.Invalid;
    [SerializeField]
    private List<StageObject> stageObject = null;

    private List<StageObject> copyObjects = null;

    private const int maxStageObject = 5;
    [SerializeField]
    private Transform ParentRoot = null;
    [SerializeField]
    private Transform DeadRoot = null;
    [SerializeField]
    private List<Transform> spawnPositions;

    public async UniTask Setup() {
        instance = this;

        //ステージ上のオブジェクトが無ければ
        if (IsEmpty(stageObject))
            return;
        copyObjects = new List<StageObject>();
        for (int i = 0; i < stageObject.Count; i++) {
            //ステージ上のオブジェクトを生成
            copyObjects.Add(Instantiate(stageObject[i]));
        }
        await UniTask.CompletedTask;
    }

    public async UniTask Initialize() {
        List<UniTask> tasks = new List<UniTask>();

        for(int i = 0,max = copyObjects.Count;i < max; i++) {
            if (copyObjects[i].isBreak) {
                tasks.Add(copyObjects[i].SetUp(DeadRoot));
                continue;
            }
            tasks.Add(copyObjects[i].SetUp(ParentRoot));
        }
        await WaitTask(tasks);
    }

    public async UniTask Execute() {
        

        while (endReason == eFloorEndReason.Door) {
            for (int i = 0; i < maxStageObject; i++) {

            }

            await UniTask.CompletedTask;
        }
    }
}
