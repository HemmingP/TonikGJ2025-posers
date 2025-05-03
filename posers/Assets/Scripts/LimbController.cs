using System.Threading;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent (typeof(PlayerInput))]
public class LimbController : MonoBehaviour
{
    public float motorSpeed = 100;
    public HingeJoint2D leftShoulder;
    public HingeJoint2D leftElbow;
    public HingeJoint2D rightShoulder;
    public HingeJoint2D rightElbow;
    public HingeJoint2D leftHip;
    public HingeJoint2D leftKnee;
    public HingeJoint2D rightHip;
    public HingeJoint2D rightKnee;
    private HingeJoint2D[] hingeJoints;

    private bool isInversed = false;

    void Start()
    {
        hingeJoints = new HingeJoint2D[] {leftShoulder, leftElbow, rightShoulder, rightElbow, leftHip, leftKnee, rightHip, rightKnee};
    }

    // Update is called once per frame
    void Update()
    {
        foreach(HingeJoint2D hingeJoint2D in hingeJoints)
        {
            MonitorJoint(hingeJoint2D);
        }
    }

    private void MonitorJoint(HingeJoint2D joint)
    {
        if (!joint.useMotor)
            return;

        JointMotor2D motor = joint.motor;

        // If there's a target speed but joint is not rotating
        if (Mathf.Abs(motor.motorSpeed) > 0.01f && Mathf.Abs(joint.jointSpeed) < 0.1f)
        {
            float angle = joint.jointAngle;
            JointAngleLimits2D limits = joint.limits;

            bool atMin = angle <= limits.min + 1;
            bool atMax = angle >= limits.max - 1;

            if ((motor.motorSpeed < 0 && atMin) || (motor.motorSpeed > 0 && atMax))
            {
                // Disable motor if stuck at limit
                joint.useMotor = false;
            }
        }
    }

    public void OnInverseDirection(InputValue value)
    {
        bool pressed = value.isPressed;

        if(pressed != isInversed)
        {
            if(isInversed)
                print("Inverse");
            else
                print("Not Inverse");
            JointMotor2D leftShoulderMotor = leftShoulder.motor;
            leftShoulderMotor.motorSpeed *= -1;
            JointMotor2D leftElbowMotor = leftElbow.motor;
            leftElbowMotor.motorSpeed *= -1;
            JointMotor2D rightShoulderMotor = rightShoulder.motor;
            rightShoulderMotor.motorSpeed *= -1;
            JointMotor2D rightElbowMotor = rightElbow.motor;
            rightElbowMotor.motorSpeed *= -1;
            JointMotor2D leftHipMotor = leftHip.motor;
            leftHipMotor.motorSpeed *= -1;
            JointMotor2D leftKneeMotor = leftKnee.motor;
            leftKneeMotor.motorSpeed *= -1;
            JointMotor2D rightHipMotor = rightHip.motor;
            rightHipMotor.motorSpeed *= -1;
            JointMotor2D rightKneeMotor = rightKnee.motor;
            rightKneeMotor.motorSpeed *= -1;

            isInversed = pressed;
        }
    }

    
    public void OnMoveLeftShoulder(InputValue value)
    {
        print("OnMoveLeftShoulder");
        bool pressed = value.isPressed;
        MoveLimb(leftShoulder, (pressed ? -motorSpeed : 0) * (isInversed ? -1 : 1));
    }
    public void OnMoveLeftElbow(InputValue value)
    {
        print("OnMoveLeftElbow");
        bool pressed = value.isPressed;
        MoveLimb(leftElbow, (pressed ? motorSpeed : 0) * (isInversed ? -1 : 1));
    }
    public void OnMoveRightShoulder(InputValue value)
    {
        print("OnMoveRightShoulder");
        bool pressed = value.isPressed;
        MoveLimb(rightShoulder, (pressed ? motorSpeed : 0) * (isInversed ? -1 : 1));
    }
    public void OnMoveRightElbow(InputValue value)
    {
        print("OnMoveRightElbow");
        bool pressed = value.isPressed;
        MoveLimb(rightElbow, (pressed ? -motorSpeed : 0) * (isInversed ? -1 : 1));
    }
    public void OnMoveLeftHip(InputValue value)
    {
        print("OnMoveLeftHip");
        bool pressed = value.isPressed;
        MoveLimb(leftHip, (pressed ? -motorSpeed : 0) * (isInversed ? -1 : 1));
    }
    public void OnMoveLeftKnee(InputValue value)
    {
        print("OnMoveLeftKnee");
        bool pressed = value.isPressed;
        MoveLimb(leftKnee, (pressed ? -motorSpeed : 0) * (isInversed ? -1 : 1));
    }
    public void OnMoveRightHip(InputValue value)
    {
        print("OnMoveRightHip");
        bool pressed = value.isPressed;
        MoveLimb(rightHip, (pressed ? motorSpeed : 0) * (isInversed ? -1 : 1));
    }
    public void OnMoveRightKnee(InputValue value)
    {
        print("OnMoveRightKnee");
        bool pressed = value.isPressed;
        MoveLimb(rightKnee, (pressed ? motorSpeed : 0) * (isInversed ? -1 : 1));
    }

private void MoveLimb(HingeJoint2D joint, float speed)
{
    joint.useMotor = speed != 0;
    joint.motor = new JointMotor2D { motorSpeed = speed, maxMotorTorque = 10000 };
}
}
