using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectManager : SystemObject
{
    public static EffectManager instance = null;

    public Vector3 offset = Vector3.up;
    public List<Transform> effectRoots { get; private set; } = null;

    [SerializeField]
    private Transform unuseRoot = null;
    [SerializeField]
    private List<GameObject> effectList = null;

    private List<GameObject> copyEffects = null;
    public override async UniTask Initialize() {
        instance = this;

        //プールするエフェクトを複製する
        copyEffects = new List<GameObject>();
        for(int i = 0,max = effectList.Count; i < max; i++) {
            copyEffects.Add(Instantiate(effectList[i],unuseRoot));
        }
        await UniTask.CompletedTask;
    }

    public async UniTask ExecuteEffect(int _effectIndex, Transform _firePoint) {
        GameObject instanceEffect = Instantiate(copyEffects[_effectIndex], _firePoint);
        await UniTask.DelayFrame(1);
    }
}
