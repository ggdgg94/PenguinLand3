using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{

    Vector3 samount = new Vector3(1f, 1f, 0);
    AnimationCurve curve = AnimationCurve.EaseInOut(0,1,1,0);
    public float cameraTime = 1.0f;
    Vector3 lpos = new Vector3();
    Vector3 npos = new Vector3();
    float lfov = 0.0f;
    float nfov = 0.0f;



    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        SetDirection();
        Move(direction, 5.0f);
        Shake();
    }
    public override void SetDirection()
    {
        direction.Set(Mathf.RoundToInt(Input.GetAxisRaw("Horizontal")), Mathf.RoundToInt(Input.GetAxisRaw("Vertical")), 0);
    }

    void ResetCamera()
    {
        Camera.main.transform.Translate(-lpos);

        lpos = npos = Vector3.zero;
        lfov = nfov = 0;
    }

    void Shake()
    {
        if(cameraTime > 0){
            cameraTime -= Time.deltaTime;
            npos = (Mathf.PerlinNoise(cameraTime * 4, cameraTime * 4 * 2) - 0.5f) * transform.right * curve.Evaluate(1.0f - cameraTime / 1.0f );
            Camera.main.transform.Translate(npos - lpos);
            lpos = npos;
        }else{
            ResetCamera();
        }
    }
    

    /* 
    void Shake(){
        if(cameraTime > 0){
            cameraTime -= Time.deltaTime;
            if(cameraTime > 0){
                npos = (Mathf.PerlinNoise(cameraTime * 2, cameraTime * 2 * 2) - 0.5f) * samount.x * transform.right * curve.Evaluate(1f - cameraTime / 1.0f) ;
                //+
                //       (Mathf.PerlinNoise(cameraTime * 2, cameraTime * 2 * 2) - 0.5f) * samount.y * transform.up * curve.Evaluate(1f - cameraTime / 1.0f);
                nfov = (Mathf.PerlinNoise(cameraTime * 2, cameraTime * 2 * 2) - 0.5f) * samount.z * curve.Evaluate(1f - cameraTime / 1.0f);
                //nfov = 0;

                Camera.main.transform.Translate(npos -lpos);
                lpos = npos;
                lfov = nfov;
            }

        }else{
            ResetCamera();
        }
    }
    */


}




/* Camera Shake methods for 3D (modified for 2D, though 3D calculations still left in)

    Vector3 samount = new Vector3(1f, 1f, 0);
    AnimationCurve curve = AnimationCurve.EaseInOut(0,1,1,0);

    float cameraTime = 1.0f;
    Vector3 lpos = new Vector3();
    Vector3 npos = new Vector3();
    float lfov = 0.0f;
    float nfov = 0.0f;




    void ResetCamera()
    {
        Camera.main.transform.Translate(-lpos);
        Camera.main.fieldOfView -= lfov;

        lpos = npos = Vector3.zero;
        lfov = nfov = 0;
    }

    void Shake(){
        if(cameraTime > 0){
            cameraTime -= Time.deltaTime;
            if(cameraTime > 0){
                npos = (Mathf.PerlinNoise(cameraTime * 2, cameraTime * 2 * 2) - 0.5f) * samount.x * transform.right * curve.Evaluate(1f - cameraTime / 1.0f) +
                       (Mathf.PerlinNoise(cameraTime * 2, cameraTime * 2 * 2) - 0.5f) * samount.y * transform.up * curve.Evaluate(1f - cameraTime / 1.0f);
                nfov = (Mathf.PerlinNoise(cameraTime * 2, cameraTime * 2 * 2) - 0.5f) * samount.z * curve.Evaluate(1f - cameraTime / 1.0f);
                //nfov = 0;

                Camera.main.transform.Translate(npos -lpos);
                lpos = npos;
                lfov = nfov;
            }

        }else{
            ResetCamera();
        }
    }



*/







