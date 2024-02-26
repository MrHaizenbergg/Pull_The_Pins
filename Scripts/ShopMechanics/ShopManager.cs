using DG.Tweening;
using DG.Tweening.Core.Easing;
using Flatformer.GameData;
using Platformer.Mechanics;
using Platformer.Observer;
using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.TextCore.Text;
using UnityEngine.UI;
using YG;


namespace ShopMechanics
{
    public class ShopManager : MonoBehaviour
    {

        [Header("reference")]
        [SerializeField] private AudioClip purcharAudio;

        [Header("UI elements")]
        [SerializeField]
        private CharacterItem[] _shopItems;
        [SerializeField]
        private CharacterShopOB _characterOB;
        [SerializeField]
        private GameObject[] _skins;


        [SerializeField] private GameVictoryUI _gameVictoryUI;
        [SerializeField] private FortuneWheelManager _fortuneWheelManager;

        [Space(20f)]
        [Header("Event Shop Manager")]
        [SerializeField]
        private GameObject _shopUI;
        [SerializeField] private Button _closeShopButton;
        [SerializeField] private Button _rewardAdsButton;

        [SerializeField] private TextMeshProUGUI _noEnoughCoinsText;
        private int newItemIndex;
        private int preousItemIndex;

        private void OnEnable()
        {
            this.RegisterListener(EventID.OpenShop, (param) => OpenShop());
            YandexGame.RewardVideoEvent += Rewarded;
            //YandexGame.CloseVideoEvent += 
            this.RegisterListener(EventID.SelectSkin, (param) => OnSelectItem((int)param));
            this.RegisterListener(EventID.PurchaseSkin, (param) => OnPurchaseItem((int)param));
        }
        private void OnDisable()
        {
            YandexGame.RewardVideoEvent -= Rewarded;
            //YandexGame.CloseVideoEvent -= ShowAdvRewardedBtn;

            if (EventDispatcher.HasInstance())
            {
                EventDispatcher.Instance.RemoveListener(EventID.OpenShop, (param) => OpenShop());

                EventDispatcher.Instance.RemoveListener(EventID.SelectSkin, (param) => OnSelectItem((int)param));
                EventDispatcher.Instance.RemoveListener(EventID.PurchaseSkin, (param) => OnPurchaseItem((int)param));
            }
        }


        private void OnValidate()
        {
            if (_shopItems == null || _shopItems.Length == 0)
            {
                _shopItems = GetComponentsInChildren<CharacterItem>();
            }
        }

        private void Start()
        {
            CloseShop();
            AddEvents();


            GenerateCharacterItem();

            SelectItem(GameDataManager.GetCharacterIndex());

            ChangeSkinCharacterUI(GameDataManager.GetCharacterIndex());
        }



        public void UpdateShop()
        {
            //GenerateCharacterItem();

            if (GameDataManager.GetLevel() >= 10)
            {
                _shopItems[1].SetPurchaseAsCharacter();
                _shopItems[1].OnSelectItem();
                _shopItems[1].SetCharacterStateBG(StateCharacterItem.Unlock);

                //_shopItems[1].SetPurchaseEqualActiveLevel("Unlock At Level 10");
                Debug.Log("Unblock_10_Lvl");
            }
            if (GameDataManager.GetLevel() >= 15)
            {
                _shopItems[3].SetPurchaseAsCharacter();
                _shopItems[3].OnSelectItem();
                _shopItems[3].SetCharacterStateBG(StateCharacterItem.Unlock);
                Debug.Log("Unblock_15_Lvl");
            }

            Debug.Log("UpdateShop");
        }

        private void SetSelectedCharacter()
        {
            int index = GameDataManager.GetCharacterIndex();
        }
        private void GenerateCharacterItem()
        {
            for (int i = 0; i < _shopItems.Length; i++)
            {
                Character character = _characterOB.GetCharacter(i);
                _shopItems[i].SetChatacterIndex(i);
                _shopItems[i].SetCharacterImage(character.image);
                if (GameDataManager.GetPurchaseAsCharacter(i))
                {

                    _shopItems[i].SetPurchaseAsCharacter();
                    _shopItems[i].OnSelectItem();
                }
                else
                {
                    _shopItems[i].SetCharacterStateBG(StateCharacterItem.Lock);
                    _shopItems[i].OnPurchaseItem();
                    if (i <= 4)
                    {
                        _shopItems[i].SetPurchaseEqualAds();
                    }
                    else
                    {
                        _shopItems[i].SetPurchaseEqualCoin(character.price);
                    }
                    if (i == 1 && GameDataManager.GetLevel() < 10)
                    {
                        _shopItems[i].SetCharacterStateBG(StateCharacterItem.AlphaLock);
                        _shopItems[i].SetPurchaseEqualActiveLevel("Unlock At Level 10");
                    }
                    if (i == 3 && GameDataManager.GetLevel() < 15)
                    {
                        _shopItems[i].SetCharacterStateBG(StateCharacterItem.AlphaLock);
                        _shopItems[i].SetPurchaseEqualActiveLevel("Unlock At Level 15");
                    }
                }
            }
        }
        private void OnSelectItem(int index)
        {


            SelectItem(index);


            // Save Data
            GameDataManager.SetCharacterIndex(index);

            //Change Skin Character UI
            ChangeSkinCharacterUI(index);

        }

        private void ChangeSkinCharacterUI(int index)
        {
            for (int i = 0; i < _skins.Length; i++)
            {
                if (i != index)
                    _skins[i].SetActive(false);
            }
            _skins[index].SetActive(true);
        }
        private void SelectItem(int newIndex)
        {
            preousItemIndex = newItemIndex;
            newItemIndex = newIndex;

            CharacterItem preCharacter = _shopItems[preousItemIndex];
            CharacterItem newCharacter = _shopItems[newItemIndex];

            preCharacter.DeSelectItem();
            newCharacter.SelectItem();
        }

        public void Rewarded(int id)
        {
            if (id == 0)
            {
                GameDataManager.AddCoin(150);
                GameSharedUI.instance.UpdateCoinsTextUI();
                Debug.Log("CoinsRewarded");
            }
            else if(id == 1)
            {
                UnlockCharacterForAdvYandex(2);
            }
            else if (id == 2)
            {
                UnlockCharacterForAdvYandex(4);
            }
            else if (id == 3)
            {
                GameManager.instance.Coin *= 3;
                GameDataManager.AddCoin(GameManager.instance.Coin);
                GameSharedUI.instance.UpdateCoinsTextUI();
                GameManager.instance.Coin = 0;
                _gameVictoryUI.HandlerAdsReward();
            }
            else if(id== 4)
            {
                _fortuneWheelManager.TurnWheelForAds();
            }
                Debug.Log("CloseAdvRewMoneyAdd");
        }

        public void OpenRewardAd(int id)
        {
            YandexGame.RewVideoShow(id);
        }

        public void UnlockCharacterForAdvYandex(int character)
        {
            _shopItems[character].SetPurchaseAsCharacter();
            _shopItems[character].OnSelectItem();
            _shopItems[character].SetCharacterStateBG(StateCharacterItem.Unlock);
            GameDataManager.AddPurchaseCharacter(character);
        }

        private void OnPurchaseItem(int index)
        {
            Debug.Log("Purchase: " + index);
            if (index <= 4)
            {
                //AdmobManager.instance.ShowReward(_shopItems[index].OnCompleteAds, FaildedAds, "error");
                if (index == 2)
                {

                }

            }
            else
            {
                // Purchase Equal coins
                Character character = _characterOB.GetCharacter(index);
                if (GameDataManager.CanSpenCoin(character.price))
                {
                    _shopItems[index].SetPurchaseAsCharacter();
                    _shopItems[index].OnSelectItem();

                    GameDataManager.SpendCoin(character.price);
                    GameDataManager.AddPurchaseCharacter(index);

                    SoundManager.instance.PlayAudioSound(purcharAudio);
                    GameSharedUI.instance.UpdateCoinsTextUI();
                }
                else
                {
                    AnimationNoMoreCoinsText();
                    AnimationShakeItem(_shopItems[index].transform);
                }
            }
        }

        private void AnimationNoMoreCoinsText()
        {
            //Complete Animation( if it's running)
            _noEnoughCoinsText.transform.DOComplete();
            _noEnoughCoinsText.DOComplete();

            _noEnoughCoinsText.transform.DOShakePosition(3f, new Vector3(5f, 0, 0), 10, 0);
            _noEnoughCoinsText.DOFade(1f, 3f).From(0f).OnComplete(() =>
            {
                _noEnoughCoinsText.DOFade(0f, 1f);
            });
        }
        public void AnimationShakeItem(Transform transform)
        {
            Debug.Log("Dotweening");
            transform.DOComplete();
            transform.DOShakePosition(1f, new Vector3(10f, 0, 0), 10, 0).SetEase(Ease.Linear);
        }

        private void AddEvents()
        {
            // Event Close Shop
            _closeShopButton.onClick.RemoveAllListeners();
            _closeShopButton.onClick.AddListener(() =>
            {
                SoundManager.instance.PlayAudioSound(SoundManager.instance.buttonAudio);
                GameManager.instance.ReplayGame();
                //this.PostEvent(EventID.IsPlayGame, true);
                this.PostEvent(EventID.Home);
                CloseShop();
            });

            //Event rewardAdsButton
            _rewardAdsButton.onClick.RemoveAllListeners();
            _rewardAdsButton.onClick.AddListener(() =>
            {
                SoundManager.instance.PlayAudioSound(SoundManager.instance.buttonAudio);
                //AdmobManager.instance.ShowReward(OnCompleteAds, FaildedAds, "Error");
            });
        }

        private void OnCompleteAds()
        {
            StartCoroutine(DelayCompleteAds(5));
        }
        private IEnumerator DelayCompleteAds(float time)
        {
            var t = time;
            while (t > 0)
            {
                yield return new WaitForEndOfFrame();
                t--;
                if (t == 0)
                    GameDataManager.AddCoin(150);
            }
            GameSharedUI.instance.UpdateCoinsTextUI();
        }
        private void FaildedAds()
            => Debug.Log("a");

        private void OpenShop()
            => _shopUI.SetActive(true);

        private void CloseShop()
            => _shopUI.SetActive(false);


    }
}
