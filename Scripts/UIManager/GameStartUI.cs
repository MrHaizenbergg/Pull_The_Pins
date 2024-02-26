using Flatformer.GameData;
using Platformer.Mechanics;
using Platformer.Observer;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using YG;

public class GameStartUI : MonoBehaviour
{
    [Header("Event UI")]
    [SerializeField]
    private Button _backHomeButton;
    [SerializeField]
    private Button _replayGameButton;
    [SerializeField]
    private Button _rewardSkipLevelButton;


    [SerializeField] private TextMeshProUGUI _currenLevelText;



    private float timeShowInter;

    private void OnEnable()
    {
        this.RegisterListener(EventID.GameStartUI, (param) => Show());
        this.RegisterListener(EventID.OnCarMove, (param) => Hide());
        this.RegisterListener(EventID.Victory, (param) => Hide());
        this.RegisterListener(EventID.Loss, (param) => Hide());
    }

    private void OnDisable()
    {
        if (EventDispatcher.HasInstance())
        {
            EventDispatcher.Instance.RemoveListener(EventID.GameStartUI, (param) => Show());
            EventDispatcher.Instance.RemoveListener(EventID.OnCarMove, (param) => Hide());
            EventDispatcher.Instance.RegisterListener(EventID.Victory, (param) => Hide());
            EventDispatcher.Instance.RemoveListener(EventID.Loss, (param) => Hide());
        }
    }
    private void Start()
    {
        //if(timeShowInter < 30)
        //{
        //    timeShowInter = 30;
        //}

        Hide();

        //if (Language.Instance.currentLanguage == "ru")
        //{
        //    SetCurrentTextRU();
        //    Debug.Log("RULang");
        //}
        //else
            SetCurrentText();

        AddEvents();
    }
    private void AddEvents()
    {
        //
        _backHomeButton.onClick.RemoveAllListeners();
        _backHomeButton.onClick.AddListener(() =>
        {
            SoundManager.instance.PlayAudioSound(SoundManager.instance.buttonAudio);
            if (timeShowInter <= 0)
            {
                //AdmobManager.instance.ShowInter("error");
            }
            OnBackHomeButton();
        });


        _replayGameButton.onClick.RemoveAllListeners();
        _replayGameButton.onClick.AddListener(() =>
        {
            SoundManager.instance.PlayAudioSound(SoundManager.instance.buttonAudio);
            OnReplayGameButton();
        });


        _rewardSkipLevelButton.onClick.RemoveAllListeners();
        _rewardSkipLevelButton.onClick.AddListener(() =>
        {
            SoundManager.instance.PlayAudioSound(SoundManager.instance.buttonAudio);
            this.PostEvent(EventID.btnSkiplevel, true);
            //AdmobManager.instance.ShowReward(OnCompleteAds, FailedAds, "Error");
        });
    }

    private IEnumerator CountDownTimeShow()
    {
        while (timeShowInter > 0)
        {
            yield return new WaitForSeconds(1f);
            timeShowInter--;

        }
    }
    private void OnReplayGameButton()
    {
        GameManager.instance.ReplayGame();
        this.PostEvent(EventID.IsPlayGame, true);
    }

    private void OnBackHomeButton()
    {
        GameManager.instance.ReplayGame();
        this.PostEvent(EventID.Home);
        Hide();
    }
    private void SetCurrentText()
    {
        _currenLevelText.text = (GameDataManager.GetLevel() + 1).ToString();
    }

    //private void SetCurrentTextRU()
    //{
    //    _currenLevelText.text = "Уровень: " + (GameDataManager.GetLevel() + 1);
    //}
    private void Show()
    {
        gameObject.SetActive(true);
        SetCurrentText();
        if (timeShowInter < 30)
        {
            timeShowInter = 30;
        }
        StartCoroutine(CountDownTimeShow());
    }
    private void Hide() => gameObject.SetActive(false);


    private void OnCompleteAds()
    {
        StartCoroutine(DelayCompleteAds());
    }

    private IEnumerator DelayCompleteAds()
    {
        var t = 5;
        while (t > 0)
        {
            yield return new WaitForEndOfFrame();
            t--;
            if (t == 0)
            {
                SoundManager.instance.PlayAudioWin();
                GameManager.instance.Coin += 50;
                GameDataManager.AddLevel(1);
                this.PostEvent(EventID.Victory);

                //if (Language.Instance.currentLanguage == "ru")
                //    SetCurrentTextRU();
                //else
                    SetCurrentText();

                //SetCurrentText();
            }
        }
    }
    private void FailedAds()
    {
        Debug.Log("Ads");
    }
}
