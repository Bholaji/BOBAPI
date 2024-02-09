using Bob.Model.DTO.UserDTO;

namespace Bob.Model.DTO
{
    public class UserCompositeDTO
    {
        public UserResponseDTO User { get; set; }
        public UserContactDTO UserContact { get; set; }
        public UserAddressDTO UserAddress { get; set; }
        public UserSocialDTO UserSocial { get; set; }
        public UserFinancialDTO UserFinancial { get; set; }
        public UserPayrollDTO UserPayroll { get; set; }
        public UserEmploymentInformationDTO UserEmploymentInformation { get; set; }
    }
}
