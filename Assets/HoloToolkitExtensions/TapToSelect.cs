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
                    var oldGameObject = BaseAppStateManager.Instance.SelectedGameObject;
                    Debug.Log("selected object");
                    if (oldGameObject)
                    {
                        Destroy(oldGameObject.GetComponent<SelectForBoundingBox>().menu);
                        oldGameObject.GetComponent<SelectForBoundingBox>().isDrawn = false;
                    }
                    gameObject.GetComponent<SelectForBoundingBox>().DrawSelectionBox();
                    BaseAppStateManager.Instance.SelectedGameObject = gameObject;
                }
                else
                {
                    Destroy(gameObject.GetComponent<SelectForBoundingBox>().menu);
                    gameObject.GetComponent<SelectForBoundingBox>().isDrawn = false;
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
                var oldGameObject = BaseAppStateManager.Instance.SelectedGameObject;
                // If not already selected - select, otherwise, deselect
                if (oldGameObject != gameObject)
                {
                    Debug.Log("Different object");
                    Debug.Log(oldGameObject);

                    if (oldGameObject)
                    {
                        Destroy(oldGameObject.GetComponent<SelectForBoundingBox>().menu);
                        //oldGameObject.GetComponent<SelectForBoundingBox>().isDrawn = false;
                    }
                    gameObject.GetComponent<SelectForBoundingBox>().DrawSelectionBox();
                    BaseAppStateManager.Instance.SelectedGameObject = gameObject;
                }
                else
                {
                    Debug.Log("Its the same");
                    Debug.Log(oldGameObject);
                    if (oldGameObject)
                    {
                        Destroy(oldGameObject.GetComponent<SelectForBoundingBox>().menu);
                    }
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
