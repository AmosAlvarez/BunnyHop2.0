using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BunnyJump : MonoBehaviour {

    [SerializeField]
    public static bool canJump;
    public bool inspectorCanJump;

    Animator anim;
    public Transform obstacleCheck;
    public Transform blockCheck;
    public Transform currentBlockCheck;

    GameObject sleigh;
    GameObject madriguera;
    GameObject world;
    bool isOnSleigh;

    public int jumpCount = 0;

    CameraRotation CR;

    public AudioClip zanahoria;
    private AudioSource audioSource;

    // Use this for initialization
    void Start () {
        canJump = true;
        anim = gameObject.GetComponent<Animator>();
        world = GameObject.FindGameObjectWithTag("Mundo");
        audioSource = GetComponent<AudioSource>();
    }

    void Awake()
    {
        CR = GameObject.Find("Center").GetComponent<CameraRotation>();
    }
	
	// Update is called once per frame
	void Update () {

        inspectorCanJump = canJump;

        if (canJump)
        {
            if (!isOnSleigh)
                transform.position = new Vector3(Mathf.RoundToInt(transform.position.x), 1, Mathf.RoundToInt(transform.position.z));

            if (CR.posicion0)
            {
                if (Input.GetKeyDown(KeyCode.D))
                {
                    transform.rotation = Quaternion.LookRotation(Vector3.forward);
                    jumpCount++;
                    Jump();
                }
                if (Input.GetKeyDown(KeyCode.S))
                {
                    transform.rotation = Quaternion.LookRotation(Vector3.right);
                    jumpCount++;
                    Jump();
                }
                if (Input.GetKeyDown(KeyCode.A))
                {
                    transform.rotation = Quaternion.LookRotation(Vector3.back);
                    jumpCount++;
                    Jump();
                }
                if (Input.GetKeyDown(KeyCode.W))
                {
                    transform.rotation = Quaternion.LookRotation(Vector3.left);
                    jumpCount++;
                    Jump();

                }
            }
            else if (CR.posicion1)

            {
                if (Input.GetKeyDown(KeyCode.S))
                {
                    transform.rotation = Quaternion.LookRotation(Vector3.forward);
                    jumpCount++;
                    Jump();
                }
                if (Input.GetKeyDown(KeyCode.A))
                {
                    transform.rotation = Quaternion.LookRotation(Vector3.right);
                    jumpCount++;
                    Jump();
                }
                if (Input.GetKeyDown(KeyCode.W))
                {
                    transform.rotation = Quaternion.LookRotation(Vector3.back);
                    jumpCount++;
                    Jump();
                }
                if (Input.GetKeyDown(KeyCode.D))
                {
                    transform.rotation = Quaternion.LookRotation(Vector3.left);
                    jumpCount++;
                    Jump();
                }
            }
            else if (CR.posicion2)

            {
                if (Input.GetKeyDown(KeyCode.A))
                {
                    transform.rotation = Quaternion.LookRotation(Vector3.forward);
                    jumpCount++;
                    Jump();
                }
                if (Input.GetKeyDown(KeyCode.W))
                {
                    transform.rotation = Quaternion.LookRotation(Vector3.right);
                    jumpCount++;
                    Jump();
                }
                if (Input.GetKeyDown(KeyCode.D))
                {
                    transform.rotation = Quaternion.LookRotation(Vector3.back);
                    jumpCount++;
                    Jump();
                }
                if (Input.GetKeyDown(KeyCode.S))
                {
                    transform.rotation = Quaternion.LookRotation(Vector3.left);
                    jumpCount++;
                    Jump();
                }
            }
            else if (CR.posicion3)

            {
                if (Input.GetKeyDown(KeyCode.W))
                {
                    transform.rotation = Quaternion.LookRotation(Vector3.forward);
                    jumpCount++;
                    Jump();
                }
                if (Input.GetKeyDown(KeyCode.D))
                {
                    transform.rotation = Quaternion.LookRotation(Vector3.right);
                    jumpCount++;
                    Jump();
                }
                if (Input.GetKeyDown(KeyCode.S))
                {
                    transform.rotation = Quaternion.LookRotation(Vector3.back);
                    jumpCount++;
                    Jump();
                }
                if (Input.GetKeyDown(KeyCode.A))
                {
                    transform.rotation = Quaternion.LookRotation(Vector3.left);
                    jumpCount++;
                    Jump();
                }
            }
        }
    }

    void Jump()
    {
        canJump = false;

        if (!isOnSleigh) // SI NO ESTA EN UN TRINEO
        {

            //MIRAR SI ESTÁ EN UN BLOQUE DE ARENA
            Collider[] currentBlock = Physics.OverlapSphere(currentBlockCheck.position, 0.1f);

            if (currentBlock[0].gameObject.tag == "Arena")
            {
                anim.Play("HeavyJump");
                return;
            }

            //SI NO, MIRAR SI TIENE UNA PIEDRA DELANTE
            else
            {
                Collider[] hitObstacles = Physics.OverlapSphere(obstacleCheck.position, 0.1f);

                if (hitObstacles.Length > 0)
                    for (int i = 0; i < hitObstacles.Length; i++)
                    {
                        if (hitObstacles[i].gameObject.tag == "Piedra")
                        {
                            anim.Play("StoneBounceNear");
                            return;
                        }
                    }

                //SI NO, MIRAR EN QUÉ TIPO DE BLOQUE VA A CAER
                else
                {
                    Collider[] hitBlocks = Physics.OverlapSphere(blockCheck.position, 0.1f);
                    if (hitBlocks.Length > 0)
                        for (int i = 0; i < hitBlocks.Length; i++)
                        {
                            if (hitBlocks[i].gameObject.tag == "Tortuga")
                            {
                                sleigh = hitBlocks[i].gameObject;
                                anim.Play("TrineoJumpOn");
                                return;
                            }
                            else if (hitBlocks[i].gameObject.tag == "Madriguera")
                            {
                                madriguera = hitBlocks[i].gameObject;
                                anim.Play("MadrigueraEnter");
                                return;
                            }
                            else if (hitBlocks[i].gameObject.tag == "Zanahoria")
                            {
                                anim.Play("NormalJump");
                                world.SendMessage("EndLevel");
                                return;
                            }
                            else if (hitBlocks[i].gameObject.tag == "Piedra")
                            {
                                anim.Play("StoneBounceFar");
                                return;
                            }
                            else if (hitBlocks[i].gameObject.tag == "Suelo" || hitBlocks[i].gameObject.tag == "Arena")
                            {
                                if (hitBlocks.Length > 1) // si hay mas de un tipo de bloque donde vamos a caer
                                {
                                    Debug.Log(hitBlocks.Length);
                                    if (hitBlocks[i+1].gameObject.tag == "Tortuga") // y el segundo es un trineo
                                    {
                                        sleigh = hitBlocks[i].gameObject;
                                        anim.Play("TrineoJumpOn");
                                        return;
                                    }
                                    else if (hitBlocks[i+1].gameObject.tag == "Zanahoria")
                                    {
                                        anim.Play("NormalJump");
                                        audioSource.clip = zanahoria;
                                        audioSource.Play();
                                        world.SendMessage("EndLevel");
                                        return;
                                    }
                                }
                                else
                                {
                                    anim.Play("NormalJump");
                                    return;
                                }

                            }
                            else if (hitBlocks[i].gameObject.tag == "Lava")
                            {
                                anim.Play("LavaJump");
                                return;
                            }
                            else if (hitBlocks[i].gameObject.tag == "Hielo")
                            {
                                anim.Play("SlideJump");
                                return;
                            }
                        }
                    //SI NO COLISIONA CON NINGUN BLOQUE CAE AL VACÍO (se puede comentar el anim play y descomantar lo que hay cometnado, no se cual queda mejor)
                    else
                    {
                        //anim.enabled = false;
                        //Rigidbody rb = gameObject.AddComponent(typeof(Rigidbody)) as Rigidbody;
                        //rb.AddForce(transform.right * 200);
                        //rb.AddForce(Vector3.up * 200);

                        anim.Play("DieJump");
                    }
                }
            }
        }
        else // SI SI ESTÁ EN UN TRINEO
        {
            isOnSleigh = false;
            anim.Play("TrineoJumpOff");
        }

    }

    void ResetCanJump() // LLAMADA AL PRINCIPIO DE LA ANIMACIÓN DE IDLE (asi solo puede saltar si está en condiciones nornales)
    {
        //check si tiene un bloque debajo
        Collider[] hitBlocks = Physics.OverlapSphere(currentBlockCheck.position, 0.1f);
        if (hitBlocks.Length > 0)
            canJump = true;
        else
            FallDie();
    }

    void FallDie() // LLAMADA DESDE DIEJUMP
    {
        anim.enabled = false;
        Rigidbody rb = gameObject.AddComponent(typeof(Rigidbody)) as Rigidbody;

        //foreach (Transform child in transform)
        //    child.gameObject.AddComponent<Rigidbody>();

        rb.AddForce (Vector3.down * 300);
    }

    void CheckIfOnIce()// LLAMDA AL FINAL DE LAS ANIMACONES DE SLIDEJUMP Y SLIDE
    {
        // SI TIENE UNA PIEDRA DELANTE PLAYEAR ANIM DE REBOTAR EN PIEDRA
        Collider[] hitObstacles = Physics.OverlapSphere(obstacleCheck.position, 0.1f);

        if (hitObstacles.Length > 0)
        {
            if (hitObstacles[0].gameObject.tag == "Piedra")
            {
                anim.Play("StoneSlideBounce");
                return;
            }
        }
        else
        {
            // SI NO, PLAYEAR ANIM DE DESLIZAR SI ESTA EN HIELO
            Collider[] CurrentBlock = Physics.OverlapSphere(currentBlockCheck.position, 0.1f);
            if (CurrentBlock[0].gameObject.tag == "Hielo")
            {
                anim.Play("Slide");
            }
            // SI NO, ICE RECOVERY QUE LUEGO VA A IDLE SOLO
            else
            {
                anim.Play("IceRecovery");
                canJump = true;
            }
        }
            
    }

    void MoveSleigh() // LLAMADA AL FINAL DE TRINEOJUMPON
    {
        isOnSleigh = true;
        sleigh.SendMessage("Move");
    }

    public void JumpOffSleigh() // LLAMADA DESDE EL CODIGO DEL TRINEO UNA VEZ TERMINA DE MOVERSE
    {
        isOnSleigh = false;
        anim.Play("TrineoJumpOff");
    }

    void MadrigueraEnter() // SE LLAMA DESD MADRIGUERA ENTER
    {
        world.SendMessage("RotateWorld");
    }

    public void RespawnBunny()
    {
        anim.Play("Idle");
    }

}
