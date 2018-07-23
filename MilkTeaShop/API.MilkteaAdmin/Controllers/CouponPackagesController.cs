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
    [Authorize(Roles = "Administrator")]
    public class CouponPackagesController : ApiController
    {
        private readonly ICouponPackageService _couponPackageService;
        private readonly IPagination _pagination;

        public CouponPackagesController(ICouponPackageService couponPackageService, IPagination pagination)
        {
            this._couponPackageService = couponPackageService;
            this._pagination = pagination;
        }

        /// <summary>
        /// Get CouponPackage Paged
        /// </summary>
        /// <remarks>
        /// - Sample Request:
        /// 
        /// {
        /// 
        /// }
        /// 
        /// </remarks>
        /// <param name="pageIndex">Page index</param>
        /// <param name="searchValue">Search value</param>
        /// <returns></returns>
        /// <response code="200">Return CouponPackage Pager</response>
        /// <response code="400">Page index negative</response> 
        /// <response code="500">Fail to Retrieve</response> 
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

        /// <summary>
        /// Get a Coupon Package
        /// </summary>
        /// <remarks>
        /// - Use an existing id.
        /// </remarks>
        /// <param name="id">Coupon Package Id</param>
        /// <returns></returns>
        /// <response code="200">Return CouponPackage with correct Id</response>
        /// <response code="400">Id negative</response> 
        /// <response code="500">Fail to Retrieve</response> 
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

        /// <summary>
        /// Create new Coupon Package
        /// </summary>
        /// <remarks>
        /// - Sample Request:
        /// 
        /// {
        ///     "Name": "Package name",
        ///     "DrinkQuantity": 5,
        ///     "Price": 50.5,
        ///     "Picture": "/9j/4AAQSkZJRgABAQEAYABgAAD/4QAiRXhpZgAATU0AKgAAAAgAAQESAAMAAAA
        ///     BAAEAAAAAAAD/2wBDAAIBAQIBAQICAgICAgICAwUDAwMDAwYEBAMFBwYHBwcGBwcICQsJCAgKCA
        ///     cHCg0KCgsMDAwMBwkODw0MDgsMDAz/2wBDAQICAgMDAwYDAwYMCAcIDAwMDAwMDAwMDAwMDAwMD
        ///     AwMDAwMDAwMDAwMDAwMDAwMDAwMDAwMDAwMDAwMDAwMDAz/wAARCAAaADADASIAAhEBAxEB/8Q
        ///     AHwAAAQUBAQEBAQEAAAAAAAAAAAECAwQFBgcICQoL/8QAtRAAAgEDAwIEAwUFBAQAAAF9AQIDA
        ///     AQRBRIhMUEGE1FhByJxFDKBkaEII0KxwRVS0fAkM2JyggkKFhcYGRolJicoKSo0NTY3ODk6Q0R
        ///     FRkdISUpTVFVWV1hZWmNkZWZnaGlqc3R1dnd4eXqDhIWGh4iJipKTlJWWl5iZmqKjpKWmp6ipq
        ///     rKztLW2t7i5usLDxMXGx8jJytLT1NXW19jZ2uHi4+Tl5ufo6erx8vP09fb3+Pn6/8QAHwEAAwE
        ///     BAQEBAQEBAQAAAAAAAAECAwQFBgcICQoL/8QAtREAAgECBAQDBAcFBAQAAQJ3AAECAxEEBSEx
        ///     BhJBUQdhcRMiMoEIFEKRobHBCSMzUvAVYnLRChYkNOEl8RcYGRomJygpKjU2Nzg5OkNERUZHS
        ///     ElKU1RVVldYWVpjZGVmZ2hpanN0dXZ3eHl6goOEhYaHiImKkpOUlZaXmJmaoqOkpaanqKmqsr
        ///     O0tba3uLm6wsPExcbHyMnK0tPU1dbX2Nna4uPk5ebn6Onq8vP09fb3+Pn6/9oADAMBAAIRAxE
        ///     APwD9/KKKKAPkr9tT9qLWof2nvDXwt8B2Oqa94m8L+Hbz4k6tpmnT+VJfrC62Wl2DnKjyp764
        ///     WWU7hsis8t8j1u/sv/thab49/aT1bwbNdSQ3Hi7RD4t02xn1Fbx9OuLW8m0rVbBWDFdkNzaI6
        ///     FMxytJdNGzJGSPhn9q34H/FXwH+3V+0Nb6P4Vs/iVe/FpdIlu7vUNP+0TWHhOVJLKeK2SBllj
        ///     2h5YC0RMwaxgkKP5pDYH7Pnib9o39pb9tv9mWTxF4b0XwrfeFdX1O8tdfttBGnpeeFreG2iuI
        ///     kjjIHkSRhIlLDas2p5QKsYVdOXQz5tT9naKKKzNAooooA8L/ba8A39pomm/FDw7q02heJvhna
        ///     alP9pQb0utPntWE9vLGSEkjE0dpcYkIUNaKdy8sI/wBk/wDZG0D4Q/EHxF4/0/xXqHjKXxRpe
        ///     m6Ppk13cLcx6PYWcCrJBBIrMp8+78+5lKbVMkgAX5Mn3LUNPt9WsJrW6ghubW5jaKaGVA8cqM
        ///     MMrKeCCCQQeCDXJfs5W8dn+z14DhhjSKKLw7p6IiLtVFFtGAAB0A9KAsdnRRRQB//Z"
        /// }
        /// 
        /// </remarks>
        /// <param name="cm">Coupon Package Create Model</param>
        /// <returns></returns>
        /// <response code="200">Return Created CouponPackage</response>
        /// <response code="400">Model State Invalid</response> 
        /// <response code="500">Fail to Create</response> 
        [HttpPost]
        public IHttpActionResult Create(CouponPackageCM cm)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
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

        /// <summary>
        /// Create new Coupon Package
        /// </summary>
        /// <remarks>
        /// - Sample Request:
        /// 
        /// {
        ///     "Id": 1,
        ///     "Name": "Package name",
        ///     "DrinkQuantity": 5,
        ///     "Price": 50.5,
        ///     "Picture": "/9j/4AAQSkZJRgABAQEAYABgAAD/4QAiRXhpZgAATU0AKgAAAAgAAQESAAMAAAA
        ///     BAAEAAAAAAAD/2wBDAAIBAQIBAQICAgICAgICAwUDAwMDAwYEBAMFBwYHBwcGBwcICQsJCAgKCA
        ///     cHCg0KCgsMDAwMBwkODw0MDgsMDAz/2wBDAQICAgMDAwYDAwYMCAcIDAwMDAwMDAwMDAwMDAwMD
        ///     AwMDAwMDAwMDAwMDAwMDAwMDAwMDAwMDAwMDAwMDAwMDAz/wAARCAAaADADASIAAhEBAxEB/8Q
        ///     AHwAAAQUBAQEBAQEAAAAAAAAAAAECAwQFBgcICQoL/8QAtRAAAgEDAwIEAwUFBAQAAAF9AQIDA
        ///     AQRBRIhMUEGE1FhByJxFDKBkaEII0KxwRVS0fAkM2JyggkKFhcYGRolJicoKSo0NTY3ODk6Q0R
        ///     FRkdISUpTVFVWV1hZWmNkZWZnaGlqc3R1dnd4eXqDhIWGh4iJipKTlJWWl5iZmqKjpKWmp6ipq
        ///     rKztLW2t7i5usLDxMXGx8jJytLT1NXW19jZ2uHi4+Tl5ufo6erx8vP09fb3+Pn6/8QAHwEAAwE
        ///     BAQEBAQEBAQAAAAAAAAECAwQFBgcICQoL/8QAtREAAgECBAQDBAcFBAQAAQJ3AAECAxEEBSEx
        ///     BhJBUQdhcRMiMoEIFEKRobHBCSMzUvAVYnLRChYkNOEl8RcYGRomJygpKjU2Nzg5OkNERUZHS
        ///     ElKU1RVVldYWVpjZGVmZ2hpanN0dXZ3eHl6goOEhYaHiImKkpOUlZaXmJmaoqOkpaanqKmqsr
        ///     O0tba3uLm6wsPExcbHyMnK0tPU1dbX2Nna4uPk5ebn6Onq8vP09fb3+Pn6/9oADAMBAAIRAxE
        ///     APwD9/KKKKAPkr9tT9qLWof2nvDXwt8B2Oqa94m8L+Hbz4k6tpmnT+VJfrC62Wl2DnKjyp764
        ///     WWU7hsis8t8j1u/sv/thab49/aT1bwbNdSQ3Hi7RD4t02xn1Fbx9OuLW8m0rVbBWDFdkNzaI6
        ///     FMxytJdNGzJGSPhn9q34H/FXwH+3V+0Nb6P4Vs/iVe/FpdIlu7vUNP+0TWHhOVJLKeK2SBllj
        ///     2h5YC0RMwaxgkKP5pDYH7Pnib9o39pb9tv9mWTxF4b0XwrfeFdX1O8tdfttBGnpeeFreG2iuI
        ///     kjjIHkSRhIlLDas2p5QKsYVdOXQz5tT9naKKKzNAooooA8L/ba8A39pomm/FDw7q02heJvhna
        ///     alP9pQb0utPntWE9vLGSEkjE0dpcYkIUNaKdy8sI/wBk/wDZG0D4Q/EHxF4/0/xXqHjKXxRpe
        ///     m6Ppk13cLcx6PYWcCrJBBIrMp8+78+5lKbVMkgAX5Mn3LUNPt9WsJrW6ghubW5jaKaGVA8cqM
        ///     MMrKeCCCQQeCDXJfs5W8dn+z14DhhjSKKLw7p6IiLtVFFtGAAB0A9KAsdnRRRQB//Z"
        /// }
        /// 
        /// </remarks>
        /// <param name="um">Coupon Package Update Model</param>
        /// <returns></returns>
        /// <response code="200">Return Updated CouponPackage</response>
        /// <response code="400">Model State Invalid</response> 
        /// <response code="500">Fail to Update</response> 
        [HttpPut]
        public IHttpActionResult Update(CouponPackageUM um)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                CouponPackage updateCouponPackage = AutoMapper.Mapper.Map<CouponPackageUM, CouponPackage>(um);
                CouponPackage oldCouponPackage = _couponPackageService.GetCouponPackageAsNoTracking(u => u.Id == um.Id);

                if (!um.Picture.Contains("/Media/CouponPackage/") && !string.IsNullOrEmpty(um.Picture))
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

        /// <summary>
        /// Delete Coupon Package By Id
        /// </summary>
        /// <remarks>
        /// - Sample Request:
        /// </remarks>
        /// <param name="id">Coupon Package id</param>
        /// <returns></returns>
        /// <response code="200">Return Empty</response>
        /// <response code="400">Id negative</response> 
        /// <response code="500">Fail to Delete</response> 
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
