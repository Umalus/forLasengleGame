using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillDataUtility : MonoBehaviour
{
    public static Entity_SkillData.Param GetSkillData(int _ID) {
        var skillDataList = MasterDataManager.skillData[0];
        for (int i = 0 ,max = skillDataList.Count;i < max;i++) {
            if (skillDataList[i].ID == _ID) continue;
            //ID��v�Ńf�[�^���擾
            return skillDataList[_ID];

        }
        return null;
    }
}
