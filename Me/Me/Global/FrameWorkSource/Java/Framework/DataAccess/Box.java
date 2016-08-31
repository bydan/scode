package ByDan.Framework.DataAccess;

public class Box<T> {

    private T t; // T stands for "Type"          

    public void add(T t) {
        this.t = t;
    }

    public T get() {
        return t;
    }
}

