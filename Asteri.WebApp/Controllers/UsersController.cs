using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Asteri.Lib.BL;
using Asteri.Lib;
using System.Web.Security;
using Asteri.Lib.DTO;

namespace Asteri.WebApp.Controllers
{
    [Authorize]
    [OutputCacheAttribute(VaryByParam = "*", Duration = 0, NoStore = true)]
    public class
    UsersController : Controller
    {
        UsersBL
        userBL = new UsersBL();

        public ActionResult
        GetAll()
        {

            Response<dynamic> result2 = new Response<dynamic>();

            var result = userBL.GetAll();
            result2.Result = result.Result;
            result2.Message = result.Message;
            if (result.Result == Asteri.Lib.DTO.Response.Results.success)
            {
                result2.Data = result.Data.Select(x => new
                {
                    Id = x.Id.ToString(),
                    x.User,
                    x.UserNames,
                    x.UsersLastNames,
                    x.Email,
                    x.IsActive
                });
            }

            return Json(result2, JsonRequestBehavior.AllowGet);
        }

        public ActionResult
        GetByUser(string user)
        {
            Response<dynamic> result2 = new Response<dynamic>();

            var result = userBL.GetByUser(user);
            var x = result.Data;
            result2.Result = result.Result;
            result2.Message = result.Message;
            if (result.Result == Asteri.Lib.DTO.Response.Results.success)
            {
                result2.Data = new
                {
                    Id = x.Id.ToString(),
                    x.User,
                    x.UserNames,
                    x.UsersLastNames,
                    x.Email,
                    x.IsActive
                };
            }

            return Json(result2, JsonRequestBehavior.AllowGet);
        }

        public ActionResult
        GetById(int id)
        {
            Response<dynamic> result2 = new Response<dynamic>();

            var result = userBL.GetById(id);
            var x = result.Data;
            result2.Result = result.Result;
            result2.Message = result.Message;
            if (result.Result == Asteri.Lib.DTO.Response.Results.success)
            {
                result2.Data = new
                {
                    Id = x.Id.ToString(),
                    x.User,
                    x.UserNames,
                    x.UsersLastNames,
                    x.Email,
                    x.IsActive
                };
            }

            return Json(result2, JsonRequestBehavior.AllowGet);
        }

        [AllowAnonymous]
        public ActionResult
        CreateAdmin()
        {
            var result = userBL.CreateAdmin();

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult
        Create(string user, string names, string lastnames, string password, string email)
        {
            UsersDTO userDTO = new UsersDTO()
            {
                User = user,
                UserNames = names,
                UsersLastNames = lastnames,
                Email = email,
                Password = password,
                IsActive = true
            };
            var result = userBL.Create(userDTO);

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult
        Update(int id, string user, string names, string lastnames, string email, bool isActive)
        {
            UsersDTO userDTO = new UsersDTO()
            {
                Id = id,
                User = user,
                UserNames = names,
                UsersLastNames = lastnames,
                Email = email,
                IsActive = isActive
            };
            var result2 = userBL.Update(userDTO);

            return Json(new Asteri.Lib.DTO.Response() { Result = Asteri.Lib.DTO.Response.Results.success, Message = "User don't Exists" }, JsonRequestBehavior.AllowGet);
        }

        [AllowAnonymous]
        public ActionResult
        Login(string userName, string pass)
        {
            var result = userBL.Validate(userName, pass);

            if (result.Result.Equals(Asteri.Lib.DTO.Response.Results.success))
            {
                FormsAuthentication.SetAuthCookie(userName, true);
            }

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult
        Logout()
        {

            FormsAuthentication.SignOut();

            return Json(new Response() { Result = Asteri.Lib.DTO.Response.Results.success, Message = "User has been logout." }, JsonRequestBehavior.AllowGet);
        }

        [AllowAnonymous]
        public ActionResult
        IsLogin()
        {
            if (User.Identity.IsAuthenticated)
            {
                var result = userBL.GetByUser(User.Identity.Name);
                string name = string.Format("{0} {1}({2})", result.Data.UserNames, result.Data.UsersLastNames, result.Data.User);

                return Json(new Asteri.Lib.DTO.Response<string>() { Result = Asteri.Lib.DTO.Response.Results.success, Message = "User has login", Data = name }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new Asteri.Lib.DTO.Response() { Result = Asteri.Lib.DTO.Response.Results.notSuccess, Message = "User has not login" }, JsonRequestBehavior.AllowGet);
            }

        }

    }
}