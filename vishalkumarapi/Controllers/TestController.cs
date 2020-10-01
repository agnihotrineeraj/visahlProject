using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using vishalkumarapi.EDM;
using System.Data.Entity;
using System.Web;

namespace vishalkumarapi.Controllers
{
    public class TestController : ApiController
    {
        VishaldbEntities db = new VishaldbEntities();

        [HttpGet]
        public List<tbladmin> getalldata()
        {
            var a = 10;
            return db.tbladmins.ToList();

        }
        [HttpGet]
        public tbladmin getdatabyid([FromUri]int id)
        {
            return db.tbladmins.Find(id);
        }
        [HttpPost]
        public HttpResponseMessage addadmin([FromBody]tbladmin obj)
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();

            try
            {
                db.tbladmins.Add(obj);
                db.SaveChanges();
                dic.Add("status", "success");
                return Request.CreateResponse(HttpStatusCode.OK, dic);

            }
            catch (Exception ex)
            {
                dic.Add("status", ex.Message);
                return Request.CreateResponse(HttpStatusCode.OK, dic);

            }

        }
        [HttpPut]
        public HttpResponseMessage updateadmin([FromBody] tbladmin obj)
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();

            try
            {
                db.Entry(obj).State = EntityState.Modified;
                db.SaveChanges();
                dic.Add("status", "success");
                return Request.CreateResponse(HttpStatusCode.OK, dic);

            }
            catch (Exception ex)
            {
                dic.Add("status", ex.Message);
                return Request.CreateResponse(HttpStatusCode.OK, dic);

            }




        }

        public void deletedata([FromUri] int id)
        {
            db.tbladmins.Remove(db.tbladmins.Find(id));
            db.SaveChanges();

        }

        [HttpPost]
        public HttpResponseMessage Logindata()
        {

            Dictionary<string, string> dic = new Dictionary<string, string>();

            try
            {
                var token = Request.Headers.GetValues("key").First();
                if (token == "123")
                {
                    // Dictionary<string, string> dic = new Dictionary<string, string>();

                    var email = HttpContext.Current.Request.Params["email"];
                    var password = HttpContext.Current.Request.Params["password"];

                    var p = db.tbladmins.Where(c => c.admin_email_id == email && c.admin_password == password).ToList();

                    if (p.Count == 1)
                    {

                        return Request.CreateResponse(HttpStatusCode.OK, p);


                    }
                    else
                    {
                        dic.Add("status", "invalid user name and password");
                        return Request.CreateResponse(HttpStatusCode.OK, dic);

                    }


                }
                else
                {
                    dic.Add("status", "invalid token");
                    return Request.CreateResponse(HttpStatusCode.OK, dic);


                }


            }
            catch (Exception ex)
            {
                dic.Add("status", ex.Message);
                return Request.CreateResponse(HttpStatusCode.OK, dic);


            }




        }
    }
}
