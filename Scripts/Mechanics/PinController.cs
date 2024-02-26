using UnityEngine;
using DG.Tweening;
using Platformer.Observer;

public class PinController : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private AudioClip pinAudio;

    private bool isGamePlay;

    public void SetIsGamePlay(bool isGamePlay)
    {
        this.isGamePlay = isGamePlay;
    }
    public bool GetIsGamePlay()
    {
        return this.isGamePlay;
    }
    private void OnEnable()
    {
        this.RegisterListener(EventID.IsPlayGame, (param) => SetIsGamePlay((bool)param));
    }
    private void OnDisable()
    {
        EventDispatcher.Instance.RemoveListener(EventID.IsPlayGame, (param) => SetIsGamePlay((bool)param));
    }
    private void OnMouseDown()
    {
        if (this.isGamePlay)
        {
            SoundManager.instance.PlayAudioSound(pinAudio);
            transform.DOMove(target.position, 0.6f);
            Destroy(gameObject, 0.6f);
        }
    }
}
