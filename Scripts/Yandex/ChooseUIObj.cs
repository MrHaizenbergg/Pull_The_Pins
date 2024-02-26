using UnityEngine;

public class ChooseUIObj : MonoBehaviour
{
    [SerializeField] private GameObject ruObj;
    [SerializeField] private GameObject enObj;

    private void Start()
    {
        if (Language.Instance.currentLanguage == "en")
        {
            enObj.SetActive(true);
            ruObj.SetActive(false);
        }
        else if (Language.Instance.currentLanguage == "ru")
        {
            ruObj.SetActive(true);
            enObj.SetActive(false);
        }
        else
        {
            enObj.SetActive(true);
            ruObj.SetActive(false);
        }
    }
}
