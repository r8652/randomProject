namespace exe1.models
{
    public class Donors
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Last_name { get; set; }

        public string Phone { get; set; }

        public string Email { get; set; }

        public List <Prize> Prizes { get; set; }
    }
}
