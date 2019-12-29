namespace dotnet_code_challenge
{
    partial class Program
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
}
