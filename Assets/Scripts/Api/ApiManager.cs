using System;
using Newtonsoft.Json;
using Proyecto26;
using UI;
using UnityEngine;

namespace Api
{
    public class ApiManager : MonoBehaviour
    {
        [SerializeField] private Loader loader;

        public event Action<ResponseHelper> GetRequestCallback;
        public event Action<ResponseHelper> PostRequestCallback;
        public event Action<ResponseHelper> PutRequestCallback;
        public event Action<ResponseHelper> DeleteRequestCallback;

        public event Action<string> OnError;

        public void Get(string endPoint)
        {
            loader.Show();
            RestClient.Get(ApiConstants.BaseUrl + endPoint)
                .Then(response => GetRequestCallback?.Invoke(response))
                .Catch(e => OnError?.Invoke(e.Message));
        }

        public void Post(string endPoint, object requestBody)
        {
            loader.Show();
            var json = JsonConvert.SerializeObject(requestBody);
            RestClient.Post(ApiConstants.BaseUrl + endPoint, json)
                .Then(response => PostRequestCallback?.Invoke(response))
                .Catch(e => OnError?.Invoke(e.Message));
        }

        public void Put(string endPoint, object requestBody)
        {
            loader.Show();
            var json = JsonConvert.SerializeObject(requestBody);
            RestClient.Put(ApiConstants.BaseUrl + endPoint, json)
                .Then(response => PutRequestCallback?.Invoke(response))
                .Catch(e => OnError?.Invoke(e.Message));
        }

        public void Delete(string endPoint)
        {
            loader.Show();
            RestClient.Delete(ApiConstants.BaseUrl + endPoint)
                .Then(response => DeleteRequestCallback?.Invoke(response))
                .Catch(e => OnError?.Invoke(e.Message));
        }
    }


    public static class ApiConstants
    {
        public const string BaseUrl = "https://65d4c0773f1ab8c63435ec07.mockapi.io/api/TABAppsTest/";
        public const string BtnEndpoint = "Buttons";

        public static string GetCreateBtnEndpoint()
        {
            return BtnEndpoint;
        }

        public static string GetUpdateBtnEndpoint(string id)
        {
            return $"{BtnEndpoint}/{id}";
        }

        public static string GetDeleteBtnEndpoint(string id)
        {
            return $"{BtnEndpoint}/{id}";
        }

        public static string GetGetOneBtnEndpoint(string id)
        {
            return $"{BtnEndpoint}/{id}";
        }
    }
}