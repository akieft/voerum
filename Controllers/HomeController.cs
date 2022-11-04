using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using Voerum.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;

namespace Voerum.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            GetTop3RatedRecepices();
            return View();
        }

        public void GetTop3RatedRecepices()
        {
            if (User.Identity.IsAuthenticated)
            {
                String userId = User.Identity.GetUserId();
                ViewBag.UserId = userId;
            }

            List<Recipe> Top3;
            int count;

            using (VoerumDbContext db = new VoerumDbContext())
            {
                Top3 = db.Recipes.OrderByDescending(r => r.AverageRating).Take(3).ToList();
            }

            if (Top3.Count == 3)
            {
                ViewBag.Activation = true;
                ViewBag.Name1 = Top3[0].Name;
                ViewBag.Name2 = Top3[1].Name;
                ViewBag.Name3 = Top3[2].Name;

                ViewBag.Id1 = Top3[0].Id;
                ViewBag.Id2 = Top3[1].Id;
                ViewBag.Id3 = Top3[2].Id;

                foreach (var check in Top3)
                {
                    count = 0;
                    while (check.AverageRating > 50)
                    {
                        check.AverageRating -= 100;
                        count += 1;
                    }

                    check.AverageRating = count;
                    count = 0;
                }

                ViewBag.Rating1 = Top3[0].AverageRating;
                ViewBag.Rating2 = Top3[1].AverageRating;
                ViewBag.Rating3 = Top3[2].AverageRating;
            }
        }

        public FileContentResult UserPhotos()
        {
            if (User.Identity.IsAuthenticated)
            {
                String userId = User.Identity.GetUserId();
                ViewBag.UserId = userId;

                if (userId == null)
                {
                    string fileName = HttpContext.Server.MapPath(@"~/Images/noImg.png");

                    byte[] imageData = null;
                    FileInfo fileInfo = new FileInfo(fileName);
                    long imageFileLength = fileInfo.Length;
                    FileStream fs = new FileStream(fileName, FileMode.Open, FileAccess.Read);
                    BinaryReader br = new BinaryReader(fs);
                    imageData = br.ReadBytes((int)imageFileLength);

                    return File(imageData, "image/png");
                }

                // to get the user details to load user Image    
                var bdUsers = HttpContext.GetOwinContext().Get<VoerumDbContext>();
                var userImage = bdUsers.Users.Where(x => x.Id == userId).FirstOrDefault();
                return new FileContentResult(userImage.UserPhoto, "image/jpeg");
            }
            else
            {
                string fileName = HttpContext.Server.MapPath(@"~/Images/noImg.png");
                byte[] imageData = null;
                FileInfo fileInfo = new FileInfo(fileName);
                long imageFileLength = fileInfo.Length;
                FileStream fs = new FileStream(fileName, FileMode.Open, FileAccess.Read);
                BinaryReader br = new BinaryReader(fs);
                imageData = br.ReadBytes((int)imageFileLength);
                return File(imageData, "image/png");
            }
        }
    }
}
