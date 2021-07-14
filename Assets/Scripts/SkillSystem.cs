using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillSystem : MonoBehaviour
{
    [SerializeField] private RPGCharacter character;
    [SerializeField] private List<Skill> skills;
    private int currentSelectSkill = 0;
    public bool canUseSkill = true;

    private void Start() {
        if (skills == null) {
            skills = new List<Skill>();
        }    
    }

    void Update()
    {
        foreach (var skill in skills) {
            skill.UpdateBy(character);
        }
    }

    private void FixedUpdate() {
        foreach (var skill in skills) {
            skill.FixedUpdateBy(character);
        }
    }

    public void UseBy(RPGCharacter character) {
        if (this.skills.Count < 1 || !canUseSkill) return;
        this.skills[this.currentSelectSkill].Use(character);
    }

    public void StopBy(RPGCharacter character) {
        if (this.skills.Count < 1) return;
        this.skills[this.currentSelectSkill].Stop(character);
    }

    public void Reset() {
        Select(0);
        foreach (var skill in skills) {
            skill.Reset(character);
        }
    }

    public void Select(int index) {
        currentSelectSkill = index;
        if (currentSelectSkill >= skills.Count) {
            currentSelectSkill = skills.Count - 1;
        }
    }
}
