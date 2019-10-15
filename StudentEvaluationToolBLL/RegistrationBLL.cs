namespace StudentEvaluationToolBLL
{
    using StudentEvaluationToolCommon;
    using StudentEvaluationToolDAL;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class RegistrationBLL
    {

        public Result Register(User user)
        {

            // pass thru code
            Result result = new Result();
            RegistrationDAL registrationDAL = new RegistrationDAL();

            // hash and salt the password
            Guid guid = Guid.NewGuid();
            Hash hash = new Hash();
            string hashedPassword = hash.SHA256HasingWithSalt(user.Password, guid.ToString(), false);
            user.Password = hashedPassword;
            user.Salt = guid.ToString();

            result = registrationDAL.CreateNewUser(user);
            return result;
        }

        public Result Login(User user)
        {

            // pass thru code
            Result result = new Result();
            RegistrationDAL registrationDAL = new RegistrationDAL();


            // this is all the users            
            result = registrationDAL.LoginAttempt(user);

           
            // fitler by username and get the salt back
            //TODO: what to do if list is emtpy. Will throw exception on the indexer
            List<User> filteredList = result.ListOfUsers.Where(u => u.Username == user.Username).ToList();


            if (filteredList != null && filteredList.Count == 1)
            {
                User filteredUserTemp = filteredList[0];
                Hash hash = new Hash();
                // check password now against the filtered User
                // make sure you hash the password first
                string hashedPasswordWithSalt = hash.SHA256HasingWithSalt(user.Password, filteredUserTemp.Salt, false);

                if (hashedPasswordWithSalt == filteredUserTemp.Password)
                {
                    // username and password is verified
                    var tempList = result.ListOfUsers.Where(u => u.Username == user.Username && u.Password == hashedPasswordWithSalt).ToList();
                    result.ListOfUsers = tempList;
                    result.ResultType = ResultType.Success;
                }
                else // did not pass the hashing password check
                {
                    result.ResultType = ResultType.Failure;
                }
            }
            else // did not find a user with username
            {
                result.ResultType = ResultType.Failure;
            }
         
            return result;
        }


    }
}
