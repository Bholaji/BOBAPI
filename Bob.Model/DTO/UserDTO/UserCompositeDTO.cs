namespace Bob.Model.DTO.UserDTO
{
    public class UserCompositeDTO
    {

        public UserRequestDTO User { get; set; }
        public UserContactDTO UserContact { get; set; }
        public UserAddressDTO UserAddress { get; set; }
        public UserSocialDTO UserSocial { get; set; }
        public UserFinancialDTO UserFinancial { get; set; }
        public UserPayrollDTO UserPayroll { get; set; }
        public UserEmploymentInformationDTO UserEmploymentInformation { get; set; }
    }
}
