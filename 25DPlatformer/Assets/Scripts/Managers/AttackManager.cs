using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace platformer
{

    public class AttackManager : Singleton<AttackManager>
    {
        public List<AttackInfo> CurrentsAttacks = new List<AttackInfo>();
    }
}
