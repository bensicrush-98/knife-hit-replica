using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogRotationScript : MonoBehaviour
{
    [System.Serializable]
    private class RotationElement
    {
#pragma warning disable 0649
        public float speed;
        public float duration;
#pragma warning restore 0649
    }

    [SerializeField]
    private RotationElement[] rotationPattern;
    private WheelJoint2D wheelJoint;
    private JointMotor2D motor;

    private void Awake()
    {
        wheelJoint = GetComponent<WheelJoint2D>();
        motor = new JointMotor2D();
        StartCoroutine("PlayRotationParent");
    }

    private IEnumerator PlayRotationParent()
    {
        int rotationIndex = 0;
        while (true)
        {
            yield return new WaitForFixedUpdate();
            motor.motorSpeed = rotationPattern[rotationIndex].speed;
            motor.maxMotorTorque = 10000;
            wheelJoint.motor = motor;
            yield return new WaitForSecondsRealtime(rotationPattern[rotationIndex].duration);
            rotationIndex++;
            rotationIndex = rotationIndex < rotationPattern.Length ? rotationIndex : 0;
        }
    }
}
