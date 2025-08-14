using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using static CommonModule;
using static GameEnum;

public class FloorManager : MonoBehaviour{
    private eFloorEndReason endReason = eFloorEndReason.Invalid;
    [SerializeField]
    private List<StageObject> stageObject = null;
    public async UniTask Initialize() {
        //�X�e�[�W��̃I�u�W�F�N�g���������
        if (IsEmpty(stageObject))
            return;
        List<UniTask> tasks = new List<UniTask>();
        for (int i = 0, max = stageObject.Count; i < max; i++) {
            //�����󂳂�Ă���(�|����Ă���)�Ȃ�
            if (stageObject[i].isBreak) continue;

            tasks.Add(stageObject[i].SetUp());
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
