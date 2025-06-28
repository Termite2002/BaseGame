using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;

namespace Termite.BaseGame
{
    public class SoundController : MonoBehaviour, IController
    {
        private List<AudioSource> m_ListAudioSFX = new List<AudioSource>();
        private List<AudioSource> m_ListAudioBGM = new List<AudioSource>();
        public GameObject audioPrefab;

        public void LocateController()
        {
            Controllers.Add(this);
        }

        public bool IsActive()
        {
            return gameObject.activeSelf;
        }

        public void OnEventRaise(string gameEvent, EventParam param)
        {
            switch (gameEvent)
            {
                //case EventList.GamePlay.ON_MUSIC_SETTING:
                //    bool musicState = PlayerData.isMusicOn;
                //    SetMuteAllBGM(!musicState);
                //    break;
            }
        }

        public void BehaviourAwake()
        {
        }

        public void BehaviourStart()
        {

        }

        AudioSource CreateAudioSFX()
        {
            var obj = Instantiate(audioPrefab, this.transform);
            var audio = obj.GetComponent<AudioSource>();
            m_ListAudioSFX.Add(audio);
            return audio;
        }

        AudioSource GetSourceSFX(AudioInfo info, bool isOnlyOne = false)
        {
            var audio = m_ListAudioSFX.Find(x => isOnlyOne ? x.clip == info.clip : !x.isPlaying);
            if (audio == null)
            {
                audio = CreateAudioSFX();
            }
            return audio;
        }

        void PlaySFX(AudioSource source)
        {
            //if (!PlayerData.isSoundOn) return;
            source.Play();
        }

        public void PlaySoundSFX(AudioInfo info, float volume = 1f)
        {
            var source = GetSourceSFX(info, false);
            source.clip = info.clip;
            source.loop = info.isLoop;
            source.volume = volume;
            PlaySFX(source);
        }
        public void SetMuteAllBGM(bool isMute)
        {
            m_ListAudioBGM.ForEach(x => x.mute = isMute);
        }

        public void StopBGM(AudioInfo info, out bool playSuccess, System.Action endAction = null)
        {
            playSuccess = false;
            bool localSuccess = false;
            m_ListAudioBGM.ForEach(x =>
            {
                if (x.clip == info.clip)
                {
                    localSuccess = true;
                    x.DOFade(0f, 2f).OnComplete(() =>
                    {
                        x.Stop();
                        endAction?.Invoke();
                    });
                }
            });

            playSuccess = localSuccess;
        }
        public void PlayBGM(AudioInfo info, bool isReplay = false, float volume = 1f)
        {
            var audio = GetSourceBGM(info, true);
            if (audio.isPlaying && !isReplay) return;
            if (isReplay)
            {
                audio.Stop();
            }
            audio.clip = info.clip;
            audio.loop = info.isLoop;
            //audio.volume = volume;

            audio.volume = 0f;
            audio.DOFade(volume, 3.5f).SetEase(Ease.InQuad);

            PlayBGM(audio);
        }
        AudioSource GetSourceBGM(AudioInfo info, bool isOnlyOne = false)
        {
            var audio = m_ListAudioBGM.Find(x => isOnlyOne ? x.clip == info.clip : !x.isPlaying);
            if (audio == null)
            {
                audio = CreateAudioBGM();
            }
            return audio;
        }
        AudioSource CreateAudioBGM()
        {
            var obj = Instantiate(audioPrefab, this.transform);
            var audio = obj.GetComponent<AudioSource>();
            m_ListAudioBGM.Add(audio);
            return audio;
        }
        void PlayBGM(AudioSource source)
        {
            //if (!PlayerData.isMusicOn) return;
            source.Play();
        }
    }
    public struct AudioInfo
    {
        public AudioClip clip;
        public bool isLoop;

        public AudioInfo(AudioClip clip, bool isLoop)
        {
            this.clip = clip;
            this.isLoop = isLoop;
        }
        public AudioInfo(AudioClip[] clips, bool isLoop)
        {
            this.clip = clips[UnityEngine.Random.Range(0, clips.Length)];
            this.isLoop = isLoop;
        }
    }
}

