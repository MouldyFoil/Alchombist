using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceTowardsAim : MonoBehaviour
{
    [SerializeField] Transform aim;
    [SerializeField] FacingState[] facingStates;
    SpriteRenderer spriteRenderer;
    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.deltaTime > 0)
        {
            HandleFacing();
        }
    }

    private void HandleFacing()
    {
        foreach (FacingState state in facingStates)
        {
            float inspectorZAngle = aim.eulerAngles.z;
            if (inspectorZAngle > 180)
            {
                inspectorZAngle -= 180;
                inspectorZAngle = 180 - inspectorZAngle;
                inspectorZAngle *= -1;
            }
            if (state.inclusive)
            {
                if (inspectorZAngle >= state.minRange && inspectorZAngle <= state.maxRange)
                {
                    spriteRenderer.sprite = state.sprite;
                }
            }
            else
            {
                if (inspectorZAngle > state.minRange && inspectorZAngle < state.maxRange)
                {
                    spriteRenderer.sprite = state.sprite;
                }
            }
        }
    }
}
[Serializable]
public class FacingState
{
    public string name;
    public Sprite sprite;
    public float minRange;
    public float maxRange;
    public bool inclusive;
}
