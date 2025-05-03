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
            JointMotor2D rightShoulderMotor = rightShoulder.motor;
            rightShoulderMotor.motorSpeed *= -1;

            isInversed = pressed;
        }
    }

    // public void OnMoveRightShoulder(int val)
    public void OnMoveRightShoulder(InputValue value)
    {
        bool pressed = value.isPressed;
        print("Test..."+pressed); 
        MoveLimb(rightShoulder, (pressed ? 100 : 0) * (isInversed ? -1 : 1));
    }

    private void MoveLimb(HingeJoint2D whatLimb, float motorSpeed)
    {
        whatLimb.motor = new JointMotor2D { motorSpeed = motorSpeed, maxMotorTorque = 10000 };
    }
}
