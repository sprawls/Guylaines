public class Pair<T, U>
{
    public Pair()
    {
    }

    public Pair(T first, U second)
    {
        this.First = first;
        this.Second = second;
    }

    public bool Empty { get { return First == null && Second == null; } }
    public T First { get; set; }
    public U Second { get; set; }
};

