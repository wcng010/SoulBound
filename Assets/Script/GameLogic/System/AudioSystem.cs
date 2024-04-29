using System;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class AudioSystem: HungrySingleton<AudioSystem>
{
        [SerializeField] [FoldoutGroup("BGM")] public AudioSource bgm1;
        [SerializeField] [FoldoutGroup("BGM")] public AudioSource bgm2;
        [SerializeField] [FoldoutGroup("BGM")] public AudioSource bgm3;
        [SerializeField] [FoldoutGroup("BGM")] public AudioSource endBgm;
        [SerializeField] [FoldoutGroup("Level1")] private AudioSource boxJumpAudio;
        [SerializeField] [FoldoutGroup("Level1")] private AudioSource boxFallAudio;
        [SerializeField] [FoldoutGroup("Level2")] private AudioSource windowAudio;
        [SerializeField] [FoldoutGroup("Level2")] private AudioSource clockAudio;
        [SerializeField] [FoldoutGroup("Level3")] private AudioSource hammerAudio;
        [SerializeField] [FoldoutGroup("Level3")] private AudioSource fanAudio;
        [SerializeField] [FoldoutGroup("Level3")] private AudioSource openDoorAudio;
        [SerializeField] [FoldoutGroup("Level3")] private AudioSource stoolBreakAudio;
        [SerializeField] [FoldoutGroup("PlayerAudio")] private AudioSource absorbBeginAudio;
        [SerializeField] [FoldoutGroup("PlayerAudio")] private AudioSource absorbLoopAudio;
        [SerializeField] [FoldoutGroup("PlayerAudio")] private AudioSource changeAudio;
        [SerializeField] [FoldoutGroup("Other")] private AudioSource fileBookAuido;
        public void BoxJumpAudPlay () => boxJumpAudio.Play();
        public void BoxFallAudioPlay () => boxFallAudio.Play();
        public void WindowAudioPlay () => windowAudio.Play();
        public void ClockAudioPlay () => clockAudio.Play();
        public void HammerAudioPlay() => hammerAudio.Play();
        public void FanAudioPlay() => fanAudio.Play();
        public void OpenDoorAudioPlay() => openDoorAudio.Play();
        public void StoolBreakAudioPlay() => stoolBreakAudio.Play();
        public void AbsorbBeginAudioPlay() => absorbBeginAudio.Play();
        public void AbsorbLoopAudioPlay() => absorbLoopAudio.Play();
        public void AbsorbLoopAudioStop() => absorbLoopAudio.Stop();
        public void ChangeAudioPlay() => changeAudio.Play();
        public void FileBookAuidoPlay() => fileBookAuido.Play();

        private bool ischange;
        private void Start()
        {
                if (SceneManager.GetActiveScene().buildIndex == 0)
                {
                        endBgm.Stop();
                        bgm1.Play();
                        bgm2.Play();
                        bgm3.Play();
                }
        }

        
        private void Update()
        {
                if (!ischange && SceneManager.GetActiveScene().buildIndex == 4)
                {
                        bgm1.Stop();
                        bgm2.Stop();
                        bgm3.Stop();
                        endBgm.Play();
                        ischange = true;
                }
        }
}
