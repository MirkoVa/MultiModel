using UnityEngine;
using HoloToolkit.Unity.InputModule;
using Mirko.HoloToolkitExtensions;

public class SpeechCommandExecuter : MonoBehaviour
{
    public bool IsActive = false;

    private static AudioSource _sound;

    void Start()
    {
        _sound = GetComponent<AudioSource>();
    }

    public static void Move()
    {
        TryChangeMode(ManipulationMode.Move);
    }

    public static void Rotate()
    {
        TryChangeMode(ManipulationMode.Rotate);
    }

    public static void Scale()
    {
        TryChangeMode(ManipulationMode.Scale);
    }

    public void Done()
    {
        TryChangeMode(ManipulationMode.None);
    }

    public void Faster()
    {
        TryChangeSpeed(true);
    }

    public void Slower()
    {
        TryChangeSpeed(false);
    }

    private static void TryChangeMode(ManipulationMode mode)
    {
        var manipulator = GetSpatialManipulator();
        if (manipulator == null)
        {
            Debug.Log("Manipulator not found");
            return;
        }

        if (manipulator.Mode != mode)
        {
            manipulator.Mode = mode;
            TryPlaySound();
        }
    }

    private void TryChangeSpeed(bool faster)
    {
        var manipulator = GetSpatialManipulator();
        if (manipulator == null)
        {
            return;
        }

        if (manipulator.Mode == ManipulationMode.None)
        {
            return;
        }

        if (faster)
        {
            manipulator.Faster();
        }
        else
        {
            manipulator.Slower();

        }
        TryPlaySound();

    }

    private static void TryPlaySound()
    {
        if (_sound != null && _sound.clip != null)
        {
            _sound.Play();
        }
    }


    public static SpatialManipulator GetSpatialManipulator()
    {
        var lastSelectedObject = AppStateManager.Instance.SelectedGameObject;
        if (lastSelectedObject == null)
        {
            Debug.Log("No selected element found");
            return null;
        }
        var manipulator = lastSelectedObject.GetComponent<SpatialManipulator>();
        if (manipulator == null)
        {
            manipulator = lastSelectedObject.GetComponentInChildren<SpatialManipulator>();
        }

        if (manipulator == null)
        {
            Debug.Log("No manipulator component found");
        }
        return manipulator;
    }
}
