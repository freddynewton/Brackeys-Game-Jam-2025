using FMOD.Studio;
using FMODUnity;
using UnityEngine;

public class SoundManager : Singleton<SoundManager>
{
    private EventInstance _playerFootstepsInstance;
    private EventInstance _levelMusicInstance;

    public void PlayHitSound()
    {
        RuntimeManager.PlayOneShot("event:/Hit sound");
    }

    public void PlayBroomSwoosh()
    {
        RuntimeManager.PlayOneShot("event:/Broom Swoosh");
    }

    public void PlayAmbientGroan(GameObject zombie)
    {
        EventInstance ambientGroanInstance = RuntimeManager.CreateInstance("event:/Ambient Groan");
        ambientGroanInstance.set3DAttributes(RuntimeUtils.To3DAttributes(zombie));
        ambientGroanInstance.start();
        ambientGroanInstance.release();
    }

    public void StopAmbientGroan(EventInstance ambientGroanInstance)
    {
        ambientGroanInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        ambientGroanInstance.release();
    }

    public void PlayKillshot()
    {
        RuntimeManager.PlayOneShot("event:/Killshot");
    }

    public void PlayNormalGrowl(GameObject zombie)
    {
        EventInstance normalGrowlInstance = RuntimeManager.CreateInstance("event:/Normal Growl");
        normalGrowlInstance.set3DAttributes(RuntimeUtils.To3DAttributes(zombie));
        normalGrowlInstance.start();
        normalGrowlInstance.release();
    }

    public void PlayPlayerFootsteps()
    {
        if (_playerFootstepsInstance.isValid())
        {
            _playerFootstepsInstance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
            _playerFootstepsInstance.release();
        }

        _playerFootstepsInstance = RuntimeManager.CreateInstance("event:/Player footsteps");
        _playerFootstepsInstance.setVolume(0.5f); // Set volume to half
        _playerFootstepsInstance.start();
    }

    public void StopPlayerFootsteps()
    {
        if (_playerFootstepsInstance.isValid())
        {
            _playerFootstepsInstance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
            _playerFootstepsInstance.release();
        }
    }

    public void PlayZombieAttack()
    {
        RuntimeManager.PlayOneShot("event:/Zombie attack");
    }

    public void PlayLevelMusic()
    {
        _levelMusicInstance = RuntimeManager.CreateInstance("event:/LvlMusic1");
        _levelMusicInstance.start();
    }

    public void SetLevelWon()
    {
        if (_levelMusicInstance.isValid())
        {
            _levelMusicInstance.setParameterByName("Level Won", 1);
        }
    }

    public void StopLevelMusic()
    {
        if (_levelMusicInstance.isValid())
        {
            _levelMusicInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
            _levelMusicInstance.release();
        }
    }

    public void PlayDanTalk()
    {
        EventInstance danTalkInstance = RuntimeManager.CreateInstance("event:/Dan Talk");
        danTalkInstance.setVolume(0.5f);
        danTalkInstance.start();
        danTalkInstance.release();
    }

    public void PlayApTalk()
    {
        EventInstance apTalkInstance = RuntimeManager.CreateInstance("event:/Ap Talk");
        apTalkInstance.setVolume(0.5f);
        apTalkInstance.start();
        apTalkInstance.release();
    }

    public void PlayPlayerDmg()
    {
        RuntimeManager.PlayOneShot("event:/PlayerDmg");
    }

    public void PlaySpitter()
    {
        RuntimeManager.PlayOneShot("event:/Spitter");
    }

    public void SetVolume(float volume)
    {
        RuntimeManager.GetBus("bus:/").setVolume(volume);
    }
}
