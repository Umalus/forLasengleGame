using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using static CommonModule;
public class ActionRangeManager{
    private static List<ActionRangeBase> actionRangeList = null;
    /// <summary>
    /// ������
    /// </summary>
    public static void Initialize() {
        actionRangeList = new List<ActionRangeBase>();
        //�s���͈͂𗅗�
        actionRangeList.Add(new ActionRange001_Solo());
    }

    /// <summary>
    /// �s���͈͂̎擾
    /// </summary>
    /// <param name="_rangeType"></param>
    /// <returns></returns>
    public static ActionRangeBase GetRangeBase(int _rangeType) {
        if (!IsEnableIndex(actionRangeList, _rangeType)) return null;

        return actionRangeList[_rangeType];
    }
}
