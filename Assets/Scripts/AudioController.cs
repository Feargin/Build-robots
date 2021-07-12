using UnityEngine;

namespace EnglishKids.BuildRobots
{
    public sealed class AudioController : Singleton<AudioController>
    {
        [SerializeField] private AudioClip[] _audioClips;
        [SerializeField] private AudioSource[] _audioSources;


        public void PlayAudioClipEffect(int indexClip, int indexSource)
        {
            if (_audioSources[indexSource].isPlaying && indexSource == 2) return;
            _audioSources[indexSource].PlayOneShot(_audioClips[indexClip], 1f);
        }

        public void PlayAudioClipEffect(AudioClip clip)
        {
            if (_audioSources[2].isPlaying) return;
            _audioSources[2].clip = clip;
            _audioSources[2].volume = 0.5f;
            _audioSources[2].Play();
        }
    }
}