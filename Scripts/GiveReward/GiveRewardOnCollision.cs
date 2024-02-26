using Platformer.Mechanics;
using UnityEngine;
using UnityEngine.Events;

public class GiveRewardOnCollision : MonoBehaviour
{
    [Header("Config")]
    [SerializeField] int coinReward;
    [SerializeField] GameObject prefabEffect;


    [Header("Events")]

    [SerializeField] UnityEvent OnSuccessGiveRewardHandler;

    private GameObject instance;

    private void Start()
    {
        instance = Instantiate(prefabEffect) as GameObject;
        instance.transform.position = transform.position;
    }
    private void Update()
    {
        transform.Rotate(0, -2f, 0, Space.World);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            var p = other.gameObject.GetComponent<PlayerController>();
            SoundManager.instance.PlayAudioSound(p.coinAudio);
            GameManager.instance.Coin += coinReward;
            Destroy(instance);
            OnSuccessGiveRewardHandler.Invoke();
        }
    }


}
