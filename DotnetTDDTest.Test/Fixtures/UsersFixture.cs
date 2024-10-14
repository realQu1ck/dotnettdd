using DotnetTDDTest.API.Models;

namespace DotnetTDDTest.Test.Fixtures;

public static class UsersFixture
{
    public static List<User> GetTestUsers() =>
        new()
        {
            new User
            {
                Id = 1,
                Name = "Test",
                Email = "admin@test.com",
                Phone = "0911111111",
                Address = new Address()
                {
                    City = "Shiraz",
                    Street = "Yas",
                    ZipCode = "1234"
                },
            },
            new User
            {
                Id = 2,
                Name = "Test2",
                Email = "admin2@test.com",
                Phone = "0911111112",
                Address = new Address()
                {
                    City = "Shiraz2",
                    Street = "Yas2",
                    ZipCode = "12342"
                },
            },
        };
}
