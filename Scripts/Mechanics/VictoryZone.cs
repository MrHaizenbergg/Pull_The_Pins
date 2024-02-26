using Flatformer.GameData;
using Platformer.Observer;
using UnityEngine;


namespace Platformer.Mechanics
{
    public class VictoryZone : MonoBehaviour
    {

        [SerializeField]
        private GameObject[] _skins;


        private Animator _myAnimator;
        private int reward = 50;

        private const string IS_FAIL = "IsFail";

      
        private void Awake()
        {
            _myAnimator = GetComponent<Animator>();
        }

        private void Start()
        {
            ChangeSkin();
        }
        private void OnCollisionEnter(Collision other)
        {
            var player = other.gameObject.GetComponent<PlayerController>();
            if (player != null)
            {
                _myAnimator.SetBool(IS_FAIL, true);
                GameManager.instance.PlayerWin(player);
                GameManager.instance.Coin += reward;
                GameDataManager.AddLevel(1);
                this.PostEvent(EventID.OnCarMove,true);
            }
        }

        private void ChangeSkin()
        {
            int index = GameDataManager.GetCharacterIndex();
            _skins[index].SetActive(true);
            for (int i = 0; i < _skins.Length; i++)
            {
                if(i!= index)
                    _skins[i].SetActive(false);
            }
            
        }

    }
}

