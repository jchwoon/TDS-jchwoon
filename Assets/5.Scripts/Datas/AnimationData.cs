using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationData
{
    private string moveName = "Run";
    private string skillName = "NormalAttack";
    private string dieName = "Die";

    public int MoveHash { get; private set; }
    public int SkillHash { get; private set; }
    public int DieHash { get; private set; }

    public AnimationData()
    {
        MoveHash = Animator.StringToHash(moveName);
        SkillHash = Animator.StringToHash(skillName);
        DieHash = Animator.StringToHash(dieName);
    }
}
