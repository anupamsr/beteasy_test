namespace dotnet_code_challenge
{
    /// <summary>
    /// A common class that is independent of the remote feed's structure. To be used in client.
    /// </summary>
    public class Horse
    {
        public int Id;
        public string Name;
        public double Price;

        public override string ToString()
        {
            return Id.ToString() + "\t" + Name + "\t" + Price.ToString();
        }
    }
}
