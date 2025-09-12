using Cysharp.Threading.Tasks;
using UnityEngine;

public class StageObject : MonoBehaviour
{
    public bool isBreak = false;
    [SerializeField]
    private Vector3 respawnPos;
    // Update is called once per frame
    public async UniTask SetUp(Transform _setParent) {
        transform.SetParent(_setParent);

        transform.position = respawnPos;
        
        await UniTask.CompletedTask;
    }

    public void SetRespawnPosition(Vector3 _pos) {
        respawnPos = _pos;
    }
}
