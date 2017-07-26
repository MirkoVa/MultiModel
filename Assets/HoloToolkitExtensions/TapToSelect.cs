using HoloToolkit.Unity.InputModule;
using UnityEngine;

namespace Mirko.HoloToolkitExtensions
{
    public class TapToSelect : MonoBehaviour, IInputClickHandler
    {
        public virtual void OnInputClicked(InputEventData eventData)
        {
            if (BaseAppStateManager.IsInitialized)
            {
                // If not already selected - select, otherwise, deselect
                if (BaseAppStateManager.Instance.SelectedGameObject != gameObject)
                {
                    Debug.Log("selected object");
                    BaseAppStateManager.Instance.SelectedGameObject = gameObject;
                }
                else
                {
                    BaseAppStateManager.Instance.SelectedGameObject = null;
                }
                var audioSource = GetAudioSource(gameObject);
                if (audioSource != null)
                {
                    audioSource.Play();
                }
            }
            else
            {
                Debug.Log("No BaseAppStateManager found or initialized");
            }
        }

        private void OnMouseDown()
        {
            OnSelect();
        }


        void OnSelect()
        {
            if (BaseAppStateManager.IsInitialized)
            {
                // If not already selected - select, otherwise, deselect
                if (BaseAppStateManager.Instance.SelectedGameObject != gameObject)
                {
                    Debug.Log("selected object");
                    BaseAppStateManager.Instance.SelectedGameObject = gameObject;
                }
                else
                {
                    BaseAppStateManager.Instance.SelectedGameObject = null;
                }
                var audioSource = GetAudioSource(gameObject);
                if (audioSource != null)
                {
                    audioSource.Play();
                }
            }
            else
            {
                Debug.Log("No BaseAppStateManager found or initialized");
            }
        }

        private AudioSource GetAudioSource(GameObject obj)
        {
            var audioSource = gameObject.GetComponent<AudioSource>();
            if (audioSource == null)
            {
                audioSource = gameObject.GetComponentInParent<AudioSource>();
            }
            return audioSource;
        }
    }
}
