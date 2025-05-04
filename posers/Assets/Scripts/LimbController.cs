using System.Threading;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Linq;

[RequireComponent (typeof(PlayerInput))]
[RequireComponent (typeof(AudioSource))]
public class LimbController : MonoBehaviour
{
    public AudioClip[] characterBendingClips;
    public float motorSpeed = 100;
    public HingeJoint2D leftShoulder;
    public HingeJoint2D leftElbow;
    public HingeJoint2D rightShoulder;
    public HingeJoint2D rightElbow;
    public HingeJoint2D leftHip;
    public HingeJoint2D leftKnee;
    public HingeJoint2D rightHip;
    public HingeJoint2D rightKnee;

    private AudioSource audioSource => GetComponent<AudioSource>();
    private HingeJoint2D[] hingeJoints;
    private bool isInversed = false;

    void Awake()
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

        if(hingeJoints.Any(hingeJoint => hingeJoint.jointSpeed > 0.1) == false)
        {
            audioSource.Stop();
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
            foreach(HingeJoint2D hingeJoint in hingeJoints)
            {
                MoveLimb(hingeJoint, -hingeJoint.motor.motorSpeed);
            }

            isInversed = pressed;
        }
    }

    
    public void OnMoveLeftShoulder(InputValue value)
    {
        bool pressed = value.isPressed;
        MoveLimb(leftShoulder, (pressed ? -motorSpeed : 0) * (isInversed ? -1 : 1));
    }
    public void OnMoveLeftElbow(InputValue value)
    {
        bool pressed = value.isPressed;
        MoveLimb(leftElbow, (pressed ? motorSpeed : 0) * (isInversed ? -1 : 1));
    }
    public void OnMoveRightShoulder(InputValue value)
    {
        bool pressed = value.isPressed;
        MoveLimb(rightShoulder, (pressed ? motorSpeed : 0) * (isInversed ? -1 : 1));
    }
    public void OnMoveRightElbow(InputValue value)
    {
        bool pressed = value.isPressed;
        MoveLimb(rightElbow, (pressed ? -motorSpeed : 0) * (isInversed ? -1 : 1));
    }
    public void OnMoveLeftHip(InputValue value)
    {
        bool pressed = value.isPressed;
        MoveLimb(leftHip, (pressed ? -motorSpeed : 0) * (isInversed ? -1 : 1));
    }
    public void OnMoveLeftKnee(InputValue value)
    {
        bool pressed = value.isPressed;
        MoveLimb(leftKnee, (pressed ? -motorSpeed : 0) * (isInversed ? -1 : 1));
    }
    public void OnMoveRightHip(InputValue value)
    {
        bool pressed = value.isPressed;
        MoveLimb(rightHip, (pressed ? motorSpeed : 0) * (isInversed ? -1 : 1));
    }
    public void OnMoveRightKnee(InputValue value)
    {
        bool pressed = value.isPressed;
        MoveLimb(rightKnee, (pressed ? motorSpeed : 0) * (isInversed ? -1 : 1));
    }

private void MoveLimb(HingeJoint2D joint, float speed)
{
    joint.useMotor = speed != 0;
    joint.motor = new JointMotor2D { motorSpeed = speed, maxMotorTorque = 10000 };

    if(speed > 0)
    {
        // Play a sound if nothing is playing

        if(!audioSource.isPlaying)
        {
            AudioClip playAudioClip = characterBendingClips[Random.Range(0,characterBendingClips.Length)];

            audioSource.resource = playAudioClip;
            audioSource.Play();
        }
    }
}

}
