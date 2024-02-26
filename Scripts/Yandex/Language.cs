using UnityEngine;
using YG;

public class Language : MonoBehaviour
{
    public static Language Instance;

    public string currentLanguage;//ru en

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            SwitchLanguage(YandexGame.savesData.language);
            //currentLanguage = YandexGame.savesData.language;
            Debug.Log("Current Language: " + currentLanguage);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnEnable() => YandexGame.SwitchLangEvent += SwitchLanguage;
    private void OnDisable() => YandexGame.SwitchLangEvent -= SwitchLanguage;

    public void SwitchLanguage(string lang)
    {
        switch (lang)
        {
            case "ru":
                currentLanguage = "ru";
                break;
            case "tr":
                currentLanguage = "tr";
                break;
            default:
                currentLanguage = "en";
                break;
        }
    }
}
