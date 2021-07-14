using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(RPGCharacter))]
public class BuffSystem : MonoBehaviour
{
    [SerializeField] private RPGCharacter character;
    [SerializeField] private Dictionary<string, BuffInfo> buffMapping;

    private void Start() {
        buffMapping = new Dictionary<string, BuffInfo>();
    }

    private void Update() {
        List<BuffInfo> infoList = new List<BuffInfo>();
        infoList.AddRange(buffMapping.Values);
        foreach (BuffInfo info in infoList)
        {
            if (ShouldRemove(info)) {
                Remove(info.buff.name);
            }
        }
    }

    public void Add(BuffBase buff) {
        float currentTime = Time.time;
        BuffInfo info = new BuffInfo{
            buff = buff,
            lastUpdated = currentTime,
            startedAt = currentTime,
            expiredAt = currentTime + buff.duration,
        };
        buffMapping.Add(buff.name ,info);
        buff.OnAdd(character);
    }

    public void Remove(string name) {
        if (!IsHave(name)) return;
        BuffInfo info = buffMapping[name];

        buffMapping.Remove(name);
        info.buff.OnRemove(character);
    }

    public bool IsHave(string buffName) {
        return buffMapping.ContainsKey(buffName);
    }

    public BuffInfo GetInfo(string buffName) {
        return buffMapping[buffName];
    }

    private bool ShouldRemove(BuffInfo info) {
        BuffBase buff = info.buff;
        if (buff.isOneTimeUse && buff.isUse) return true;
        if (info.expiredAt < Time.time) return true;
        return false;
    }
}
