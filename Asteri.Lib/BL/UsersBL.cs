using Asteri.Lib.DL;
using Asteri.Lib.DTO;
using Asteri.Lib.HL;
using LiteDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Asteri.Lib.BL
{
    public class UsersBL
    {

        public readonly IUsersDL
        usersDL;

        public
        UsersBL()
        {
            usersDL = new UsersDL();
        }

        public Response
        CreateAdmin()
        {
            try
            {
                var adminUserExist = GetByUser("Admin");
                if (adminUserExist.Result == Response.Results.notSuccess)
                {
                    var userAdmin = new DTO.UsersDTO()
                    {
                        User = "Admin",
                        Password = "Admin",
                        UserNames = "Administrator",
                        UsersLastNames = "",
                        Email = "",
                        IsActive = true
                    };

                    Create(userAdmin);
                    return new Response() { Result = Response.Results.success, Message = "Admin user is been created." };
                }
                else
                {
                    return new Response() { Result = Response.Results.notSuccess, Message = "Admin user already exist." };
                }
            }
            catch (Exception ex)
            {
                return new Response() { Result = Response.Results.error, Message = ex.Message };
            }
        }

        public Response<List<DTO.UsersDTO>>
        GetAll()
        {
            try
            {
                var users = usersDL.GetAll();
                return new Response<List<DTO.UsersDTO>>() { Result = Response.Results.success, Data = (List<DTO.UsersDTO>) users.Data };
            }
            catch (Exception ex)
            {
                return new Response<List<DTO.UsersDTO>>() { Result = Response.Results.error, Message = ex.Message };
            }
        }

        public Response<DTO.UsersDTO>
        GetByUser(string user)
        {
            try
            {
                Response<List<DTO.UsersDTO>> dataSet = (Response<List<DTO.UsersDTO>>)usersDL.GetBy(u => u.User == user);
                Response<DTO.UsersDTO> response = null;
                if (dataSet.Result == Response.Results.success)
                {
                    response = new Response<DTO.UsersDTO>() { Result = Response.Results.success, Data = dataSet.Data.FirstOrDefault() };
                }
                else
                {
                    response = new Response<DTO.UsersDTO>() { Result = Response.Results.notSuccess, Message = "Not Found" };
                }

                return response;
            }
            catch (Exception ex)
            {
                return new Response<DTO.UsersDTO> { Result = Response.Results.error, Message = ex.Message };
            }
        }

        public Response<DTO.UsersDTO>
        GetById(int id)
        {
            try
            {
                Response<DTO.UsersDTO> response = (Response<DTO.UsersDTO>)usersDL.GetById(id);

                return response;
            }
            catch (Exception ex)
            {
                return new Response<DTO.UsersDTO> { Result = Response.Results.error, Message = ex.Message };
            }
        }

        public Response<DTO.UsersDTO>
        Create(DTO.UsersDTO user)
        {
            try
            {
                var result = GetByUser(user.User);
                if (result.Result == Asteri.Lib.DTO.Response.Results.success)
                {
                    return new Response<DTO.UsersDTO> { Result = Asteri.Lib.DTO.Response.Results.notSuccess, Message = "User already Exists" };
                }
                else
                {
                    user.PasswordSalt = Guid.NewGuid().ToString();
                    user.Password = HashHL.SHA256Of(user.Password + user.PasswordSalt);
                    usersDL.Insert(user);

                    return new Response<DTO.UsersDTO>() { Result = Response.Results.success, Message = "User Created", Data = user };
                }
            }
            catch (Exception ex)
            {
                return new Response<DTO.UsersDTO>() { Result = Response.Results.error, Message = ex.Message };
            }
        }

        public Response<DTO.UsersDTO>
        Update(DTO.UsersDTO user)
        {
            try
            {
                var requestUser = GetById(user.Id);
                if (requestUser.Result == Response.Results.success)
                {
                    var userOriginal = requestUser.Data;
                    userOriginal.UserNames = user.UserNames;
                    userOriginal.UsersLastNames = user.UsersLastNames;
                    userOriginal.Email = user.Email;
                    userOriginal.IsActive = user.IsActive;

                    usersDL.Update(userOriginal);
                }
                else
                {
                    return requestUser;
                }

                return new Response<DTO.UsersDTO>() { Result = Response.Results.success, Data = user };
            }
            catch (Exception ex)
            {
                return new Response<DTO.UsersDTO>() { Result = Response.Results.error, Message = ex.Message };
            }
        }

        public Response
        Validate(string user, string password)
        {
            try
            {
                var requestUser = GetByUser(user);
                if (requestUser.Result.Equals(Response.Results.notSuccess))
                {
                    return new Response<DTO.UsersDTO>() { Result = Response.Results.notSuccess, Message = "Invalid User or Password." };
                }
                else if (!requestUser.Data.Password.Equals(HashHL.SHA256Of(password + requestUser.Data.PasswordSalt)))
                {
                    return new Response<DTO.UsersDTO>() { Result = Response.Results.notSuccess, Message = "Invalid User or Password." };
                }
                else
                {
                    return new Response<DTO.UsersDTO>() { Result = Response.Results.success, Message = "The User is valid." };
                }
            }
            catch (Exception ex)
            {
                return new Response<DTO.UsersDTO>() { Result = Response.Results.error, Message = ex.Message };
            }
            
        }

        public Response
        ChangePassword(string user, string newPassword)
        {
            //try
            //{
            //    var requestUser = GetByUser(user);

            //    requestUser.Data.PasswordSalt = Guid.NewGuid().ToString();
            //    requestUser.Data.Password = HashHL.SHA256Of(newPassword + requestUser.Data.PasswordSalt);

            //    usersColl.Update(requestUser.Data);
            //    return new Response<DTO.UsersDTO>() { Result = Response.Results.success, Message = "Password has been updated" };
            //}
            //catch (Exception ex)
            //{
            //    return new Response<DTO.UsersDTO>() { Result = Response.Results.error, Message = ex.Message };
            //}
            throw new NotImplementedException();
        }
    }
}
