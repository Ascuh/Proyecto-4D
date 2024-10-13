using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movimientoArma : MonoBehaviour
{
    public Transform cameraTransform; // Asigna la cámara en el inspector
    public float horizontalFollowIntensity = 0.1f; // Intensidad del movimiento horizontal
    public float followSpeed = 5f; // Velocidad de interpolación

    private Vector3 initialWeaponPosition;
    private float lastCameraYaw; // Último valor de la rotación horizontal (yaw)

    void Start()
    {
        initialWeaponPosition = transform.localPosition; // Guardamos la posición inicial del arma
        lastCameraYaw = cameraTransform.rotation.eulerAngles.y; // Guardamos la rotación Y inicial
    }

    void Update()
    {
        // Detectar el cambio en la rotación horizontal (yaw)
        float currentCameraYaw = cameraTransform.rotation.eulerAngles.y;
        float yawDelta = Mathf.DeltaAngle(lastCameraYaw, currentCameraYaw);

        // Aplicar un movimiento sutil al arma basado en el cambio de yaw
        Vector3 targetPosition = initialWeaponPosition + new Vector3(yawDelta * horizontalFollowIntensity, 0, 0);
        transform.localPosition = Vector3.Lerp(transform.localPosition, targetPosition, followSpeed * Time.deltaTime);

        // Actualizar el valor de la última rotación horizontal
        lastCameraYaw = currentCameraYaw;
    }
}