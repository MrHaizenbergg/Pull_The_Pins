
using UnityEngine;

public class TrapController : MonoBehaviour
{

    public AudioClip trapClip;

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Dumbbells"))
        {
            Destroy(gameObject);
            return;
        }
        if (other.gameObject.CompareTag("Bomb"))
        {
            Destroy(gameObject);
            return;
        }
        
        // collision in Enemy
        if (other.gameObject.CompareTag("Gangster"))
        {
            SoundManager.instance.PlayAudioSound(trapClip);
            Destroy(gameObject);
            return;
        }
        if (other.gameObject.CompareTag("Zombie"))
        {
            SoundManager.instance.PlayAudioSound(trapClip);
            Destroy(gameObject);
            return;
        }

        // collison in Player
        if (other.gameObject.CompareTag("Player"))
        {
            SoundManager.instance.PlayAudioSound(trapClip);
            Destroy(gameObject);
        }
    }

}
