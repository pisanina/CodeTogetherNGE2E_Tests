﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace CodeTogetherNGE2E_Tests
{
    internal class Repository: IRepository
    {
        public void PrepareDbBeforeTest()
        {
            PurgeDB();


            var testUserId= AddUser("TestUser@a.com", "AQAAAAEAACcQAAAAEMjfV0AsErgwEJqu3B/qoRZupd0gee/MR+kTVqHSiw+hyQRqVkwqckRqnX8KWszcAQ=="
                , "22LCTUSUQEQ3TRILLNAI4UYKWW35DMU4", "9daa59f7-3e7d-43fa-a162-0b10d51fac57");
            var coderId= AddUser("coder@a.com", "AQAAAAEAACcQAAAAEJlMmu9bbvEoXU0HgptFcer92br0MuNyDOrgEAU2Ida224ErGE80+/FE67lavr43Ng=="
                ,"2LY6TUSKFRDEW7TUDNQBWUZEGGKHMBNV", "162697f6-0ce8-494c-8025-bccb535120bd");

            var newcoderId= AddUser("newcoder@a.com", "AQAAAAEAACcQAAAAELkajO8zHGpVNNUgPSj4GEjKOSVeTqluY8z0CDkjGaULwwFYAanloPiJeQFBfFee/A=="
                ,"TYTVUUWKI7JKKIENUVRDXKNS47ZIBZJE", "50ea8dd5-cd61-4785-a2d1-a0d5f72245c8");

            var firstProjectId= AddProject("FirstProject", "Our first project will be SUPRISE Hello World"
                ,testUserId, false, ProjectState.InProgress
                ,new DateTime(2019,2,28));

            var secondProjectId= AddProject("SecondProject"
                , "Yes we will print Hello Kity",testUserId, false
                , ProjectState.Preparing,new DateTime(2019,2,27));

            var funnyBunnyProjectId= AddProject("Funny bunny"
                , "We want to create web aplication with many funny bunes"
                ,testUserId, true, ProjectState.Preparing
                ,new DateTime(2019,2,26));

            var polishCharactersProjectId= AddProject("Polish title like pamięć"
                , "Test for polish worlds like pamięć"
                ,testUserId, false, ProjectState.Preparing
                ,new DateTime(2019,2,25));

            var fourTechsProjectId= AddProject("Test for adding Project with four Tech"
                , "Test for adding Project with four Technologies"
                ,testUserId, false, ProjectState.Preparing
                ,new DateTime(2019,2,24));

            var twoTechsProjectId= AddProject("Project with Two Tech"
                , "Project with Two Tech (Test)"
                ,newcoderId, false, ProjectState.Preparing
                ,new DateTime(2019,2,23));




            AddTechnlogyToProject(fourTechsProjectId, Technology.Assembly);
            AddTechnlogyToProject(fourTechsProjectId, Technology.Cpp);
            AddTechnlogyToProject(fourTechsProjectId, Technology.Java);
            AddTechnlogyToProject(fourTechsProjectId, Technology.JavaScript);

            AddTechnlogyToProject(twoTechsProjectId, Technology.Java);
            AddTechnlogyToProject(twoTechsProjectId, Technology.JavaScript);



            AddMemberToProject(firstProjectId, coderId
            , new DateTime(2019, 2, 2), "Message", false);

            AddMemberToProject(secondProjectId, newcoderId
            , new DateTime(2019, 2, 2), "Message", false);

            AddMemberToProject(secondProjectId, newcoderId
           , new DateTime(2019, 3, 3), "Message", true);

            AddMemberToProject(polishCharactersProjectId, newcoderId
            , new DateTime(2019, 2, 3), "Message", true);

            AddMemberToProject(fourTechsProjectId, newcoderId
           , new DateTime(2019, 2, 3), "Message", false);

            AddMemberToProject(funnyBunnyProjectId, newcoderId
            , new DateTime(2019, 2, 3), "Message", null);

        }

        private void PurgeDB()
        {
            using (SqlConnection SQLConnect =
                new SqlConnection(Configuration.ConnectionString))
            {
                SQLConnect.Open();

                SqlCommand ClearDB = new SqlCommand(
                    "Delete from ProjectMember " +
                    "Delete from UserTechnologyLevel " +
                    "Delete from UserITRole " +
                    "Delete From ProjectTechnology " +
                    "Delete From Project " +
                    "DBCC CHECKIDENT('Project', RESEED, 0) "+
                    "Delete From AspNetUsers;", SQLConnect);

                ClearDB.ExecuteNonQuery();
            }
        }

        private String AddUser(string userName, string passwordHash
            , string SecurityStamp, string concurrencyStamp)
        {
            using (SqlConnection SQLConnect =
                new SqlConnection(Configuration.ConnectionString))
            {
                SQLConnect.Open();

                Guid userId=Guid.NewGuid();

                SqlCommand addUserCommand = new SqlCommand(
                    "Insert Into AspNetUsers (Id, UserName, NormalizedUserName, " +
                    "Email, NormalizedEmail, PasswordHash, SecurityStamp, ConcurrencyStamp, EmailConfirmed,  " +
                    "PhoneNumberConfirmed, TwoFactorEnabled, LockoutEnabled, AccessFailedCount) Values " +
                    "(@UId, @UserName,@NormalizedUserName, @Email, @NormalizedEmail, " +
                    "@PasswordHash, @SecurityStamp, @ConcurrencyStamp, 0, 0, 0, 1, 0)", SQLConnect);

                addUserCommand.Parameters.AddWithValue("@UId", userId);
                addUserCommand.Parameters.AddWithValue("@UserName", userName);
                addUserCommand.Parameters.AddWithValue("@NormalizedUserName", userName.ToUpper());
                addUserCommand.Parameters.AddWithValue("@Email", userName);
                addUserCommand.Parameters.AddWithValue("@NormalizedEmail", userName.ToUpper());
                addUserCommand.Parameters.AddWithValue("@PasswordHash", passwordHash);
                addUserCommand.Parameters.AddWithValue("@SecurityStamp", SecurityStamp);
                addUserCommand.Parameters.AddWithValue("@ConcurrencyStamp", concurrencyStamp);

                addUserCommand.ExecuteNonQuery();

                return userId.ToString();
            }
        }

        private int AddProject(string title, string description
            , string ownerId, bool isLookingForNewMembers
            , ProjectState stateid, DateTime creationDate)
        {
            using (SqlConnection SQLConnect =
                new SqlConnection(Configuration.ConnectionString))
            {
                SQLConnect.Open();

                SqlCommand addProjectCommand = new SqlCommand(
                    "Insert Into Project(Title, Description, OwnerId" +
                    ", NewMembers, StateId, CreationDate)" +
                    "Values (@title, @description, @ownerid, @isLookingForNewMembers" +
                    ", @stateid, @creationDate);" +
                    "SELECT SCOPE_IDENTITY()", SQLConnect);

                addProjectCommand.Parameters.AddWithValue("@title", title);
                addProjectCommand.Parameters.AddWithValue("@description", description);
                addProjectCommand.Parameters.AddWithValue("@ownerid", ownerId);
                addProjectCommand.Parameters.AddWithValue("@isLookingForNewMembers", isLookingForNewMembers);
                addProjectCommand.Parameters.AddWithValue("@stateid", stateid);
                addProjectCommand.Parameters.AddWithValue("@creationDate", creationDate);

                return Convert.ToInt32(addProjectCommand.ExecuteScalar());
            }
        }

        private enum ProjectState
        {
            Preparing =1,
            InProgress=2,
            Closed=3
        }

        private void AddTechnlogyToProject(int projectId, Technology technologyId)
        {
            using (SqlConnection SQLConnect =
                new SqlConnection(Configuration.ConnectionString))
            {
                SQLConnect.Open();

                SqlCommand addtechToProjectCommand = new SqlCommand(
                    "Insert Into ProjectTechnology Values(@projectId,@techId)", SQLConnect);

                addtechToProjectCommand.Parameters.AddWithValue("@projectId", projectId);
                addtechToProjectCommand.Parameters.AddWithValue("@techId", (int)technologyId);

                addtechToProjectCommand.ExecuteNonQuery();
            }
        }

        private enum Technology
        {
            Assembly =2,
            Cpp=4,
            Java=6,
            JavaScript = 7
        }

        public void AddMemberToProject(int projectId, string userId
            , DateTime messageDate, string message, bool? isAccepted)
        {
            using (SqlConnection SQLConnect =
                new SqlConnection(Configuration.ConnectionString))
            {
                SQLConnect.Open();

                SqlCommand addtechToProjectCommand = new SqlCommand(
                    "INSERT INTO ProjectMember (ProjectId,MemberId,MessageDate" +
                    ",Message,AddMember) " +
                    "VALUES(@projectId, @userId, @messageDate, @message, @isAccepted)", SQLConnect);

                addtechToProjectCommand.Parameters.AddWithValue("@projectId", projectId);
                addtechToProjectCommand.Parameters.AddWithValue("@userId", userId);
                addtechToProjectCommand.Parameters.AddWithValue("@messageDate", messageDate);
                addtechToProjectCommand.Parameters.AddWithValue("@message", message);
                SqlParameter isAcceptedParam = new SqlParameter("@isAccepted",System.Data.SqlDbType.Bit);
                if(isAccepted.HasValue)
                    isAcceptedParam.Value=  isAccepted.Value;
                else
                    isAcceptedParam.Value = DBNull.Value;

                addtechToProjectCommand.Parameters.Add(isAcceptedParam);

                addtechToProjectCommand.ExecuteNonQuery();
            }
        }
    }
}
