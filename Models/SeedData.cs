using System;
using System.Linq;
using LibApp.Data;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Identity;

namespace LibApp.Models
{
    public static class SeedData
    {
        public static async void Initialize(IServiceProvider serviceProvider)
        {
            await using var context = serviceProvider.GetRequiredService<ApplicationDbContext>();
            using var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            if (!roleManager.Roles.Any())
            {
                await roleManager.CreateAsync(new IdentityRole { Name = "Owner" });
                await roleManager.CreateAsync(new IdentityRole { Name = "StoreManager" });
                await roleManager.CreateAsync(new IdentityRole { Name = "User" });
            }

            if (context.MembershipTypes.Any())
            {
                Console.WriteLine("Database already seeded");
                return;
            }

            context.MembershipTypes.AddRange(
                new MembershipType
                {
                    Id = 1,
                    Name = "test",
                    SignUpFee = 0,
                    DurationInMonths = 0,
                    DiscountRate = 0
                },
                new MembershipType
                {
                    Id = 2,
                    Name = "test2",
                    SignUpFee = 30,
                    DurationInMonths = 1,
                    DiscountRate = 10
                },
                new MembershipType
                {
                    Id = 3,
                    Name = "test3",
                    SignUpFee = 90,
                    DurationInMonths = 3,
                    DiscountRate = 15
                },
                new MembershipType
                {
                    Id = 4,
                    Name = "test4",
                    SignUpFee = 300,
                    DurationInMonths = 12,
                    DiscountRate = 20
                });


            if (!context.Genre.Any())
            {
                context.Genre.AddRange(
                    new Genre
                    {
                        Id = 1,
                        Name = "Fiction"
                    },
                    new Genre
                    {
                        Id = 2,
                        Name = "Science fiction"
                    },
                    new Genre
                    {
                        Id = 3,
                        Name = "Novel"
                    }
                );
            }

            if (!context.Customers.Any())
            {
                context.Customers.AddRange(
                    new Customer
                    {
                        Name = "Tom",
                        HasNewsletterSubscribed = false,
                        MembershipTypeId = 2,
                        Birthdate = DateTime.Now.AddYears(-20)
                    },
                    new Customer
                    {
                        Name = "Mustafa",
                        HasNewsletterSubscribed = true,
                        MembershipTypeId = 1,
                        Birthdate = DateTime.Now.AddYears(-40)
                    },
                    new Customer
                    {
                        Name = "Jade",
                        HasNewsletterSubscribed = false,
                        MembershipTypeId = 2,
                        Birthdate = DateTime.Now.AddYears(-30)
                    }
                );
            }

            if (!context.Books.Any())
            {
                context.Books.AddRange(
                    new Book
                    {
                        Name = "It ends with us",
                        AuthorName = "J. K. Rowling",
                        GenreId = 1,
                        DateAdded = DateTime.Now.AddDays(-100),
                        ReleaseDate = DateTime.Now.AddDays(-200),
                        NumberInStock = 70
                    },
                    new Book
                    {
                        Name = "No Longer Human",
                        AuthorName = "Lucy Maud Montgomery",
                        GenreId = 2,
                        DateAdded = DateTime.Now.AddDays(-10),
                        ReleaseDate = DateTime.Now.AddDays(-150),
                        NumberInStock = 10
                    },
                    new Book
                    {
                        Name = "Seven Husbands of Evelyn Hugo",
                        AuthorName = "Scott Fitzgerald",
                        GenreId = 1,
                        DateAdded = DateTime.Now.AddDays(-50),
                        ReleaseDate = DateTime.Now.AddDays(-80),
                        NumberInStock = 500
                    }
                    );
            }

            await context.SaveChangesAsync();
        }
    }
}