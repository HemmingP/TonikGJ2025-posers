using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent (typeof(PlayerInput))]
public class LimbController : MonoBehaviour
{
    public HingeJoint2D leftShoulder;
    public HingeJoint2D leftElbow;
    public HingeJoint2D rightShoulder;
    public HingeJoint2D rightElbow;
    public HingeJoint2D leftHip;
    public HingeJoint2D leftKnee;
    public HingeJoint2D rightHip;
    public HingeJoint2D rightKnee;

    private bool isInversed = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        JointMotor2D a = new JointMotor2D();
        a.motorSpeed = 123;
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void OnInverseDirection(InputValue value)
    {
        bool pressed = value.isPressed;

        if(pressed != isInversed)
        {
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
        bool pressed = value.isPressed;
        MoveLimb(leftShoulder, (pressed ? 100 : 0) * (isInversed ? -1 : 1));
    }
    public void OnMoveLeftElbow(InputValue value)
    {
        bool pressed = value.isPressed;
        MoveLimb(leftElbow, (pressed ? 100 : 0) * (isInversed ? -1 : 1));
    }
    public void OnMoveRightShoulder(InputValue value)
    {
        bool pressed = value.isPressed;
        MoveLimb(rightShoulder, (pressed ? 100 : 0) * (isInversed ? -1 : 1));
    }
    public void OnMoveRightElbow(InputValue value)
    {
        bool pressed = value.isPressed;
        MoveLimb(rightElbow, (pressed ? 100 : 0) * (isInversed ? -1 : 1));
    }
    public void OnMoveLeftHip(InputValue value)
    {
        bool pressed = value.isPressed;
        MoveLimb(leftHip, (pressed ? 100 : 0) * (isInversed ? -1 : 1));
    }
    public void OnMoveLeftKnee(InputValue value)
    {
        bool pressed = value.isPressed;
        MoveLimb(leftKnee, (pressed ? 100 : 0) * (isInversed ? -1 : 1));
    }
    public void OnMoveRightHip(InputValue value)
    {
        bool pressed = value.isPressed;
        MoveLimb(rightHip, (pressed ? 100 : 0) * (isInversed ? -1 : 1));
    }
    public void OnMoveRightKnee(InputValue value)
    {
        bool pressed = value.isPressed;
        MoveLimb(rightKnee, (pressed ? 100 : 0) * (isInversed ? -1 : 1));
    }

    private void MoveLimb(HingeJoint2D whatLimb, float motorSpeed)
    {
        whatLimb.motor = new JointMotor2D { motorSpeed = motorSpeed, maxMotorTorque = 10000 };
    }
}
