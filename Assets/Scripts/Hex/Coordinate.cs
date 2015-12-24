public class Coordinate {
    int x, y;

    public Coordinate(int x, int y)
    {
        this.x = x;
        this.y = y;
    }

    public override int GetHashCode() //ensure coordinate return the same hash if they are the same x and y. 
    {
        return x * 10000 + y; //coordinates not greater than 10000.
    }

    public override bool Equals(object obj)
    {
        return (this.GetHashCode() == obj.GetHashCode());
    }
}
