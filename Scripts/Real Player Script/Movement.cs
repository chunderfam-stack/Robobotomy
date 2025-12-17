using Godot;
using System;

public static class Movement
{
    public static void MoveMe(RigidBody3D rb, bool isGrounded, int speed, int jumpHeight)
    {
        Vector2 inputDir = Input.GetVector("Player_Move_Left", "Player_Move_Right", "Player_Move_Up", "Player_Move_Down");
        Vector3 direction = (rb.Transform.Basis * new Vector3(inputDir.X, 0, 0)).Normalized();
        Vector3 desiredVelocity = direction * speed;

        if(MathF.Sign(rb.LinearVelocity.X) == MathF.Sign(desiredVelocity.X))
        {
            if(MathF.Abs(rb.LinearVelocity.X) > Mathf.Abs(desiredVelocity.X))
            {
                return;
            }
        }
        if(!(MathF.Abs(rb.LinearVelocity.X) > 1 * speed))
        {
            Vector3 movement = desiredVelocity - rb.LinearVelocity;
            movement[1] = 0;
            rb.LinearVelocity += movement;
        }
        else
        {
            if(MathF.Sign(rb.LinearVelocity.X) != MathF.Sign(desiredVelocity.X))
            {
                rb.LinearVelocity += direction;
            }
        }
        
        if (Input.IsActionJustPressed("Player_Jump"))
        {
            if (isGrounded)
            {
                rb.ApplyCentralImpulse(Vector3.Up * jumpHeight);
            }
        }

        if (Input.IsActionPressed("Player_Recall"))
        {
            rb.LinearVelocity = new Vector3(40, rb.LinearVelocity.Y, rb.LinearVelocity.Z);
        }
    }

    public static bool OnHitFloor(Node3D body, int groundContacts)
    {
        groundContacts += 1;
        return (groundContacts > 0);
    }

    public static bool OnLeaveFloor(Node3D body, int groundContacts)
    {
        groundContacts -= 1;
        return groundContacts > 0;
    }

}
