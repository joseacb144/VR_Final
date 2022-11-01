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

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        

        // SHOOTING
        if(OVRInput.GetDown(OVRInput.Button.SecondaryIndexTrigger))
        {
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
            }
        }
    }
}
