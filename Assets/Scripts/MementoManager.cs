using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MementoManager
{
    public List<Memento> mementos { get; set; } = JsonUtility.FromJson<List<Memento>>("path");
    private IMementable _mementoObject;

    public MementoManager(IMementable mementoObject) => _mementoObject = mementoObject;

    public void Backup()
    {
        mementos.Add(_mementoObject.Save());
    }

    public void Restore()
    {
        if (mementos.Count == 0) return;
        var memento = mementos.Last();
        _mementoObject.Restore(memento);
        mementos.Remove(memento);
    }
}

public interface IMementable
{
    public Memento Save();
    public void Restore(Memento memento);
}