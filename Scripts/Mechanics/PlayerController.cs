using UnityEngine;



namespace Platformer.Mechanics
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private float maxSpeed = 2f;

        [SerializeField] private LayerMask moveLayerMask;


        public AudioClip coinAudio;
        public AudioClip deathAudio;
        public bool isControlEnable = true;


        private bool isPlayerDir;
        private bool canMove;


        private float dirY;

        internal Rigidbody _myRigidbody;
        public Animator _myAnimator;




        private const string IS_RUN = "IsRun";

        private void Awake()
        {
            _myRigidbody = GetComponent<Rigidbody>();
            _myAnimator = GetComponent<Animator>();
        }



        private void Update()
        {
            if (isControlEnable)
            {
                if (!canMove)
                {
                    HandleInterection();
                }
                else
                {
                    HandleMovement();
                }
            }
        }


        private void HandleInterection()
        {
            var DerictionLeft = new Ray(transform.position + new Vector3(0, 1f, 0), transform.TransformDirection(Vector3.left));
            var DerictionRight = new Ray(transform.position + new Vector3(0, 1f, 0), transform.TransformDirection(Vector3.right));
            var DerictionBotton = new Ray(transform.position, transform.TransformDirection(Vector3.down));
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.down), Color.red);


            Physics.Raycast(DerictionRight, out RaycastHit hitRight, 3f, moveLayerMask);
            Physics.Raycast(DerictionLeft, out RaycastHit hitLeft, 3f, moveLayerMask);
            Physics.Raycast(DerictionBotton, out RaycastHit hitBotton, 0.1f, moveLayerMask);

            if (hitRight.collider != null)
            {
                dirY = -90;
                return;
            }
            if (hitLeft.collider != null)
            {
                dirY = 90;
                return;
            }
            if (hitBotton.collider != null)
            {

                dirY = 90;
                return;
            }
            {
                transform.rotation = Quaternion.Euler(0, dirY, 0);
                canMove = true;
                Debug.Log("RayNotNull");
            }


        }
        private void HandleMovement()
        {
            var translate = Vector3.forward * Time.deltaTime * maxSpeed;
            bool isRun = translate != Vector3.zero;
            if (isRun)
            {
                _myAnimator.SetBool(IS_RUN, isRun);
            }
            var forwardDirection = new Ray(transform.position + new Vector3(0,0.5f,0), transform.TransformDirection(Vector3.forward));
            isPlayerDir = Physics.Raycast(forwardDirection, 0.5f, LayerMask.GetMask("Ground", "Pin"));
            //Debug.DrawRay(transform.position + new Vector3(0, 0.5f, 0), transform.TransformDirection(Vector3.forward), Color.red);
            if (isPlayerDir)
            {

                transform.Rotate(0, 180, 0);
                Debug.Log("isGround");
            }
            transform.Translate(translate);
        }
    }
}

