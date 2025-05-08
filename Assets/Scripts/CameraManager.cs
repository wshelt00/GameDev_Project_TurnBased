using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public GameObject CameraMain;
    public GameObject Camera2;
    public GameObject Camera3;
    public GameObject Camera4;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            CameraMain.SetActive(true);
            Camera2.SetActive(false);
            Camera3.SetActive(false);
            Camera4.SetActive(false);
        }
    }
    
}
