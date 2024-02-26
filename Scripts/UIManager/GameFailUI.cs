using Flatformer.GameData;
using Platformer.Mechanics;
using Platformer.Observer;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GameFailUI : MonoBehaviour
{
    [Header("Event UI")]
    [SerializeField] 
    private Button _replayButton;
    [SerializeField] 
    private Button _rewardButton;


    private void OnEnable()
    {
        this.RegisterListener(EventID.Loss, (param) => Invoke("Show", 1f));
    }

    private void OnDisable()
    {
        if (EventDispatcher.HasInstance())
        {
            EventDispatcher.Instance.RemoveListener(EventID.Loss, (param) => Invoke("Show", 1f));
        }
    }
    private void Start()
    {
        
        Hide();

        AddEvents();
    }

    private void AddEvents()
    {
        //
        
        _replayButton.onClick.AddListener(() =>
        {
            if (GameManager.instance.currentLevels >= 3)
            {
                //AdmobManager.instance.ShowInter("error");
            }
            SoundManager.instance.PlayAudioSound(SoundManager.instance.buttonAudio);
            OnReplayGame();
        });

        //
        
        _rewardButton.onClick.AddListener(() =>
        {
            SoundManager.instance.PlayAudioSound(SoundManager.instance.buttonAudio);
            //AdmobManager.instance.ShowReward(OnCompleteAds,FailedAds, "error");
        });
    }

    private void OnReplayGame()
    {
        this.PostEvent(EventID.GameStartUI);
        GameManager.instance.ReplayGame();
        this.PostEvent(EventID.IsPlayGame, true);
        Hide();
    }


    private void Show()
        => gameObject.SetActive(true);

    private void Hide()
        => gameObject.SetActive(false);



    private void OnCompleteAds()
        => StartCoroutine(DelayAdsReward());
    
    private IEnumerator DelayAdsReward()
    {
        var t = 5;
        while (t > 0)
        {
            yield return new WaitForEndOfFrame();
            --t;
        }
        GameManager.instance.NextLevel();
        GameDataManager.AddLevel(1);
        this.PostEvent(EventID.GameStartUI);
        this.PostEvent(EventID.IsPlayGame, true);
        Hide();
    }
    private void FailedAds()
    {
        Debug.Log("Ads not avilable");
    }
}
