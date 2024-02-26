using UnityEngine;
using YG;

public class ShowAdvOnScene : MonoBehaviour
{
    void Start()
    {
        YandexGame.FullscreenShow();
        Debug.Log("SHowFullscreen");
    }
}
