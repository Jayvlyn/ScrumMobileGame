using UnityEngine;

public class SimpleRotate : MonoBehaviour
{
    public float speed = 10;
    private void Update()
    {
        transform.localRotation = Quaternion.Euler(0,0, transform.localRotation.eulerAngles.z+(Time.deltaTime * speed));
    }
}
