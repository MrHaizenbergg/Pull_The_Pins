
using Platformer.Observer;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Platformer.Mechanics;
using Flatformer.GameData;

public class GameVictoryUI : MonoBehaviour
{

    [Header("User Interface")]
    [SerializeField]
    private GameObject _victoryUI;
    [SerializeField]
    private TextMeshProUGUI _coinText;
    [SerializeField]
    private TextMeshProUGUI _coinRewardText;
    [SerializeField]
    private TextMeshProUGUI _currentCoinText;




    [Header("Events")]
    [SerializeField]
    private Button _tapContinueButton;
    [SerializeField]
    private Button _rewardButton;


    private bool isInteractBtnSkipLevel;



    private void SetIsInteractBtnSkipLevel(bool isInterac)
    {
        this.isInteractBtnSkipLevel = isInterac;
    }

    private void OnEnable()
    {
        this.RegisterListener(EventID.Victory, (param) => OpenVictoryPanel());
        this.RegisterListener(EventID.btnSkiplevel, (param) => SetIsInteractBtnSkipLevel((bool)param));
    }

    private void OnDisable()
    {
        if (EventDispatcher.HasInstance())
        {
            EventDispatcher.Instance.RemoveListener(EventID.Victory, (param) => OpenVictoryPanel());
            EventDispatcher.Instance.RemoveListener(EventID.btnSkiplevel, (param) => SetIsInteractBtnSkipLevel((bool)param));
        }
    }

    private void Start()
    {

        

        CloseVictoryPanel();

        

        AddEvents();

        
    }

    private void SetAllCoinText()
    {
        Debug.Log(GameManager.instance.Coin);
        _coinText.text = GameManager.instance.Coin.ToString();
        _coinRewardText.text = (GameManager.instance.Coin * 3).ToString();
        _currentCoinText.text = GameDataManager.GetCoin().ToString();
    }

    private void OpenVictoryPanel()
    {
        _victoryUI.gameObject.SetActive(true);
        SoundManager.instance.PlayAudioWin();
        SetAllCoinText();
        _tapContinueButton.gameObject.SetActive(false);
        StartCoroutine(DelayShowTapcontinueButton(3));
    }

    private void CloseVictoryPanel()
    {
        _victoryUI.SetActive(false);
    }

    private void AddEvents()
    {
        // Envet Tap to continue
        _tapContinueButton.onClick.RemoveAllListeners();
        _tapContinueButton.onClick.AddListener(() =>
        {
            SoundManager.instance.PlayAudioSound(SoundManager.instance.buttonAudio);
            HandleEventTapContinue();
            CloseVictoryPanel();
            GameSharedUI.instance.UpdateCoinsTextUI();
            this.PostEvent(EventID.IsPlayGame, true);
        });


        // Event Ads Reward
        _rewardButton.onClick.RemoveAllListeners();
        _rewardButton.onClick.AddListener(() =>
        {
            SoundManager.instance.PlayAudioSound(SoundManager.instance.buttonAudio);
            //AdmobManager.instance.ShowReward(OnCompleteAds, FailedAds, "Error");
        });
    }
    private void HandleEventTapContinue()
    {
       
        // xu ly next level cho use
        TapContinueButton();

        // Save Data
        ChangeDataGame();

        // hien thi lai reward button
        ShowButtonReward();
        this.PostEvent(EventID.GameStartUI);

    }
    private void TapContinueButton()
    {
        if (GameManager.instance.currentLevels >= 3)
        {
            if (!isInteractBtnSkipLevel)
            {
                //AdmobManager.instance.ShowInter("error");
            }
        }
        SoundManager.instance.PlayAudioSound(SoundManager.instance.buttonAudio);
        GameManager.instance.NextLevel();
    }

    private IEnumerator DelayShowTapcontinueButton(float time)
    {
        var t = time;
        while (t > 0)
        {
            yield return new WaitForSeconds(1f);
            t--;
        }
        _tapContinueButton.gameObject.SetActive(true);
    }

    private void ChangeDataGame()
    {
        GameDataManager.AddCoin(GameManager.instance.Coin);
        GameManager.instance.Coin = 0;
    }




    // Handle Button Reward: Show, Hide
    private void ShowButtonReward()
        => _rewardButton.gameObject.SetActive(true);

    private void HideButtonReward()
        => _rewardButton.gameObject.SetActive(false);
    



    // Handle Ads Reward
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
                GameManager.instance.Coin *= 3;
                GameDataManager.AddCoin(GameManager.instance.Coin);
            }
        }
        GameSharedUI.instance.UpdateCoinsTextUI();
        GameManager.instance.Coin = 0;
        HandlerAdsReward();
    }

    public void HandlerAdsReward()
    {
        GameManager.instance.NextLevel();
        CloseVictoryPanel();
        this.PostEvent(EventID.GameStartUI);
        this.PostEvent(EventID.IsPlayGame, true);
    }

    private void FailedAds()
    {
        Debug.Log("Ads not");
    }
}
