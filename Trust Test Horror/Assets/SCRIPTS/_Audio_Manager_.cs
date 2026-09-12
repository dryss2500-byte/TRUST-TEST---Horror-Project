using UnityEngine;
[RequireComponent(typeof(AudioSource))]
public class _Audio_Manager_ : MonoBehaviour
{
    [Header("utils")]
    [field: SerializeField] private AudioSource sfx_source;
    [field: SerializeField] private AudioSource env_source;

    public static _Audio_Manager_ manager;

    private void Awake()
    {
        if (manager == null) manager = this; else { Destroy(manager);
            manager = this;
        }
    }

    public void play_sfx(AudioClip clip) { sfx_source.PlayOneShot(clip); }

    public void stop_sfx(AudioClip clip) { sfx_source.Stop(); }

    public void play_env(AudioClip clip) { env_source.PlayOneShot(clip);  }

    public void stop_env(AudioClip clip) { env_source.Stop(); }
}
