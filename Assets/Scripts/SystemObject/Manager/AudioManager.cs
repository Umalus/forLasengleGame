using Cysharp.Threading.Tasks;
using System.Threading;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using static CommonModule;

public class AudioManager : SystemObject {
    [SerializeField]
    private AudioSource bgmSource = null;
    [SerializeField]
    private List<AudioSource> seSource = null;


    [SerializeField]
    private BGMAssign bgmAssigner = null;
    [SerializeField]
    private SEAssign seAssigner = null;

    public static AudioManager instance { get; private set; } = null;

    private CancellationToken token;
    public override async UniTask Initialize() {
        instance = this;

        token = this.GetCancellationTokenOnDestroy();

        await UniTask.CompletedTask;
    }

    public void PlayBGM(int _index) {
        if (IsEnableIndex(bgmAssigner.BGMList, _index)) return;

        bgmSource.clip = bgmAssigner.BGMList[_index];
        bgmSource.Play();
    }

    public void StopBGM() {
        bgmSource.Stop();
    }

    /// <summary>
	/// SE再生
	/// </summary>
	/// <param name="seID"></param>
	public async UniTask PlaySE(int seID) {
        if (!IsEnableIndex(seAssigner.SEList, seID)) return;
        // 再生中でないオーディオソースを探してそれで再生
        for (int i = 0, max = seSource.Count; i < max; i++) {
            AudioSource audioSource = seSource[i];
            if (audioSource == null ||
                audioSource.isPlaying) continue;
            // 再生中でないオーディオソースが見つかったので再生
            audioSource.clip = seAssigner.SEList[seID];
            audioSource.Play();
            // SEの終了待ち

            while (audioSource.isPlaying) await UniTask.DelayFrame(1, PlayerLoopTiming.Update, token);

            return;
        }
    }
}
