using MeuBlog.Api.Validations;

namespace MeLevaAi.Api.Contracts.Responses.DriversLicense
{
    public class DriversLicenseResponse : Notifiable
    {
        public DriversLicenseDto? DriversLicense { get; set; }
    }
}
