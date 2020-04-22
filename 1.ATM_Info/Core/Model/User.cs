using System;
using System.Collections.Generic;
using System.Linq;

namespace _1.ATM_Info.Core.Model
{
    class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Fathername { get; set; }
        public List<Phone> PhoneList { get; set; }
        public List<IdentityCard> IdentityCardList { get; set; }
        public DateTime RegistrationDate { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string FullName =>
            string.Join(" ", Surname, Name, Fathername);
        public string PhoneListFormated =>
            string.Join(", ", PhoneList.Select(x => x.ToString()));
        public string IdentCardsListFormated =>
            string.Join(", ", IdentityCardList.Select(x => x.ToString()));

        public static List<User> GetDefaultList()
        {
            return new List<User>()
            {
                new User()
                {
                    Id = 1,
                    Name = "Ігор",
                    Surname = "Пасєка",
                    Fathername = "Ігорович",
                    PhoneList = new List<Phone>() { new Phone() { Text = "0956663839", Type = PhoneType.Mobile }, new Phone() { Text = "0448956968", Type = PhoneType.LandLine } },
                    IdentityCardList = new List<IdentityCard> { new IdentityCard() { Series = "AH", Number = "2365598", IssuedBy = "2366", IssuedDate = DateTime.Parse("08.08.2008"), IdentityCardType = IdentityCardType.Passport } },
                    RegistrationDate = DateTime.Parse("03.04.2020"),
                    Login = "ip",
                    Password = "SimplePass"
                },
                new User()
                {
                    Id = 2,
                    Name = "Олександр",
                    Surname = "Дашкевич",
                    Fathername = "Юрійович",
                    PhoneList = new List<Phone>() { new Phone() { Text = "0956663333", Type = PhoneType.Mobile } },
                    IdentityCardList = new List<IdentityCard> { new IdentityCard() { Series = "ВВ", Number = "2365598", IssuedBy = "2366", IssuedDate = DateTime.Parse("08.03.2008"), IdentityCardType = IdentityCardType.Passport } },
                    RegistrationDate = DateTime.Parse("21.04.2020"),
                    Login = "od",
                    Password = "qwer"
                },
                new User()
                {
                    Id = 3,
                    Name = "Веніамін",
                    Surname = "Величко",
                    Fathername = "Вольфович",
                    PhoneList = new List<Phone>() { new Phone() { Text = "0448956968", Type = PhoneType.LandLine } },
                    IdentityCardList = new List<IdentityCard> { new IdentityCard() { Series = "ФФ", Number = "2365598", IssuedBy = "2366", IssuedDate = DateTime.Parse("08.03.2008"), IdentityCardType = IdentityCardType.Passport } },
                    RegistrationDate = DateTime.Parse("04.12.2020"),
                    Login = "vv",
                    Password = "9999"
                },
                new User()
                {
                    Id = 4,
                    Name = "Дмитро",
                    Surname = "Матрос",
                    Fathername = "Віталійович",
                    PhoneList = new List<Phone>() { new Phone() { Text = "0955663839", Type = PhoneType.Mobile } },
                    IdentityCardList = new List<IdentityCard> { new IdentityCard() { Series = "AH", Number = "2365598", IssuedBy = "2366", IssuedDate = DateTime.Parse("08.12.2008"), IdentityCardType = IdentityCardType.Passport } },
                    RegistrationDate = DateTime.Parse("03.04.2020"),
                    Login = "dm",
                    Password = "1234"
                }
            };
        }
    }
}
