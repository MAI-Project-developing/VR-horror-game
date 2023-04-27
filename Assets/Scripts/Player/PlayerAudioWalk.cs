using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAudioWalk : MonoBehaviour
{
    public CharacterController characterController;
    public Camera playerCamera;

    [Header("Mechanics Possibility")]
    [SerializeField] private bool canAudioWalk = true;

    [Header("Audio Settings")]
    [SerializeField] private float baseStepSpeed = 0.75f;
    [SerializeField] private AudioSource footstepsAudioSorce = default;
    [SerializeField] private AudioClip[] floorAudio = default;
    private float footstepsTimer = 0f;
    private float GetCurrentOffset => baseStepSpeed;
    private Coroutine footstepsRoutine;

    private float currentPosition;
    private float previousPosition;
    Vector3 OldPosition;

    // Start is called before the first frame update
    void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        characterController = GetComponent<CharacterController>();
        playerCamera = GetComponentInChildren<Camera>();
        OldPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (canAudioWalk)
        {
            HandleFootsteps();
        }
    }

    private void HandleFootsteps()
    {
        if (OldPosition != transform.position)
        {
            footstepsTimer -= Time.deltaTime;

            if (footstepsTimer <= 0)
            {
                if (Physics.Raycast(playerCamera.transform.position, Vector3.down, out RaycastHit hit, 5f))
                {
                    if (hit.collider.tag == "Footsteps/Floor")
                    {
                        footstepsAudioSorce.PlayOneShot(floorAudio[Random.Range(0, floorAudio.Length - 1)]);
                    }
                }
                footstepsTimer = GetCurrentOffset;
            }
        }
        OldPosition = transform.position;
    }
}
