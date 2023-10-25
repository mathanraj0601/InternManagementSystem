using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace LogAPI.Models
{
    public class Log
    {
        [Key]
        public int LogId { get; set; }
        [DefaultValue(0)]
        [Range(1,int.MaxValue,ErrorMessage ="Id Should be positive")]
        public int UserID { get; set; } 
        public DateTime LogInTime { get; set; }
        public DateTime LogOutTime { get; set; }
        public DateTime Date { get; set; }
    }
}
