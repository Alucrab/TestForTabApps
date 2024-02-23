using System;
using System.Collections.Generic;
using System.Linq;
using Api;
using Newtonsoft.Json;
using PopUps;
using Proyecto26;
using UI;
using UnityEngine;

namespace Data
{
    public class DataManager : MonoBehaviour
    {
        public List<Data> Data => _data;

        [SerializeField] private ApiManager apiManager;
        [SerializeField] private Loader loader;
        [SerializeField] private ErrorPopUp errorPopUp;
        
        private List<Data> _data;
        
        public event Action OnDataUpdated;
        public event Action<Data> OnButtonAdded;
        public event Action<Data> OnElementUpdate;
        public event Action<string> OnElementDelete;

        private void Start()
        {
            apiManager.GetRequestCallback += GetDataCallback;
            apiManager.PutRequestCallback += PutRequestCallback;
            apiManager.DeleteRequestCallback += DeleteRequestCallback;
            apiManager.PostRequestCallback += PostRequestCallback;
            apiManager.OnError += OnErrorCatched;
            GetBaseData();
        }

        private void OnErrorCatched(string obj)
        {
            loader.Hide();
            errorPopUp.Init(obj);
            errorPopUp.Open();
        }

        private void PostRequestCallback(ResponseHelper obj)
        {
            var response = JsonConvert.DeserializeObject<Data>(obj.Text);
            Data.Add(response);
            OnButtonAdded?.Invoke(response);
        }

        private void DeleteRequestCallback(ResponseHelper obj)
        {
            var splitedUrl = obj.Request.url.Split('/');
            var q = _data.FirstOrDefault(d => d.id == splitedUrl[^1]);
            if (q != null)
            {
                _data.Remove(q);
            }

            OnElementDelete?.Invoke(splitedUrl[^1]);
        }

        private void PutRequestCallback(ResponseHelper obj)
        {
            var splitedUrl = obj.Request.url.Split('/');
            var q = _data.FirstOrDefault(d => d.id == splitedUrl[^1]);
            var data = JsonConvert.DeserializeObject<Data>(obj.Text);
            if (q != null)
            {
                var index = _data.IndexOf(q);
                _data[index] = data;
            }

            OnElementUpdate?.Invoke(data);
            return;
        }

        private void GetDataCallback(ResponseHelper obj)
        {
            var splitedUrl = obj.Request.url.Split('/');
            if (splitedUrl[^1] == ApiConstants.BtnEndpoint)
            {
                _data = JsonConvert.DeserializeObject<List<Data>>(obj.Text);
                Debug.Log(JsonConvert.SerializeObject(_data));
            }
            else
            {
                var q = _data.FirstOrDefault(d => d.id == splitedUrl[^1]);
                if (q != null)
                {
                    var index = _data.IndexOf(q);
                    _data[index] = JsonConvert.DeserializeObject<Data>(obj.Text);
                }
            }

            OnDataUpdated?.Invoke();
            return;
        }

        private void GetBaseData()
        {
            apiManager.Get(ApiConstants.BtnEndpoint);
        }
    }

    [Serializable]
    public class Data
    {
        private string _createdAt;
        private string _name;
        private string _id;

        public string createdAt => _createdAt;
        public string name => _name;
        public string id => _id;

        public Data(string createdAt, string name, string id)
        {
            _createdAt = createdAt;
            _name = name;
            _id = id;
        }
    }

    [Serializable]
    public class UpdatingData
    {
        private string _name;
        public string name => _name;

        public UpdatingData(string name)
        {
            _name = name;
        }
    }
}