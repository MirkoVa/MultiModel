using UnityEngine;
using System.Collections;
using HoloToolkit.Unity.InputModule;

namespace Mirko.HoloToolkitExtensions
{
    public class SpatialManipulator : MonoBehaviour
    {
        public float MoveSpeed = 0.1f;

        public float RotateSpeed = 6f;

        public float ScaleSpeed = 0.2f;

        public bool rotateY = true;
        public bool rotateX = false;
        public bool rotateZ = false;


        public BaseRayStabilizer Stabilizer = null;

        public BaseSpatialMappingCollisionDetector CollisonDetector = null;

        void Start()
        {
            Mode = ManipulationMode.None;
            if (CollisonDetector == null)
            {
                CollisonDetector = gameObject.AddComponent<DefaultMappingCollisionDetector>();
            }
        }

        public void Faster()
        {
            if (Mode == ManipulationMode.Move)
            {
                MoveSpeed *= 2f;
            }

            if (Mode == ManipulationMode.Rotate)
            {
                RotateSpeed *= 2f;
            }
            if (Mode == ManipulationMode.Scale)
            {
                ScaleSpeed *= 2f;
            }
        }

        public ManipulationMode Mode { get; set; }


        public void Slower()
        {
            if (Mode == ManipulationMode.Move)
            {
                MoveSpeed /= 2f;
            }

            if (Mode == ManipulationMode.Rotate)
            {
                RotateSpeed /= 2f;
            }

            if (Mode == ManipulationMode.Scale)
            {
                ScaleSpeed /= 2f;
            }
        }

        public void Manipulate(Vector3 manipulationData)
        {
            switch (Mode)
            {
                case ManipulationMode.Move:
                    Move(manipulationData);
                    break;
                case ManipulationMode.Rotate:
                    Rotate(manipulationData);
                    break;
                case ManipulationMode.Scale:
                    Scale(manipulationData);
                    break;
            }
        }

        void Move(Vector3 manipulationData)
        {
            var delta = manipulationData * MoveSpeed;
            if (CollisonDetector.CheckIfCanMoveBy(delta))
            {
                transform.localPosition += delta;
            }
        }
        /*
         * The rotate for each direction can be toggled in the unity editor.
         * When manipulating, the direction which is manipulated most will be used to rotate.
         * This to make the rotating easier for the user.
         */
        void Rotate(Vector3 manipulationData)
        {
            Vector3 manip = manipulationData;

            transform.RotateAround(transform.position, Vector3.up, Camera.main.transform.InverseTransformVector(manipulationData).x * RotateSpeed);


            /*if (rotateX && Mathf.Abs(manip.x) > Mathf.Abs(manip.y) && Mathf.Abs(manip.x) > Mathf.Abs(manip.z))
            {
               transform.RotateAround(transform.position, Camera.main.transform.up,
                    -manipulationData.x * RotateSpeed);
            }
            else if (rotateY && Mathf.Abs(manip.y) > Mathf.Abs(manip.x) && Mathf.Abs(manip.y) > Mathf.Abs(manip.z))
            {
                transform.RotateAround(transform.position, Camera.main.transform.forward,
                    manipulationData.y * RotateSpeed);
            }
            else if (rotateZ && Mathf.Abs(manip.z) > Mathf.Abs(manip.x) && Mathf.Abs(manip.z) > Mathf.Abs(manip.y))
            {
                transform.RotateAround(transform.position, Camera.main.transform.right,
                    manipulationData.z * RotateSpeed);
            }*/
        }

        void Scale(Vector3 manipulationData)
        {
            transform.localScale *= 1.0f - (Camera.main.transform.InverseTransformVector(manipulationData).z * ScaleSpeed);
        }

        IEnumerator waitFunction()
        {
            yield return new WaitForSecondsRealtime(1f);
        }
    }

}
