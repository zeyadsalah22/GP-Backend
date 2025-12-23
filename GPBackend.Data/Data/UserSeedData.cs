using GPBackend.Models;

namespace GPBackend.Data
{
    public class UserSeedData
    {
        public static User[] GetAdminUsers()
        {
            return
            [
                new User
                {
                    UserId = 2,
                    Fname = "Admin",
                    Lname = "Admin2",
                    Email = "admin2@gmail.com",
                    Password = "O04maomAXJ0CD5rKZjitY+hwH8jHXAyhlS0UBU0fEM8=",
                    Role = Models.Enums.UserRole.Admin,
                    CreatedAt = new DateTime(2025, 10, 14),
                    UpdatedAt = new DateTime(2025, 10, 14),
                    IsDeleted = false
                },
                new User
                {
                    UserId = 3,
                    Fname = "Admin",
                    Lname = "Admin3",
                    Email = "admin3@gmail.com",
                    Password = "O04maomAXJ0CD5rKZjitY+hwH8jHXAyhlS0UBU0fEM8=",
                    Role = Models.Enums.UserRole.Admin,
                    CreatedAt = new DateTime(2025, 10, 14),
                    UpdatedAt = new DateTime(2025, 10, 14),
                    IsDeleted = false
                },
                new User
                {
                    UserId = 4,
                    Fname = "Admin",
                    Lname = "Admin4",
                    Email = "admin4@gmail.com",
                    Password = "O04maomAXJ0CD5rKZjitY+hwH8jHXAyhlS0UBU0fEM8=",
                    Role = Models.Enums.UserRole.Admin,
                    CreatedAt = new DateTime(2025, 10, 14),
                    UpdatedAt = new DateTime(2025, 10, 14),
                    IsDeleted = false
                },
            ];
        }
    }
}
