using Cysharp.Threading.Tasks;
using UnityEngine;

public class StageObject : MonoBehaviour
{
    public bool isBreak = false;
    private Transform respawnPos;
    // Update is called once per frame
    public async UniTask SetUp(Transform _setParent) {
        transform.SetParent(_setParent);
        await UniTask.CompletedTask;
    }
    public async UniTask SetUp(Transform _setParent, Transform _spawnPos) {
        transform.SetParent(_setParent);

        transform.position = _spawnPos.position;

        await UniTask.CompletedTask;
    }
    

    public void SetRespawnPosition(Transform _pos) {
        respawnPos = _pos;
    }
}
