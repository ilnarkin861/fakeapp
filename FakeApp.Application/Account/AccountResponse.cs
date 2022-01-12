namespace FakeApp.Application.Account
{
    /// <summary>
    /// Класс для маппинга информации о юзере
    /// </summary>
    public class AccountResponse
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string WebSite { get; set; }
        public string Avatar { get; set; }
        public string AddressZipCode { get; set; }
        public string AddressCity { get; set; }
        public string AddressStreet { get; set; }
        public string AddressSuit { get; set; }
        public string CompanyName { get; set; }
        public string CompanyCatchPhrase { get; set; }
        public string CompanyBs { get; set; }
    }
}