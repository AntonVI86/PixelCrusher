#pragma warning disable

using System.Collections;
using Agava.YandexGames;
using Agava.YandexGames.Samples;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Ads : MonoBehaviour
{
    public event UnityAction Rewarded;


    private void Awake()
    {
        YandexGamesSdk.CallbackLogging = true;
    }

    private IEnumerator Start()
    {
#if !UNITY_WEBGL || UNITY_EDITOR
        yield break;
#endif

        yield return YandexGamesSdk.Initialize();
    }

    public void OnShowInterstitialButtonClick()
    {
        InterstitialAd.Show();
    }

    public void OnShowVideoButtonClick()
    {
        VideoAd.Show();
        Rewarded?.Invoke();
    }

    public void OnAuthorizeButtonClick()
    {
        PlayerAccount.Authorize();
    }

    public void OnRequestPersonalProfileDataPermissionButtonClick()
    {
        PlayerAccount.RequestPersonalProfileDataPermission();
    }

    public void OnGetProfileDataButtonClick()
    {
        PlayerAccount.GetProfileData((result) =>
        {
            string name = result.publicName;
            if (string.IsNullOrEmpty(name))
                name = "Anonymous";
            Debug.Log($"My id = {result.uniqueID}, name = {name}");
        });
    }

    public void OnSetLeaderboardScoreButtonClick()
    {
        Leaderboard.SetScore("PlaytestBoard", Random.Range(1, 100));
    }

    public void OnGetLeaderboardEntriesButtonClick()
    {
        Leaderboard.GetEntries("PlaytestBoard", (result) =>
        {
            Debug.Log($"My rank = {result.userRank}");
            foreach (var entry in result.entries)
            {
                string name = entry.player.publicName;
                if (string.IsNullOrEmpty(name))
                    name = "Anonymous";
                Debug.Log(name + " " + entry.score);
            }
        });
    }

    public void OnGetLeaderboardPlayerEntryButtonClick()
    {
        Leaderboard.GetPlayerEntry("PlaytestBoard", (result) =>
        {
            if (result == null)
                Debug.Log("Player is not present in the leaderboard.");
            else
                Debug.Log($"My rank = {result.rank}, score = {result.score}");
        });
    }

    public void OnGetDeviceTypeButtonClick()
    {
        Debug.Log($"DeviceType = {Device.Type}");
    }

    public void OnGetEnvironmentButtonClick()
    {
        Debug.Log($"Environment = {JsonUtility.ToJson(YandexGamesSdk.Environment)}");
    }
}

