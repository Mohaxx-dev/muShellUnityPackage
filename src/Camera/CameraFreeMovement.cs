using UnityEngine;

namespace muShell.Camera3D
{
    public class CameraFreeMovement : MonoBehaviour
    {
        [SerializeField] private Camera Camera;
        [SerializeField] private float Speed = 10f;
        [SerializeField] private float MouseSensitivity = 100f;
        [SerializeField] private bool AllowSprint = false;
        [SerializeField] private float SprintSpeed = 20f;

        private float CurrentSpeed;
        private Vector3 MoveVector;



        void Start()
        {
            Cursor.lockState = CursorLockMode.Locked;
        }

        void Update()
        {
            MouseLook();
            CameraMovement();
        }

        void MouseLook()
        {
            float mouseX = Input.GetAxis("Mouse X") * this.MouseSensitivity * Time.deltaTime;
            float mouseY = Input.GetAxis("Mouse Y") * this.MouseSensitivity * Time.deltaTime;

            this.Camera.transform.localEulerAngles += new Vector3(-mouseY, mouseX);
        }

        void CameraMovement()
        {
            Transform cameraTransform = this.Camera.transform;

            this.MoveVector = Input.GetAxisRaw("Horizontal") * cameraTransform.right + Input.GetAxisRaw("Vertical") * cameraTransform.forward;
            if (Input.GetAxisRaw("Jump") > 0)
            {
                this.MoveVector.y = 1;
            }
            else if (Input.GetKey(KeyCode.LeftControl))
            {
                this.MoveVector.y = -1;
            }

            if (AllowSprint && Input.GetKey(KeyCode.LeftShift))
            {
                this.CurrentSpeed = this.SprintSpeed;
            }
            else
            {
                this.CurrentSpeed = this.Speed;
            }

            this.Camera.transform.position += this.MoveVector * this.CurrentSpeed * Time.deltaTime;
        }
    }
}
