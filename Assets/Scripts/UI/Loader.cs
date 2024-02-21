using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using UnityEngine;

namespace UI
{
    public class Loader : MonoBehaviour
    {
        [SerializeField] private AudioSource audioSource;
        [SerializeField] private RectTransform loaderImage;

        private TweenerCore<Vector3, Vector3, VectorOptions> _currentSequence;

        public void Show()
        {
            gameObject.SetActive(true);
            _currentSequence = loaderImage.DOMoveX(Screen.width, 2);
            audioSource.Play();
        }

        public void Hide()
        {
            gameObject.SetActive(false);
            _currentSequence.Kill();
            audioSource.Stop();
            var position = loaderImage.position;
            position = new Vector3(0, position.y, position.z);
            loaderImage.position = position;
        }
    }
}