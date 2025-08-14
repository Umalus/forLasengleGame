using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageObject : MonoBehaviour
{
    public bool isBreak = false;
    [SerializeField]
    private Vector3 startPos;
    // Update is called once per frame
    public async UniTask SetUp() {
        transform.position = startPos;

        await UniTask.CompletedTask;
    }
}
