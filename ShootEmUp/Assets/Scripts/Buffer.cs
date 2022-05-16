using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Buffer : MonoBehaviour
{
    [SerializeField] private List<Buff> buffs = new List<Buff>();
    private Player p;
    private void Start()
    {
        p = GetComponent<Player>();
        if (p == null) Destroy(this.gameObject);
    }
    private void AddBuff(Buff b) 
    {
        if (ApplyBuff(b))
        {
            buffs.Add(b);
        }
    }
    private void Update()
    {
        if (buffs.Count != 0) 
        {
            for (int i = 0; i < buffs.Count; i++) 
            {
                buffs[i].RunTimer(Time.deltaTime);
                if (buffs[i].GetTimer() <= 0) 
                {
                    print($"effect wears out: {buffs[i].GetName()}");
                    DestroyBuff(buffs[i]);
                    buffs.RemoveAt(i);
                    i--;
                }
            }
        }
        if (Input.GetKeyDown(KeyCode.F)) // for testing only
        {
            AddBuff(new Buff("test_speed", TimedBuffType.speed, 2f, 5f, false));
        }
    }
    private bool ApplyBuff(Buff b) 
    {
        switch (b.GetBuffType()) 
        {
            case TimedBuffType.speed:
                // speed is not stackable (maybe yet)
                if (!b.isStackable()) 
                {
                    for (int i = 0; i < buffs.Count; i++) 
                    {
                        if (buffs[i].GetBuffType() == TimedBuffType.speed) 
                        {
                            if (buffs[i].GetMagnitude() < b.GetMagnitude())
                            {
                                p.SendMessage("SetBuff_speed", b.GetMagnitude());
                                buffs[i].ResetTimer();
                            }
                            else if ((buffs[i].GetMagnitude() == b.GetMagnitude()))
                            {
                                buffs[i].ResetTimer();
                            }
                            return false;
                        }
                    }
                    p.SendMessage("SetBuff_speed", b.GetMagnitude()); 
                    return true;
                }
                return false;
            case TimedBuffType.shield:
                return false;
            default:
                return false;
        }
    }
    private void DestroyBuff(Buff b) 
    {
        switch (b.GetBuffType())
        {
            case TimedBuffType.speed:
                p.SendMessage("SetBuff_speed", 1);
                break;
            case TimedBuffType.shield:
                break;
            default:
                break;
        }
    }
}

public class Buff 
{
    private string _name;
    private TimedBuffType _type;
    private float _magnitude;
    private float _time;
    private bool _stackable;
    private float timer;
    public Buff (string name, TimedBuffType type, float magnitude, float time, bool stackable) 
    {
        _name = name;
        _type = type;
        _magnitude = magnitude;
        _time = time;
        timer = time;
        _stackable = stackable;
    }
    public void ResetTimer() { timer = _time; }
    public void RunTimer(float t) { timer -= Mathf.Abs(t); }
    public string GetName() { return _name; }
    public TimedBuffType GetBuffType() { return _type; }
    public float GetMagnitude() { return _magnitude; }
    public float GetTimer() { return timer; }
    public bool isStackable() { return _stackable; }

}

public enum TimedBuffType 
{
    speed,
    shield,
}
