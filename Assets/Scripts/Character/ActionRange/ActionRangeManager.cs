using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using static CommonModule;
public class ActionRangeManager{
    private static List<ActionRangeBase> actionRangeList = null;
    /// <summary>
    /// ‰Šú‰»
    /// </summary>
    public static void Initialize() {
        actionRangeList = new List<ActionRangeBase>();
        //s“®”ÍˆÍ‚ğ—…—ñ
        actionRangeList.Add(new ActionRange001_Solo());
    }

    /// <summary>
    /// s“®”ÍˆÍ‚Ìæ“¾
    /// </summary>
    /// <param name="_rangeType"></param>
    /// <returns></returns>
    public static ActionRangeBase GetRangeBase(int _rangeType) {
        if (!IsEnableIndex(actionRangeList, _rangeType)) return null;

        return actionRangeList[_rangeType];
    }
}
