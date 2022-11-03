using UnityEngine;
///   <summary>
///   Camera follow an Object, in this Case Player
///   </summary>
public class CameraFollow : MonoBehaviour
{
    [Header("Follow Parameters")]

    [Tooltip("Gameobject you want the camera to follow")]
    public Transform target = null;

    [SerializeField, Range(0.01f, 1f), Tooltip("How fast the camera follows the Object")]
    private float smoothSpeed = 0.125f;

    [SerializeField, Tooltip("Camera offset from the transform target")]
    private Vector3 offset = new Vector3(0f, 2.25f, -1.5f);

    private Vector3 velocity = Vector3.zero;


    ///   <summary>
    ///   Late Update runs after all the other Updates, this is to take into account new player movement 
    ///   </summary>
   
    private void LateUpdate()
    {
        //Get the posotion the camera should move to

        Vector3 desirePosition = target.position + offset;

        // Move the camera using a SmoothDamp funtion 

        transform.position = Vector3.SmoothDamp(transform.position, desirePosition, ref velocity, smoothSpeed);
    }


    public void CenterOnTarget()
    {
        transform.position = target.position + offset;
    }
}
