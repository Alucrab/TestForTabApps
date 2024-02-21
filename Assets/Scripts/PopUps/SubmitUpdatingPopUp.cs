using Api;
using Data;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace PopUps
{
    public class SubmitUpdatingPopUp : PopUpBase
    {
        [SerializeField] private ApiManager apiManager;
        [SerializeField] private TMP_InputField idInputField;
        [SerializeField] private TMP_InputField nameInputField;
        [SerializeField] private Button submitBtn;
        [SerializeField] private Button closeBtn;

        private void Start()
        {
            closeBtn.onClick.AddListener(Close);
            submitBtn.onClick.AddListener(Submit);
        }

        private void Submit()
        {
            if (idInputField.text != "" && nameInputField.text != "")
            {
                var data = new UpdatingData(nameInputField.text);
                apiManager.Put(ApiConstants.GetUpdateBtnEndpoint(idInputField.text), data);
                Close();
            }
        }
    }
}