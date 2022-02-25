using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    private Rigidbody playerRb;
    [SerializeField] float speed;
    public float jumpForce;
    [SerializeField] float jumpTime;
    [SerializeField] float timeInAir;
    [SerializeField] GameObject mainCamera;
    // 1, 2, 3, and 4 for North, East, South, and West, respectively.
    private int cameraDir;
    public bool isOnGround { get; private set; }
    private Vector3 cameraOffset;
    private Vector2 cameraInput;
    public bool hasPowerup;
    public Powerup powerup;
    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        isOnGround = true;
        cameraDir = 1;
        cameraOffset = new Vector3(0, 15, -5);
        cameraInput = new Vector2(1, 1);
        hasPowerup = false;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 movement = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        if (movement != Vector2.zero) {
            Move(mainCamera.transform.forward * movement.y * Time.deltaTime * speed, ForceMode.Force);
            Move(mainCamera.transform.right * movement.x * Time.deltaTime * speed, ForceMode.Force);
        }
        if (Input.GetKeyDown(KeyCode.Space) && isOnGround) {
            Jump(1);
        } else if (Input.GetKey(KeyCode.Space) && timeInAir < jumpTime) {
            timeInAir += Time.deltaTime;
            playerRb.AddForce(Vector3.up * Time.deltaTime * jumpForce * 2, ForceMode.Acceleration);
        }
        if (Input.GetKey(KeyCode.C) && hasPowerup) {
            powerup.UsePowerup();
        }
        mainCamera.transform.position = transform.position + cameraOffset;
        if (Input.GetKeyDown(KeyCode.Q) || Input.GetKeyDown(KeyCode.E)) {
            MoveCamera((Input.GetKeyDown(KeyCode.Q) ? 1 : -1));
        }
    }

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Ground") || other.CompareTag("Platform")) {
            isOnGround = true;
        }
        if (other.CompareTag("Powerup")) {
            other.GetComponent<Powerup>().GetPowerup(this);
            Destroy(other.gameObject);
        }
    }

    private void MoveCamera(int direction) {
        mainCamera.transform.Rotate(new Vector3(-45, 0, 0));
        mainCamera.transform.Rotate(new Vector3(0, 90, 0) * direction);
        mainCamera.transform.Rotate(new Vector3(45, 0, 0));
        cameraDir += direction;
        if (cameraDir > 4) cameraDir = 1;
        if (cameraDir < 1) cameraDir = 4;
        switch (cameraDir) {
            case 1:
                cameraOffset = new Vector3(0, 15, -5);
                break;
            case 2:
                cameraOffset = new Vector3(-5, 15, 0);
                break;
            case 3:
                cameraOffset = new Vector3(0, 15, 5);
                break;
            case 4:
                cameraOffset = new Vector3(5, 15, 0);
                break;
        }
    }
    public Vector3 GetCameraForward() {
        return mainCamera.transform.forward;
    }
    public void Jump(float multiplier) {
        Move(Vector3.up * jumpForce * multiplier, ForceMode.Impulse);
        isOnGround = false;
        timeInAir = 0;
    }
    public void Move(Vector3 direction, ForceMode fm) {
        playerRb.AddForce(direction, fm);
    }
}
