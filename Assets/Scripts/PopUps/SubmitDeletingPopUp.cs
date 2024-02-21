using Api;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace PopUps
{
    public class SubmitDeletingPopUp : PopUpBase
    {
        [SerializeField] private ApiManager apiManager;
        [SerializeField] private TMP_InputField idInputField;
        [SerializeField] private Button submitBtn;
        [SerializeField] private Button closeBtn;

        private void Start()
        {
            closeBtn.onClick.AddListener(Close);
            submitBtn.onClick.AddListener(Submit);
        }

        private void Submit()
        {
            if (idInputField.text != "")
            {
                apiManager.Delete(ApiConstants.GetDeleteBtnEndpoint(idInputField.text));
                Close();
            }
        }
    }
}