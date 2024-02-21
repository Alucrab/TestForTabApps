using System;
using Api;
using Data;
using PopUps;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class UIButtonsController : MonoBehaviour
    {
        [SerializeField] private Button createButton;
        [SerializeField] private Button deleteButton;
        [SerializeField] private Button updateButton;
        [SerializeField] private Button refreshButton;

        [SerializeField] private ApiManager apiManager;

        [SerializeField] private DataManager dataManager;

        [SerializeField] private SubmitDeletingPopUp submitDeletingPopUp;
        [SerializeField] private SubmitUpdatingPopUp submitUpdatingPopUp;
        [SerializeField] private SubmitRefreshPopUp submitRefreshPopUp;

        void Start()
        {
            createButton.onClick.AddListener(CreateNewButton);
            deleteButton.onClick.AddListener(DeleteButtonFromDB);
            updateButton.onClick.AddListener(UpdateButtonInBd);
            refreshButton.onClick.AddListener(RefreshAllData);
        }

        private void RefreshAllData()
        {
            submitRefreshPopUp.Open();
        }

        private void UpdateButtonInBd()
        {
            submitUpdatingPopUp.Open();
        }

        private void DeleteButtonFromDB()
        {
            submitDeletingPopUp.Open();
        }

        private void CreateNewButton()
        {
            var id = dataManager.Data.Count + 1;
            var data = new Data.Data($"{DateTime.Now}", $"name {id}", $"{id}");
            apiManager.Post(ApiConstants.GetCreateBtnEndpoint(), data);
        }
    }
}