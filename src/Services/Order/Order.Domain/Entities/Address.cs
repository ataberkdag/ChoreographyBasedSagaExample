namespace Order.Domain.Entities
{
    public class Address
    {
        public string Line { get; set; }
        public string Province { get; set; }
        public string District { get; set; }

        private Address()
        {

        }

        private Address(string line, string province, string district)
        {
            this.Line = line;
            this.Province = province;
            this.District = district;
        }

        public static Address CreateAddress(string line, string province, string district)
        {
            return new Address(line, province, district);
        }
    }
}
