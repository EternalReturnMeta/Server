using UnityEngine;
using Fusion;

public struct NetworkInputData : INetworkInput
{
    public Vector3 MoveDirection;
    public NetworkButtons Buttons;
}

public class Player : NetworkBehaviour
{
    private NetworkCharacterController characterController;

    private void Awake()
    {
        characterController = GetComponent<NetworkCharacterController>();
    }
    
    public override void FixedUpdateNetwork()
    {
        if (GetInput(out NetworkInputData data))
        {
            if (!HasStateAuthority)
            {
                return;
            }
            
            data.MoveDirection.Normalize();
            characterController.Move(3 * data.MoveDirection * Runner.DeltaTime);
        }
    }
}
