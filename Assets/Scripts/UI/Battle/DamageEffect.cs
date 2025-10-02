using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;

public class DamageEffect : MonoBehaviour
{
    //　DamageUIプレハブ
    [SerializeField]
    private GameObject damageUI;

    public async Task Damage(Collider _col,float _damage) {
        var damageText = damageUI.GetComponentInChildren<TextMeshProUGUI>();
        damageText.text = _damage.ToString();
        //　DamageUIをインスタンス化。登場位置は接触したコライダの中心からカメラの方向に少し寄せた位置
        Instantiate(damageUI, _col.bounds.center - Camera.main.transform.forward * 1.0f, Quaternion.identity);
        await UniTask.DelayFrame(100);
    }
}
