using Imenik_JN.Server.Data.Interfaces;
using Imenik_JN.Server.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;

namespace Imenik_JN.Server.Data
{
    public class DB_Init
    {

        public static void InitializeDatabase(IServiceProvider serviceProvider, int amount)
        {
            Hrcloud_DB_Context DB_context = serviceProvider.GetRequiredService<Hrcloud_DB_Context>();

            DB_context.Database.Migrate();

            if (DB_context.User.Any() || DB_context.Tag.Any() || DB_context.Phone.Any() || DB_context.Email.Any())
                return;

            if (CreateTestData(serviceProvider, amount))
                DB_context.SaveChanges();

        }

        private static bool CreateTestData(IServiceProvider serviceProvider, int amount)
        {
            try
            {
                IUserRepository userRepository = serviceProvider.GetRequiredService<IUserRepository>();
                ITagRepository tagRepository = serviceProvider.GetRequiredService<ITagRepository>();
                IPhoneRepository phoneRepository = serviceProvider.GetRequiredService<IPhoneRepository>();
                IEmailRepository emailRepository = serviceProvider.GetRequiredService<IEmailRepository>();

                for (int i = 0; i < amount; i++)
                {
                    int randomI = new Random().Next(0, 99);

                    User user = new User()
                    {
                        FirstName = Format("firstName", "-", i, randomI),
                        LastName = Format("lastName", "-", i, randomI),
                        Address = Format("address", "-", i, randomI)
                    };
                    userRepository.Add(user);

                    Tag tag = new Tag()
                    {
                        TagName = Format("tag", "-", i, randomI),
                        UserId = user.Id
                    };
                    tagRepository.Add(tag);

                    Phone phone = new Phone()
                    {
                        PhoneNumber = Format("+3851111111", string.Empty, i, randomI),
                        UserId = user.Id
                    };
                    phoneRepository.Add(phone);

                    Email email = new Email()
                    {
                        EmailAddress = string.Format("tester{0}{1}@hrtest.com", i, randomI),
                        UserId = user.Id
                    };
                    emailRepository.Add(email);
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        private static Func<string, string, int, int, string> Format = (text, delimiter, index, randomInt) => $"{text}{delimiter}{index}{delimiter}{randomInt}";
    }
}