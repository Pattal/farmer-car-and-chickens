using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovment : MonoBehaviour
{
    [SerializeField] private float turningSpeed = 15f;
    [SerializeField] private WheelCollider leftFrontCar;
    [SerializeField] private WheelCollider rightFrontCar;
    [SerializeField] private Fuel fuel;
    [SerializeField] int points = 10;
    [SerializeField] TextMeshProUGUI pointsText;
    [SerializeField] GameObject chickenPrefab;
    [SerializeField] Transform placeToSpawnChicken;

    private Rigidbody rb;
    private float horizontalInput = 0;
    private bool jumpInput;
    private bool canJump = false;
    private float time;
    private int jumpForce = 15000;

    void Start()
    {
        fuel = GetComponent<Fuel>();
        rb = GetComponent<Rigidbody>();
        rb.centerOfMass = new Vector3(rb.centerOfMass.x, -1.0f, rb.centerOfMass.z);

        Chicken.Dead.AddListener(() => IncreasePoints(10));
    }

    void Update()
    {
        CaptureInput();

        if (points < 0) points = 0;
        pointsText.text = "Points: " + points.ToString();

        time += Time.deltaTime / 100;
    }

    private void FixedUpdate()
    {
        HandleTurning();

        leftFrontCar.motorTorque = 50f * time;
        rightFrontCar.motorTorque = 50f * time;
 
        if (jumpInput && canJump)
        {
            StartCoroutine(Jumping());
        }

    }

    private void CaptureInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        jumpInput = Input.GetKey(KeyCode.Space);

        if (Input.GetKey("escape"))
        {
            Application.Quit();
        }
    }

    private void HandleTurning()
    {
        var currentAngle = turningSpeed * horizontalInput;

        leftFrontCar.steerAngle = currentAngle;
        rightFrontCar.steerAngle = currentAngle;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Fuel"))
        {
            Destroy(other.gameObject);
            fuel.IncreaseFuel();

        }

        if (other.gameObject.CompareTag("Carrot"))
        {
            Destroy(other.gameObject);
            IncreasePoints();

        }
    }
    

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.gameObject.CompareTag("Chicken"))
        {
            return;
        }

        if (collision.collider.gameObject.CompareTag("Water"))
        {
            Invoke("EndOfTheGame", .5f);
        }
        else if (!collision.collider.gameObject.CompareTag("Cube"))
        {
            StartCoroutine(CollideWithObstacle(collision.gameObject));
            SpawnChicken();
        }

    }

    IEnumerator CollideWithObstacle(GameObject go)
    {
        yield return new WaitForSeconds(0.2f);
        fuel.DecreaseFuel(-10);
        leftFrontCar.motorTorque = 0;
        rightFrontCar.motorTorque = 0;
        Destroy(go);
    }

    private void IncreasePoints()
    {
        points++;
        pointsText.text = "Points: " + points.ToString();

        if (points >= 10) canJump = true;
    }

    public void IncreasePoints(int amount)
    {
        points += amount;
        pointsText.text = "Points: " + points.ToString();

        if (points >= 10) canJump = true;

    }

    private void Jump()
    {
        if(transform.position.y < 5.0f)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            Debug.Log("Jump");
        }
    }

    private IEnumerator Jumping()
    {
        points -= 10;
        canJump = false;
        while (transform.position.y < 2.0f)
        {
            rb.AddForce(Vector3.up * jumpForce * Time.deltaTime, ForceMode.Impulse);
            Debug.Log("Jump");
            yield return new WaitForEndOfFrame();
        }
    }

    private void SpawnChicken()
    {
        if(!Chicken.isExisted && Random.Range(0, 100) > 50)
        {
            Instantiate(chickenPrefab, placeToSpawnChicken);
        }   
    }

    private void ReloadTheGame()
    {
        SceneManager.LoadScene(0);
    }
}
