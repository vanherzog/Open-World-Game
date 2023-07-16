// Die Kiste in der Nähe wird nicht aufgehoben, es wird eien neue erstellt und das Wechseln klappt noch nicht perfekt.

using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
public class MoveController : MonoBehaviour
{
    public GameObject[] boxPrefabs;
    public LayerMask boxLayerMask;
    public float throwForce = 10f;
    public TextMeshProUGUI boxSelectionText;
    public RawImage[] boxSelectionImages;
    public GameObject ballPrefab;

    private Camera mainCamera;
    private GameObject currentBox;
    private bool isCarryingBox = false;
    private RaycastHit boxHit;
    private int selectedBoxIndex = 0;
    public GameObject menu;

    void Start()
    {
        mainCamera = Camera.main;
        UpdateBoxSelectionUI();
    }


    void Update()
    {   
        
        if (Keyboard.current.tKey.wasPressedThisFrame)
        {
            menu.SetActive(true);
        }
        

        if (Mouse.current.rightButton.wasPressedThisFrame)
        {
            ThrowBall();
        }

        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            if (isCarryingBox)
            {
                ReleaseBox();
            }
            else if (Physics.Raycast(mainCamera.transform.position, mainCamera.transform.forward, out boxHit, Mathf.Infinity, boxLayerMask))
            {
                PickUpBox();
            }
            else
            {
                SpawnBox();
            }
        }

        if (Mouse.current.leftButton.wasReleasedThisFrame)
        {
            ReleaseBox();
        }

        if (isCarryingBox)
        {
            MoveBoxWithPlayer();
        }

        if (Keyboard.current.digit1Key.wasPressedThisFrame)
        {
            SelectBox(0);
        }
        else if (Keyboard.current.digit2Key.wasPressedThisFrame)
        {
            SelectBox(1);
        }
    }

    private void ThrowBall()
    {
        GameObject ball = Instantiate(ballPrefab, mainCamera.transform.position, mainCamera.transform.rotation);
        Rigidbody ballRigidbody = ball.GetComponent<Rigidbody>();
        ballRigidbody.AddForce(mainCamera.transform.forward * throwForce, ForceMode.Impulse);
    }

    private void SpawnBox()
    {
        Vector3 spawnPosition = mainCamera.transform.position + mainCamera.transform.forward * 40f;
        currentBox = Instantiate(boxPrefabs[selectedBoxIndex], spawnPosition, Quaternion.identity);
        isCarryingBox = true;
    }

    private void PickUpBox()
    {
        currentBox = boxHit.collider.gameObject;
        currentBox.transform.parent = mainCamera.transform;
        isCarryingBox = true;
    }

    private void ReleaseBox()
    {
        currentBox.transform.parent = null;
        currentBox = null;
        isCarryingBox = false;
    }

    private void MoveBoxWithPlayer()
    {
        currentBox.transform.position = mainCamera.transform.position + mainCamera.transform.forward;
    }

    private void SelectBox(int index)
    {
        if (index >= 0 && index < boxPrefabs.Length)
        {
            selectedBoxIndex = index;
            UpdateBoxSelectionUI();
        }
    }

    private void UpdateBoxSelectionUI()
    {
        for (int i = 0; i < boxSelectionImages.Length; i++)
        {
            if (i == selectedBoxIndex)
            {
                boxSelectionImages[i].color = Color.yellow;
            }
            else
            {
                boxSelectionImages[i].color = Color.white;
            }
        }

    }
}
/**
using UnityEngine;
using UnityEngine.InputSystem;

public class MoveController : MonoBehaviour
{
    public GameObject ballPrefab;
    public GameObject boxPrefab;
    public LayerMask boxLayerMask;
    public float throwForce = 10f;

    private Camera mainCamera;
    private GameObject currentBox;
    private bool isCarryingBox = false;
    private RaycastHit boxHit;

    void Start()
    {
        mainCamera = Camera.main;
    }

    void Update()
    {
        if (Mouse.current.rightButton.wasPressedThisFrame)
        {
            ThrowBall();
        }

        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            if (isCarryingBox)
            {
                ReleaseBox();
            }
            else if (Physics.Raycast(mainCamera.transform.position, mainCamera.transform.forward, out boxHit, Mathf.Infinity, boxLayerMask))
            {
                PickUpBox();
            }
            else
            {
                SpawnBox();
            }
        }

        if (Mouse.current.leftButton.wasReleasedThisFrame)
        {
            ReleaseBox();
        }


        if (isCarryingBox)
        {
            MoveBoxWithPlayer();
        }
    }

    private void ThrowBall()
    {
        GameObject ball = Instantiate(ballPrefab, mainCamera.transform.position, mainCamera.transform.rotation);
        Rigidbody ballRigidbody = ball.GetComponent<Rigidbody>();
        ballRigidbody.AddForce(mainCamera.transform.forward * throwForce, ForceMode.Impulse);
    }

    private void SpawnBox()
    {
        Vector3 spawnPosition = mainCamera.transform.position + mainCamera.transform.forward * 7f;
        currentBox = Instantiate(boxPrefab, spawnPosition, Quaternion.identity);
        isCarryingBox = true;
    }

    private void PickUpBox()
    {
        currentBox = boxHit.collider.gameObject;
        currentBox.transform.parent = mainCamera.transform;
        isCarryingBox = true;
    }

    private void ReleaseBox()
    {
        currentBox.transform.parent = null;
        currentBox = null;
        isCarryingBox = false;
    }

    private void MoveBoxWithPlayer()
    {
        currentBox.transform.position = mainCamera.transform.position + mainCamera.transform.forward;
    }
}
**/