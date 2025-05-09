using Unity.Mathematics;
using UnityEngine;

public enum LookingDir
{
    Down,
    OtherPlayer
}

public class EyeFollow : MonoBehaviour
{
    public GameObject[] eyes;
    [SerializeField] public Transform downTrans, otherPlayerTrans;

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
        for (int i = 0; i < eyes.Length; i++)
        {
            Vector3 targetPosition;
            if (lookingDir == LookingDir.Down)
                targetPosition = (downTrans.position - eyes[i].transform.position).normalized;
            else
                targetPosition = (otherPlayerTrans.position - eyes[i].transform.position).normalized;

            Vector3 newDir = Vector3.RotateTowards(eyes[i].transform.forward, targetPosition, _speed * Time.deltaTime, 0.0F);

            eyes[i].transform.rotation = Quaternion.LookRotation(newDir);
        }


        // transform.LookAt(targetPosition * Time.deltaTime);

        // targetRotation = Quaternion.LookRotation(targetPosition);

        // transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, _speed * Time.deltaTime);
    }
}
