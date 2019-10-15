namespace StudentEvaluationToolCommon
{
 
    public class User
    {
      
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Salt { get; set; }
        public int RoleID { get; set; }
        public string Email { get; set; }
        public string RoleName { get; set; }
        public int UserID { get; set; }

        public User() { }

        public User(string iFirstName, string iLastName, string iEmail, string iUsername, string iPassword, string iSalt, string iRoleName, int iRoleId, int iUserId)
        {
            this.FirstName = iFirstName;
            this.LastName = iLastName;
            this.Email = iEmail;
            this.Username = iUsername;
            this.Password = iPassword;
            this.Salt = iSalt;
            this.RoleID = iRoleId;
            this.RoleName = iRoleName;
            this.UserID = iUserId;
        }


    }
}
