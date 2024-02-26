using TMPro;
using UnityEngine;

public class InternationalText : MonoBehaviour
{
    [SerializeField] string en;
    [SerializeField] string ru;

    private void Start()
    {
        if (Language.Instance.currentLanguage == "en")
        {
            GetComponent<TextMeshProUGUI>().text = en;
        }
        else if (Language.Instance.currentLanguage == "ru")
        {
            GetComponent<TextMeshProUGUI>().text = ru;
        }
        else
        {
            GetComponent<TextMeshProUGUI>().text = en;
        }
    }
}