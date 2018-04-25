using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ObjectPool<T> where T : class, new()
{
    public Stack<T> objectStack;

    private Action<T> resetAction;
    private Action<T> onetimeInitAction;

    public ObjectPool(int initialBufferSize,Action<T> resetAction=null,Action<T> onetimeInitAction = null)
    {
        objectStack = new Stack<T>(initialBufferSize);
        this.resetAction = resetAction;
        this.onetimeInitAction = onetimeInitAction;
    }

	public T New()
    {
        if (objectStack.Count > 0)
        {
            T t = objectStack.Pop();

            if (resetAction != null)
                resetAction(t);
            
            return t;
        }
        else
        {
            T t = new T();
            if (onetimeInitAction != null)
                onetimeInitAction(t);
            return t;
        }
    }
    public void Store(T obj)
    {
        objectStack.Push(obj);
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
