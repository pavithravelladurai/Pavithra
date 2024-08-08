using System.ComponentModel.DataAnnotations;

namespace Book.Models
{
    public class Bookstore
    {
        public string Publisher { get; set; }
        public string Title { get; set; }
        public string AuthorLastName { get; set; }
        public string AuthorFirstName { get; set; }
        public decimal Price { get; set; }
        public string MLACitation
        {
            get
            {
                return $"{AuthorLastName}, {AuthorFirstName}. *{Title}*. {Publisher}.";
            }
        }

        public string ChicagoCitation
        {
            get
            {
                return $"{AuthorLastName}, {AuthorFirstName}. {Title}. {Publisher}.";
            }
        }
    }
}
