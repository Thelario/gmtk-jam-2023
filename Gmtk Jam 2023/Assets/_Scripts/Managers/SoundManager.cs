using UnityEngine;
using System.Collections.Generic;

namespace Game.Managers
{
    [RequireComponent(typeof(AudioSource))]
    public class SoundManager : Singleton<SoundManager>
    {
        [SerializeField] private SoundAudioClip[] soundAudioClipArray;
        private Dictionary<SoundType, SoundAudioClip> _audioClipDictionary;

        [SerializeField] private float pitch;

        [SerializeField] private AudioSource musicAudioSource;
        [SerializeField] private AudioSource sfxAudioSource;

        private Dictionary<SoundType, float> _soundTimerDictionary;

        [SerializeField] private AudioClip backgroundMusicOne;
        [SerializeField] private AudioClip backgroundMusicTwo;

        protected override void Awake()
        {
            base.Awake();

            Initialize();
        }

        private void Initialize()
        {
            _soundTimerDictionary = new Dictionary<SoundType, float>
            {
                [SoundType.PlayerWalk] = 0f
            };

            _audioClipDictionary = new Dictionary<SoundType, SoundAudioClip>();
            foreach (SoundAudioClip sac in soundAudioClipArray)
                _audioClipDictionary.Add(sac.sound, sac);
            
            ChangeMusicAudioclip(false);
        }

        public void ChangeMusicAudioclip(bool inGame)
        {
            musicAudioSource.clip = inGame ? backgroundMusicOne : backgroundMusicTwo;
            musicAudioSource.Play();
        }
        
        public void ChangeMusicVolume()
        {
            //musicAudioSource.volume = OptionsManager.Instance.MasterVolume * OptionsManager.Instance.MusicVolume * OptionsManager.Instance.MusicVolume;
            if (musicAudioSource.volume < 0.001)
                musicAudioSource.volume = 0f;
        }

        public void PlaySound(SoundType st)
        {
            if (!CanPlaySound(st)) 
                return;
            
            sfxAudioSource.pitch = Random.Range(pitch - 0.1f, pitch + 0.1f);
            SoundAudioClip sac = SearchSound(st);
            sfxAudioSource.PlayOneShot(sac.audioClip, sac.volumeMultiplier);
        }
        
        public void PlaySound(SoundType st, float volumeMultiplier)
        {
            if (!CanPlaySound(st)) 
                return;
            
            sfxAudioSource.pitch = Random.Range(pitch - 0.1f, pitch + 0.1f);
            SoundAudioClip sac = SearchSound(st);
            sfxAudioSource.PlayOneShot(sac.audioClip, sac.volumeMultiplier * volumeMultiplier);
        }

        private bool CanPlaySound(SoundType sound)
        {
            switch (sound)
            {
                default:
                    return true;
                case SoundType.PlayerWalk:
                    if (_soundTimerDictionary.ContainsKey(sound))
                    {
                        float lastTimePlayed = _soundTimerDictionary[sound];
                        const float playerMoveTimerMax = .4f;
                        if (!(lastTimePlayed + playerMoveTimerMax < Time.time))
                            return false;
                        
                        _soundTimerDictionary[sound] = Time.time;
                        return true;
                    }
                    else return true;
            }
        }

        private SoundAudioClip SearchSound(SoundType st)
        {
            _audioClipDictionary.TryGetValue(st, out SoundAudioClip outP);
            return outP;
        }
    }

    [System.Serializable]
    public class SoundAudioClip
    {
        public SoundType sound;
        public AudioClip audioClip;
        public float volumeMultiplier;
    }

    public enum SoundType
    {
        PlayerWalk, DejarItem, EnntregarItem, FuegoHorno, FuegoHornoTerminado, Guillotina, 
        RecogerItem, Blop
    }
}
