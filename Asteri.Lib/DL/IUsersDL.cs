using Asteri.Lib.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Asteri.Lib.DL
{
    public interface IUsersDL
    {
        Response<List<DTO.UsersDTO>> GetAll();
        Response<List<DTO.UsersDTO>> GetBy(Expression<Func<DTO.UsersDTO, bool>> predicate);
        Response<DTO.UsersDTO> GetById(int id);
        Response<DTO.UsersDTO> Insert(DTO.UsersDTO data);
        Response<DTO.UsersDTO> Update(DTO.UsersDTO data);
    }
}
