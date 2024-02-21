using TMPro;
using UnityEngine;

namespace UI
{
    public class ButtonObject : MonoBehaviour
    {
        [SerializeField] private TMP_Text nameText;

        private string _id;

        public string Id => _id;

        public void Init(string buttonName, string id)
        {
            _id = id;
            nameText.text = buttonName;
        }
    }
}