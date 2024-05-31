using System;
using System.Collections;
using System.Collections.Generic;
using UI;
using UnityEngine;
using UnityEngine.Networking;
// ReSharper disable ConvertToUsingDeclaration

public class ProfileManager : MonoBehaviour
{
    [SerializeField] private UserInfo _currentUserInfo;
    private const string PROFILES_URL_FORMAT = "http://ec2-18-157-169-245.eu-central-1.compute.amazonaws.com:7569/profile/Profiles?userId={0}";
    public static ProfileManager Instance { get; private set; }

    public UserInfo CurrentUserInfo
    {
        get => _currentUserInfo;
        private set => _currentUserInfo = value;
    }

    public string Username { get; private set; }
    public string Id { get; private set; }
    
    [Serializable]
    public struct UserInfo
    {
        public string user_id;
        public int HighScore;
        public int Coins;
        public string TonWallet;
        public string Username;
        public bool ShowInLeaderBoard;
    }
    
    [Serializable]
    public struct Response
    {
        public UserInfo body;
        public bool isSuccess;
        public int statusCode;
        public string[] errors;
    }
    
    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        Username = URLParameters.GetSearchParameters().GetValueOrDefault("username", "username");
        Id = URLParameters.GetSearchParameters().GetValueOrDefault("id", "id");
        
        var loadingPopup = PopupRouter.Instance.Router.Show<LoadingPopup>();
        StartCoroutine(GetProfile(Id, (x)=> loadingPopup.Hide()));
    }

    private void AddTestData()
    {
        StartCoroutine(AddOrUpdateProfile(new UserInfo()
        {
            user_id = Id,
            Username = Username,
            Coins = 69,
            TonWallet = "unity wallet",
            HighScore = 3399,
        }));
        /*
         * {"isSuccess":false,"statusCode":400,"errors":["User can update only his profile"]}
         */
    }

    private IEnumerator GetProfile(string userId, Action<bool> callback = null) 
    {
        var url = string.Format(PROFILES_URL_FORMAT, userId);
        
        using (UnityWebRequest request = UnityWebRequest.Get(url))
        {
            request.SetRequestHeader("Access-Control-Allow-Origin", "*");
            yield return request.SendWebRequest();


            if (request.result == UnityWebRequest.Result.Success)
            {
                var response = JsonUtility.FromJson<Response>(request.downloadHandler.text);
                if (response.isSuccess)
                {
                    CurrentUserInfo = response.body;
                    callback?.Invoke(true);
                }
                else
                {
                    var errors = response.errors is null ? "" : string.Join(" ", response.errors);
                    Debug.LogError($"Failed {nameof(GetProfile)}. Status: {response.statusCode}. Errors: {errors}");
                    callback?.Invoke(false);
                    
                    PopupRouter.Instance.Router.Show<ErrorPopup>();
                }
            }
            else
            {
                Debug.LogError(request.error);
                callback?.Invoke(false);
                PopupRouter.Instance.Router.Show<ErrorPopup>();
            }
        }
    }
    
    private IEnumerator AddOrUpdateProfile(UserInfo userInfo, Action<bool> callback = null) 
    {
        var url = string.Format(PROFILES_URL_FORMAT, userInfo.user_id);
        
        string jsonData = JsonUtility.ToJson(userInfo);
        using (UnityWebRequest request = UnityWebRequest.Post(url, jsonData, "application/json"))
        {
            yield return request.SendWebRequest();

            if (request.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError(request.error);
                callback?.Invoke(false);
                
                PopupRouter.Instance.Router.Show<ErrorPopup>();
            }
            else
            {
                var response = JsonUtility.FromJson<Response>(request.downloadHandler.text);
                if (!response.isSuccess)
                {
                    var errors = response.errors is null ? "" : string.Join(" ", response.errors);
                    Debug.LogError($"Failed {nameof(GetProfile)}. Status: {response.statusCode}. Errors: {errors}");
                    callback?.Invoke(false);
                    
                    PopupRouter.Instance.Router.Show<ErrorPopup>();
                }
                else
                {
                    callback?.Invoke(true);
                }
            }
        }
    }
    
}
