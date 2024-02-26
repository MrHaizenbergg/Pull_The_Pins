using UnityEngine;
using UnityEngine.UI;
using YG;

public class RewardedBtn : MonoBehaviour
{
    private Button _btnRewarded;

    private void Awake()
    {
        _btnRewarded = GetComponent<Button>();
    }

    private void OnEnable()
    {
        _btnRewarded.onClick.AddListener(delegate { YandexGame.RewVideoShow(0); });
        Debug.Log("RewAddListener");
    }

    private void OnDisable()
    {
        _btnRewarded.onClick.RemoveListener(delegate { YandexGame.RewVideoShow(0); });
        _btnRewarded.onClick.RemoveAllListeners();
        Debug.Log("RewDisable RemoveListeners");
    }
}
