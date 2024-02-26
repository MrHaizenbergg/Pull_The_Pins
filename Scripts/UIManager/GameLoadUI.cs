using Platformer.Mechanics;
using Platformer.Observer;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using YG;

public class GameLoadUI : MonoBehaviour
{
    [SerializeField] private Image loadingImage; // Tham chiếu đến thành phần Image của đối tượng loading

    private float loading = 0.7f;

    void Start()
    {
        StartCoroutine(StartLoadingAnimation());
        //StartCoroutine(StartGameI());
    }

    //private IEnumerator StartGameI()
    //{
    //    yield return new WaitForSeconds(loading);
    //    GameManager.instance.StartGame();
    //    this.PostEvent(EventID.Home);
    //    gameObject.SetActive(false);
    //}

    private IEnumerator StartLoadingAnimation()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.1f);
            loadingImage.fillAmount += Time.deltaTime*4;
            if (loadingImage.fillAmount >= loading)
            {
                yield return new WaitForSeconds(0.3f);
                GameManager.instance.StartGame();
                this.PostEvent(EventID.Home);
                YandexGame.GameReadyAPI();
                gameObject.SetActive(false);
            }
        }
    }
}
