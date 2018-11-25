using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GridMove : MonoBehaviour
{
    public GameObject[,] allTiles;
    public GameObject OldTile;
    public GameObject RabbitTile;
    public GameObject EndTile;
    public int StartTileX;
    public int StartTileZ;

    public int jumpDistance = 2;
    public float moveSpeed = 6f;
    public float gridSize = 4f;
    private Vector2 input;
    public bool isMoving = false;
    public Vector3 startPosition;
    public Vector3 endPosition;
    public GameObject BounceTile;
    public Vector3 currPosition;
    private float t;
    private float timePress = 1;
    //private Vector3 prevLoc = Vector3.zero;
    public bool isGrounded = false;
    private Animator anim;
    public bool inputTouch = false;

    public ParticleSystem groundGreenPar;
    public ParticleSystem groundBrownPar;
    public ParticleSystem rockPar;
    public GameObject Manager;
    public bool canMove = true;
    private bool touchedLava = false;

    private AudioSource audioSource;
    public AudioClip groundHit;
    public AudioClip rockHit;
    public AudioClip carrotHit;
    public AudioClip jump;
    public AudioClip victory;
    public AudioClip defeat;

    public bool Victory = false;
    public bool Death = false;
    public string NextLevel;
    public AudioSource musicLevel;

    CameraRotation CR;

    [HideInInspector] public bool isOnTortuga = false;
    GameObject turtle;

    void Awake()
    {

        CR = GameObject.Find("Center").GetComponent<CameraRotation>();
        
    }

    private void Start()
    {
        allTiles = GameObject.Find("BoardGen").GetComponent<BoardGenerator>().allTiles;
        RabbitTile = allTiles[StartTileX, StartTileZ];

        anim = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        Manager = GameObject.Find("Mundo");

    }

    public void Update()
    {
        if (!canMove)
        {
            GetComponent<Rigidbody>().isKinematic = true;
        }
        else GetComponent<Rigidbody>().isKinematic = false;

        if (!isMoving && isGrounded && canMove && !touchedLava)
        {
            if (CR.posicion0)
            {

                input = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

            }
            else if (CR.posicion1)
            {

                input = new Vector2(Input.GetAxis("Vertical") * (-1), Input.GetAxis("Horizontal"));

            }
            else if (CR.posicion2)
            {

                input = new Vector2(Input.GetAxis("Horizontal") * (-1), Input.GetAxis("Vertical")* (-1));

            }
            else if (CR.posicion3)
            {

                input = new Vector2(Input.GetAxis("Vertical"), Input.GetAxis("Horizontal") * (-1));

            }

            if (Mathf.Abs(input.x) > Mathf.Abs(input.y))
            {
                input.y = 0;
            }
            else if (Mathf.Abs(input.x) < Mathf.Abs(input.y))
            {
                input.x = 0;
            }
            else
            {
                transform.position = new Vector3 (RabbitTile.transform.position.x, transform.position.y, RabbitTile.transform.position.z);
            }

            if (input != Vector2.zero && (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.D)) && timePress > 0.8f)
            {
                inputTouch = true;
                timePress = 0;
                anim.SetTrigger("Move");
                audioSource.clip = jump;
                audioSource.Play();
                StartCoroutine(move(transform, jumpDistance));
            }
        }
        timePress += Time.deltaTime;

        if (Victory)
        {
            Victory = false;
            canMove = false;
            musicLevel.enabled = false;
            audioSource.clip = victory;
            audioSource.Play();
            Invoke("VictoryG", 2.5f);
        }

        if (Death)
        {
            Death = false;
            musicLevel.enabled = false;
            audioSource.clip = defeat;
            audioSource.Play();
            CR.posicion0 = true;
            CR.posicion1 = false;
            CR.posicion2 = false;
            CR.posicion3 = false;
        }

        if (isOnTortuga)
        {
            transform.position = new Vector3(turtle.transform.position.x, transform.position.y, turtle.transform.position.z);
        }
    }

    public void VictoryG()
    {
        SceneManager.LoadScene(NextLevel);
    }

    public IEnumerator move(Transform transform, int distance)
    {
        isMoving = true;
        startPosition = RabbitTile.transform.position;
        t = 0;

        if (input.y == 0)
        {
            EndTile = allTiles[RabbitTile.GetComponent<TileData>().PosX + Mathf.RoundToInt(Mathf.Sign(input.x)) * distance, RabbitTile.GetComponent<TileData>().PosZ];
        }
        else
        {
            EndTile = allTiles[RabbitTile.GetComponent<TileData>().PosX, RabbitTile.GetComponent<TileData>().PosZ + Mathf.RoundToInt(Mathf.Sign(input.y)) * distance];
        }

        endPosition = EndTile.transform.position;
        //endPosition = new Vector3(startPosition.x + System.Math.Sign(input.x) * gridSize, startPosition.y, startPosition.z + System.Math.Sign(input.y) * gridSize);

        if(startPosition.x > endPosition.x)
        {
            transform.eulerAngles = new Vector3(0,180,0);
        }
        else if (startPosition.x < endPosition.x)
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
        }
        else if (startPosition.z > endPosition.z)
        {
            transform.eulerAngles = new Vector3(0, 90, 0);
        }
        else
        {
            transform.eulerAngles = new Vector3(0, -90, 0);
        }
        
        while (t < 1f)
        {
            t += Time.deltaTime * (moveSpeed / gridSize);
            transform.position = Vector3.Lerp(startPosition, endPosition, t);
            yield return null;
        }
        isMoving = false;
        if (RabbitTile.GetComponent<TileData>().PosX != EndTile.GetComponent<TileData>().PosX || RabbitTile.GetComponent<TileData>().PosZ != EndTile.GetComponent<TileData>().PosZ)
        {
            OldTile = RabbitTile;
        }

        RabbitTile = allTiles[EndTile.GetComponent<TileData>().PosX, EndTile.GetComponent<TileData>().PosZ];
        yield return 0;
    }

    public IEnumerator slide(Transform transform)
    {
        isMoving = true;
        startPosition = RabbitTile.transform.position;
        t = 0;

        if (RabbitTile.GetComponent<TileData>().PosX < OldTile.GetComponent<TileData>().PosX)
        {
            EndTile = allTiles[RabbitTile.GetComponent<TileData>().PosX -1, RabbitTile.GetComponent<TileData>().PosZ];
        }
        else if (RabbitTile.GetComponent<TileData>().PosX > OldTile.GetComponent<TileData>().PosX)
        {
            EndTile = allTiles[RabbitTile.GetComponent<TileData>().PosX + 1, RabbitTile.GetComponent<TileData>().PosZ];
        }
        else if (RabbitTile.GetComponent<TileData>().PosZ < OldTile.GetComponent<TileData>().PosZ)
        {
            EndTile = allTiles[RabbitTile.GetComponent<TileData>().PosX, RabbitTile.GetComponent<TileData>().PosZ -1];
        }
        else if (RabbitTile.GetComponent<TileData>().PosZ > OldTile.GetComponent<TileData>().PosZ)
        {
            EndTile = allTiles[RabbitTile.GetComponent<TileData>().PosX, RabbitTile.GetComponent<TileData>().PosZ + 1];
        }

        endPosition = EndTile.transform.position;
        //endPosition = new Vector3(startPosition.x + System.Math.Sign(input.x) * gridSize, startPosition.y, startPosition.z + System.Math.Sign(input.y) * gridSize);

        if (startPosition.x > endPosition.x)
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
        }
        else if (startPosition.x < endPosition.x)
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
        }
        else if (startPosition.z > endPosition.z)
        {
            transform.eulerAngles = new Vector3(0, 90, 0);
        }
        else
        {
            transform.eulerAngles = new Vector3(0, -90, 0);
        }

        while (t < 1f)
        {
            t += Time.deltaTime * (moveSpeed / gridSize);
            transform.position = Vector3.Lerp(startPosition, endPosition, t);
            yield return null;
        }
        isMoving = false;
        if (RabbitTile.GetComponent<TileData>().PosX != EndTile.GetComponent<TileData>().PosX || RabbitTile.GetComponent<TileData>().PosZ != EndTile.GetComponent<TileData>().PosZ)
        {
            OldTile = RabbitTile;
        }
        RabbitTile = allTiles[EndTile.GetComponent<TileData>().PosX, EndTile.GetComponent<TileData>().PosZ];
        yield return 0;
    }

    public IEnumerator bounceOff(Transform transform, GameObject bounceTile)
    {
        isMoving = true;
        BounceTile = bounceTile;
        currPosition = transform.position;

        if (startPosition.x > endPosition.x)
        {
            EndTile = allTiles[BounceTile.GetComponent<TileData>().PosX + 1, BounceTile.GetComponent<TileData>().PosZ];
        }
        else if (startPosition.x < endPosition.x)
        {
            EndTile = allTiles[BounceTile.GetComponent<TileData>().PosX - 1, BounceTile.GetComponent<TileData>().PosZ];
        }
        else if (startPosition.z > endPosition.z)
        {
            EndTile = allTiles[BounceTile.GetComponent<TileData>().PosX, BounceTile.GetComponent<TileData>().PosZ + 1];
        }
        else
        {
            EndTile = allTiles[BounceTile.GetComponent<TileData>().PosX, BounceTile.GetComponent<TileData>().PosZ - 1];
        }

        endPosition = EndTile.transform.position;

        t = 0;
        while (t < 1f)
        {
            t += Time.deltaTime * (moveSpeed / (gridSize/2));
            transform.position = Vector3.Lerp(currPosition, endPosition, t);
            yield return null;
        }

        isMoving = false;
        if (RabbitTile.GetComponent<TileData>().PosX != EndTile.GetComponent<TileData>().PosX || RabbitTile.GetComponent<TileData>().PosZ != EndTile.GetComponent<TileData>().PosZ)
        {
            OldTile = RabbitTile;
        }
        RabbitTile = allTiles[EndTile.GetComponent<TileData>().PosX, EndTile.GetComponent<TileData>().PosZ];
        yield return 0;
    }

    public void ExitTurtle()
    {
        //anim.enabled = true;
        StartCoroutine(move(transform, 1));
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Piedra"))
        {
            StopAllCoroutines();
            rockPar.Play();
            audioSource.clip = rockHit;
            audioSource.Play();
            StartCoroutine(bounceOff(transform, collision.gameObject.GetComponent<BounceData>().bounceTile));
        }
        if (collision.gameObject.CompareTag("Suelo"))
        {
            isGrounded = true;
            if (canMove)
            {
                audioSource.clip = groundHit;
                audioSource.Play();
            }
            if (Manager.GetComponent<Manager>().levelUp)
            {
                groundGreenPar.Play();
            }
            else if (!Manager.GetComponent<Manager>().levelUp)
            {
                groundBrownPar.Play();
            }
        }

        if (collision.gameObject.CompareTag("Arena"))
        {
            isGrounded = true;
            if (canMove)
            {
                audioSource.clip = groundHit;
                audioSource.Play();
            }
            if (Manager.GetComponent<Manager>().levelUp)
            {
                groundGreenPar.Play();
            }
            else if (!Manager.GetComponent<Manager>().levelUp)
            {
                groundBrownPar.Play();
            }
        }

        if (collision.gameObject.CompareTag("Lava"))
        {
            isGrounded = true;
            if (canMove)
            {
                audioSource.clip = groundHit;
                audioSource.Play();
                touchedLava = true;
            }
        }
        if (collision.gameObject.CompareTag("Hielo"))
        {
            isGrounded = true;
            if (canMove)
            {
                audioSource.clip = groundHit;
                audioSource.Play();
                touchedLava = true;
            }
        }

        if (collision.gameObject.CompareTag("Tortuga"))
        {
            turtle = collision.gameObject;
            anim.enabled = false;
            isGrounded = true;
            isOnTortuga = true;

        }
    }
    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Suelo"))
        {
            isGrounded = true;
            jumpDistance = 2;
        }

        if (collision.gameObject.CompareTag("Arena"))
        {
            isGrounded = true;
            jumpDistance = 1;
        }

        if (collision.gameObject.CompareTag("Lava"))
        {
            isGrounded = true;
            if (!isMoving && isGrounded && canMove)
            {
                isGrounded = false;
                isMoving = true;
                timePress = 0;
                anim.SetTrigger("Move");
                audioSource.clip = jump;
                audioSource.Play();
                StartCoroutine(move(transform, jumpDistance));
            }
        }
        if (collision.gameObject.CompareTag("Hielo"))
        {
            isGrounded = true;
            if (!isMoving && isGrounded && canMove)
            {
                isGrounded = false;
                isMoving = true;
                timePress = 0;
                //anim.SetTrigger("Move");
                //audioSource.clip = jump;
                //audioSource.Play();
                StartCoroutine(slide(transform));
            }
        }
        if (collision.gameObject.CompareTag("Tortuga"))
        {
            isGrounded = true;
            if (!isMoving && isGrounded && canMove)
            {
                isGrounded = false;
                isMoving = true;
                timePress = 0;
                //anim.SetTrigger("Move");
                //audioSource.clip = jump;
                //audioSource.Play();
                //Turtle(collision.gameObject.transform);
            }
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Suelo"))
        {
            isGrounded = false;
        }
        if (collision.gameObject.CompareTag("Arena"))
        {
            isGrounded = false;
        }
        if (collision.gameObject.CompareTag("Lava"))
        {
            isGrounded = false;
            touchedLava = false;
        }
        if (collision.gameObject.CompareTag("Hielo"))
        {
            isGrounded = false;
            touchedLava = false;
        }
        if (collision.gameObject.CompareTag("Tortuga"))
        {
            isGrounded = false;
            isOnTortuga = false;
        }
    }
}


