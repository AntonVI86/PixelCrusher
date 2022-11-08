using System.Collections;
using Agava.YandexGames;
using UnityEngine;

public class ChangeLanguage : MonoBehaviour
{
    private void Awake()
    {
        YandexGamesSdk.CallbackLogging = true;
    }
    private IEnumerator Start()
    {
        yield return YandexGamesSdk.Initialize();
    }

    public void Change()
    {
        Lean.Localization.LeanLocalization.SetCurrentLanguageAll(YandexGamesSdk.Environment.i18n.lang);
    }
}
