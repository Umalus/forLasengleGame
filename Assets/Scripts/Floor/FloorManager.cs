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

        //�X�e�[�W��̃I�u�W�F�N�g���������
        if (IsEmpty(stageObject))
            return;
        List<UniTask> tasks = new List<UniTask>();
        for (int i = 0, max = stageObject.Count; i < max; i++) {
            //�����󂳂�Ă���(�|����Ă���)�Ȃ珈���𑱂���
            if (stageObject[i].isBreak) continue;

            //�X�e�[�W��̃I�u�W�F�N�g�𐶐�
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
