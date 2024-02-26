using UnityEngine;
using UnityEngine.UI;

public class InternationalTextOrigin : MonoBehaviour
{
    [SerializeField] string en;
    [SerializeField] string ru;

    private void Start()
    {
        if (Language.Instance.currentLanguage == "en")
        {
            GetComponent<Text>().text = en;
        }
        else if (Language.Instance.currentLanguage == "ru")
        {
            GetComponent<Text>().text = ru;
        }
        else
        {
            GetComponent<Text>().text = en;
        }
    }
}
