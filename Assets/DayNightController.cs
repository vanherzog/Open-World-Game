using UnityEngine;

public class DayNightController : MonoBehaviour
{
    public Light sunLight;
    public Material daySkybox;
    public Material nightSkybox;
    public float dayRotationSpeed = 10f;
    public float nightRotationSpeed = 8f;

    private Quaternion dayRotation;
    private Quaternion nightRotation;

    private bool isDay = true;
    private bool switchToNight = false;
    public float switchToNightDelay = 5f;
    public float switchToNightTimer = 0f;

    private void Start()
    {
        // Store the initial rotation of the light as the day rotation
        dayRotation = sunLight.transform.rotation;

        // Calculate the night rotation by rotating the day rotation 180 degrees around the x-axis
        nightRotation = Quaternion.Euler(dayRotation.eulerAngles.x + 180f, dayRotation.eulerAngles.y, dayRotation.eulerAngles.z);

        // Apply the initial skybox
        RenderSettings.skybox = daySkybox;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            // Start the delay timer to switch to night
            switchToNight = true;
            switchToNightTimer = 0f;
        }
        else if (Input.GetKeyDown(KeyCode.E))
        {
            // Switch to day immediately
            isDay = true;
            RenderSettings.skybox = daySkybox;
        }

        // Check if the delay timer has reached the desired delay time
        if (switchToNight)
        {
            isDay = false;
            StartCoroutine(Night());
        }

        // Rotate the light based on the current time of day
        float rotationSpeed = isDay ? dayRotationSpeed : nightRotationSpeed;
        Quaternion targetRotation = isDay ? dayRotation : nightRotation;
        sunLight.transform.rotation = Quaternion.RotateTowards(sunLight.transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

        
    }

    private System.Collections.IEnumerator Night()
    {
        yield return new WaitForSeconds(15);
        // Switch to night after the delay
        RenderSettings.skybox = nightSkybox;
        switchToNight = false;
    }
}
