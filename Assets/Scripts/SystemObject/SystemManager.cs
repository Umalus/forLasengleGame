using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SystemManager : SystemObject
{
    /// <summary>
    /// ���̃Q�[���ŊǗ�����V�X�e���I�u�W�F�N�g�Q
    /// </summary>
    [SerializeField]
    private SystemObject[] systemObjectList = null;
    void Start()
    {
        UniTask task = Initialize();
    }

    /// <summary>
    /// ����������
    /// </summary>
    /// <returns></returns>
    public override async UniTask Initialize() {
        //for����systemObject�𕡐�
        for(int i = 0, max = systemObjectList.Count(); i < max; i++) {
            SystemObject origin = systemObjectList[i];
            if (origin == null) continue;
            //���������I�u�W�F�N�g�̏�����
            SystemObject createObject = Instantiate(origin, transform);
            await createObject.Initialize();
        }
        //�X�^���o�C�p�[�g�ɑJ��

        UniTask task = PartManager.instance.TransitionPart(GameEnum.eGamePart.Standby);
    }
    
}
