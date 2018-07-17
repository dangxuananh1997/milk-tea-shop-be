using API.MilkteaAdmin.Models;
using Core.AppService.Business;
using Core.AppService.Pagination;
using Core.ObjectModel.ConstantManager;
using Core.ObjectModel.Entity;
using Core.ObjectModel.Pagination;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web.Http;

namespace API.MilkteaAdmin.Controllers
{
    public class CouponPackagesController : ApiController
    {
        private readonly ICouponPackageService _couponPackageService;
        private readonly IPagination _pagination;

        public CouponPackagesController(ICouponPackageService couponPackageService, IPagination pagination)
        {
            this._couponPackageService = couponPackageService;
            this._pagination = pagination;
        }

        [HttpGet]
        public IHttpActionResult Get(int pageIndex, string searchValue)
        {
            if (pageIndex <= 0)
            {
                return BadRequest(ErrorMessage.INVALID_PAGEINDEX);
            }

            try
            {
                List<CouponPackage> couponPackages;
                if (String.IsNullOrEmpty(searchValue))
                {
                    // GET ALL
                    couponPackages = _couponPackageService.GetAllCouponPackage().ToList();
                }
                else
                {
                    // GET SEARCH RESULT
                    couponPackages = _couponPackageService.GetAllCouponPackage().Where(p => p.Name.Contains(searchValue)).ToList();
                }

                List<CouponPackageVM> couponPackageVMs = AutoMapper.Mapper.Map<List<CouponPackage>, List<CouponPackageVM>>(couponPackages);
                Pager<CouponPackageVM> result = _pagination.ToPagedList<CouponPackageVM>(pageIndex, ConstantDataManager.PAGESIZE, couponPackageVMs);
                return Ok(result);
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }
        }

        [HttpGet]
        public IHttpActionResult Get(int id)
        {
            if (id <= 0)
            {
                return BadRequest(ErrorMessage.INVALID_ID);
            }

            try
            {
                CouponPackageVM result = AutoMapper.Mapper.Map<CouponPackage, CouponPackageVM>
                    (_couponPackageService.GetCouponPackage(id));
                return Ok(result);
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }
        }

        [HttpPost]
        public IHttpActionResult Create(CouponPackageCM cm)
        {
            try
            {
                CouponPackage couponPackage = AutoMapper.Mapper.Map<CouponPackageCM, CouponPackage>(cm);
                couponPackage.Picture = null;
                _couponPackageService.CreateCouponPackage(couponPackage);
                _couponPackageService.SaveCouponPackageChanges();

                if (!String.IsNullOrEmpty(cm.Picture))
                {

                    // image stream
                    var bytes = Convert.FromBase64String(cm.Picture);
                    // physical server path
                    string filePath = System.Web.HttpContext.Current.Server.MapPath("~/Media/CouponPackage/");
                    Guid guid = Guid.NewGuid();
                    // SAVE IMAGE TO SERVER

                    Image image;
                    using (MemoryStream ms = new MemoryStream(bytes))
                    {
                        image = Image.FromStream(ms);
                        image.Save(filePath + guid + ".jpg");
                    }
                    // UPDATE IMAGE PATH
                    couponPackage.Picture = "/Media/CouponPackage/" + guid + ".jpg";
                    _couponPackageService.UpdateCouponPackage(couponPackage);
                    _couponPackageService.SaveCouponPackageChanges();
                }
                // RESPONSE
                CouponPackageVM couponPackageVM = AutoMapper.Mapper.Map<CouponPackage, CouponPackageVM>(couponPackage);
                return Ok(couponPackageVM);
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }
        }

        [HttpPut]
        public IHttpActionResult Update(CouponPackageUM um)
        {
            try
            {
                CouponPackage updateCouponPackage = AutoMapper.Mapper.Map<CouponPackageUM, CouponPackage>(um);
                CouponPackage oldCouponPackage = _couponPackageService.GetCouponPackageAsNoTracking(u => u.Id == um.Id);
                if (!um.Picture.Contains("/Media/CouponPackage/"))
                {
                    // DELETE OLD AVATAR
                    // physical path to folder contain user avatar
                    string folderPath = System.Web.HttpContext.Current.Server.MapPath("~/Media/CouponPackage/");
                    // physical path to this user avatar
                    string physicalPath = null;
                    if (!String.IsNullOrEmpty(oldCouponPackage.Picture))
                    {
                        physicalPath = folderPath + oldCouponPackage.Picture.Substring(oldCouponPackage.Picture.LastIndexOf("/") + 1);
                    }
                    // delete old picture
                    if (File.Exists(physicalPath))
                    {
                        File.Delete(physicalPath);
                    }


                    // MAPPING NEW PICTURE
                    // new Guid
                    Guid newGuid = Guid.NewGuid();
                    // image stream
                    var bytes = Convert.FromBase64String(um.Picture);
                    // save image to server
                    Image image;
                    using (MemoryStream ms = new MemoryStream(bytes))
                    {
                        image = Image.FromStream(ms);
                        image.Save(folderPath + newGuid + ".jpg");
                    }
                    updateCouponPackage.Picture = "/Media/CouponPackage/" + newGuid + ".jpg";
                }
                else
                {
                    updateCouponPackage.Picture = oldCouponPackage.Picture;
                }

                // UPDATE
                _couponPackageService.UpdateCouponPackage(updateCouponPackage);
                _couponPackageService.SaveCouponPackageChanges();

                CouponPackageVM result = AutoMapper.Mapper.Map<CouponPackage, CouponPackageVM>(updateCouponPackage);
                return Ok(result);
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }
        }

        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            if (id <= 0)
            {
                return BadRequest(ErrorMessage.INVALID_ID);
            }

            try
            {
                _couponPackageService.DeleteCouponPackage(id);
                _couponPackageService.SaveCouponPackageChanges();

                return Ok();
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }
        }
    }
}
