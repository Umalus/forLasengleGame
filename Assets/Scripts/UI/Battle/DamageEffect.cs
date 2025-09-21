using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class DamageEffect : MonoBehaviour
{
    //　DamageUIプレハブ
    [SerializeField]
    private GameObject damageUI;

    public async Task Damage(Collider col) {
        //　DamageUIをインスタンス化。登場位置は接触したコライダの中心からカメラの方向に少し寄せた位置
        var obj = Instantiate<GameObject>(damageUI, col.bounds.center - Camera.main.transform.forward * 0.2f, Quaternion.identity);
        await UniTask.DelayFrame(100);
    }
}
