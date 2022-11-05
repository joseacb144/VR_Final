using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; //for text

public class MainScript : MonoBehaviour
{

    // GAME OBJECTS
    public GameObject myRightHand; // SHOOTING
    public Text scoreTextField;
    public int score_count = 0;

    private Vector3 dir = Vector3.left; // USED to move SPHERE
    public GameObject mySphere;

    private Vector3 dir1 = Vector3.down; // USED to move LIL SPHERE
    public GameObject myLilSphere;

    public GameObject myBigSphere;
    int myBigSphere_life = 5;
    bool bigSphereActive = true;

    //TIMER
    public Text gameTimer_text;
    public float TimeLeft;
    public bool TimerOn = false;

    //GUN AUDIO
    public AudioSource gunShot;

    // Start is called before the first frame update
    void Start()
    {
        TimerOn = true;
    }

    // Update is called once per frame
    void Update()
    {
        int TimeLeft_int = (int)TimeLeft;
        // TIMER
        if (TimerOn)
        {
            if(TimeLeft > 0)
            {
                TimeLeft -= Time.deltaTime;
                updateTimer(TimeLeft);
            }
            else
            {
                Debug.Log("Time is up!!!");
                TimeLeft = 0;
                TimerOn = false;
            }
        }

        // MOVE SPHERE
        mySphere.transform.Translate(dir * 0.5f * Time.deltaTime);

        if (mySphere.transform.position.x <= -4)
        {
            dir = Vector3.right;
        }
        else if (mySphere.transform.position.x >= 4)
        {
            dir = Vector3.left;
        }

        // MOVE LIL SPHERE
        myLilSphere.transform.Translate(dir1 * 0.80f * Time.deltaTime);
        if(myLilSphere.transform.position.y <= 0.5)
        {
            dir1 = Vector3.up;
        }
        else if(myLilSphere.transform.position.y >= 3)
        {
            dir1 = Vector3.down;
        }

        // SHOOTING
        if(OVRInput.GetDown(OVRInput.Button.SecondaryIndexTrigger))
        {

            // GUN SHOT audio when shooting
            gunShot.Play();

            Ray myRay = new Ray(myRightHand.transform.position, myRightHand.transform.forward);
            RaycastHit myHits;

            if(Physics.Raycast(myRay, out myHits, 1000f))
            {
                //myHits.transform.gameObject is the GameObject of what it hits
                //Destroy(myHits.transform.gameObject);
                GameObject whatIhit = myHits.transform.gameObject;

                if(whatIhit.name == "Sphere")
                {
                    score_count += 1;
                    scoreTextField.text = "Score: " + score_count;
                }
                else if(whatIhit.name == "lil_sphere")
                {
                    score_count += 3;
                    scoreTextField.text = "Score: " + score_count;
                }
                else if(whatIhit.name == "big_sphere")
                {
                    myBigSphere_life -= 1;
                    if (myBigSphere_life == 0)
                    {
                        score_count += 5;
                        scoreTextField.text = "Score: " + score_count;
                        bigSphereActive = false;
                        myBigSphere.SetActive(bigSphereActive);
                    }
                }
            }
        }

        if(bigSphereActive == false && TimeLeft_int % 5 == 0)
        {
            bigSphereActive = true;
            myBigSphere.SetActive(bigSphereActive);
            myBigSphere_life = 5;
        }
    }

    void updateTimer(float currentTime)
    {
        currentTime += 1;
        float minutes = Mathf.FloorToInt(currentTime / 60);
        float seconds = Mathf.FloorToInt(currentTime % 60);

        gameTimer_text.text = string.Format("{0:00} : {1:00}", minutes, seconds);
    }
}
