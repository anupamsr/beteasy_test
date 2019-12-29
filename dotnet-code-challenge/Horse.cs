namespace dotnet_code_challenge
{
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
