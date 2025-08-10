using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;

public class CommonModule{

    /// <summary>
    /// ���X�g���󂩔���
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="_list"></param>
    /// <returns></returns>
    public static bool IsEmpty<T>(List<T> _list) {
        //�Z���]���Ȃ̂ő��v
        return _list == null || _list.Count <= 0;
    }
    public static bool IsEmpty<T>(T[] _array) {
        //�Z���]���Ȃ̂ő��v
        return _array == null || _array.Length <= 0;
    }

    /// <summary>
    /// ���X�g�ɑ΂��ėL���ȃC���f�b�N�X������
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="_list"></param>
    /// <param name="_index"></param>
    /// <returns></returns>
    public static bool IsEnableIndex<T>(List<T> _list, int _index) {
        //��
        if (IsEmpty(_list)) return false;

        return _index >= 0 && _list.Count > _index;
    }
    public static bool IsEnableIndex<T>(T[] _array, int _index) {
        //��
        if (IsEmpty(_array)) return false;

        return _index >= 0 && _array.Length > _index;
    }
    public static async UniTask WaitTask(List<UniTask> _taskList) {
        //�I�������^�X�N�����X�g���珜���A���X�g����ɂȂ�܂ő҂�
        while (true) {
            for (int i = _taskList.Count - 1; i >= 0; i--) {
                //�^�X�N���I�������烊�X�g���甲��
                if (!_taskList[i].Status.IsCompleted()) continue;

                _taskList.RemoveAt(i);
            }
            //�������X�g����Ȃ烋�[�v�𔲂���
            if (IsEmpty(_taskList)) break;
            //1�t���[���҂�
            await UniTask.DelayFrame(1);
        }
    }
}
