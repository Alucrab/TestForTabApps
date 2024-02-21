using System.Collections.Generic;
using System.Linq;
using Data;
using UnityEngine;

namespace UI
{
    public class UIModelButtonsManager : MonoBehaviour
    {
        [SerializeField] private Loader loader;
        [SerializeField] private Transform parent;
        [SerializeField] private ButtonObject prefab;
        [SerializeField] private DataManager dataManager;

        private List<ButtonObject> buttons = new List<ButtonObject>();

        void Start()
        {
            dataManager.OnDataUpdated += OnDataUpdated;
            dataManager.OnElementUpdate += OnElementUpdate;
            dataManager.OnElementDelete += OnElementDelete;
            dataManager.OnButtonAdded += OnButtonAdded;
        }

        private void OnButtonAdded(Data.Data data)
        {
            var btnobj = Instantiate(prefab, parent);
            btnobj.Init(data.name, data.id);
            buttons.Add(btnobj);
            loader.Hide();
        }

        private void OnElementDelete(string id)
        {
            var button = buttons.FirstOrDefault(b => b.Id == id);
            if (button != null)
            {
                buttons.Remove(button);
                Destroy(button.gameObject);
                loader.Hide();
            }
        }

        private void OnElementUpdate(Data.Data data)
        {
            var button = buttons.FirstOrDefault(b => b.Id == data.id);
            if (button != null)
            {
                button.Init(data.name, data.id);
                loader.Hide();
            }
        }

        private void OnDataUpdated()
        {
            if (buttons.Count != 0)
            {
                foreach (var button in buttons)
                {
                    Destroy(button.gameObject);
                }

                buttons.Clear();
            }

            foreach (var data in dataManager.Data)
            {
                var btnobj = Instantiate(prefab, parent);
                btnobj.Init(data.name, data.id);
                buttons.Add(btnobj);
            }

            loader.Hide();
        }
    }
}