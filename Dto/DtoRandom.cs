using exe1.models;

namespace exe1.Dto
{
    public class DtoRandom
    {
    }
    public class DtoWinnersReport
    {
        public Prize prize { get; set; }

        public User user { get; set; }
    }
}
