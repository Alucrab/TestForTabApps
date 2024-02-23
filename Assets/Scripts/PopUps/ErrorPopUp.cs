using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace PopUps
{
    public class ErrorPopUp : PopUpBase
    {
        [SerializeField] private TMP_Text errorText;
        [SerializeField] private Button closeBtn;

        private void Start()
        {
            closeBtn.onClick.AddListener(Close);
        }

        public void Init(string error)
        {
            errorText.text = error;
        }
    }
}
