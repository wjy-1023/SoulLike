﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdCamera : MonoBehaviour
{
    //// Start is called before the first frame update
    //public float cameraMoveSpeed = 10f;
    //public float cameraRotSpeed = 30f;
    //bool isRotateCamera = false;

    //private float trans_y = 0;
    //private float trans_x = 0;
    //private float trans_z = 0;

    //private float eulerAngles_x;
    //private float eulerAngles_y;

    //// Use this for initialization
    //void Start()
    //{

    //    Vector3 eulerAngles = this.transform.eulerAngles;//当前物体的欧拉角

    //    this.eulerAngles_x = eulerAngles.y;

    //    this.eulerAngles_y = eulerAngles.x;
    //}


    //void FixedUpdate()
    //{


    //    if (Input.GetMouseButton(1))
    //    {

    //        this.eulerAngles_x += (Input.GetAxis("Mouse X") * this.cameraRotSpeed) * Time.deltaTime;

    //        this.eulerAngles_y -= (Input.GetAxis("Mouse Y") * this.cameraRotSpeed) * Time.deltaTime;

    //        Quaternion quaternion = Quaternion.Euler(this.eulerAngles_y, this.eulerAngles_x, (float)0);

    //        this.transform.rotation = quaternion;

    //        moveCameraByKey(cameraMoveSpeed);
    //    }


    //    this.trans_z = (Input.GetAxis("Mouse ScrollWheel") * this.cameraMoveSpeed * 2) * Time.deltaTime;
    //    this.transform.Translate(Vector3.forward * this.trans_z);

    //    //if (Input.GetMouseButton(2))
    //    //{
    //    //    this.trans_y = (Input.GetAxis("Mouse Y") * this.ySpeed / 2) * 0.02f;
    //    //    this.trans_x = (Input.GetAxis("Mouse X") * this.xSpeed / 2) * 0.02f;

    //    //    this.transform.Translate(-1 *Vector3.right * this.trans_x);
    //    //    this.transform.Translate(-1 *Vector3.up * this.trans_y);
    //    //}
    //}


    //void moveCameraByKey(float speed)
    //{

    //    if (Input.GetKey(KeyCode.Q))
    //    {
    //        this.transform.Translate(Vector3.down * speed * Time.deltaTime);
    //    }

    //    if (Input.GetKey(KeyCode.E))
    //    {
    //        this.transform.Translate(Vector3.up * speed * Time.deltaTime);
    //    }

    //    float moveV = Input.GetAxis("Vertical");
    //    float moveH = Input.GetAxis("Horizontal");

    //    this.transform.Translate(Vector3.forward * speed * moveV * Time.deltaTime + Vector3.right * speed * moveH * Time.deltaTime);
    //}
    // 摄像机跟随的对象

    public Transform target;

    // The speed with which the camera will be following.

    public float smoothing = 5f;

    //偏移量

    Vector3 offset;

    void Start()
    {

        //计算偏移量

        offset = transform.position - target.position;

    }



    void LateUpdate()
    {
        Vector3 nextpos = target.forward;
        Vector3 targetCamPos = target.position + offset;

        transform.position = Vector3.Lerp(transform.position, targetCamPos, smoothing * Time.deltaTime);

        this.transform.position = nextpos;

        this.transform.LookAt(target);
    }
}
