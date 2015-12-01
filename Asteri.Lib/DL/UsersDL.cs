using Asteri.Lib.DTO;
using Asteri.Lib.HL;
using LiteDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Asteri.Lib.DL
{
    public class UsersDL : EnviromentDL, IUsersDL
    {
        private LiteCollection<DTO.UsersDTO>
        dataColl = null;

        public 
        UsersDL() 
        {
            if (dataColl == null)
            {
                dataColl = db.GetCollection<DTO.UsersDTO>("Users");
            }

            dataColl.EnsureIndex("User");
        }

        public Response<List<DTO.UsersDTO>>
        GetAll()
        {
            try
            {
                return new Response<List<DTO.UsersDTO>>() { Result = Response.Results.success, Data = dataColl.FindAll().ToList() };
            }
            catch (Exception ex)
            {
                return new Response<List<DTO.UsersDTO>>() { Result = Response.Results.error, Message = ex.Message };
            }
        }

        public Response<List<DTO.UsersDTO>>
        GetBy(Expression<Func<DTO.UsersDTO, bool>> predicate)
        {
            try
            {
                IEnumerable<DTO.UsersDTO> dataSet = dataColl.Find(predicate);
                Response<List<DTO.UsersDTO>> response = null;
                if (dataSet.Any())
                {
                    response = new Response<List<DTO.UsersDTO>>() { Result = Response.Results.success, Data = dataSet.ToList() };
                }
                else
                {
                    response = new Response<List<DTO.UsersDTO>>() { Result = Response.Results.notSuccess, Message = "Not Found" };
                }

                return response;
            }
            catch (Exception ex)
            {
                return new Response<List<DTO.UsersDTO>>() { Result = Response.Results.error, Message = ex.Message };
            }
        }

        public Response<DTO.UsersDTO>
        GetById(int id)
        {
            try
            {
                DTO.UsersDTO dataSet = dataColl.FindById(id);
                Response<DTO.UsersDTO> response = null;
                if (dataSet != null)
                {
                    response = new Response<DTO.UsersDTO>() { Result = Response.Results.success, Data = dataSet };
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
        Insert(DTO.UsersDTO data)
        {
            try
            {
                dataColl.Insert(data);

                return new Response<DTO.UsersDTO>() { Result = Response.Results.success, Data = data };
            }
            catch (Exception ex)
            {
                return new Response<DTO.UsersDTO>() { Result = Response.Results.error, Message = ex.Message };
            }
        }

        public Response<DTO.UsersDTO>
        Update(DTO.UsersDTO data)
        {
            try
            {
                var requestUser = GetById(data.Id);
                if (requestUser.Result == Response.Results.success)
                {
                    dataColl.Update(data);
                    return new Response<DTO.UsersDTO>() { Result = Response.Results.success, Data = data };
                }
                else
                {
                    return new Response<DTO.UsersDTO>() { Result = Response.Results.notSuccess, Message ="Not found" };
                }
            }
            catch (Exception ex)
            {
                return new Response<DTO.UsersDTO>() { Result = Response.Results.error, Message = ex.Message };
            }
        }
    }
}
