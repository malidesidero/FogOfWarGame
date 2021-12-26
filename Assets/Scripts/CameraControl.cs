using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CameraControl : MonoBehaviour
{
    private Transform player;

    Camera mainCamera;
    Vector3 firstPos, secondPos, deltaPos;
    [Header("Camera Movement")]
    public bool camMovement;
    public float movementSpeed;
    [Header("Camera Rotation")]
    public bool camRotation;
    public float rotationSpeed;
    [Header("Camera Zoom")]
    public bool camZoom;
    public float zoomSpeed;
    
    private void Start()
    {
        player = GameManager.Instance.player.transform;
        mainCamera = Camera.main;
    }
    private void Update()
    {
        switch (GameManager.Instance.gameState)
        {
            case GameManager.GameState.Play:

                if (Input.GetKey(KeyCode.Space))
                {
                    SetCameraToPlayerPosition();
                }

                if (camZoom)
                {
                    CamZoom();
                }

                if (camRotation)
                {
                    CamRotation();
                }

                if (camMovement)
                {
                    CamMovement();
                }
                break;
            case GameManager.GameState.Pause:

                break;
        }
           
    }
    void CamRotation()
    {
        if (Input.GetMouseButtonDown(1))
        {
            firstPos = Input.mousePosition;
        }
        if (Input.GetMouseButton(1))
        {
            secondPos = Input.mousePosition;

            deltaPos = firstPos - secondPos;

            firstPos = secondPos;


            if (deltaPos.y > 1f && AngleConvert(transform.localEulerAngles.x) < 0)
            {
                Vector3 rot = transform.localEulerAngles;
                rot.x += rotationSpeed * Time.deltaTime;
                transform.localEulerAngles = rot;
            }
            else if (deltaPos.y < -1f && AngleConvert(transform.localEulerAngles.x) > -45)
            {
                Vector3 rot = transform.localEulerAngles;
                rot.x -= rotationSpeed * Time.deltaTime;
                transform.localEulerAngles = rot;
            }
        }
    }
    void CamMovement()
    {
        Vector3 tempPos = transform.position;
        if (Input.mousePosition.x < 0)
        {
            tempPos.x -= Time.deltaTime * movementSpeed * 1.5f;
        }
        else if (Input.mousePosition.x > Screen.width)
        {
            tempPos.x += Time.deltaTime * movementSpeed * 1.5f;
        }
        if (Input.mousePosition.y < 0)
        {
            tempPos.z -= Time.deltaTime * movementSpeed * 1.5f;
        }
        else if (Input.mousePosition.y > Screen.height)
        {
            tempPos.z += Time.deltaTime * movementSpeed * 1.5f;
        }

        if (Input.mousePosition.x < Screen.width / 10)
        {
            tempPos.x -= Time.deltaTime * movementSpeed;
        }
        else if (Input.mousePosition.x > Screen.width - Screen.width / 10)
        {
            tempPos.x += Time.deltaTime * movementSpeed;
        }
        if (Input.mousePosition.y < Screen.height / 10)
        {
            tempPos.z -= Time.deltaTime * movementSpeed;
        }
        else if (Input.mousePosition.y > Screen.height - Screen.height / 10)
        {
            tempPos.z += Time.deltaTime * movementSpeed;
        }

        transform.position = tempPos;
    }
    void CamZoom()
    {
        if (Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            if (Vector3.Distance(transform.position, mainCamera.transform.position) > 10)
                mainCamera.transform.position += mainCamera.transform.forward * Time.deltaTime * zoomSpeed;
        }

        if (Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            if (Vector3.Distance(transform.position, mainCamera.transform.position) < 30)
                mainCamera.transform.position -= mainCamera.transform.forward * Time.deltaTime * zoomSpeed;
        }
    }
    public void CameraOnMap()
    {
        camMovement = false;
    }
    public void CameraExitOnMap()
    {
        camMovement = true;
    }
    float AngleConvert(float _angle)
    {
        float angle = _angle;
        angle = (angle > 180) ? angle - 360 : angle;
        return angle;
    }
    void SetCameraToPlayerPosition()
    {
        transform.DOMove(player.position, 0.6f).SetEase(Ease.OutQuad);
    }
}
