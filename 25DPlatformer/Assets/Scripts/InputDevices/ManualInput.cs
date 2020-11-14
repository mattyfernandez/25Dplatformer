using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace platformer
{
    public class ManualInput : MonoBehaviour
    {
       private CharacterControl characterControl;
        // Start is called before the first frame update
        private void Awake()
        {
            characterControl = this.gameObject.GetComponent<CharacterControl>();
        }
        // Update is called once per frame
        void Update()
        {
            if (VirtualInputManager.Instance.MoveRight)
            {
                characterControl.MoveRight = true;
            }
            else
            {
                characterControl.MoveRight = false;
            }
            if (VirtualInputManager.Instance.MoveLeft)
            {
                characterControl.MoveLeft = true;
            }
            else
            {
                characterControl.MoveLeft = false;
            }
            if (VirtualInputManager.Instance.Jump)
            {
                characterControl.Jump = true;
            }
            else
            {
                characterControl.Jump = false;
            }

            if (VirtualInputManager.Instance.Attack)
            {
                characterControl.Attack = true;
            }
            else
            {
                characterControl.Attack = false;
            }
        }
    }

}
