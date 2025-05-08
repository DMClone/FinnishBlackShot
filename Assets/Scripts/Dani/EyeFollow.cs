using Unity.Mathematics;
using UnityEngine;

public enum LookingDir
{
    Down,
    OtherPlayer
}

public class EyeFollow : MonoBehaviour
{
    [SerializeField] private Transform downTrans, otherPlayerTrans;

    [SerializeField] private float _speed;
    public LookingDir lookingDir;

    public void LookDown()
    {
        lookingDir = LookingDir.Down;
    }

    public void LookAtOtherPlayer()
    {
        lookingDir = LookingDir.OtherPlayer;
    }

    private void Update()
    {
        //     Quaternion targetRotation;
        Vector3 targetPosition;
        if (lookingDir == LookingDir.Down)
            targetPosition = (downTrans.position - gameObject.transform.position).normalized;
        else
            targetPosition = (otherPlayerTrans.position - gameObject.transform.position).normalized;

        Vector3 newDir = Vector3.RotateTowards(transform.forward, targetPosition, _speed * Time.deltaTime, 0.0F);

        transform.rotation = Quaternion.LookRotation(newDir);

        // transform.LookAt(targetPosition * Time.deltaTime);

        // targetRotation = Quaternion.LookRotation(targetPosition);

        // transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, _speed * Time.deltaTime);
    }
}
