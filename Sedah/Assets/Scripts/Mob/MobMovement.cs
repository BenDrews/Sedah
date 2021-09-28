using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sedah
{
    public class MobMovement : ServerMovement
    {
        public void CallMove(Vector3 point)
        {
            base.Update();
            SetMoveState(point);
                        
        }   
    }
}

