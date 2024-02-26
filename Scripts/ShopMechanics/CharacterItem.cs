using DG.Tweening;
using Flatformer.GameData;
using Platformer.Observer;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;



namespace ShopMechanics
{
    public enum StateCharacterItem
    {
        Lock,
        Unlock,
        AlphaLock
    }
    public class CharacterItem : MonoBehaviour
    {
        [Header("Information Character Item")]
        [SerializeField] private int _index;
        [SerializeField] private Image _characterStateBG;
        [SerializeField] private Image _characterImage;
        [SerializeField] private GameObject _characterSelect;
        [SerializeField] private TextMeshProUGUI _priceText;
        [SerializeField] private TextMeshProUGUI _headerText;

        [Space(20f)]
        [Header("Event Character Item")]
        [SerializeField] private Button _characterPurchaseButton;
        [SerializeField] private Button _selectItemButton;


        [Header("State Character Image")]
        [SerializeField] private Sprite _backGroundLock;
        [SerializeField] private Sprite _backGroundUnlock;
        [SerializeField] private Sprite _backGroundAlphaLock;



        // methods

        public void SetChatacterIndex(int index) => this._index = index;

        public void SetCharacterImage(Sprite sprite) => _characterImage.sprite = sprite;


        public void SetPriceCharacter(int price) => _priceText.text = price.ToString();

        public void SetPurchaseEqualAds()
        {
            _characterPurchaseButton.transform.GetChild(1).gameObject.SetActive(false);
            _characterPurchaseButton.transform.GetChild(0).gameObject.SetActive(true);
        }

        public void SetPurchaseEqualActiveLevel(string header)
        {
            _characterPurchaseButton.gameObject.SetActive(false);
            _headerText.gameObject.SetActive(true);
            _headerText.text = header;
        }

        public void SetPurchaseEqualCoin(int price)
        {
            _characterPurchaseButton.transform.GetChild(0).gameObject.SetActive(false);
            _characterPurchaseButton.transform.GetChild(1).gameObject.SetActive(true);
            
            _priceText.text = price.ToString();
        }

        public void SetCharacterStateBG(StateCharacterItem stateItem)
        {
            switch (stateItem)
            {
                case StateCharacterItem.Lock:
                    _headerText.gameObject.SetActive(false);
                    _characterStateBG.sprite = _backGroundLock;
                    break;
                case StateCharacterItem.Unlock:
                    _headerText.gameObject.SetActive(false);
                    _characterStateBG.sprite = _backGroundUnlock;
                    break;
                case StateCharacterItem.AlphaLock:
                    _characterStateBG.sprite = _backGroundAlphaLock;
                    break;
            }
        }

        public void OnPurchaseItem()
        {
            _characterPurchaseButton.onClick.RemoveAllListeners();
            _characterPurchaseButton.onClick.AddListener(() =>
            {
                SoundManager.instance.PlayAudioSound(SoundManager.instance.buttonAudio);
                this.PostEvent(EventID.PurchaseSkin, this._index);
            });
        }

        public void SetPurchaseAsCharacter()
        {
            _characterPurchaseButton.gameObject.SetActive(false);
            _selectItemButton.interactable = true;
            _characterStateBG.sprite = _backGroundUnlock;
        }

        public void OnSelectItem()
        {
            _selectItemButton.onClick.RemoveAllListeners();
            _selectItemButton.onClick.AddListener(() =>
            {
                SoundManager.instance.PlayAudioSound(SoundManager.instance.buttonAudio);
                this.PostEvent(EventID.SelectSkin, this._index);
            });
        }

        public void SelectItem()
        {
            _characterSelect.SetActive(true);
            _selectItemButton.interactable = false;
        }

        public void DeSelectItem()
        {
            _characterSelect.gameObject.SetActive(false);
            _selectItemButton.interactable = true;
        }

        
        public void OnCompleteAds()
        {
            StartCoroutine(DelayCompleteAds(5));
        }
        private IEnumerator DelayCompleteAds(float delay)
        {
            var t = delay;
            while(t > 0)
            {
                yield return new WaitForEndOfFrame();
                t--;
               
            }
            GameDataManager.AddPurchaseCharacter(_index);
            SetPurchaseAsCharacter();
            OnSelectItem();
        }

    }
}
