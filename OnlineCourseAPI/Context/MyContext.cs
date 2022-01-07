
using Microsoft.EntityFrameworkCore;
using OnlineCourseAPI.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineCourseAPI.Context
{
    public class MyContext : DbContext
    {
        public MyContext(DbContextOptions<MyContext> options) : base(options)
        {

        }
        public DbSet<User> Users { get; set; }
        public DbSet<UserNumber> UserNumbers { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<BankAccount> BankAccounts { get; set; }

        public DbSet<Course> Courses { get; set; }
        public DbSet<MyCourse> MyCourses { get; set; }
        public DbSet<ReviewCourse> ReviewCourses { get; set; }
        public DbSet<SubCourse> SubCourses { get; set; }
        public DbSet<SubCourseLog> SubCourseLogs { get; set; }
        public DbSet<Payment> Payments { get; set; }

        public DbSet<PaymentMidtrans> PaymentMidtranses { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<FAQ> FAQs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
       .HasOne(a => a.Account)
       .WithOne(b => b.User)
       .HasForeignKey<Account>(b => b.id);

            modelBuilder.Entity<User>()
      .HasOne(a => a.BankAccount)
      .WithOne(b => b.User)
      .HasForeignKey<BankAccount>(b => b.Userid);

            modelBuilder.Entity<ReviewCourse>()
       .HasOne(a => a.User)
       .WithMany(b => b.ReviewCourses);

            modelBuilder.Entity<ReviewCourse>()
     .HasOne(a => a.Course)
     .WithMany(b => b.ReviewCourses);

            modelBuilder.Entity<Course>()
      .HasOne(a => a.User)
      .WithMany(b => b.Courses);

            modelBuilder.Entity<Course>()
   .HasOne(a => a.Category)
   .WithMany(b => b.Courses);

            modelBuilder.Entity<Account>()
       .HasOne(a => a.Role)
       .WithMany(b => b.Accounts);

            modelBuilder.Entity<MyCourse>()
 .HasOne(a => a.User)
 .WithMany(b => b.MyCourses);

            modelBuilder.Entity<MyCourse>()
  .HasOne(a => a.Course)
  .WithMany(b => b.MyCourses);

 //           modelBuilder.Entity<MyCourse>()
 //.HasOne(a => a.Payment)
 //.WithMany(b => b.MyCourses);

            modelBuilder.Entity<Payment>()
     .HasOne(a => a.User)
     .WithMany(b => b.Payments);

            modelBuilder.Entity<Payment>()
     .HasOne(a => a.Course)
     .WithMany(b => b.Payments);

            modelBuilder.Entity<PaymentMidtrans>()
    .HasOne(a => a.User)
    .WithMany(b => b.PaymentMidtranses);

            modelBuilder.Entity<PaymentMidtrans>()
     .HasOne(a => a.Course)
     .WithMany(b => b.PaymentMidtranses);

            modelBuilder.Entity<SubCourseLog>()
   .HasOne(a => a.MyCourse)
   .WithMany(b => b.SubCourseLogs);

  //          modelBuilder.Entity<SubCourseLog>()
  //.HasOne(a => a.SubCourse)
  //.WithMany(b => b.SubCourseLogs);

            modelBuilder.Entity<SubCourse>()
  .HasOne(a => a.Course)
  .WithMany(b => b.SubCourses);



        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies();
        }
    }
}
