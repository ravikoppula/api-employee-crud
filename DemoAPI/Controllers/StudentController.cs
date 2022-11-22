using DemoAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace DemoAPI.Controllers
{
    [System.Web.Http.RoutePrefix("api/student")]
    public class StudentController : ApiController
    {
        // GET: Student
        DemoAPIEntities wmsEN = new DemoAPIEntities();

        /// <summary>
        /// StudentInsert
        /// </summary>
        /// <param name="objVM"></param>
        /// <returns></returns>
        [System.Web.Http.Route("StudentInsert")]
        [System.Web.Http.HttpPost]
        public object StudentInsert(StudentData objVM)
        {
            try
            {
                if (objVM.Id == 0)
                {
                    var objlm = new StudentData();
                    objlm.StudentName = objVM.StudentName;
                    objlm.FName = objVM.FName;
                    objlm.MName = objVM.MName;
                    objlm.ContactNo = objVM.ContactNo;
                    wmsEN.StudentDatas.Add(objlm);
                    wmsEN.SaveChanges();
                    return new ResultVM
                    { Status = "Success", Message = "SuccessFully Saved." };
                }
                else
                {
                    var objlm = wmsEN.StudentDatas.Where(s => s.Id == objVM.Id).ToList<StudentData>().FirstOrDefault();
                    if (objlm.Id > 0)
                    {
                        objlm.StudentName = objVM.StudentName;
                        objlm.FName = objVM.FName;
                        objlm.MName = objVM.MName;
                        objlm.ContactNo = objVM.ContactNo;
                        wmsEN.SaveChanges();
                        return new ResultVM
                        { Status = "Success", Message = "SuccessFully Update." };
                    }
                    return new ResultVM
                    { Status = "Error", Message = "Invalid." };
                }
            }
            catch (Exception ex)
            {
                return new ResultVM
                { Status = "Error", Message = ex.Message.ToString() };
            }
        }

        /// <summary>
        /// GetStudentData
        /// </summary>
        /// <returns></returns>
        [System.Web.Http.Route("GetStudentData")]
        [System.Web.Http.HttpGet]
        public object GetStudentData()
        {
            var obj = from u in wmsEN.StudentDatas
                      select u;
            return obj;
        }
        /// <summary>
        /// edit 
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [System.Web.Http.Route("GetStudentById")]
        [System.Web.Http.HttpGet]
        public object GetStudentById(int Id)
        {
            return wmsEN.StudentDatas.Where(s => s.Id == Id).ToList<StudentData>().FirstOrDefault();
        }

        /// <summary>
        /// delete
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [System.Web.Http.Route("DeleteStudent")]
        [System.Web.Http.HttpGet]
        public object DeleteStudent(int Id)
        {
            try
            {
                var objlm = wmsEN.StudentDatas.Where(s => s.Id == Id).ToList<StudentData>().FirstOrDefault();
                wmsEN.StudentDatas.Remove(objlm);
                wmsEN.SaveChanges();
                return new ResultVM
                { Status = "Success", Message = "SuccessFully Delete." };
            }
            catch (Exception ex)
            {
                return new ResultVM
                { Status = "Error", Message = ex.Message.ToString() };
            }
        }

    }
}