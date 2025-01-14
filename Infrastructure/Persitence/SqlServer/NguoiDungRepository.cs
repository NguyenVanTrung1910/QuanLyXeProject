using Domain.Entities;
using Domain.IRepositories.SqlServer;
using Domain.Models;
using Domain.Querys;
using Domain.Querys.Base;
using Infrastructure.DependencyInjection;
using Shared;
using System;
using System.Collections.Generic;
using System.Data;

namespace Infrastructure.Persitence.SqlServer
{
    public partial class NguoiDungRepository : INguoiDungRepository
    {
        public List<NguoiDung_Entity> GetPaged(NguoiDungQuery SearchOption)
        {
            var context = (DBContext)UnitOfWork.Context;
            var query = from obj in context.NguoiDungs
                        //where (!SearchOption.isgetBylisID || SearchOption.lstIDGet.Any(id => id == obj.Id))
                        select new NguoiDung_Entity
                        {
                            Id = obj.Id,
                            TenDangNhap = obj.TenDangNhap,
                            TenNguoiDung = obj.TenNguoiDung,
                            DienThoai = obj.DienThoai,
                            Email = obj.Email,
                            ModerationStatus = obj.ModerationStatus,
                            ModerationStatusTxt = obj.ModerationStatusTxt,
                        };
            return  query.GetByGridRequest(SearchOption.oGridRequest, ref Total).ToList();

        }
        public NguoiDung CheckNguoiDungDangNhap(string oTenDangNhap, string pass)
        {
            if (string.IsNullOrEmpty(oTenDangNhap) || String.IsNullOrEmpty(pass)) return null;

            var context = (DBContext)UnitOfWork.Context;
            var query = from obj in context.NguoiDungs
                        where (obj.TenDangNhap == oTenDangNhap && obj.MatKhau.Equals(pass))
                        select new NguoiDung
                        {
                            Id = obj.Id,
                            TenDangNhap = oTenDangNhap,
                            Email = obj.Email,
                            TenNguoiDung = obj.TenNguoiDung,
                        };
            var listUser = query.ToList();
            return listUser.Count() >= 1 ? listUser.First() : new NguoiDung();
        }
        public List<VaiTroNguoiDung> GetVaiTroNguoiDungByNguoiDungId(int Id)
        {
            var context = (DBContext)UnitOfWork.Context;
            var lst = context.VaiTroNguoiDungs.Where(a=>a.UserId.Equals(Id)).ToList();
            if (lst.Count == 0) return new List<VaiTroNguoiDung>();
            return lst;
        }
    }
}
