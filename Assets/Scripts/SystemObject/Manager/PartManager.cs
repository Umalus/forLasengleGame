﻿using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static GameEnum;
using static CommonModule;

public class PartManager : SystemObject
{
    //自身のインスタンス
    public static PartManager instance = null;
    //パートのオリジナル
    [SerializeField] private BasePart[] partOrigin = null;
    //パートの配列
    private BasePart[] partList = null;
    //現在のパート
    private BasePart currentPart = null;

    /// <summary>
    /// 初期化関数
    /// </summary>
    /// <returns></returns>
    public override async UniTask Initialize() {
        //インスタンスの自身を設定
        instance = this;
        
        //配列をパートの数分容量確保
        int partMax = (int)eGamePart.PartMax;
        partList = new BasePart[partMax];
        //非同期処理のリストに各パート初期化関数を積む
        List<UniTask> tasks = new List<UniTask>();
        for(int i = 0; i < partMax; i++) {
            partList[i] = Instantiate(partOrigin[i], transform);
            tasks.Add(partList[i].Init());
        }
        //一気に処理
        await WaitTask(tasks);
    }
    /// <summary>
    /// パート変更処理
    /// </summary>
    /// <param name="_nextPart"></param>
    /// <returns></returns>
    public async UniTask TransitionPart(eGamePart _nextPart) {
        //現在のパートの片付け
        if (currentPart != null) {
            await currentPart.Teardown();
        }
        //次のパートに移動
        currentPart = partList[(int)_nextPart];
        await currentPart.Setup();

        //実行処理
        UniTask task = currentPart.Execute();
    }

}
