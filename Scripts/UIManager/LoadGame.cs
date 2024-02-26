using Platformer.Observer;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class LoadGame : MonoBehaviour
{
    [SerializeField] private Image loadGameImage; // Tham chiếu đến thành phần Image của đối tượng loading


    private float time = 1f;
    void Start()
    {

        this.RegisterListener(EventID.LoadGame, (param) => Show());
        Hide();
    }

    // Delay Game
    private IEnumerator StartLoading(Image loadGameImage, float fadeTime)
    {
        yield return new WaitForSeconds(fadeTime);
        loadGameImage.fillAmount = 0;
        this.PostEvent(EventID.Home);
        Hide();
    }
    private void Show()
    {
        gameObject.SetActive(true);
        loadGameImage.fillAmount = 1;
        StartCoroutine(StartLoading(loadGameImage, 0.5f));
    }

    private void Hide() => gameObject.SetActive(false);

}
