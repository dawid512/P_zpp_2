using System.ComponentModel.DataAnnotations;

namespace P_zpp_2.Data
{
    public class Company
    {
        [Key]
        public int Id { get; set; }
        public int BossId { get; set; }
        public string CompanyId { get; set; }
    }
}