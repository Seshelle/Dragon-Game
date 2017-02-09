using UnityEngine;
using System.Collections;
using UnitySampleAssets.CrossPlatformInput;

[RequireComponent(typeof(DragonController))]
public class DragoInput : MonoBehaviour
{
    private DragonController mydrago;
    private Vector3 m_CamForward;
    private Vector3 m_Move;
    private Transform m_Cam;

    private Animator anim;
    private Transform neck;
    private float chargeTime = 0;

    [Header("INPUTS")]
    public string Attack1 = "Fire1";
    public string Attack2 = "Fire2";
    public string Shift = "Fire3";

    public string Jump = "Jump";

    public KeyCode fly = KeyCode.Q;
    public KeyCode action = KeyCode.E;
    public KeyCode down = KeyCode.C;
    public KeyCode dodge = KeyCode.F;

    [Header("Swap Speed with Shift instead of 1 2 3")]
    public bool SpeedShiftSwap;
    //int SpeedCont;

    void Awake()
    {
        mydrago = GetComponent<DragonController>();
    }

    private void Start()
    {
        // get the transform of the main camera
        if (Camera.main != null)
        {
            m_Cam = Camera.main.transform;
        }
        else
        {
            Debug.LogWarning(
                "Warning: no main camera found. Third person character needs a Camera tagged \"MainCamera\", for camera-relative controls.");
            // we use self-relative controls in this case, which probably isn't what the user wants, but hey, we warned them!
        }
        //SpeedCont = (int)mydrago.StartSpeed-1;

        anim = GetComponent<Animator>();
        neck = anim.GetComponent<Transform>().Find("CG/Pelvis/Spine/Spine1/Neck/Neck1/Neck2");
    }


    void Update()
    {
        GetInput();
    }

    // Fixed update is called in sync with physics
    private void FixedUpdate()
    {
        // read inputs
        float h = CrossPlatformInputManager.GetAxis("Horizontal");
        float v = CrossPlatformInputManager.GetAxis("Vertical");
        if (mydrago.glideOnly && mydrago.Fly)
        {
            v = 1;
        }

        // calculate move direction to pass to character
        if (m_Cam != null)
        {
            // calculate camera relative direction to move:
            m_CamForward = Vector3.Scale(m_Cam.forward, new Vector3(1, 1, 1)).normalized;
            m_Move = v * m_CamForward + h * m_Cam.right;
        }
        else
        {
            // we use world-relative directions in the case of no main camera
            m_Move = v * Vector3.forward + h * Vector3.right;
        }

        mydrago.CameraMove(m_Move);
    }

    //----------------------Link the buttons pressed with correspond variables- here you can change the type of input------------------------------------------------------------------------------
   
    void GetInput()
    {

        mydrago.Horizontal = CrossPlatformInputManager.GetAxis("Horizontal");   //Get the Horizontal Axis
        mydrago.Vertical = CrossPlatformInputManager.GetAxis("Vertical");       //Get the Vertical Axis
        mydrago.Attack1 = CrossPlatformInputManager.GetButton(Attack1);         //Get the Attack1 button
        mydrago.Attack2 = CrossPlatformInputManager.GetButtonDown(Attack2);         //Get the Attack1 button

        if (Input.GetKeyDown(fly)) mydrago.Fly = !mydrago.Fly;          //Toogle the Fly button

        mydrago.Action = Input.GetKeyDown(action); //Get the Action/Emotion button


        /// <summary> 
        /// To activate Emotions/Actions  
        /// Set |mydrago.Action =true| and  mydrago.ActionID = to the Animator Action ID
        /// Theres and example using Zones to activate the actions or emotions. 
        /// </summary>  


        mydrago.Jump = CrossPlatformInputManager.GetButton(Jump);       //Get the Jump button
        mydrago.Shift = CrossPlatformInputManager.GetButton(Shift);                 //Get the Shift button  
        mydrago.Down = Input.GetKey(down);                         //Get the Down button
        mydrago.Dodge = Input.GetKey(dodge);                        //Get the Dodge button      

        //mydrago.Damage = Input.GetKeyDown(KeyCode.H);                   //Get the Damage button change the variable entry to manipulate how the damage works
        mydrago.Stun = Input.GetMouseButton(2);                         //Get the Stun button change the variable entry to manipulate how the stun works
        mydrago.Death = Input.GetKeyDown(KeyCode.K);                    //Get the Death button change the variable entry to manipulate how the death works


        //This will swap the velocities with one input so you dont need the 1 2 3 speed match
        if (SpeedShiftSwap)
        {

            if (CrossPlatformInputManager.GetButtonDown(Shift))
            {
                //SpeedCont++;
                mydrago.Speed3 = true;
                mydrago.Speed1 = mydrago.Speed2 = false;
            }
            else
            {
                mydrago.Speed2 = true;
                mydrago.Speed1 = mydrago.Speed3 = false;
            }

            /*if ((SpeedCont % 3) == 0)
            {
                mydrago.Speed1 = true;
                mydrago.Speed3 = mydrago.Speed2 = false;
               
            }
            if ((SpeedCont % 3) == 1)
            {
                mydrago.Speed2 = true;
                mydrago.Speed1 = mydrago.Speed3 = false;
            }
            if ((SpeedCont % 3) == 2)
            {
                mydrago.Speed3 = true;
                mydrago.Speed1 = mydrago.Speed2 = false;
            }*/

        }

        else
        {
            mydrago.Speed1 = Input.GetKeyDown(KeyCode.Alpha1);              //Walk
            mydrago.Speed2 = Input.GetKeyDown(KeyCode.Alpha2);              //Trot
            mydrago.Speed3 = Input.GetKeyDown(KeyCode.Alpha3);              //Run
        }

    }

    public void LateUpdate()
    {
        if (anim.GetBool("Shift"))
        {
            chargeTime += Time.deltaTime * 5;
            if (chargeTime > 1)
            {
                chargeTime = 1;
            }
        }
        else
        {
            if (chargeTime > 0)
            {
                chargeTime -= Time.deltaTime * 3;
            }
            else
            {
                chargeTime = 0;
            }
        }
        neck.transform.localRotation = Quaternion.Lerp(neck.transform.localRotation, Quaternion.Euler(0, 0, -30), chargeTime);
    }

}
