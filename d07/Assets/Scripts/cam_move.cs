using UnityEngine;
using System.Collections;
 
public class cam_move : MonoBehaviour {
    void Start()
    {
        Cursor.visible = false;
    }
    float mainSpeed = 30.0f;
    float shiftAdd = 250.0f; 
    float maxShift = 1000.0f; 
    float camSens = 0.25f; 
    private Vector3 lastMouse = new Vector3(0, 0, 0); 
    private float totalRun= 1.0f;
    
    void Update () {
        if (lastMouse[0] == 0 && lastMouse[1] == 0  && lastMouse[2] == 0)
            lastMouse = Input.mousePosition;
        if (Input.GetKey("q"))
        {
            transform.Rotate(0, -0.1f, 0,  Space.World);
        }
        if (Input.GetKey("e"))
        {
            transform.Rotate(0, 0.1f, 0,  Space.World);
        }
        if (Input.GetKey("p"))
        {
            transform.position = new Vector3(0, 15, -20);
            transform.rotation = Quaternion.Euler(25, 0, 0);
        }
        if (Input.GetKey("i"))
        {
            Debug.Log(transform.rotation);
            Debug.Log(transform.rotation[0]);

        }
        lastMouse = Input.mousePosition - lastMouse ;
        lastMouse = new Vector3(-lastMouse.y * camSens, lastMouse.x * camSens, 0 );
        lastMouse = new Vector3(transform.eulerAngles.x + lastMouse.x , transform.eulerAngles.y + lastMouse.y, 0);
        transform.eulerAngles = lastMouse;
        lastMouse =  Input.mousePosition; 
       
        Vector3 p = GetBaseInput();
        if (Input.GetKey (KeyCode.LeftShift)){
            totalRun += Time.deltaTime;
            p  = p * totalRun * shiftAdd;
            p.x = Mathf.Clamp(p.x, -maxShift, maxShift);
            p.y = Mathf.Clamp(p.y, -maxShift, maxShift);
            p.z = Mathf.Clamp(p.z, -maxShift, maxShift);
        }
        else{
            totalRun = Mathf.Clamp(totalRun * 0.5f, 1f, 1000f);
            p = p * mainSpeed;
        }
       
        p = p * Time.deltaTime;
        Vector3 newPosition = transform.position;
        if (Input.GetKey(KeyCode.Space)){
            transform.Translate(p);
            newPosition.x = transform.position.x;
            newPosition.z = transform.position.z;
            transform.position = newPosition;
        }
        else{
            transform.Translate(p);
        }
       
    }
    private Vector3 GetBaseInput() {
        Vector3 p_Velocity = new Vector3();
        if (Input.GetKey (KeyCode.W)){
            p_Velocity += new Vector3(0, 0 , 1);
        }
        if (Input.GetKey (KeyCode.S)){
            p_Velocity += new Vector3(0, 0, -1);
        }
        if (Input.GetKey (KeyCode.A)){
            p_Velocity += new Vector3(-1, 0, 0);
        }
        if (Input.GetKey (KeyCode.D)){
            p_Velocity += new Vector3(1, 0, 0);
        }
        return p_Velocity;
    }
}
