using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SystemObject : MonoBehaviour{

    /// <summary>
    /// ����������
    /// </summary>
    /// <returns></returns>
    public abstract UniTask Initialize();
    
}
