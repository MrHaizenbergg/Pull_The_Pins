using UnityEngine;

public class PagePanelInfo : MonoBehaviour
{
    [SerializeField] private GameObject firstPage;
    [SerializeField] private GameObject secondPage;

    private RectTransform rectTransform;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    private void Update()
    {
        if (rectTransform.position.x > -500)
        {
            firstPage.SetActive(true);
            secondPage.SetActive(false);
        }
        else if(rectTransform.position.x < -500)
        {
            firstPage.SetActive(false);
            secondPage.SetActive(true);
        }
    }
}
