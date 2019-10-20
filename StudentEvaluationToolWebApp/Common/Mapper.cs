namespace StudentEvaluationToolWebApp.Common
{

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using StudentEvaluationToolCommon;
    using StudentEvaluationToolWebApp.Models;

    public class Mapper
    {
        internal User UserModelToCommonUser(UserModel userModel)
        {
            //// empty constructor
            //User user = new User();
            //user.FirstName = userModel.FirstName;
            //user.LastName = userModel.LastName;
            //user.Email = userModel.Email;
            //user.Password = userModel.Password;
            //user.Salt = userModel.Salt;
            //user.Username = userModel.Username;

            // using constructor with parameters
            User user = new User(userModel.FirstName, userModel.LastName, userModel.Email, userModel.Username,
                userModel.Password, userModel.Salt, userModel.RoleName, userModel.RoleID, userModel.UserID);

            return user;

        }

        internal List<UserModel> UserListToUserModelList(List<User> listOfUsers)
        {
            List<UserModel> userModelsList = new List<UserModel>();


            foreach (var item in listOfUsers)
            {
                UserModel userModel = new UserModel();
                userModel = this.CommonUserToUserModel(item);
                userModelsList.Add(userModel);
            }
            return userModelsList;
        }

        private UserModel CommonUserToUserModel(User item)
        {
            // empty constructor
            UserModel userModel = new UserModel();
            userModel.FirstName = item.FirstName;
            userModel.LastName = item.LastName;
            userModel.Email = item.Email;
            userModel.Password = item.Password;
            userModel.Salt = item.Salt;
            userModel.Username = item.Username;
            userModel.RoleID = item.RoleID;
            userModel.RoleName = item.RoleName;
            userModel.UserID = item.UserID;
            return userModel;
        }

        internal IList<CandidateModel> CandidateListToCandidateModelList(IList<Candidate> listOfCandidates)
        {
            List<CandidateModel> candidateModelList = new List<CandidateModel>();


            foreach (var item in listOfCandidates)
            {
                CandidateModel candidateModel = new CandidateModel();
                candidateModel.UserId = item.UserId;
                candidateModel.CandidateId = item.CapstoneCandidateId;

                candidateModel.CandidateFirstName = item.CandidateFirstName;
                candidateModel.CandidateLastName = item.CandidateLastName;
                candidateModel.LMSUserId = item.CandidateLMSUserId;
                candidateModel.LMSGroupId = item.CandidateLMSGroupId;
                candidateModel.LMSGroupName = item.CandidateLMSGroupName;
                candidateModel.LMSCourseId = item.CandidateLMSCourseId;
                candidateModel.CandidateActive = item.CandidateActiveFlag;
                candidateModel.EvaluatorId = item.CapstoneEvaluatorId;
                candidateModel.EvaluatorFirstName = item.EvaluatorFirstName;
                candidateModel.EvaluatorFLastName = item.EvaluatorLastName;
                candidateModel.EvaluatorActive = item.EvaluatorActiveFlag;
                candidateModelList.Add(candidateModel);
            }

            return candidateModelList;
        }

        internal ChangeRoleModel UserToChangeModel(User user)
        {
            ChangeRoleModel changeRoleModel = new ChangeRoleModel();
            changeRoleModel.Name = user.FirstName + " " + user.LastName;
            changeRoleModel.Username = user.Username;
            changeRoleModel.Password = user.Password;
            changeRoleModel.SelectedRoleId = user.RoleID;
            changeRoleModel.UserRoles = Utility.GetRoles();
            changeRoleModel.UserId = user.UserID;
            return changeRoleModel;
        }

        internal User ChangeModelToCommonUser(ChangeRoleModel iChangeRoleModel)
        {
            User user = new User();
            user.UserID = iChangeRoleModel.UserId;
            user.RoleID = iChangeRoleModel.SelectedRoleId;
            return user;
        }
    }
}