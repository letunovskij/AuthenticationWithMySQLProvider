using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using AspNet.Identity.MySQL;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace AuthenticationWithMySQLProvider.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public virtual ApplicationUserInfo ApplicationUserInfo { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Обратите внимание, что authenticationType должен совпадать с типом, определенным в CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Здесь добавьте утверждения пользователя
            return userIdentity;
        }
    }

    [Table("user_info")]
    public class ApplicationUserInfo
    {
        [Key]
        [Display(Name = "ФИО")]
        [StringLength(200, ErrorMessage = "Значение не должно превышать 200 символов")]
        public string FullName { get; set; }
    }


    public class ApplicationDbContext : MySQLDatabase
    {
        public System.Data.Entity.DbSet<ApplicationUserInfo> ApplicationUserInfo { get; set; }

        public ApplicationDbContext(string connectionName)
            : base(connectionName)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext("DefaultConnection");
        }
    }
}